using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using MediatR;
using RothschildHouse.Library.Common.Clients.DataContracts.Common;

namespace RothschildHouse.Library.Common.Clients.DataContracts
{
    public class CreateCustomerCommand : IRequest<CreatedResponse<Guid?>>, IValidatableObject
    {
        public CreateCustomerCommand()
        {
        }

        public PersonModel Person { get; set; }
        public CompanyModel Company { get; set; }

        public string Country { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public Guid? AlienGuid { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Person == null && Company == null)
                yield return new ValidationResult("There is no information for person or company", new string[] { nameof(Person), nameof(Company) });

            if (Person != null && Company != null)
                yield return new ValidationResult("The information provided for customer is ambiguous", new string[] { nameof(Person), nameof(Company) });

            if (!string.IsNullOrEmpty(Country))
                yield return new ValidationResult("The country is required", new string[] { nameof(Country) });

            if (!string.IsNullOrEmpty(Email))
                yield return new ValidationResult("The email is required", new string[] { nameof(Email) });

            if (!AlienGuid.HasValue)
                yield return new ValidationResult("The alien guid is required", new string[] { nameof(AlienGuid) });
        }

        public virtual string ToJson()
            => JsonSerializer.Serialize(this);
    }

    public class PersonModel
    {
        public string GivenName { get; set; }
        public string MiddleName { get; set; }
        public string FamilyName { get; set; }
        public string FullName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Gender { get; set; }
    }

    public class CompanyModel
    {
        public string Name { get; set; }
    }
}
