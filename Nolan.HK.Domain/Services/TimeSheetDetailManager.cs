using Nolan.Domain.Shared;
using Nolan.HK.Domain.Entities;
using Nolan.HK.Migrations;
using Nolan.HK.Repository;
using Nolan.Infra.Repository.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nolan.HK.Domain.Services
{

    //public class TimeSheetDetailManager : IDomainService
    //{
    //   // private readonly IEfBasicRepository<TimeSheetDetail> _TimeSheetDetailManager;

    //    public TimeSheetDetailManager(
    //        //IEfBasicRepository<TimeSheetDetail> timeSheetDetailManager
    //         )
    //    {
    //      //  _TimeSheetDetailManager = timeSheetDetailManager;
    //    }
    //}
    public class ClassRepository : BaseRepository<TimeSheetDetail, Guid> 
    {
        private HomeWorkContext _context;
        public ClassRepository(HomeWorkContext Dbcontext) : base(Dbcontext)
        {
            _context = Dbcontext;
            
        }


    }
}
