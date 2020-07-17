using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWO_Orders.Models
{
    public class BasePageModel:PageModel
    {
        private LoginItem loginItem;

        /// <summary>
        /// Setzt die allgemeinen Werte
        /// </summary>
        /// <param name="Model"></param>
        protected void SetBaseProbertiesOnPost(BaseModel Model)
        {
            Model.Changed = DateTime.Now;
            Model.ChangedBy = SessionLoginItem.EmployeeId;
        }

        public LoginItem GetLogin()
        {
            if (HttpContext.Session != null && HttpContext.Session.Keys.Contains("Login"))
            {
                var value = HttpContext.Session.GetString("Login");
                return value == null ? default(LoginItem) : JsonConvert.DeserializeObject<LoginItem>(value);
            }
            return new LoginItem() { EmployeeId = 0 };
        }

        protected void SetLogin(LoginItem item)
        {
            loginItem = item;
            HttpContext.Session.SetString("Login", JsonConvert.SerializeObject(item));
        }

        public LoginItem SessionLoginItem 
        { 
            get 
            {
                if (loginItem == null) loginItem = GetLogin();
                return loginItem;
            } 
        }
    }
}
