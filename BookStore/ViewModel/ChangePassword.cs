using System.ComponentModel.DataAnnotations;

namespace BookStore.ViewModel
{
    public class ChangePassword
    {
        public string Id { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Compare("Password", ErrorMessage = "Confirm password doesn't match, Type again !")]
        public string ConfirmPassword { get; set; } = null!;
    }
}
