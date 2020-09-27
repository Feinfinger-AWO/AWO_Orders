using AWO_Orders.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;

namespace AWO_Orders.Pages
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class ErrorModel : BasePageModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        private readonly ILogger<ErrorModel> _logger;

        public ErrorModel(ILogger<ErrorModel> logger)
        {
            _logger = logger;
        }

        public IActionResult OnGet(Exception ex)
        {
            LastException = ex;
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            if (SessionLoginItem.Right == null && SessionLoginItem.EmployeeId == 0)
            {
                return RedirectToPage("/Index");
            }
            return null;
        }

        public Exception LastException { get; set; }
    }
}