using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ChatSimple.Data
{
    public class Account
    {
        [AllowNull]
        public string username {  get; set; }
        [Key]
        [Required(ErrorMessage = "email is required")]
        public string email {  get; set; }
        [Required(ErrorMessage = "password is required")]
        public string password { get; set; }
        public ICollection<Message>? messages { get; set; }
    }
}
