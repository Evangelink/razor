﻿// <auto-generated/>
#pragma warning disable 1591
namespace Test
{
    #line default
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Linq;
    using global::System.Threading.Tasks;
#nullable restore
#line (1,2)-(1,40) "x:\dir\subdir\Test\TestComponent.cshtml"
using Microsoft.AspNetCore.Components;

#line default
#line hidden
#nullable disable
    #nullable restore
    public partial class TestComponent<
#nullable restore
#line (2,12)-(2,18) "x:\dir\subdir\Test\TestComponent.cshtml"
TItem1

#line default
#line hidden
#nullable disable
    ,
#nullable restore
#line (3,12)-(3,18) "x:\dir\subdir\Test\TestComponent.cshtml"
TItem2

#line default
#line hidden
#nullable disable
    ,
#nullable restore
#line (4,12)-(4,18) "x:\dir\subdir\Test\TestComponent.cshtml"
TItem3

#line default
#line hidden
#nullable disable
    > : global::Microsoft.AspNetCore.Components.ComponentBase
#nullable restore
#line (2,19)-(2,39) "x:\dir\subdir\Test\TestComponent.cshtml"
where TItem1 : Image

#line default
#line hidden
#nullable disable
#nullable restore
#line (3,19)-(3,38) "x:\dir\subdir\Test\TestComponent.cshtml"
where TItem2 : ITag

#line default
#line hidden
#nullable disable
#nullable restore
#line (4,19)-(4,46) "x:\dir\subdir\Test\TestComponent.cshtml"
where TItem3 : Image, new()

#line default
#line hidden
#nullable disable
    #nullable disable
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(global::Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.AddMarkupContent(0, "<h1>Item1</h1>");
#nullable restore
#line (7,2)-(9,5) "x:\dir\subdir\Test\TestComponent.cshtml"
foreach (var item2 in Items2)
{
    

#line default
#line hidden
#nullable disable

            __builder.OpenElement(1, "p");
            __builder.AddContent(2, 
#nullable restore
#line (10,6)-(10,25) "x:\dir\subdir\Test\TestComponent.cshtml"
ChildContent(item2)

#line default
#line hidden
#nullable disable
            );
            __builder.AddMarkupContent(3, ";\r\n    ");
            __builder.CloseElement();
#nullable restore
#line (12,1)-(12,2) "x:\dir\subdir\Test\TestComponent.cshtml"
}

#line default
#line hidden
#nullable disable

            __builder.AddMarkupContent(4, "<p>Item3</p>");
        }
        #pragma warning restore 1998
#nullable restore
#line (16,8)-(21,1) "x:\dir\subdir\Test\TestComponent.cshtml"

    [Parameter] public TItem1 Item1 { get; set; }
    [Parameter] public List<TItem2> Items2 { get; set; }
    [Parameter] public TItem3 Item3 { get; set; }
    [Parameter] public RenderFragment<TItem2> ChildContent { get; set; }

#line default
#line hidden
#nullable disable

    }
}
#pragma warning restore 1591
