using System.Reflection;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RothschildHouse.API.PaymentGateway.Domain.Entities;
using RothschildHouse.API.PaymentGateway.Infrastructure.Persistence.QueryModels;

namespace RothschildHouse.API.PaymentGateway.Infrastructure.Persistence
{
#pragma warning disable CS1591
    public class RothschildHouseDbContext : DbContext
    {
        private readonly IMediator _mediator;

        public RothschildHouseDbContext(DbContextOptions<RothschildHouseDbContext> options, IMediator mediator)
            : base(options)
        {
            _mediator = mediator;
        }

        public DbSet<EnumDescription> EnumDescription { get; set; }
        public DbSet<ClientApplication> ClientApplication { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<Person> Person { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Card> Card { get; set; }
        public DbSet<Currency> Currency { get; set; }
        public DbSet<PaymentTransaction> PaymentTransaction { get; set; }
        public DbSet<PaymentTransactionLog> PaymentTransactionLog { get; set; }

        public DbSet<VCardType> VCardType { get; set; }
        public DbSet<VPaymentTransactionStatus> VPaymentTransactionStatus { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            _mediator?.DispatchNotifications(this);

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
