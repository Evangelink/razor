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
#line (1,2)-(1,51) "x:\dir\subdir\Test\TestComponent.cshtml"
using Microsoft.AspNetCore.Components.RenderTree;

#line default
#line hidden
#nullable disable
    #nullable restore
    public partial class TestComponent : global::Microsoft.AspNetCore.Components.ComponentBase
    #nullable disable
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(global::Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
#nullable restore
#line (3,3)-(7,5) "x:\dir\subdir\Test\TestComponent.cshtml"

    var output = string.Empty;
    if (__builder == null) output = "Builder is null!";
    else output = "Builder is not null!";
    

#line default
#line hidden
#nullable disable

            __builder.OpenElement(0, "p");
            __builder.AddContent(1, "Output: ");
            __builder.AddContent(2, 
#nullable restore
#line (7,17)-(7,23) "x:\dir\subdir\Test\TestComponent.cshtml"
output

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
        }
        #pragma warning restore 1998
    }
}
#pragma warning restore 1591
