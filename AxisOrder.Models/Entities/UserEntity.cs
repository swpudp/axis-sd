using Syllab.Data;

namespace AxisOrder.Models.Entities
{
    /// <summary>
    /// 用户注册实体
    /// </summary>
    public class UserRegisterEntity : BaseEntity<string>
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// 登录名
        /// </summary>
        public string LoginName { get; set; }

        /// <summary>
        /// 身份证
        /// </summary>
        public string IdNo { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 用户密码
        /// </summary>
        public string Password { get; set; }
    }
}
