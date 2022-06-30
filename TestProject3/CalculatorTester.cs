using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestProject3
{
    // 添加TestFixture标识类是测试类
    [TestFixture]
    public class CalculatorTester
    {
        // 定义mock的逻辑
        private IUSD_RMB_ExchangeRateFeed prvGetMockExchangeRateFeed()
        {
            Mock<IUSD_RMB_ExchangeRateFeed> mockObject = new Mock<IUSD_RMB_ExchangeRateFeed>();
            mockObject.Setup(m => m.GetActualUSDValue()).Returns(500);
            return mockObject.Object;
        }
        // 测试divide方法
        [Test(Description = "Divide 9 by 3. Expected result is 3.")]
        public void TC1_Divide9By3()
        {
            IUSD_RMB_ExchangeRateFeed feed = this.prvGetMockExchangeRateFeed();
            ICalculator calculator = new Calculator(feed);
            int actualResult = calculator.Divide(9, 3);
            int expectedResult = 3;
            Assert.AreEqual(expectedResult, actualResult);
        }
       // [Test(Description = "Divide any number by zero. Should throw an System.DivideByZeroException exception.")]
       // [ExpectedException(typeof(System.DivideByZeroException))]
        //public void TC2_DivideByZero()
        //{
        //    IUSD_RMB_ExchangeRateFeed feed = this.prvGetMockExchangeRateFeed();
        //    ICalculator calculator = new Calculator(feed);
        //    int actualResult = calculator.Divide(9, 0);
        //}
        [Test(Description = "Convert 1 USD to RMB. Expected result is 500.")]
        public void TC3_ConvertUSDtoRMBTest()
        {
            IUSD_RMB_ExchangeRateFeed feed = this.prvGetMockExchangeRateFeed();
            ICalculator calculator = new Calculator(feed);
            int actualResult = calculator.ConvertUSDtoRMB(1);
            int expectedResult = 500;
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
