using System.ComponentModel.DataAnnotations;

namespace BlazorServerTestDynamicAccess.Models.DTOs
{
    public class UserRegisterDTO
    {

        public int Id { get; set; }
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Username { get; set; }
        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "بازنویسی کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Compare("Password", ErrorMessage = "{0} و {1} یکسان نیست.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        [Display(Name = "نام نمایشی")]
        public string DisplayName { get; set; }

    }
}
