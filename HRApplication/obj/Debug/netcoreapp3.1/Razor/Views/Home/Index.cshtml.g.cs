#pragma checksum "D:\Users\bsi50128\source\repos\HRApplication\HRApplication\Views\Home\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "2ef7f1ec5192de2b0f98581eafba6ad3ac9371a0"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Index), @"mvc.1.0.view", @"/Views/Home/Index.cshtml")]
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
#line 1 "D:\Users\bsi50128\source\repos\HRApplication\HRApplication\Views\_ViewImports.cshtml"
using HRApplication;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Users\bsi50128\source\repos\HRApplication\HRApplication\Views\_ViewImports.cshtml"
using HRApplication.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"2ef7f1ec5192de2b0f98581eafba6ad3ac9371a0", @"/Views/Home/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"85d87a4a1ff03333950017c890cc7bf6fbfbb43e", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "D:\Users\bsi50128\source\repos\HRApplication\HRApplication\Views\Home\Index.cshtml"
  
    ViewData["Title"] = "Cart";
    ViewData["Notif"] = ViewBag.Notif;
    ViewData["Name"] = ViewBag.Name;

#line default
#line hidden
#nullable disable
            WriteLiteral(@"<!-- BREADCRUMB-->
<section class=""au-breadcrumb m-t-75"">
    <div class=""section__content section__content--p30"">
        <div class=""container-fluid"">
            <div class=""row"">
                <div class=""col-md-12"">
                    <div class=""au-breadcrumb-content"">
                        <div class=""au-breadcrumb-left"">
                            <span class=""au-breadcrumb-span"">You are here:</span>
                            <ul class=""list-unstyled list-inline au-breadcrumb__list"">
                                <li class=""list-inline-item active"">
                                    <a href=""#"">Home</a>
                                </li>
                                <li class=""list-inline-item seprate"">
                                    <span>/</span>
                                </li>
                                <li class=""list-inline-item"">Dashboard</li>
                            </ul>
                        </div>
                    </div>
        ");
            WriteLiteral(@"        </div>
            </div>
        </div>
    </div>
</section>
<!-- END BREADCRUMB-->
<!-- STATISTIC-->
<section class=""statistic"" style=""padding-top: 20px;"">
    <div class=""section__content section__content--p20"">
        <div class=""container-fluid"">
            <div class=""row"">
                <div class=""col-md-6 col-lg-4"">
                    <div class=""statistic__item"">
                        <h2 class=""number"">");
#nullable restore
#line 39 "D:\Users\bsi50128\source\repos\HRApplication\HRApplication\Views\Home\Index.cshtml"
                                      Write(ViewBag.Emp);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h2>\r\n                        <span class=\"desc\">Total Employee</span>\r\n                        <h3 class=\"number\" style=\"font-weight: 300;color: #4272d7;\">");
#nullable restore
#line 41 "D:\Users\bsi50128\source\repos\HRApplication\HRApplication\Views\Home\Index.cshtml"
                                                                               Write(ViewBag.Pria);

#line default
#line hidden
#nullable disable
            WriteLiteral("/");
#nullable restore
#line 41 "D:\Users\bsi50128\source\repos\HRApplication\HRApplication\Views\Home\Index.cshtml"
                                                                                             Write(ViewBag.Wanita);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</h3>
                        <span class=""desc"">Males/Females</span>
                        <div class=""icon"">
                            <i class=""zmdi zmdi-account-o""></i>
                        </div>
                    </div>
                </div>
                <div class=""col-md-6 col-lg-4"">
                    <div class=""statistic__item"">
                        <h2 class=""number"">");
#nullable restore
#line 50 "D:\Users\bsi50128\source\repos\HRApplication\HRApplication\Views\Home\Index.cshtml"
                                      Write(ViewBag.Out);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</h2>
                        <span class=""desc"">Out Today</span>
                        <div class=""icon"">
                            <i class=""zmdi zmdi-airplane""></i>
                        </div>
                    </div>
                </div>
                <div class=""col-md-6 col-lg-4"">
                    <div class=""statistic__item"">
                        <h2 class=""number"">");
#nullable restore
#line 59 "D:\Users\bsi50128\source\repos\HRApplication\HRApplication\Views\Home\Index.cshtml"
                                      Write(ViewBag.Att);

#line default
#line hidden
#nullable disable
            WriteLiteral("/");
#nullable restore
#line 59 "D:\Users\bsi50128\source\repos\HRApplication\HRApplication\Views\Home\Index.cshtml"
                                                   Write(ViewBag.Emp);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</h2>
                        <span class=""desc"">Today's Present</span>
                        <div class=""icon"">
                            <i class=""zmdi zmdi-accounts-add""></i>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</section>
<!-- END STATISTIC-->

<section>
    <div class=""section__content section__content--p20"">
        <div class=""container-fluid"">
            <div class=""row"">
                <div class=""col-xl-6"">
                    <div class=""recent-report2"">
                        <h3 class=""title-5"">UpComing Events</h3>
                        <!-- DATA TABLE-->
                        <div class=""table-responsive m-b-40"">
                            <table class=""table table-borderless table-data3"">
                                <thead>
                                    <tr>
                                        <th>Event</th>
                                        <th>Day<");
            WriteLiteral("/th>\r\n\r\n                                    </tr>\r\n                                </thead>\r\n                                <tbody>\r\n");
#nullable restore
#line 91 "D:\Users\bsi50128\source\repos\HRApplication\HRApplication\Views\Home\Index.cshtml"
                                         foreach (var x in ViewBag.Event)
                                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                            <tr>\r\n                                                <td>");
#nullable restore
#line 94 "D:\Users\bsi50128\source\repos\HRApplication\HRApplication\Views\Home\Index.cshtml"
                                               Write(x.EventName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                                <td>");
#nullable restore
#line 95 "D:\Users\bsi50128\source\repos\HRApplication\HRApplication\Views\Home\Index.cshtml"
                                               Write(String.Format("{0:dd MMMM}", x.TimeEvent));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                            </tr>\r\n");
#nullable restore
#line 97 "D:\Users\bsi50128\source\repos\HRApplication\HRApplication\Views\Home\Index.cshtml"

                                        }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
                                </tbody>
                            </table>
                        </div>
                        <!-- END DATA TABLE-->
                    </div>
                </div>
                <div class=""col-xl-6"">
                    <div class=""recent-report2"">
                        <h3 class=""title-5"">New Applicants</h3>
                        <!-- DATA TABLE-->
                        <div class=""table-responsive m-b-40"">
                            <table class=""table table-borderless table-data3"">
                                <thead>
                                    <tr>
                                        <th>Name</th>
                                        <th>Position</th>
                                        <th></th>
                                    </tr>
                                </thead>
                                <tbody>
");
#nullable restore
#line 122 "D:\Users\bsi50128\source\repos\HRApplication\HRApplication\Views\Home\Index.cshtml"
                                         foreach (var x in ViewBag.Appl)
                                        {
                                            var y = DateTime.Now - x.Created_at;
                                            var day = DateTime.Now.Day - x.Created_at.Day;                                           

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    <tr>                                        \r\n                                        <td>");
#nullable restore
#line 127 "D:\Users\bsi50128\source\repos\HRApplication\HRApplication\Views\Home\Index.cshtml"
                                       Write(x.Fullname);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                        <td>");
#nullable restore
#line 128 "D:\Users\bsi50128\source\repos\HRApplication\HRApplication\Views\Home\Index.cshtml"
                                       Write(x.Position);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>                                        \r\n");
#nullable restore
#line 129 "D:\Users\bsi50128\source\repos\HRApplication\HRApplication\Views\Home\Index.cshtml"
                                         if (day > 0)
                                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                            <td>");
#nullable restore
#line 131 "D:\Users\bsi50128\source\repos\HRApplication\HRApplication\Views\Home\Index.cshtml"
                                           Write(day);

#line default
#line hidden
#nullable disable
            WriteLiteral(" days ago</td>\r\n");
#nullable restore
#line 132 "D:\Users\bsi50128\source\repos\HRApplication\HRApplication\Views\Home\Index.cshtml"
                                        }

#line default
#line hidden
#nullable disable
#nullable restore
#line 133 "D:\Users\bsi50128\source\repos\HRApplication\HRApplication\Views\Home\Index.cshtml"
                                         if (day == 0 && int.Parse(y.ToString("hh")) > 0)
                                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                            <td>");
#nullable restore
#line 135 "D:\Users\bsi50128\source\repos\HRApplication\HRApplication\Views\Home\Index.cshtml"
                                           Write(y.ToString("hh"));

#line default
#line hidden
#nullable disable
            WriteLiteral(" hours ago</td>\r\n");
#nullable restore
#line 136 "D:\Users\bsi50128\source\repos\HRApplication\HRApplication\Views\Home\Index.cshtml"
                                        }

#line default
#line hidden
#nullable disable
#nullable restore
#line 137 "D:\Users\bsi50128\source\repos\HRApplication\HRApplication\Views\Home\Index.cshtml"
                                         if (int.Parse(y.ToString("hh")) == 0 && day == 0 && int.Parse(y.ToString("mm")) > 0 )
                                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                            <td>");
#nullable restore
#line 139 "D:\Users\bsi50128\source\repos\HRApplication\HRApplication\Views\Home\Index.cshtml"
                                           Write(y.ToString("mm"));

#line default
#line hidden
#nullable disable
            WriteLiteral(" minutes ago</td>\r\n");
#nullable restore
#line 140 "D:\Users\bsi50128\source\repos\HRApplication\HRApplication\Views\Home\Index.cshtml"
                                        }

#line default
#line hidden
#nullable disable
#nullable restore
#line 141 "D:\Users\bsi50128\source\repos\HRApplication\HRApplication\Views\Home\Index.cshtml"
                                         if (y.ToString("hh") == "00" && day == 0 && y.ToString("mm") == "00")
                                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                            <td>");
#nullable restore
#line 143 "D:\Users\bsi50128\source\repos\HRApplication\HRApplication\Views\Home\Index.cshtml"
                                           Write(y.ToString("ss"));

#line default
#line hidden
#nullable disable
            WriteLiteral(" seconds ago</td>\r\n");
#nullable restore
#line 144 "D:\Users\bsi50128\source\repos\HRApplication\HRApplication\Views\Home\Index.cshtml"
                                        }

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    </tr>\r\n");
#nullable restore
#line 146 "D:\Users\bsi50128\source\repos\HRApplication\HRApplication\Views\Home\Index.cshtml"
                                        }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                                </tbody>
                            </table>
                        </div>
                        <!-- END DATA TABLE-->
                    </div>
                </div>
            </div>
        </div>
    </div>
</section>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
