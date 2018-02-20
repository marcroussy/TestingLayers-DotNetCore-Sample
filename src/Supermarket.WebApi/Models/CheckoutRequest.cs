using System.ComponentModel.DataAnnotations;

namespace Supermarket.WebApi.Models
{
    public class CheckoutRequest
    {
        [Required]
        [RegularExpression("[ABCEGHJKLMNPRSTVXY][0-9][ABCEGHJKLMNPRSTVWXYZ] ?[0-9][ABCEGHJKLMNPRSTVWXYZ][0-9]")]
        public string PostalCode { get; set; }
    }
}