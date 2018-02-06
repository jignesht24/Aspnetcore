## Introduction
 
The Anchor Tag Helper generates an HTML anchor (<a> </a>) element by adding new attribute. The "href" attribute of the anchor tag is created by using the new attributes.
 
### Anchor Tag Helper Attributes

The Anchor Tag Helper has introduced many attributes that help us to generate the HTML for Anchor Tag Helper. In this section, we will learn each attribute that can be used with Anchor Tag Helper.
 
#### asp-controller

It is used to associate the Controller name that is used to generate the URL. The Controller that is specified here must exist in the current project.
 
#### asp-action

It is used to specify the name of the action method (in the Controller) that helps us to generate the URL. The generated URL is placed in the href attribute of the anchore tag.
 
##### Example

In this following example, the Controller and the action both are set as default so it will generate the URL differently. 
```
<a asp-controller="Home" asp-action="Index">All Users</a>  
```
##### Generated HTML
```
<a href="/">All Users</a>  
```
##### Other Example
```
<a asp-controller="Home1" asp-action="Index1">All Users</a>  
```
##### Generated HTML
```
<a href="/Home1/Index1">Users</a>  
```
If the asp-action attribute is not specified with asp-controller attribute, it will use the default action method of the Controller to generate the URL. If asp-controller attribute is not specified with asp-action attribute, it will use the current View Controller by default. If the asp-action attribute is defined as "index", no action is specified to the URL and the default index method is called.
 
#### asp-route-{value}

This is a wild card route prefix. Any value that is placed after the trailing dash (in place of {value}) will be consider as a route parameter. If a default route is not found, this value will be used to generate the URL as a request parameter and value.

##### Example
```
<a asp-controller="Home1" asp-action="Detail" asp-route-id ="12">User Data</a>  
```
##### Generated HTML
```
<a href="/Home1/Detail/12">User Data</a>  
```
If route prefix is not a part of the route template, it will pass the route parameter as query string.
 
##### Example
```
<a asp-controller="Home1" asp-action="Detail" asp-route-userid="12">User Data1</a>
```
##### Generated HTML
```
<a href="/Home1/Detail?userid=12">User Data1</a>  
```

#### asp-route

It provides the way to create a URL which links directly to a named route.
 
#### asp-all-route-data

It allows us to create a dictionary of the key-value pairs where the key is the parameter name and the value is the value associated with that key and passed to the Controller action method.
 
In the following example, I have created an in-line dictionary and passed the dictionary object to asp-all-route-data attribute. This tag helper will generate the URL in form of query string with all the parameters defined in the dictionary.
 
##### Example
```
@{  
    var data =  
        new Dictionary<string, string>  
        {  
            {"Id", "11"},  
            {"name", "Rakesh"}  
        };  
}  
<a asp-route="userCurrent" asp-all-route-data="data">User Info</a> 
```
##### Generated HTML
```
<a href="/Home1/userCurrent?Id=11&name=Rakesh">User Info</a> 
```

#### asp-fragment

It defines the URL fragment applied to the URL. The Anchor Tag Helper adds the hash character (#). It is useful with client-side application. It can be used to easy marking and searching in JavaScript.
 
##### Example
```
<a asp-controller="Home1" asp-action="User" asp-fragment="#UserInfo">About User Information</a>  
```
##### Generated HTML
```
<a href="/Home1/User##UserInfo">About User Information</a>  
```

#### asp-area

It is used to set the area name which ASP.NET Core uses to set an appropriate route.

##### Example
```
<a asp-action="User" asp-controller="Home1" asp-area="UserArea">Area Example</a>  
```
##### Generated HTML
```
<a href="/UserArea /Home1/User ">Area Example</a>  
```

#### asp-protocol

It is used to specify a protocol such as HTTPS in our URL. The Anchor Tag Helper uses the website domain when generating the URL.

##### Example 
```
<a asp-action="User" asp-controller="Home1" asp-protocol="https">Protocol Example</a>  
```
##### Generated HTML 
```
<a href="https://localhost:20618/Home1/User">Protocol Example</a>   
```

### Summary

This article helps us to understand Anchor Tag Helper in ASP.NET Core MVC.
