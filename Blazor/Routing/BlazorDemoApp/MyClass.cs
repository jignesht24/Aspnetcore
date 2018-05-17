using Microsoft.AspNetCore.Blazor.Components;
using Microsoft.AspNetCore.Blazor.Services;
using System;

namespace BlazorDemoApp
{
    [Route("/test3/{parameter1}")]
    public class MyClass : BlazorComponent
    {
        [Inject]
        public IUriHelper UriHelper { get; set; }
        public void Init(RenderHandle renderHandle)
        {
            throw new NotImplementedException();
        }
    }

    public class MyClass1 : IComponent
    {
        public void Init(RenderHandle renderHandle)
        {
            throw new NotImplementedException();
        }

        public void SetParameters(ParameterCollection parameters)
        {
            throw new NotImplementedException();
        }
    }
}
