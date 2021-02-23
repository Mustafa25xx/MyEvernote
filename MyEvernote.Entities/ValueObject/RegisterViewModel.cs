using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyEvernote.Entities.ValueObject
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Kullanıcı Adı Boş Geçilemez"), StringLength(50)]
        public string Username { get; set; }
        [Required(ErrorMessage = " E Mail Adresi Boş Geçilemez"), StringLength(50),EmailAddress(ErrorMessage ="lÜTFEN gEÇERLİ BİR EMAİL GİRİNİZ")]

        public string Email { get; set; }
        [Required(ErrorMessage = "Password alanı Boş Geçilemez"), StringLength(50), DataType(DataType.Password)]

        public string Password { get; set; }
        [Required(ErrorMessage = "Password Alanı Boş Geçilemez"), StringLength(50), DataType(DataType.Password),Compare("Password",ErrorMessage ="Şifreler uyuşmuyor")]
        public string Repassword { get; set; }
    }
}