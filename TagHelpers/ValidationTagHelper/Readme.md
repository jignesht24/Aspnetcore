### Introduction
 
This article explains about the validation tag helpers of ASP.NET Core MVC. There are two validation tag helpers available.
* Validation Message Tag Helper: It displays the validation message for a single property of a Model.
* Validation Summary Tag Helper: It displays a summary of validation messages for all the properties of a Model.
The input tag helper adds a client-side validation to the input elements based on the data annotation defined in our Model class. The validation tag helper displays the error messages for invalid inputs.
 
#### Validation Message Tag Helper (asp-validation-for)
 
It displays the error message for a single property of our Model. It can be achieved by asp-validation-for tag helper. It adds the data-valmsg-for="property name" attribute to the element which it carries for example span. It attaches the validation message on the input field of the specified Model property. The client-side validation can be done with jQuery. It is an alternative to Html.ValidationMessageFor.
 
Model
```
namespace ValidationTagHelper.Models  
{  
    using System.ComponentModel.DataAnnotations;  
    public class UserViewModel  
    {  
        public int Id { get; set; }  
        [StringLength(200)]  
        [Required]  
        public string Name { get; set; }  
        [EmailAddress]  
        [Required]  
        public string Email { get; set; }  
    }  
}  
```
View (Views\Home\Index.cshtml)
```
<div class="row">  
    <form asp-controller="Home" asp-action="Save" method="post">  
        Email Address: <input asp-for="Email" /><br />  
        <span asp-validation-for="Email"></span>  
    </form>  
</div>  
```
 
The Validation message tag helper is generally used after an input tag helper for the same property to display any validation message.
We must include the jquery.validate and jquery.validate.unobtrusive files for performing client-side validation. When an error in our model validation framework occurs, the jQuery places the error message to the body of the span.

#### Validation Summary Tag Helper (asp-validation-summary)
 
It displays a summary of validation error messages. It targets "DIV" element with "asp-validation-summary" attribute. It is similar to @Html.ValidationSummary. The attribute "asp-validation-summary” can have one of the following values.
ValidationSummary.All: It displays both, the property and model level validations.
ValidationSummary.ModelOnly: it displays Model-level validation only.
ValidationSummary.None: It does not perform any validation. It is equivalent to that the tag is not applied.
Example

To demonstrate the example, I have used the same Model as in the previous example. Following is my Controller and View code.

```
public IActionResult Index1()  
{  
    return View();  
}  
[HttpPost]  
public object Save(UserViewModel user)  
{  
    if (!ModelState.IsValid)  
    {  
        //Model has Error(s)  
        return View("index1", user);  
    }  
    return null;  
}  
```
View
```
@model ValidationTagHelper.Models.UserViewModel  
<div class="row">  
    <form asp-controller="Home" asp-action="Save" method="post">  
        <div asp-validation-summary="All"></div>  
        <br />  
  
        Name: <input asp-for="Name" /><br />  
        Email Address: <input asp-for="Email" /><br />  
        <br />  
        <button type="submit">Test Validation</button>  
    </form>  
</div>  
```

#### Summary
 
The validation tag helpers are very easy to use. They are very similar to the previous versions of ASP.NET MVC tag helpers.
