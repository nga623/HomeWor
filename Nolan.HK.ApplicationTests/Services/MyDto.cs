using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nolan.HK.ApplicationTests.Services
{
    public class MyDto
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
    public interface IDataBaseContext<out T> where T : new()
    {
        T GetElementById(string id);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetElementsByName(string name);
        IEnumerable<T> GetPageElementsByName(string name, int startPage = 0, int pageSize = 20);
        IEnumerable<T> GetElementsByDate(DateTime? startDate, DateTime? endDate);
    }

    public class MyBll
    {
        private readonly IDataBaseContext<MyDto> _dataBaseContext;

        public MyBll(IDataBaseContext<MyDto> dataBaseContext)
        {
            _dataBaseContext = dataBaseContext;
        }

        public MyDto GetADto(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) return null;
            return _dataBaseContext.GetElementById(id);
        }
        [Test]
        public void SimpleTest()
        {
            var moq = new Mock<IDataBaseContext<MyDto>>();
            MyBll bll = new MyBll(moq.Object);
            var result = bll.GetADto(null);
            Assert.Null(result);
        }

    }

}
