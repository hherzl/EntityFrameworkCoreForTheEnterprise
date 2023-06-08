using System;
using System.Reflection;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RothschildHouse.Application.Core.Common.Contracts;
using RothschildHouse.Domain.Core.Common;
using RothschildHouse.Domain.Core.Entities;

namespace RothschildHouse.Infrastructure.Core.Persistence
{
    public class RothschildHouseDbContext : DbContext, IRothschildHouseDbContext
    {
        private readonly IMediator _mediator;

        public RothschildHouseDbContext(DbContextOptions<RothschildHouseDbContext> options, IMediator mediator)
            : base(options)
        {
            _mediator = mediator;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly())
                ;

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Card> Card { get; set; }
        public DbSet<ClientApplication> ClientApplication { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Currency> Currency { get; set; }
        public DbSet<EnumDescription> EnumDescription { get; set; }
        public DbSet<Transaction> Transaction { get; set; }
        public DbSet<TransactionLog> TransactionLog { get; set; }
        public DbSet<Person> Person { get; set; }

        public DbSet<VCardType> VCardType { get; set; }
        public DbSet<VTransactionStatus> VTransactionStatus { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.State == EntityState.Added && entry.Entity is AuditableEntity auditEntity)
                {
                    auditEntity.Active = true;

                    if (string.IsNullOrEmpty(auditEntity.CreationUser))
                        auditEntity.CreationUser = "dbContext";

                    auditEntity.CreationDateTime = DateTime.Now;
                }
            }

            _mediator?.DispatchNotifications(this);

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
