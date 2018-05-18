namespace AxisOrder.WebApi.Extensions
{
    /// <summary>
    /// JwtBeare Token认证配置选项
    /// </summary>
    public class JwtOptions
    {
        /// <summary>
        /// token是谁颁发的
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// token可以给哪些客户端使用
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// 加密的key
        /// </summary>
        public string SecretKey { get; set; }

        /// <summary>
        /// 过期时间 默认720分钟过期
        /// </summary>
        public int Expires { get; set; } = 720;

        /// <summary>
        /// 是否验证Token是否过期
        /// </summary>
        public bool ValidateLifetime { get; set; }
    }
}
