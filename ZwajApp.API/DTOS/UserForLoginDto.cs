using System.ComponentModel.DataAnnotations;

namespace ZwajApp.API.DTOS
{
    public class UserForLoginDto
    {
         //[Required]
        public string UserName { get; set; }
        //[StringLength(8,MinimumLength=4,ErrorMessage="كلمة السر لا تزيد عن 8 ولا تقل عن 4")]
        public string Password { get; set; }
    }
}