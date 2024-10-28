using MediatR;
using RothschildHouse.API.PaymentGateway.Domain.Entities;
using RothschildHouse.API.PaymentGateway.Domain.Exceptions;
using RothschildHouse.API.PaymentGateway.Infrastructure.Persistence;
using RothschildHouse.Library.Common.Clients.DataContracts;
using RothschildHouse.Library.Common.Clients.DataContracts.Common;

namespace RothschildHouse.API.PaymentGateway.Features.Customers.Commands
{
#pragma warning disable CS1591
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, CreatedResponse<Guid?>>
    {
        private readonly RothschildHouseDbContext _dbContext;

        public CreateCustomerCommandHandler(RothschildHouseDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CreatedResponse<Guid?>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var alien = await _dbContext.GetCustomerByAlienGuidAsync(request.AlienGuid, tracking: false, include: false, cancellationToken);
            if (alien != null)
                throw new RothschildHouseException($"There is alredy a customer with Id: '{request.AlienGuid}'");

            var country = await _dbContext.GetCountryByTwoLetterIsoCodeAsync(request.Country)
                ?? throw new RothschildHouseException($"There is no definition for Country with ISO Code '{request.Country}'");

            int? personId = null;
            if (request.Person != null)
            {
                var person = new Person
                {
                    GivenName = request.Person.GivenName,
                    MiddleName = request.Person.MiddleName,
                    FamilyName = request.Person.FamilyName,
                    BirthDate = request.Person.BirthDate,
                    Gender = request.Person.Gender,
                    Active = true,
                    CreationUser = "api",
                    CreationDateTime = DateTime.Now
                };

                person.FullName = string
                    .Join(" ", new string[] { request.Person.GivenName, request.Person.MiddleName, request.Person.FamilyName }.Where(item => !string.IsNullOrEmpty(item)))
                    ;

                _dbContext.Person.Add(person);

                await _dbContext.SaveChangesAsync(cancellationToken);

                personId = person.Id;
            }

            int? companyId = null;
            if (request.Company != null)
            {
                var company = new Company
                {
                    Name = request.Company.Name,
                    Active = true,
                    CreationUser = "api",
                    CreationDateTime = DateTime.Now
                };

                _dbContext.Company.Add(company);

                await _dbContext.SaveChangesAsync(cancellationToken);

                companyId = company.Id;
            }

            var customer = new Customer
            {
                Id = Guid.NewGuid(),
                PersonId = personId,
                CompanyId = companyId,
                CountryId = country.Id,
                AddressLine1 = request.AddressLine1,
                AddressLine2 = request.AddressLine2,
                Phone = request.Phone,
                Email = request.Email,
                AlienGuid = request.AlienGuid,
                Active = true,
                CreationUser = "api",
                CreationDateTime = DateTime.Now
            };

            _dbContext.Customer.Add(customer);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return new CreatedResponse<Guid?>(customer.Id);
        }
    }
}
