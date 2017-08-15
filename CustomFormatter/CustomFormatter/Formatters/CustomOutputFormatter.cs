namespace CustomFormatter.Formatters
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Formatters;
    using Microsoft.Net.Http.Headers;
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;
    public class CustomOutputFormatter : TextOutputFormatter
    {
        public CustomOutputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/cu-for"));

            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }

        protected override bool CanWriteType(Type type)
        {
            if (typeof(Employee).IsAssignableFrom(type)
                || typeof(IEnumerable<Employee>).IsAssignableFrom(type))
            {
                return base.CanWriteType(type);
            }
            return false;
        }

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
    }
}
