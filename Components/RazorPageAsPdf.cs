using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.DependencyInjection;
using Rotativa.AspNetCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AWO_Orders.Components
{
    /// <summary>
    /// Das ist geklaut (;
    /// </summary>
    public class RazorPageAsPdf : AsPdfResultBase
    {
        private readonly IRazorViewEngine _razorViewEngine;
        private readonly ITempDataProvider _tempDataProvider;
        private readonly IRazorPageActivator _activator;
        private string _razorPageName { get; set; }
        public PageModel PageModel { get; set; }
        public RazorPageAsPdf(PageModel pageModel)
        {
            PageModel = pageModel;
            var httpContext = pageModel.HttpContext;
            this._razorPageName = httpContext.Request.RouteValues["page"].ToString().Trim('/');
            if (string.IsNullOrEmpty(_razorPageName))
            {
                throw new ArgumentException("there's no such a 'page' in this context");
            }
            this._razorViewEngine = httpContext.RequestServices.GetRequiredService<IRazorViewEngine>();
            this._tempDataProvider = httpContext.RequestServices.GetRequiredService<ITempDataProvider>();
            this._activator = httpContext.RequestServices.GetRequiredService<IRazorPageActivator>();
        }

        private ViewContext GetViewContext(ActionContext actionContext, IRazorPage page, StringWriter sw)
        {
            var view = new RazorView(_razorViewEngine, _activator, new List<IRazorPage>(), page, HtmlEncoder.Default, new DiagnosticListener(nameof(RazorPageAsPdf)));
            return new ViewContext(actionContext, view, this.PageModel.ViewData, this.PageModel.TempData, sw, new HtmlHelperOptions());
        }

        private async Task<string> RenderPageAsString(ActionContext actionContext)
        {
            using (var sw = new StringWriter())
            {
                var pageResult = this._razorViewEngine.FindPage(actionContext, this._razorPageName); ;
                if (pageResult.Page == null)
                {
                    throw new ArgumentNullException($"The page {this._razorPageName} cannot be found.");
                }
                var viewContext = this.GetViewContext(actionContext, pageResult.Page, sw);
                var page = (Page)pageResult.Page;
                page.PageContext = this.PageModel.PageContext;
                page.ViewContext = viewContext;
                _activator.Activate(page, viewContext);
                await page.ExecuteAsync();
                return sw.ToString();
            }
        }

        protected override async Task<byte[]> CallTheDriver(ActionContext actionContext)
        {
            var html = await this.RenderPageAsString(actionContext);
            // copied from https://github.com/webgio/Rotativa.AspNetCore/blob/c907afa8c7dd6a565d307901741c336c429fc698/Rotativa.AspNetCore/ViewAsPdf.cs#L147-L151
            string baseUrl = string.Format("{0}://{1}", actionContext.HttpContext.Request.Scheme, actionContext.HttpContext.Request.Host);
            var htmlForWkhtml = Regex.Replace(html.ToString(), "<head>", string.Format("<head><base href=\"{0}\" />", baseUrl), RegexOptions.IgnoreCase);
            byte[] fileContent = WkhtmltopdfDriver.ConvertHtml(this.WkhtmlPath, this.GetConvertOptions(), htmlForWkhtml);
            return fileContent;
        }
        protected override string GetUrl(ActionContext context) => string.Empty;
    }
}
