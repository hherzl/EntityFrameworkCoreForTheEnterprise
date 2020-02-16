using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RothschildHouse.Domain
{
#pragma warning disable CS1591
    public class Person
    {
        public Guid? PersonID { get; set; }

        public string GivenName { get; set; }

        public string MiddleName { get; set; }

        public string FamilyName { get; set; }

        public string FullName { get; set; }

        public DateTime? BirthDate { get; set; }
    }

    public class CreditCard
    {
        public Guid? CreditCardID { get; set; }

        public Guid? PersonID { get; set; }

        public string CardHolderName { get; set; }

        public string IssuingNetwork { get; set; }

        public string CardNumber { get; set; }

        public string Last4Digits { get; set; }

        public DateTime? ExpirationDate { get; set; }

        public string Cvv { get; set; }

        public decimal? Limit { get; set; }

        public decimal? AvailableFounds { get; set; }
    }

    public class PaymentMethod
    {
        public Guid? PaymentMethodID { get; set; }

        public string PaymentMethodName { get; set; }
    }

    public class PaymentTransaction
    {
        public Guid? PaymentTransactionID { get; set; }

        public Guid? CreditCardID { get; set; }

        public Guid? ConfirmationID { get; set; }

        public decimal? Amount { get; set; }

        public DateTime? PaymentDateTime { get; set; }
    }

    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.HasKey(p => p.PersonID);

            builder.Property(p => p.PersonID).IsRequired();
            builder.Property(p => p.GivenName).IsRequired();
            builder.Property(p => p.FamilyName).IsRequired();
            builder.Property(p => p.FullName).IsRequired();
        }
    }

    public class CreditCardConfiguration : IEntityTypeConfiguration<CreditCard>
    {
        public void Configure(EntityTypeBuilder<CreditCard> builder)
        {
            builder.HasKey(p => p.CreditCardID);

            builder.Property(p => p.CreditCardID).IsRequired();
            builder.Property(p => p.PersonID).IsRequired();
            builder.Property(p => p.CardHolderName).IsRequired();
            builder.Property(p => p.IssuingNetwork).IsRequired();
            builder.Property(p => p.CardNumber).IsRequired();
            builder.Property(p => p.Last4Digits).IsRequired();
            builder.Property(p => p.ExpirationDate).IsRequired();
            builder.Property(p => p.Cvv).IsRequired();
            builder.Property(p => p.Limit).IsRequired();
            builder.Property(p => p.AvailableFounds).IsRequired();
        }
    }

    public class PaymentMethodConfiguration : IEntityTypeConfiguration<PaymentMethod>
    {
        public void Configure(EntityTypeBuilder<PaymentMethod> builder)
        {
            builder.HasKey(p => p.PaymentMethodID);
        }
    }

    public class PaymentTransactionConfiguration : IEntityTypeConfiguration<PaymentTransaction>
    {
        public void Configure(EntityTypeBuilder<PaymentTransaction> builder)
        {
            builder.HasKey(p => p.PaymentTransactionID);

            builder.Property(p => p.PaymentTransactionID).IsRequired();
            builder.Property(p => p.CreditCardID).IsRequired();
            builder.Property(p => p.ConfirmationID).IsRequired();
            builder.Property(p => p.PaymentDateTime).IsRequired();
            builder.Property(p => p.PaymentDateTime).IsRequired();
        }
    }

    public class PaymentDbContext : DbContext
    {
        public PaymentDbContext(DbContextOptions<PaymentDbContext> options)
            : base(options)
        {
        }

        public DbSet<Person> People { get; set; }

        public DbSet<CreditCard> CreditCards { get; set; }

        public DbSet<PaymentMethod> PaymentMethods { get; set; }

        public DbSet<PaymentTransaction> PaymentTransactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .ApplyConfiguration(new PersonConfiguration())
                .ApplyConfiguration(new CreditCardConfiguration())
                .ApplyConfiguration(new PaymentMethodConfiguration())
                .ApplyConfiguration(new PaymentTransactionConfiguration())
            ;

            base.OnModelCreating(modelBuilder);
        }
    }
#pragma warning restore CS1591
}
