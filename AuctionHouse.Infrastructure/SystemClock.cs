﻿using AuctionHouse.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouse.Infrastructure
{
    public class SystemClock : IClock
    {
        public DateTime Time()
        {
            return DateTime.Now;
        }
    }
}
