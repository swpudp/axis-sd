namespace AxisOrder.Models.Params
{
    /// <summary>
    /// 基础查询参数
    /// </summary>
    public class BaseParam
    {
        /// <summary>
        /// 每页显示数量
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 当前页数
        /// </summary>
        public int PageIndex { get; set; }
    }
}
