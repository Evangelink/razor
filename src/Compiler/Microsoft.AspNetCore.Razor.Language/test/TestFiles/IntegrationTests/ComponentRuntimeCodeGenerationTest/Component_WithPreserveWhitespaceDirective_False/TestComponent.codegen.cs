﻿// <auto-generated/>
#pragma warning disable 1591
namespace Test
{
    #line default
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Linq;
    using global::System.Threading.Tasks;
    using global::Microsoft.AspNetCore.Components;
    #line default
    #line hidden
    #nullable restore
    public partial class TestComponent : global::Microsoft.AspNetCore.Components.ComponentBase
    #nullable disable
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(global::Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.OpenElement(0, "ul");
#nullable restore
#line (4,6)-(6,9) "x:\dir\subdir\Test\TestComponent.cshtml"
foreach (var item in Enumerable.Range(1, 100))
    {
        

#line default
#line hidden
#nullable disable

            __builder.OpenElement(1, "li");
            __builder.AddContent(2, 
#nullable restore
#line (7,14)-(7,18) "x:\dir\subdir\Test\TestComponent.cshtml"
item

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
#nullable restore
#line (9,1)-(9,6) "x:\dir\subdir\Test\TestComponent.cshtml"
    }

#line default
#line hidden
#nullable disable

            __builder.CloseElement();
        }
        #pragma warning restore 1998
    }
}
#pragma warning restore 1591
