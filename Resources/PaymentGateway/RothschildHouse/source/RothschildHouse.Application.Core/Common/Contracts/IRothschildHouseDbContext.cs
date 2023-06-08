﻿using Microsoft.EntityFrameworkCore;
using RothschildHouse.Domain.Core.Entities;

namespace RothschildHouse.Application.Core.Common.Contracts
{
    public interface IRothschildHouseDbContext
    {
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

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
