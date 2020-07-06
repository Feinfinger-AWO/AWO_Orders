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

namespace AWO_Orders.Pages
{
    public class IndexModel : PageModel
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
            if (!String.IsNullOrWhiteSpace(Name) && !String.IsNullOrWhiteSpace(Password))
            {
                var options = new DbContextOptionsBuilder<EmployeeContext>();
                options.UseSqlServer(LoginItem.ConnectionString);

                using (var context = new EmployeeContext(options.Options))
                {
                    byte[] salt = new byte[128 / 8];
                    var passwordHash = Convert.ToBase64String(KeyDerivation.Pbkdf2(Password, salt, KeyDerivationPrf.HMACSHA1, 10000, 256 / 8));

                    var employee = context.Employees.Where(e => e.SureName.ToLower() == Name.ToLower() && e.PasswordHash == passwordHash);

                    if (employee == null || !employee.Any())
                    {
                        LoginItem.LoginFailed = true;
                    }
                    else
                    {
                        LoginItem.LoggedIn = DateTime.Now;
                        LoginItem.EmployeeId = employee.First().Id;
                        LoginItem.LoginFailed = false;
                        LoginItem.Right = context.Rights.SingleOrDefault(r => r.Id == employee.First().RightId);
                    }
                }
            }
        }

        public string Name { get; set; }

        public string Password { get; set; }
    }
}
