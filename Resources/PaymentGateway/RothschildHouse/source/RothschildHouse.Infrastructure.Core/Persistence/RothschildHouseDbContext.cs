﻿using System.Reflection;
using Microsoft.EntityFrameworkCore;
using RothschildHouse.Application.Core.Common.Contracts;
using RothschildHouse.Domain.Core.Entities;

namespace RothschildHouse.Infrastructure.Core.Persistence
{
    public class RothschildHouseDbContext : DbContext, IRothschildHouseDbContext
    {
        public RothschildHouseDbContext(DbContextOptions<RothschildHouseDbContext> options)
            : base(options)
        {
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
    }
}