using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWO_Orders
{
    /// <summary>
    /// Enthält alle Eigenschaften eines Userlogin
    /// </summary>
    public static class LoginItem
    {
        public static DateTime LoggedIn { get; set; }
        public static int EmployeeId { get; set; }
    }
}
