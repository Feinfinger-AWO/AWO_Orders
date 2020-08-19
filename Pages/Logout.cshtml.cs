using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AWO_Orders.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AWO_Orders.Pages
{
    public class LogoutModel : BasePageModel
    {
        public void OnGet()
        {
            Logout();
        }
    }
}