using Syllab.Data;

namespace AxisOrder.Models.Views
{
    /// <summary>
    /// 产品视图
    /// </summary>
    [Table("[dbo].[Product]")]
    public class ProductView : BaseEntity<string>
    {
        /// <summary>
        /// 产品名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 产品编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 价格
        /// </summary>
        public decimal Price { get; set; }
    }
}
