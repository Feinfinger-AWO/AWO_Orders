using AWO_Orders.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace AWO_Orders
{
    /// <summary>
    /// Enthält alle Eigenschaften eines Userlogin
    /// </summary>
    public static class LoginItem
    {
        /// <summary>
        /// Gets or sets the login timestamp
        /// </summary>
        public static DateTime LoggedIn { get; set; }
        /// <summary>
        /// Gets or sets the logged in user id
        /// </summary>
        public static int EmployeeId { get; set; }
        /// <summary>
        /// Get or sets the current access right
        /// </summary>
        public static RightModel Right { get; set; }
        /// <summary>
        /// Gets or sets whether the login failed
        /// </summary>
        public static bool LoginFailed { get; set; }
        /// <summary>
        /// Gets or sets the current connection string
        /// </summary>
        public static string ConnectionString { get; set; }
    }
}
