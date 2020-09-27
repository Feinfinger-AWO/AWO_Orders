﻿using Microsoft.EntityFrameworkCore;

namespace AWO_Orders.Data
{
    public class OpenOrdersContext : DbContext
    {
        public OpenOrdersContext(DbContextOptions<OpenOrdersContext> options)
            : base(options)
        {
        }

        public DbSet<AWO_Orders.Models.V_OrdersModel> V_Orders { get; set; }
    }
}