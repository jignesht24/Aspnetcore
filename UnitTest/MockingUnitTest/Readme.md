## Moq: Unit Test .net core App using mock object

### Introduction
The Unit test is a block of code that help us in verifying the expected behaviour of the other code in isolation i.e. there is no dependency between the tests. This is good way to test the application code before it goes for  quality assurance (QA). There are three different test frameworks for Unit Testing supported by ASP.NET Core: MSTest, xUnit, and NUnit. All Unit test frameworks, offer a similar end goal and help us to write unit tests that are simpler, easier and faster. 

In my previous articles, I have explain about how to write unit test with different frameworks (i.e. MSTest, xUnit, and NUnit). These areticles having very simple and very straightforward exmple but real world, class's constructor may have complex object and injected as dependency. In this case, we can create mocked object and use it for unit test.

The mock object is object that can act as a real object but can be controller in test code. Moq (https://github.com/moq/moq4) is a library that allows us to create mock object in test code. It is also available in NuGet(https://www.nuget.org/packages/Moq/). This library also supports .net core.

The Moq library can add to test project either by packge manager or .net CLI tool.

Using Package Manager
```
PM> Install-Package Moq
```
Using .net CLI
```
dotnet add package Moq
```


