﻿#pragma checksum "TestFiles/IntegrationTests/CodeGenerationIntegrationTest/SingleLineControlFlowStatements.cshtml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "a66d420fecd4b03184392e5ffae1b5cbd3050be4e5c1a1d641eb271a35d9099d"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.TestFiles_IntegrationTests_CodeGenerationIntegrationTest_SingleLineControlFlowStatements), @"mvc.1.0.view", @"/TestFiles/IntegrationTests/CodeGenerationIntegrationTest/SingleLineControlFlowStatements.cshtml")]
namespace AspNetCore
{
    #line default
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Linq;
    using global::System.Threading.Tasks;
    using global::Microsoft.AspNetCore.Mvc;
    using global::Microsoft.AspNetCore.Mvc.Rendering;
    using global::Microsoft.AspNetCore.Mvc.ViewFeatures;
    #line default
    #line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"a66d420fecd4b03184392e5ffae1b5cbd3050be4e5c1a1d641eb271a35d9099d", @"/TestFiles/IntegrationTests/CodeGenerationIntegrationTest/SingleLineControlFlowStatements.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemMetadataAttribute("Identifier", "/TestFiles/IntegrationTests/CodeGenerationIntegrationTest/SingleLineControlFlowStatements.cshtml")]
    [global::System.Runtime.CompilerServices.CreateNewOnMetadataUpdateAttribute]
    #nullable restore
    public class TestFiles_IntegrationTests_CodeGenerationIntegrationTest_SingleLineControlFlowStatements : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("<p>Before Text</p>\r\n\r\n");
#nullable restore
#line (3,3)-(4,43) "TestFiles/IntegrationTests/CodeGenerationIntegrationTest/SingleLineControlFlowStatements.cshtml"

    if (DateTime.Now.ToBinary() % 2 == 0) 

#line default
#line hidden
#nullable disable

            Write(
#nullable restore
#line (4,45)-(4,77) "TestFiles/IntegrationTests/CodeGenerationIntegrationTest/SingleLineControlFlowStatements.cshtml"
"Current time is divisible by 2"

#line default
#line hidden
#nullable disable
            );
#nullable restore
#line (4,78)-(4,84) "TestFiles/IntegrationTests/CodeGenerationIntegrationTest/SingleLineControlFlowStatements.cshtml"
 else 

#line default
#line hidden
#nullable disable

            Write(
#nullable restore
#line (4,85)-(4,97) "TestFiles/IntegrationTests/CodeGenerationIntegrationTest/SingleLineControlFlowStatements.cshtml"
DateTime.Now

#line default
#line hidden
#nullable disable
            );
#nullable restore
#line (4,97)-(20,5) "TestFiles/IntegrationTests/CodeGenerationIntegrationTest/SingleLineControlFlowStatements.cshtml"


    object Bar()
    {
        if (DateTime.Now.ToBinary() % 2 == 0)
            return "Current time is divisible by 2";
        else if (DateTime.Now.ToBinary() % 3 == 0)
            return "Current time is divisible by 3";
        else
            return DateTime.Now;
    }

    for (var i = 0; i < 10; i++)
        // Incrementing a number
        i--;

    

#line default
#line hidden
#nullable disable

#nullable restore
#line (20,6)-(21,9) "TestFiles/IntegrationTests/CodeGenerationIntegrationTest/SingleLineControlFlowStatements.cshtml"
foreach (var item in new[] {"hello"})
        

#line default
#line hidden
#nullable disable

            Write(
#nullable restore
#line (21,10)-(21,14) "TestFiles/IntegrationTests/CodeGenerationIntegrationTest/SingleLineControlFlowStatements.cshtml"
item

#line default
#line hidden
#nullable disable
            );
#nullable restore
#line (21,14)-(24,9) "TestFiles/IntegrationTests/CodeGenerationIntegrationTest/SingleLineControlFlowStatements.cshtml"


    do
        

#line default
#line hidden
#nullable disable

            Write(
#nullable restore
#line (24,10)-(24,22) "TestFiles/IntegrationTests/CodeGenerationIntegrationTest/SingleLineControlFlowStatements.cshtml"
currentCount

#line default
#line hidden
#nullable disable
            );
#nullable restore
#line (24,22)-(31,9) "TestFiles/IntegrationTests/CodeGenerationIntegrationTest/SingleLineControlFlowStatements.cshtml"

    while (--currentCount >= 0);

    while (--currentCount <= 10)
        currentCount++;

    using (var reader = new System.IO.StreamReader("/something"))
        

#line default
#line hidden
#nullable disable

            Write(
#nullable restore
#line (31,10)-(31,28) "TestFiles/IntegrationTests/CodeGenerationIntegrationTest/SingleLineControlFlowStatements.cshtml"
reader.ReadToEnd()

#line default
#line hidden
#nullable disable
            );
#nullable restore
#line (33,6)-(34,24) "TestFiles/IntegrationTests/CodeGenerationIntegrationTest/SingleLineControlFlowStatements.cshtml"
lock (this)
        currentCount++;

#line default
#line hidden
#nullable disable

            WriteLiteral("\r\n");
            WriteLiteral("\r\n\r\n");
#nullable restore
#line (77,2)-(78,5) "TestFiles/IntegrationTests/CodeGenerationIntegrationTest/SingleLineControlFlowStatements.cshtml"
for (var i = 0; i < 10; i++)
    

#line default
#line hidden
#nullable disable

            Write(
#nullable restore
#line (78,6)-(78,7) "TestFiles/IntegrationTests/CodeGenerationIntegrationTest/SingleLineControlFlowStatements.cshtml"
i

#line default
#line hidden
#nullable disable
            );
            WriteLiteral("\r\n\r\n");
#nullable restore
#line (80,2)-(81,5) "TestFiles/IntegrationTests/CodeGenerationIntegrationTest/SingleLineControlFlowStatements.cshtml"
foreach (var item in new[] {"hello"})
    

#line default
#line hidden
#nullable disable

            Write(
#nullable restore
#line (81,6)-(81,10) "TestFiles/IntegrationTests/CodeGenerationIntegrationTest/SingleLineControlFlowStatements.cshtml"
item

#line default
#line hidden
#nullable disable
            );
            WriteLiteral("\r\n\r\n");
#nullable restore
#line (83,2)-(84,5) "TestFiles/IntegrationTests/CodeGenerationIntegrationTest/SingleLineControlFlowStatements.cshtml"
do
    

#line default
#line hidden
#nullable disable

            Write(
#nullable restore
#line (84,6)-(84,18) "TestFiles/IntegrationTests/CodeGenerationIntegrationTest/SingleLineControlFlowStatements.cshtml"
currentCount

#line default
#line hidden
#nullable disable
            );
#nullable restore
#line (84,18)-(85,29) "TestFiles/IntegrationTests/CodeGenerationIntegrationTest/SingleLineControlFlowStatements.cshtml"

while (--currentCount >= 0);

#line default
#line hidden
#nullable disable

            WriteLiteral("\r\n\r\n");
#nullable restore
#line (87,2)-(88,20) "TestFiles/IntegrationTests/CodeGenerationIntegrationTest/SingleLineControlFlowStatements.cshtml"
while (--currentCount <= 10)
    currentCount++;

#line default
#line hidden
#nullable disable

            WriteLiteral("\r\n\r\n");
#nullable restore
#line (90,2)-(92,5) "TestFiles/IntegrationTests/CodeGenerationIntegrationTest/SingleLineControlFlowStatements.cshtml"
using (var reader = new System.IO.StreamReader("/something"))
    // Reading the entire file
    

#line default
#line hidden
#nullable disable

            Write(
#nullable restore
#line (92,6)-(92,24) "TestFiles/IntegrationTests/CodeGenerationIntegrationTest/SingleLineControlFlowStatements.cshtml"
reader.ReadToEnd()

#line default
#line hidden
#nullable disable
            );
            WriteLiteral("\r\n\r\n");
#nullable restore
#line (94,2)-(95,20) "TestFiles/IntegrationTests/CodeGenerationIntegrationTest/SingleLineControlFlowStatements.cshtml"
lock (this)
    currentCount++;

#line default
#line hidden
#nullable disable

            WriteLiteral("\r\n\r\n");
#nullable restore
#line (97,2)-(97,12) "TestFiles/IntegrationTests/CodeGenerationIntegrationTest/SingleLineControlFlowStatements.cshtml"
if (true) 

#line default
#line hidden
#nullable disable

#nullable restore
#line (97,13)-(99,1) "TestFiles/IntegrationTests/CodeGenerationIntegrationTest/SingleLineControlFlowStatements.cshtml"
@GitHubUserName <p>Hello!</p>


#line default
#line hidden
#nullable disable

#nullable restore
#line (99,2)-(100,5) "TestFiles/IntegrationTests/CodeGenerationIntegrationTest/SingleLineControlFlowStatements.cshtml"
if (true) 
    

#line default
#line hidden
#nullable disable

            WriteLiteral(" <p>The time is ");
            Write(
#nullable restore
#line (100,24)-(100,36) "TestFiles/IntegrationTests/CodeGenerationIntegrationTest/SingleLineControlFlowStatements.cshtml"
DateTime.Now

#line default
#line hidden
#nullable disable
            );
            WriteLiteral("</p>\r\n");
            WriteLiteral("\r\n<p>After Text</p>");
        }
        #pragma warning restore 1998
#nullable restore
#line (37,13)-(52,13) "TestFiles/IntegrationTests/CodeGenerationIntegrationTest/SingleLineControlFlowStatements.cshtml"

    public string Foo()
    {
        var x = "";

        if (DateTime.Now.ToBinary() % 2 == 0)
            return "Current time is divisible by 2";
        else
            return "It isn't divisible by two";
        
        for (var i = 0; i < 10; i++)
            // Incrementing a number
            i--;

        foreach (var item in new[] {"hello"})
            

#line default
#line hidden
#nullable disable

        Write(
#nullable restore
#line (52,14)-(52,18) "TestFiles/IntegrationTests/CodeGenerationIntegrationTest/SingleLineControlFlowStatements.cshtml"
item

#line default
#line hidden
#nullable disable
        );
#nullable restore
#line (52,18)-(55,13) "TestFiles/IntegrationTests/CodeGenerationIntegrationTest/SingleLineControlFlowStatements.cshtml"


        do
            

#line default
#line hidden
#nullable disable

        Write(
#nullable restore
#line (55,14)-(55,26) "TestFiles/IntegrationTests/CodeGenerationIntegrationTest/SingleLineControlFlowStatements.cshtml"
currentCount

#line default
#line hidden
#nullable disable
        );
#nullable restore
#line (55,26)-(62,13) "TestFiles/IntegrationTests/CodeGenerationIntegrationTest/SingleLineControlFlowStatements.cshtml"

        while (--currentCount >= 0);

        while (--currentCount <= 10)
            currentCount++;

        using (var reader = new System.IO.StreamReader("/something"))
            

#line default
#line hidden
#nullable disable

        Write(
#nullable restore
#line (62,14)-(62,32) "TestFiles/IntegrationTests/CodeGenerationIntegrationTest/SingleLineControlFlowStatements.cshtml"
reader.ReadToEnd()

#line default
#line hidden
#nullable disable
        );
#nullable restore
#line (62,32)-(75,1) "TestFiles/IntegrationTests/CodeGenerationIntegrationTest/SingleLineControlFlowStatements.cshtml"


        lock (this)
            currentCount++;
    }

    int currentCount = 0;

    public void IncrementCount()
    {
        if (true) currentCount++;
    }


#line default
#line hidden
#nullable disable

        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
