
# Introduction
This article explains about working with form and other HTML elements that used with form using tag helper. The HTML form provides a way to post the data back to the server. 

# Form Tag Helper
It generate the HTML Form element with action attribute value for MVC controller action(named route). It is an alternative of HTML helper: Html.BeginForm and Html.BeginRouteForm. It generates hidden  "Request Verification Token" to prevent cross-site request forgery , if post method decorated with ValidateAntiForgeryToken attribute. We can also supply route value parameter of post method using "asp-route-<parameter name>" attribute. Here <pameter name> is added to the route value. This is same as routeValues parameter pass to Html.BeginForm and Html.BeginRouteForm. 

# Example:
```
<form asp-controller="Home" asp-action="Save" method="post">
    <!-- Input and Submit elements -->
</form>
```

The action attribute value is generated from the FORM tag helper attributes: asp-controller and asp-action. The Form tag helper is also generate "Request Verification Token" (in hidden field) to prevent cross-site request forgery.

# Input Tag Helper

It binds an HTML input element to a model expression in the razor view. It can generate the id and name attribute based on expression specified in asp-for attribute. It able to set HTML type attribute value based on the model type and data annotation applied to model property. It never overwrite type attribute if we have specified. It generate HTML5 base validation attribute s from the data annotation applied to model property. It provides strong typing with model. If we change model property name and not change the tag helper, it throw the error.

The HTML type set by the tag helper based on .net type. For example, if .net type is bool, it will generate type="checkbox".

It also consider the data annotation attributes of model property to generate type for input. For example, if model property contain "EmailAddress" data annotation, it will generate type = "email".

# Example
Model Class
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
HTML in cshtml
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
In preceding example, the data annotation applied to the email property generated metadata on the model and input tag helper use this metadata and generate HTML5 attribute.

The "asp-for" attribute is a ModelExpression and assignment (right hand) side is lambda expression. It means, asp-for = "propertyName" is equvalent to o=>o.propertyName in the generated code that does not required prefix model. We can also use @ character to start an inline expression and used with tag helper

```
@{
    var myNumber = 5;
}
<input asp-for="@myNumber" />
```

# Textarea Tag Helper
It is very similar to Input tag helper. It generates the name and id attribute based on model and data annotation attribute from the model. It provide strongly typing with model. It is alternative of Html.TextAreaFor.

# Example:
```
Address : <textarea asp-for="Address"></textarea>
```
# Label Tag Helper
It generates the label caption for expression supplied with asp-for attribute. It is alternative of Html.LabelFor. It will get description of lable value from "Display" attribute automatically. It will generate less markup than the HTML label element. It is strong typing with model property.

# Example:
```
<label asp-for="PhoneNumber"></label>
<input asp-for="PhoneNumber" /> <br />
```

It generate consistent id and for attribute, so that they can be correctly associated. The caption of label comes from "Display" attribute on model property. If the model property does not contain the "Display" attribute, it will generate the caption same as property name.

# Select Tag Helper
It generate select and associate option element for our model property. It is alternative of Html.DropDownListFor and Html.ListBoxFor. It support binding to model property using asp-for attribute and asp-items attribute use for specifies the options.

# Example:
Model
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
In the preceding example, the “Fruit” model property has value “2” hence selected attribute is generated HTML.

A view model is more robust to provide MVC metadata and generally less problematic than the ViewBag and ViewData.

Select tag helper can also be used with enum property and generate the SelectListItem element from the enum values.

# Example:
Model:
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

<select asp-for="EnumCity" asp-items="Html.GetEnumSelectList<CityEnum>()">
</select>
```

# Option Group
The Option Group (optgroup) element is generated View model contains one or more SelectListGroup object. In the following example, CityViewModelGroup groups the SelectListItem element into the "Gujarat" and "Maharashtra".

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

# Summary 
In this article, I have explain about form tag helper and other common tag helper such as input, textArea, drop down and label. Also explain about the equivalent HTML helper. 





