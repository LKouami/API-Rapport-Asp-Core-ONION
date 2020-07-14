using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Rapport.Domaine.Models
{
    public class AuthenticateModel
    {
        [Required]
        public string UserLogin { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
