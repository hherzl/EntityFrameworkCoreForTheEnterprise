using System.Reflection;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RothschildHouse.Application.Core.Common.Contracts;
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
        public DbSet<PaymentTransaction> PaymentTransaction { get; set; }
        public DbSet<PaymentTransactionLog> PaymentTransactionLog { get; set; }
        public DbSet<Person> Person { get; set; }

        public DbSet<VCardType> VCardType { get; set; }
        public DbSet<VPaymentTransactionStatus> VPaymentTransactionStatus { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            _mediator?.DispatchNotifications(this);

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
