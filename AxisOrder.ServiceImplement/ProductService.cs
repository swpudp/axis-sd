using AxisOrder.ServcieContracts;
using Syllab.Components;
using System.Threading.Tasks;

namespace AxisOrder.ServiceImplement
{
    /// <summary>
    /// 测试服务
    /// </summary>
    [Component(LifeStyle.Scoped)]
    public class ProductService : IProductService
    {
        /// <summary>
        /// 更据代码获取
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public string GetByCode(string code)
        {
            return $"this is a random name:{code}";
        }

        /// <summary>
        /// 获取名称
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<string> GetByName(string name)
        {
            return await Task.FromResult($"this is a random name:{name}");
        }
    }
}
