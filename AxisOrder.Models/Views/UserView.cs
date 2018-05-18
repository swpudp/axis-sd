using Newtonsoft.Json;
using Syllab.Data;

namespace AxisOrder.Models.Views
{
    /// <summary>
    /// 用户信息
    /// </summary>
    [Table("[dbo].[User]")]
    public class UserView : BaseEntity<string>
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
        [JsonIgnore]
        public string Password { get; set; }
    }
}
