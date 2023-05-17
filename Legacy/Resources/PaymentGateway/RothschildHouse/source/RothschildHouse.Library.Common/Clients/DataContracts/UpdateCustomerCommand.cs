using System.ComponentModel.DataAnnotations;
using MediatR;
using RothschildHouse.Library.Common.Clients.DataContracts.Common;

namespace RothschildHouse.Library.Common.Clients.DataContracts
{
    public class UpdateCustomerCommand : IRequest<Response>, IValidatableObject
    {
        public Guid? Id { get; set; }

        public string CountryId { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public Guid? UCommerceGuid { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(Email))
                yield return new ValidationResult("The email field is required", new string[] { nameof(Email) });
        }
    }
}
