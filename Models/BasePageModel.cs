using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWO_Orders.Models
{
    public class BasePageModel:PageModel
    {
        /// <summary>
        /// Setzt die allgemeinen Werte
        /// </summary>
        /// <param name="Model"></param>
        protected void SetBaseProbertiesOnPost(BaseModel Model)
        {
            Model.Changed = DateTime.Now;
            Model.ChangedBy = LoginItem.EmployeeId;
        }
    }
}
