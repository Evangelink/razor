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
            __builder.AddMarkupContent(0, "\r\n    ");
            __builder.OpenElement(1, "elem");
            __builder.AddAttribute(2, "attr", 
#nullable restore
#line (3,17)-(3,20) "x:\dir\subdir\Test\TestComponent.cshtml"
Foo

#line default
#line hidden
#nullable disable
            );
            __builder.AddMarkupContent(3, "\r\n        <child></child>\r\n    ");
            __builder.CloseElement();
            __builder.AddMarkupContent(4, "\r\n\r\n\r\n\r\n");
        }
        #pragma warning restore 1998
#nullable restore
#line (7,12)-(9,5) "x:\dir\subdir\Test\TestComponent.cshtml"

        int Foo = 18;
    

#line default
#line hidden
#nullable disable

    }
}
#pragma warning restore 1591
