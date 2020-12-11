using System.ComponentModel.DataAnnotations;

namespace Adiq.Common.Models
{
    public class Credential
    {
        [Required(ErrorMessage = "O campo usuário é obrigatório.")]
        public string Username { get; set; }
        [Required(ErrorMessage = "O campo senha é obrigatório.")]
        public string Password { get; set; }
    }
}
