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
#nullable restore
#line (1,2)-(1,49) "x:\dir\subdir\Test\TestComponent.cshtml"
using Microsoft.AspNetCore.Components.Rendering

#line default
#line hidden
#nullable disable
    ;
    #nullable restore
    public partial class TestComponent : global::Microsoft.AspNetCore.Components.ComponentBase
    #nullable disable
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(global::Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#nullable restore
#line (2,8)-(5,9) "x:\dir\subdir\Test\TestComponent.cshtml"

    void MyMethod(RenderTreeBuilder __builder)
    {
        

#line default
#line hidden
#nullable disable

        __builder.OpenElement(0, "ul");
#nullable restore
#line (6,14)-(8,17) "x:\dir\subdir\Test\TestComponent.cshtml"
for (var i = 0; i < 100; i++)
            {
                

#line default
#line hidden
#nullable disable

        __builder.OpenElement(1, "li");
        __builder.AddContent(2, 
#nullable restore
#line (9,22)-(9,23) "x:\dir\subdir\Test\TestComponent.cshtml"
i

#line default
#line hidden
#nullable disable
        );
        __builder.CloseElement();
#nullable restore
#line (11,1)-(11,14) "x:\dir\subdir\Test\TestComponent.cshtml"
            }

#line default
#line hidden
#nullable disable

        __builder.CloseElement();
#nullable restore
#line (13,1)-(14,1) "x:\dir\subdir\Test\TestComponent.cshtml"
    }

#line default
#line hidden
#nullable disable

    }
}
#pragma warning restore 1591
