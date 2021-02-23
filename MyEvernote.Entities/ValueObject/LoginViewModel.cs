using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyEvernote.Entities.ValueObject
{
    public class LoginViewModel
    {
        [Required]
        public string Username { get; set; }
        [Required(ErrorMessage ="Boş Geçilemez"),DataType(DataType.Password)]
        public string Password { get; set; }

    }
}