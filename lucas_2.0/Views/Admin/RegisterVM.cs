using System.ComponentModel.DataAnnotations;

namespace lucas_2._0.Views.Admin
{
    public class RegisterVM
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Repeat password")]
        [Compare(nameof(Password))]
        public string PasswordRepeat { get; set; }

		public string FirstName { get; set; }
		public string LastName { get; set; }
        //[LegalAge]
        
        public DateTime BirthDate { get; set; }
	}
}
