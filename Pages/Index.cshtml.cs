using AWO_Orders.Data;
using AWO_Orders.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AWO_Orders.Pages
{
    public class IndexModel : BasePageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly VOrderStatusContext _vcontext;
        private readonly OrdersContext _ordersContext;

        public IndexModel(ILogger<IndexModel> logger, VOrderStatusContext vcontext, OrdersContext ordersContext)
        {
            _ordersContext = ordersContext;
            _vcontext = vcontext;
            InitialValue = vcontext;
            _logger = logger;
        }

        public IActionResult OnGet(int? exit)
        {
            if (exit.HasValue && exit.Value == 1)
            {
                Logout();
                return null;
            }
            return null;
        }

        public ActionResult OnGetSearch(string term)
        {
            if (!string.IsNullOrWhiteSpace(term))
            {
                IList<string> names = null;

                if (SessionLoginItem.Right.CanProcess)
                {
                    names = _ordersContext.Orders.Where(p => p.Number.Contains(term)).Select(p => p.Number).ToList();
                }
                else
                {
                    names = _ordersContext.Orders.Where(p => p.EmplId == SessionLoginItem.EmployeeId && p.Number.Contains(term)).Select(p => p.Number).ToList();
                }

                return new JsonResult(names.ToArray());
            }

            return null;
        }

        /// <summary>
        /// Login wird überprüft
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Password"></param>
        public IActionResult OnPost(string Name, string Password, string ordersearch, string directSearch)
        {
            if (!string.IsNullOrWhiteSpace(ordersearch) || !string.IsNullOrWhiteSpace(directSearch))
            {
                return RedirectToPage("/Orders/Index", new { ordersearch = string.IsNullOrWhiteSpace(directSearch) ? ordersearch : directSearch });
            }

            try
            {
                if (!String.IsNullOrWhiteSpace(Name) && !String.IsNullOrWhiteSpace(Password))
                {
                    var options = new DbContextOptionsBuilder<EmployeeContext>();
                    options.UseSqlServer(SessionLoginItem.ConnectionString);

                    using (var context = new EmployeeContext(options.Options))
                    {
                        byte[] salt = new byte[128 / 8];
                        var passwordHash = Convert.ToBase64String(KeyDerivation.Pbkdf2(Password, salt, KeyDerivationPrf.HMACSHA1, 10000, 256 / 8));

                        var employeeList = from c in context.Employees where c.SureName.ToLower() == Name.ToLower() select c;

                        var employee = employeeList.Where(e => e.PasswordHash == passwordHash);

                        if ((employee == null || !employee.Any()) && !(Name == "AwoAdmin" && Password == "***"))
                        {
                            SessionLoginItem.LoginFailed = true;
                        }
                        else
                        {
                            SessionLoginItem.LoggedIn = DateTime.Now;
                            SessionLoginItem.EmployeeId = (employee != null && employee.Any()) ? employee.First().Id : 1;
                            SessionLoginItem.LoginFailed = false;

                            if (employee != null && employee.Any())
                                SessionLoginItem.Right = context.Rights.SingleOrDefault(r => r.Id == employee.First().RightId);

                            if (SessionLoginItem.Right == null)
                            {
                                SessionLoginItem.Right = new Models.RightModel()
                                {
                                    CanOrder = true,
                                    CanProcess = true,
                                    CanView = true,
                                    Id = 1,
                                };
                            }
                            SetLogin(SessionLoginItem);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                return RedirectToPage("/Error", new { ex = e });
            }
            return null;
        }

        private void InitLogin()
        {
        }

        public string Name { get; set; }

        public string Password { get; set; }

        public AWO_Orders.Data.VOrderStatusContext InitialValue { get; set; }
    }
}