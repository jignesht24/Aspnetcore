## Introduction
This article explains about working with forms and other HTML elements that are used with the form using tag helper. The HTML form provides a way to post the data back to the server.

### Form Tag Helper

Form Tag Helper generates the HTML form elements with action attribute value for MVC Controller action (named route). It is an alternative to HTML helpers Html.BeginForm and Html.BeginRouteForm. It generates hidden "Request Verification Token" to prevent the cross-site request forgery if the post method is decorated with ValidateAntiForgeryToken attribute. We can also supply route value parameter of post method using "asp-route-<parameter name>" attribute. Here, <pameter name> is added to the route value. This is same as routeValues parameter passed to Html.BeginForm and Html.BeginRouteForm.
Example
```
<form asp-controller="Home" asp-action="Save" method="post">  
   <!-- Input and Submit elements -->  
</form> 
```
The action attribute value is generated from the FORM tag helper attributes: asp-controller and asp-action. The Form Tag Helper also generates "Request Verification Token" (in the hidden field) to prevent cross-site request forgery.
### Input Tag Helper

It binds an HTML input element to the Model expression in Razor View. It can generate the id and name attribute based on the expression specified in the asp-for attribute. It is capable to set HTML type attribute value based on the Model type and data annotation applied to the Model property. It never overwrites the type of an attribute if we have specified. It generates HTML5 base validation attributes from the data annotation applied to the model property. It provides strong typing with model. If we change the Model property name and do not change the tag helper, it throws an error.

|   .net Type  | Input Type  |
| ------------ |:-------------:|
|  string	      |  text |
| bool      | 	checkbox      |
| datetime | datetime      |
| byte | Number      |
| int | Number      |
| double | Number      |
| single | Number      |

It also considers the data annotation attributes of Model property to generate the type of input. The following table contains some examples of data annotation and type.

|   Data Annotation attribute  | Input Type  |
| ------------ |:-------------:|
|  DataType(DataType.Password	      |  password |
| EmailAddress      | 	email      |
| Uri | uri      |
| HiddenInput | hidden      |
| Phone | tel      |
| [DataType(DataType.Date)] | date      |
| [DataType(DataType.Time)] | time      |

##### Example - Model Class
```
using System.ComponentModel.DataAnnotations;  
  
namespace FormTagHelper.Models  
{  
    public class UserViewModel  
    {  
        public int UserId { get; set; }  
        public string UserName { get; set; }  
        [EmailAddress]  
        public string EmailAdress { get; set; }  
    }  
}   
```
##### HTML in cshtml(\Views\Home\index.cshtml)
```
<div class="row">  
  
    <form asp-controller="Home" asp-action="Save" method="post">  
        <!-- Input and Submit elements -->  
        UserId : <input asp-for="UserId" /><br />  
        User Name:<input asp-for="UserName" /><br />  
        Email Address: <input asp-for="EmailAdress" />  
    </form>  
</div>   
```
In the preceding example, the data annotation applied to the email property generated the metadata on the Model and the input tag helper uses this metadata to generate HTML5 attribute.
 
The "asp-for" attribute is a ModelExpression and assignment (right-hand side) is lambda expression. It means, asp-for = "propertyName" is equvalent to o=>o.propertyName in the generated code that does not require prefix model. We can also use @ character to start an inline expression used with the tag helper.
```
@{  
    var myNumber = 5;  
}  
<input asp-for="@myNumber" />  
```

### Textarea Tag Helper

It is very similar to Input Tag Helper. It generates the name and id attribute based on the Model and data annotation attributes from the Model. It provides strong typing with Models. It is an alternative to Html.TextAreaFor.
 
Example
```
Address : <textarea asp-for="Address"></textarea> 
```
### Label Tag Helper
 
It generates the label caption for expression supplied with asp-for attribute. It is an alternative to Html.LabelFor. It will get the description of the lable value from "Display" attribute automatically. It generates less markup than the HTML label element. It is strongly typed with model property.
 
Example:
```
<label asp-for="PhoneNumber"></label>  
<input asp-for="PhoneNumber" /> <br />  
```

It generates consistent id and for attribute, so that they can be correctly associated. The caption of the label comes from "Display" attribute on the Model property. If the Model property does not contain the "Display" attribute, it will generate the caption same as property name.
 
### Select Tag Helper
 
It generates select and associate option elements for our model property. It is an alternative to Html.DropDownListFor and Html.ListBoxFor. It supports binding to the Model property using asp-for attribute and asp-items attribute used for specifying the options.
 
Example - Model
```
public class FruitViewModel  
{  
    public string Fruit { get; set; }  
    public List<SelectListItem> Fruits { get; set; }  
}  
```
Controller Action method (HomeController)
```
public IActionResult SelectExample()  
{  
    FruitViewModel fruit = new FruitViewModel();  
    fruit.Fruits = new List<SelectListItem>  
    {  
        new SelectListItem { Value = "1", Text = "Apple" },  
        new SelectListItem { Value = "2", Text = "Banana" },  
        new SelectListItem { Value = "3", Text = "Mango"  },  
        new SelectListItem { Value = "4", Text = "Orange"  },  
    };  
    fruit.Fruit = "2";  
    return View(fruit);  
}  
```
View (\Views\Home\SelectExample.cshtml)
```
@model FormTagHelper.Models.FruitViewModel  
  
<select asp-for="Fruit" asp-items="Model.Fruits"></select>  
```
In the preceding example, the “Fruit” model property has value “2” hence selected attribute has generated HTML.

A View Model is more robust to provide MVC metadata and generally less problematic than the ViewBag and ViewData. The Select Tag Helper can also be used with enum property and generate the SelectListItem element from the enum values.
 
Example

Model
```
public class CityEnumViewModel  
{  
    public CityEnum EnumCity { get; set; }  
}  
  
public enum CityEnum  
{  
    [Display(Name = "Amdavad")]  
    Ahmedabad,  
    [Display(Name = "Vadodara")]  
    Baroda,  
    Gandhinagar,  
    Bhavnagar,  
    Surat,  
    Bharuch,  
    Rajkot  
}  
```
View (Views\Home\SelectWithEnum.cshtml)
```
@model FormTagHelper.Models.CityEnumViewModel  
  
<select asp-for="EnumCity" asp-items="Html.GetEnumSelectList<CityEnum>()"></select>  
```
### Option Group
 
The Option Group (optgroup) element is generated View Model that contains one or more SelectListGroup objects. In the following example, CityViewModelGroup groups the SelectListItem element into the "Gujarat" and "Maharashtra".
 
Model
```
public class CityViewModel  
{  
    public string City { get; set; }  
    public List<SelectListItem> CityList { get; set; }  
} 
```
Controller
```
public IActionResult SelectOptionGroup()  
{  
    var gujaratGroup = new SelectListGroup { Name = "Gujarat" };  
    var maharastraGroup = new SelectListGroup { Name = "Maharastra" };  
  
    CityViewModel city = new CityViewModel();  
    city.CityList = new List<SelectListItem>  
    {  
        new SelectListItem  
        {  
            Value = "1",  
            Text = "Ahmedabad",  
            Group = gujaratGroup  
        },  
        new SelectListItem  
        {  
            Value = "2",  
            Text = "Gandhinagar",  
            Group = gujaratGroup  
        },  
        new SelectListItem  
        {  
            Value = "3",  
            Text = "Bhavangar",  
            Group = gujaratGroup  
        },  
        new SelectListItem  
        {  
            Value = "4",  
            Text = "Mumbai",  
            Group = maharastraGroup  
        },  
        new SelectListItem  
        {  
            Value = "5",  
            Text = "Pune",  
            Group = maharastraGroup  
        },  
        new SelectListItem  
        {  
            Value = "6",  
            Text = "Nasik",  
            Group = maharastraGroup  
        }  
    };  
    return View(city);  
}  
```
View (Views\Home\SelectOptionGroup.cshtml)
```
@model FormTagHelper.Models.CityViewModel  
  
<select asp-for="City" asp-items="Model.CityList"></select>  
```
Summary
 
In this article, I have explained about form tag helper and other common tag helpers, such as input, textarea, drop-down, and label. Also, we saw the equivalent HTML helpers


