using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWO_Orders.Components
{
    public class PaginatedList<T> : List<T>
    {
        public int PageIndex { get; private set; }
        public int TotalPages { get; private set; }

        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);

            this.AddRange(items);
        }

        public bool HasPreviousPage
        {
            get
            {
                return (PageIndex > 1);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (PageIndex < TotalPages);
            }
        }

        public static async Task<PaginatedList<T>> CreateAsync(
            IQueryable<T> source, int pageIndex, int pageSize)
        {
            List<T> items = null;
            var count = 0;

            if (source.Any())
            {
                count  = await source.CountAsync();

                items = await source.Skip(
                    (pageIndex - 1) * pageSize)
                    .Take(pageSize).ToListAsync();
            }
            else
            {
                 items = new List<T>();
            }

            return new PaginatedList<T>(items, count, pageIndex, pageSize);
        }
    }
}
