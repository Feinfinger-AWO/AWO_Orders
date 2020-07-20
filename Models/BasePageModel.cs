using AWO_Orders.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
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

        /// <summary>
        /// Login Daten werden der Session entnommen
        /// </summary>
        /// <returns></returns>
        public LoginItem GetLogin()
        {
            if (HttpContext.Session != null && HttpContext.Session.Keys.Contains("Login"))
            {
                var value = HttpContext.Session.GetString("Login");
                return value == null ? default(LoginItem) : JsonConvert.DeserializeObject<LoginItem>(value);
            }
            return new LoginItem() { EmployeeId = 0 };
        }

        /// <summary>
        /// Login Daten werden in die Session geschrieben
        /// </summary>
        /// <param name="item"></param>
        protected void SetLogin(LoginItem item)
        {
            loginItem = item;
            HttpContext.Session.SetString("Login", JsonConvert.SerializeObject(item));
        }

        /// <summary>
        /// Login Daten werden aus der Session gelöscht
        /// </summary>
        protected void Logout()
        {
            HttpContext.Session.Clear();
        }

        /// <summary>
        /// Veränderungen an den Bestelungen werden mit gelogt
        /// </summary>
        /// <param name="order"></param>
        /// <param name="typ"></param>
        /// <param name="positionId"></param>
        public async Task WriteLog(int orderId, LogChangeTypesEnum typ,int? positionId)
        {
            var options = new DbContextOptionsBuilder<OrderLogEntriesContext>();
            options.UseSqlServer(SessionLoginItem.ConnectionString);
            var logContext = new OrderLogEntriesContext(options.Options);
            await logContext.WriteLog(orderId, typ, SessionLoginItem.EmployeeId,positionId);
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
