using System.ComponentModel.DataAnnotations;
//用于进行数据验证的特性，例如[Required]、[EmailAddress]和[MinLength]，这些特性可以确保在创建UserLoginRequestDto对象时，Email和Password属性满足特定的验证规则。
//方便使用数据验证的特性，不然得手动写完整格式，不方便。


namespace RealEstateMediaPlatform.API.DTOs.User
{
    public class UserLoginRequestDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        //string是非空类型，所以初始化 =string.empty 这是为了避免null异常产生报错信息。写上之后编译器知道这个一定是有初始值的，就不会报错

        [Required]
        [MinLength(6)]
        public string Password { get; set; } = string.Empty;
    }
}
