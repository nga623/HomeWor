﻿using Microsoft.EntityFrameworkCore;
using Nolan.Infra.Repository;
using Nolan.Infra.Repository.Entities;
using Nolan.Infra.Repository.IRepositories.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Nolan.Infra.EfCore.PostGresSql
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Major Code Smell", "S125:Sections of code should not be commented out", Justification = "<挂起>")]
    public class HomeWorkContext : DbContext
    {
        private readonly IOperater _operater;
        private readonly IEntityInfo _entityInfo;
        private readonly UnitOfWorkStatus _unitOfWorkStatus;
        public HomeWorkContext(DbContextOptions<HomeWorkContext> options
            , [NotNull] IEntityInfo entityInfo)

            : base(options)
        {
            _entityInfo = entityInfo;
        }
        //public HomeWorkContext([NotNull] DbContextOptions options, IOperater operater, [NotNull] IEntityInfo entityInfo, UnitOfWorkStatus unitOfWorkStatus)
        //    : base(options)
        //{
        //    _operater = operater;
        //    _entityInfo = entityInfo;
        //    _unitOfWorkStatus = unitOfWorkStatus;

        //    //关闭DbContext默认事务
        //    Database.AutoTransactionsEnabled = false;
        //    //关闭查询跟踪
        //    //ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        //}

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var changedEntities = this.SetAuditFields();

            //没有自动开启事务的情况下,保证主从表插入，主从表更新开启事务。
            var isManualTransaction = false;
            if (!Database.AutoTransactionsEnabled && !_unitOfWorkStatus.IsStartingUow && changedEntities > 1)
            {
                isManualTransaction = true;
                Database.AutoTransactionsEnabled = true;
            }

            var result = base.SaveChangesAsync(cancellationToken);

            //如果手工开启了自动事务，用完后关闭。
            if (isManualTransaction)
                Database.AutoTransactionsEnabled = false;

            return result;
        }

        private int SetAuditFields()
        {
            var allEntities = ChangeTracker.Entries<Entity>();

            //var allBasicAuditEntities = ChangeTracker.Entries<IBasicAuditInfo>().Where(x => x.State == EntityState.Added);
            //foreach (var entry in allBasicAuditEntities)
            //{
            //    var entity = entry.Entity;
            //    {
            //        entity.CreateBy = _operater.Id;
            //        entity.CreateTime = DateTime.Now;
            //    }
            //}

            //var auditFullEntities = ChangeTracker.Entries<IFullAuditInfo>().Where(x => x.State == EntityState.Modified);
            //foreach (var entry in auditFullEntities)
            //{
            //    var entity = entry.Entity;
            //    {
            //        entity.ModifyBy = _operater.Id;
            //        entity.ModifyTime = DateTime.Now;
            //    }
            //}

            //var cusAudit = ChangeTracker.Entries<ICusAudit>().Where(x => x.State == EntityState.Modified || x.State == EntityState.Added);
            //foreach (var entry in cusAudit)
            //{
            //    var entity = entry.Entity;
            //    {
            //        if (string.IsNullOrEmpty(entity.CustomerId) == true)
            //            entity.CustomerId = _operater.CustomerId;

            //        if (string.IsNullOrEmpty(_operater.Name) == false)
            //            entity.CreateName = _operater.Name;
            //    }
            //}


            return allEntities.Count();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           // modelBuilder.HasCharSet("utf8mb4 ");

            var (Assembly, Types) = _entityInfo.GetEntitiesInfo();

            foreach (var entityType in Types)
            {
                modelBuilder.Entity(entityType);
            }

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly);

            base.OnModelCreating(modelBuilder);
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.LogTo(Console.WriteLine);
    }
}
