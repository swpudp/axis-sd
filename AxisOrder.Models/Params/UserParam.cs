namespace AxisOrder.Models.Params
{
    /// <summary>
    /// 用户查询参数
    /// </summary>
    public class UserParam : BaseParam
    {
        /// <summary>
        /// 登录名
        /// </summary>
        public string LoginName { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// 身份证
        /// </summary>
        public string IdNo { get; set; }
    }
}
