﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouse.Application.Services
{
    public interface IClock
    {
        DateTimeOffset Time();
    }
}
