using Nolan.HK.ApplicationTests.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CalculatorPkg
{
    public interface ICalculator
    {
        int Add(int param1, int param2);
        int Subtract(int param1, int param2);
        int Multipy(int param1, int param2);
        int Divide(int param1, int param2);
        int ConvertUSDtoRMB(int unit);
    }
    public class Calculator : ICalculator
    {
        private IUSD_RMB_ExchangeRateFeed _feed;
        public Calculator(IUSD_RMB_ExchangeRateFeed feed)
        {
            this._feed = feed;
        }
        #region ICalculator Members
        public int Add(int param1, int param2)
        {
            throw new NotImplementedException();
        }
        public int Subtract(int param1, int param2)
        {
            throw new NotImplementedException();
        }
        public int Multipy(int param1, int param2)
        {
            throw new NotImplementedException();
        }
        public int Divide(int param1, int param2)
        {
            return param1 / param2;
        }
        public int ConvertUSDtoRMB(int unit)
        {
            return unit * this._feed.GetActualUSDValue();
        }
        #endregion
    }
}