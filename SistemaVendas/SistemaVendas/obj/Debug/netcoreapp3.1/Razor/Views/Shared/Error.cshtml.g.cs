#pragma checksum "D:\Fabiana\Desenvolvimento_Full_Stack\TCC\GestaoVendas\SistemaVendas\SistemaVendas\Views\Shared\Error.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "3b90077b7c71f0b64e54b12b6bcc2acecd4b6058"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared_Error), @"mvc.1.0.view", @"/Views/Shared/Error.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "D:\Fabiana\Desenvolvimento_Full_Stack\TCC\GestaoVendas\SistemaVendas\SistemaVendas\Views\_ViewImports.cshtml"
using SistemaVendas;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Fabiana\Desenvolvimento_Full_Stack\TCC\GestaoVendas\SistemaVendas\SistemaVendas\Views\_ViewImports.cshtml"
using SistemaVendas.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3b90077b7c71f0b64e54b12b6bcc2acecd4b6058", @"/Views/Shared/Error.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"92b8143e28e2ecf99681febd0458bd3a62631f08", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared_Error : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<ErrorViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "D:\Fabiana\Desenvolvimento_Full_Stack\TCC\GestaoVendas\SistemaVendas\SistemaVendas\Views\Shared\Error.cshtml"
  
    ViewData["Title"] = "Ops. Houve um erro durante o processamento de sua requisição.";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"

<div class=""alert alert-danger alert-dismissible"" id=""msgBoxErro"" style=""width:100%; margin: 0 auto; margin-top:10px;"">
    <button type=""button"" class=""close""> </button>
    <span class=""glyphicon glyphicon-remove-sign"" aria-hidden=""true""></span>
    <span class=""errorMsg"">");
#nullable restore
#line 11 "D:\Fabiana\Desenvolvimento_Full_Stack\TCC\GestaoVendas\SistemaVendas\SistemaVendas\Views\Shared\Error.cshtml"
                      Write(ViewData["Title"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n</div>\r\n\r\n<br />\r\n<span class=\"errorMsg\" style=\"color:#a30f0f\">");
#nullable restore
#line 15 "D:\Fabiana\Desenvolvimento_Full_Stack\TCC\GestaoVendas\SistemaVendas\SistemaVendas\Views\Shared\Error.cshtml"
                                        Write(Model.Message);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n\r\n<br />\r\n<br />\r\n<br />\r\n");
#nullable restore
#line 20 "D:\Fabiana\Desenvolvimento_Full_Stack\TCC\GestaoVendas\SistemaVendas\SistemaVendas\Views\Shared\Error.cshtml"
 if (Model.ShowRequestId)
{


#line default
#line hidden
#nullable disable
            WriteLiteral("    <p>\r\n        <strong>Request ID:</strong> <code style=\"color:#8b0909\">");
#nullable restore
#line 24 "D:\Fabiana\Desenvolvimento_Full_Stack\TCC\GestaoVendas\SistemaVendas\SistemaVendas\Views\Shared\Error.cshtml"
                                                            Write(Model.RequestId);

#line default
#line hidden
#nullable disable
            WriteLiteral("</code>\r\n    </p>\r\n");
#nullable restore
#line 26 "D:\Fabiana\Desenvolvimento_Full_Stack\TCC\GestaoVendas\SistemaVendas\SistemaVendas\Views\Shared\Error.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<hr />\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ErrorViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
