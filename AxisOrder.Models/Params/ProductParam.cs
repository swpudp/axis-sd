namespace AxisOrder.Models.Params
{
    /// <summary>
    /// 产品查询参数
    /// </summary>
    public class ProductParam : BaseParam
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 代码
        /// </summary>
        public string Code { get; set; }
    }
}
