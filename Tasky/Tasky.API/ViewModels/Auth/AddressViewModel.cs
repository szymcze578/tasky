using System.ComponentModel.DataAnnotations;

namespace Tasky.API.ViewModels.Auth
{
    public class AddressViewModel
    {
        [StringLength(100)]
        public string Street { get; set; }

        [StringLength(50)]
        public string City { get; set; }

        [RegularExpression(@"^\d{2}-\d{3}$|\d{5}(-\d{4})?$", ErrorMessage = "Invalid postal code format.")]
        public string PostalCode { get; set; }

        [StringLength(50)]
        public string Country { get; set; }
    }
}
