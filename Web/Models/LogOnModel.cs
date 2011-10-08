using System.ComponentModel.DataAnnotations;

namespace Thoughtology.Expresso.Models
{
    /// <summary>
    /// Ther user authentication model.
    /// </summary>
    public class LogOnModel
    {
        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the user should be remembered on the client.
        /// </summary>
        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
