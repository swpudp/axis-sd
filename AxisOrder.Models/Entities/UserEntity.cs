using Syllab.Data;
using System.ComponentModel.DataAnnotations;

namespace AxisOrder.Models.Entities
{
    /// <summary>
    /// 用户注册实体
    /// </summary>
    public class UserRegister : BaseEntity<string>
    {
        /// <summary>
        /// 姓名
        /// </summary>
        [Required(ErrorMessage = "姓名必填")]
        public string FullName { get; set; }

        /// <summary>
        /// 登录名
        /// </summary>
        [Required(ErrorMessage = "登录名必填")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "登录名长度为3-20位")]
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "登录名只能有字母、数字、_组合")]
        public string LoginName { get; set; }

        /// <summary>
        /// 身份证
        /// </summary>
        public string IdNo { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        [Required(ErrorMessage = "手机号必填")]
        [RegularExpression(@"^(13[0-9]|14[579]|15[0-3,5-9]|16[6]|17[0135678]|18[0-9]|19[89])\d{8}$", ErrorMessage = "手机号格式不对")]
        public string Mobile { get; set; }

        /// <summary>
        /// 用户密码
        /// </summary>
        [Required(ErrorMessage = "密码必填")]
        public string Password { get; set; }
    }

    /// <summary>
    /// 修改用户
    /// </summary>
    public class UserUpdate : BaseEntity<string>
    {
        /// <summary>
        /// 姓名
        /// </summary>
        [Required(ErrorMessage = "姓名必填")]
        public string FullName { get; set; }

        /// <summary>
        /// 身份证
        /// </summary>
        //[CustomValidation(typeof(StringExtensions),"",ErrorMessage ="")]
        public string IdNo { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        [Required(ErrorMessage = "手机号必填")]
        [RegularExpression(@"^(13[0-9]|14[579]|15[0-3,5-9]|16[6]|17[0135678]|18[0-9]|19[89])\d{8}$", ErrorMessage = "手机格式不对")]
        public string Mobile { get; set; }
    }
}
