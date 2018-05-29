using System;
using System.ServiceModel;
using System.Threading.Tasks;

namespace AxisOrder.ServcieContracts
{
    [ServiceContract]
    public interface IProductService
    {
        /// <summary>
        /// wcf测试接口
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [OperationContract]
        Task<string> GetByName(string name);

        [OperationContract]
        string GetByCode(string code);
    }
}
