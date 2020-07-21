using AWO_Orders.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using AWO_Orders.Data;

namespace AWO_Orders.Components
{
    public partial class VOrderStatusComp : ComponentBase
    {

        public IList<V_Order_StatusModel> Models 
        { 
            get 
            { 
                return context.V_Order_Status.ToList(); 
            } 
        }

        [Parameter]
        public VOrderStatusContext context { get; set; }
    }
}
