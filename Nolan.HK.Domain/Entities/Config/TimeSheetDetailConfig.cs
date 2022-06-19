
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nolan.Infra.Repository.Entities.EfEnities.Config;
using System;
using System.Collections.Generic;
 
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nolan.HK.Domain.Entities.Config
{
     
    public class TimeSheetDetailConfig : EntityTypeConfiguration<TimeSheetDetail>
    {
        public override void Configure(EntityTypeBuilder<TimeSheetDetail> builder)
        {
            base.Configure(builder);
            builder.Property(x => x.ProjectID);
            builder.Property(x => x.ProjectID).HasComment("项目id");
        }
    }
}
