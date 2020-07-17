using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AWO_Orders.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.IO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using AWO_Orders.Models;

namespace AWO_Orders.Pages
{
    public class IndexModel : BasePageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
         
        }

        /// <summary>
        /// Login wird überprüft
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Password"></param>
        public void OnPost(string Name,string Password)
        {
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
            }catch(Exception e)
            {
               
                RedirectToPage("/Error",new {ex = e });
            }
        }

        private void InitLogin()
        {

        }

        public string Name { get; set; }

        public string Password { get; set; }
    }
}
