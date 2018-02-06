## Introduction

Tag helper is a new feature in ASP.NET MVC. It enables server-side code to create and render HTML elements in Razor View. It is a feature of Razor View engine. They are the C# classes which participate in view generation by creating the HTML elements. Using the tag helper, we can change the content of HTML element and add the additional attributes to the HTML element. It is very similar to HTML helper in ASP.NET MVC.

### HTML Helper 
```
@Html.TextBoxFor(model => model.Name, new { @class = "form-control", placeholder = "Enter Your Name" }) 
```
### Equivalent with tag helper
```
<input asp-for="Name" placeholder="Enter Your Name" class="form-control" />  
```
### Equivalent HTML
```
<input placeholder="Enter Your Name" class="form-control" id="Name" name="Name" value="" type="text">  
```

* Advantages of Tag Helper
An HTML-friendly development experience
The tag helper looks like standard HTML attribute, so any front-end developer can easily understand Razor View and can edit Razor without learning the C# Razor syntax.

* A rich IntelliSense support
Visual Studio provides better intelliSense support to build the HTML elements with Tag Helpers.

* More robust, reliable, and maintainable code
The tag helper provides a way to produce robust, reliable, and maintainable code using information that is available only on the server.

* It also executes the metadata that sets using Data Annotation in the View Models/Models. 

### Compared with HTML Helpers
|  HTML Helper        | Tag Helper           |
| -------------------------------------- |:--------------------------------------:|
| HTML helpers invoke the methods that are represented with HTML in Razor View      | Tag helper attaches with HTML elements in View |
| HTML helper starts with "@" symbol which tells Razor where is the start of code      | 	In Tag helper, no such symbol      |
| The anonymous object is used to represent attributes in HTML helper | Tag helper is injected within HTML so no such anonymous object is required to pass the HTML attributet      |
| It is very difficult to have intelliSense with HTML helper | There is very rich support of intellisense with Tag helper with visual studio      |
| The marker with HTML helper is very difficult to understand and maintain | The marker with tag helper is very much clean, easy to understand and maintain      |

### Customizing the Tag Helper element font

We can customize the font and color of the tag helper from Tools > Options > Environment > Fonts and Colors.

Tag helper is very similar to attribute directive of AngularJS that provides better HTML code readability compared to HTML helper.

### How to add support Tag Helper to View Pages

The _ViewImports.cshtml file provided by the MVC structure. It serves the  major purpose of providing namespaces that can be used by all the other views. Previously, this functionality was provided by using web config.

The namespace of tag helpers can be registered to this page because it is used by almost all the views in the project.

_ViewImports.cshtml
```
@using TagHelper  
@addTagHelper *, Microsoft.AspNetCore.Mvc.TagHelpers  
```
The above code includes all the tag helpers within assembly "Microsoft.AspNetCore.Mvc.TagHelpers". Here the wild card character (*) includes all the tag helpers under assembly and visible to the all views.

### Managing Tag Helper scope

The tag helper scope can be controlled by the @addTagHelper, @removeTagHelper, and the "!" (opt-out character). The directive @addTagHelper makes tag helper available to use in view. The wild card character (*) includes all the taghelpers from a specified assembly. Adding @addTagHelper directive to the Views/_ViewImports.cshtml file makes the Tag Helper available to all view files which are inhereited from _ViewImports file. We can also specify this directive to specific view if we want to expose the tag helper to only those views.

The @removeTagHelper directive removes the tag helper. Same as @addTagHelper directive, it has two parameters and it removes tag helper which is previously added. We can also add _ViewImports.cshtml file to any of view folder. The view engine applies the directive from both _ViewImports.cshtml files.

### Disable a Tag Helper at the element level

We can also disable a Tag Helper at the element level using the Tag Helper opt-out character ("!"). Here, we must apply Tag Helper opt-out character to the opening and closing tag. If we add the opt-out character, the element and Tag Helper attributes are no longer displayed in a distinctive font.

### Tag Helper Prefix

The @tagHelperPrefix directive allows us to specify the tag prefix string to enable tag helper support and it makes tag helper usage explicit.

For example, if we added "th" tag prefix to _ViewImports.cshtml file, it allows us to add tag helper support with this prefix only. In for code Snippet, label element has the tag prefix so tag helper is enabled but for span element does not.

Views/_ViewImports.cshtml
```
@tagHelperPrefix th:
```
Index.cshtml
```
<div class="form-group">  
    <th:label asp-for="name" class="col-md-4"></th:label>  
    <span asp-for="name" class="col-md-4"></span>  
</div>  
```
### Summary

In this article, I have explained about the new feature of asp.net core Tag helper and also explained how it differs from HTML helper.

