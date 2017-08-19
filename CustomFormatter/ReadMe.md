# Introduction
 
ASP.net core (MVC / Web API) applications has communicated with other application by using built-in format such as JSON, XML or plain text. By default asp.net core MVC supports built-in format for data exchange using JSON, XML or plain text. We can also add the support for custom format by creating custom formatters.
 
In this article, we learn how to create custom formatters in asp.net core MVC. We can use custom formatter if we want the content negotiation process to support a content type which is not supported by built-in formatters.
 
Followings are the step to create custom formatter
1) Create output formatter (if we required to serialize data to send to the client)
2) Create input formatter (if we required to de-serialize data received from the client)
3) Add the instance(s) of our formatter to the InputFormatters and OutputFormatters collections in MvcOptions

# Create Custom formatter class
 
To create custom formatter class, we need to derive class from appropriate base class. There are many built-in formatter class available for input formatter such as TextInputFormatter, JsonInputFormatter, and XmlDataContractSerializerInputFormatter etc. The InputFormatter is the base class of all input formatter and also all the input formatter implements IInputFormatter interface. Same as for the output formatter, there are many built-in formatter class available for output formatter such as TextOutputFormatter, JsonOutputFormatter, and XmlDataContractSerializerOutputFormatter etc. The OutputFormatter is the base class of all output formatter and also all the output formatter implements IOutputFormatter interface.
 
To demonstrate the example, I have inherited the class from the TextInputFormatter or TextOutputFormatter base class.

namespace CustomFormatter.Formatters  
{  
    using Microsoft.AspNetCore.Mvc.Formatters;  
    using System;  
    using System.Text;  
    using System.Threading.Tasks;  
    public class CustomOutputFormatter : TextOutputFormatter  
    {  
        public override Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)  
        {  
            throw new NotImplementedException();  
        }  
    }  
}

Next step is to specify valid media type and encoding within constructor of the formatter class. We can specify the media types and encodings by adding to the SupportedMediaTypes and SupportedEncodings collections.

public CustomOutputFormatter()  
{  
    SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/CustomFormat"));  
  
    SupportedEncodings.Add(Encoding.UTF8);  
    SupportedEncodings.Add(Encoding.Unicode);  
}

Next step is to override CanReadType and CanWriteType method of the base class. In this methods, we can de-serialize into specify the type (CanReadType) or serialize from the type (CanWriteType).
 
To demonstrate the example, I have create user define type called "Employee". So we might only able to create custom formatter text from this type (Employee) and vice versa.
 
In some of the case, we might need to use CanWriteResult instead of CanWriteType method. Followings are some scenarios in which we need to use CanWriteResult method
 
Our Action method returns the Model class
The derived type classes might be returned at runtime
If we need to know which derived class was returned by the action at runtime?
 
For example, our action method return a "User" type but it might return either "Employee" or "Customer" type that derives from "User" type. If we want that our formatter only handle "Employee" objects, check type of Object in contextObject provided by CanWriteResult method. It is not necessary to use CanWriteResult method when our action method returns IActionResult. In this case CanWriteType method can receives the runtime type.

protected override bool CanWriteType(Type type)  
{  
    if (typeof(Employee).IsAssignableFrom(type)  
        || typeof(IEnumerable<Employee>).IsAssignableFrom(type))  
    {  
        return base.CanWriteType(type);  
    }  
    return false;  
} 


Next step is to override WriteResponseBodyAsync method. This method is return response in required format. To demonstrate the example, I have sent employee data in pipe format.
public override Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)  
{  
    IServiceProvider serviceProvider = context.HttpContext.RequestServices;  
    var response = context.HttpContext.Response;  
  
    var buffer = new StringBuilder();  
    if (context.Object is IEnumerable<Employee>)  
    {  
        foreach (var employee in context.Object as IEnumerable<Employee>)  
        {  
            FormatData(buffer, employee);  
        }  
    }  
    else  
    {  
        var employee = context.Object as Employee;  
        FormatData(buffer, employee);  
    }  
    return response.WriteAsync(buffer.ToString());  
}  
  
private static void FormatData(StringBuilder buffer, Employee employee)  
{  
    buffer.Append("BEGIN|");  
    buffer.AppendFormat("VERSION:1.0|");  
    buffer.AppendFormat($"Data:{employee.Id}|{employee.EmployeeCode}|{employee.FirstName}|{employee.LastName}");  
    buffer.Append("|END");  
}  

The final step is to configure MVC to use a custom formatter. To use custom formatter, add the instance of the formatter class to OutputFormatters or InputFormatters collection MvsOption.
public void ConfigureServices(IServiceCollection services)  
{  
    // Add framework services.  
    services.AddMvc(  
            option =>  
            {  
                option.OutputFormatters.Insert(0, new CustomOutputFormatter());  
                option.InputFormatters.Add(new CustomInputFormatter());  
                option.FormatterMappings.SetMediaTypeMappingForFormat("cu-for", MediaTypeHeaderValue.Parse("text/cu-for"));  
            }  
        );  
}  
Here I have added our custom formatter at 0 (zero) position of the OutputFormatters or InputFormatters collection, so our custom formatter has highest priority.

Same as, we can also create input formatter that accept the data in particular format and convert it in to required format. Following example code is input formatter that that format incoming data to employee model.

namespace CustomFormatter.Formatters  
{  
    using Microsoft.AspNetCore.Mvc.Formatters;  
    using Microsoft.Net.Http.Headers;  
    using System;  
    using System.IO;  
    using System.Text;  
    using System.Threading.Tasks;  
    public class CustomInputFormatter : TextInputFormatter  
    {  
        public CustomInputFormatter()  
        {  
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/cu-for"));  
  
            SupportedEncodings.Add(Encoding.UTF8);  
            SupportedEncodings.Add(Encoding.Unicode);  
        }  
  
  
        protected override bool CanReadType(Type type)  
        {  
            if (type == typeof(Employee))  
            {  
                return base.CanReadType(type);  
            }  
            return false;  
        }  
        public override async Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context, Encoding encoding)  
        {  
            if (context == null)  
            {  
                throw new ArgumentNullException(nameof(context));  
            }  
  
            if (encoding == null)  
            {  
                throw new ArgumentNullException(nameof(encoding));  
            }  
  
            var request = context.HttpContext.Request;  
  
            using (var reader = new StreamReader(request.Body, encoding))  
            {  
                try  
                {  
                    var line = await reader.ReadLineAsync();  
                    if (!line.StartsWith("BEGIN|VERSION:1.0|"))  
                    {  
                        var errorMessage = $"Data must start with 'BEGIN|VERSION:1.0|'";  
                        context.ModelState.TryAddModelError(context.ModelName, errorMessage);  
                        throw new Exception(errorMessage);  
                    }  
                    if (!line.EndsWith("|END"))  
                    {  
                        var errorMessage = $"Data must end with '|END'";  
                        context.ModelState.TryAddModelError(context.ModelName, errorMessage);  
                        throw new Exception(errorMessage);  
                    }  
  
                    var split = line.Substring(line.IndexOf("Data:") + 5).Split(new char[] { '|' });  
                    var emp = new Employee()  
                    {  
                        Id = Convert.ToInt32(split[0]),  
                        EmployeeCode = split[1],  
                        FirstName = split[2],  
                        LastName = split[3]  
                    };  
  
                    return await InputFormatterResult.SuccessAsync(emp);  
                }  
                catch  
                {  
                    return await InputFormatterResult.FailureAsync();  
                }  
            }  
        }  
    }  
} 

# Summary
 
ASP.net core provide facility to create our own formatter. There are many built-in formatter supported by the ASP.net core such as JSON, XML and plain text. This custom formatter is very useful when we interact with any third party system which accept data in specific format and send the data in specific format. Using custom formatter we can add support for additional format.
