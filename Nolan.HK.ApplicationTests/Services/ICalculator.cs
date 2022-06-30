﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nolan.HK.ApplicationTests.Services
{
    public interface ICalculator
    {
        int Add(int param1, int param2);
        int Subtract(int param1, int param2);
        int Multipy(int param1, int param2);
        int Divide(int param1, int param2);
        int ConvertUSDtoRMB(int unit);
    }
    public interface IUSD_RMB_ExchangeRateFeed
    {
        int GetActualUSDValue();
    }
}

