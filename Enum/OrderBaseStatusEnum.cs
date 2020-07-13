using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWO_Orders
{
    public enum OrderBaseStatusEnum
    {
        Open = 0,
        InProcess = 1,
        Okay = 2,
        Ordered = 3,
        Delivered = 4,
        Canceled =5
    }
}
