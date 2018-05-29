using System;
using System.Collections.Concurrent;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace AxisOrder.SoapMiddleware
{
    /// <summary>
    /// 企业总线访问客户端
    /// </summary>
    public class EsbClient : IDisposable
    {
        /// <summary>
        /// 通道工厂
        /// </summary>
        public ChannelFactory ChannelFactory { get; private set; }

        /// <summary>释放资源</summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
            DisposeFactory(ChannelFactory);
        }

        /// <summary>
        /// 获取客户端代理
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetClientProxy<T>(string uri)
        {
            var binding = CreateBinding();
            ChannelFactory = new ChannelFactory<T>(binding, new EndpointAddress(uri));
            return ((ChannelFactory<T>)ChannelFactory).CreateChannel();
        }

        /// <summary>
        /// 创建binding
        /// </summary>
        /// <param name="endpoint"></param>
        /// <returns></returns>
        private Binding CreateBinding()
        {
            return new BasicHttpBinding();
        }

        /// <summary>
        /// 释放ChannelFactory资源
        /// </summary>
        /// <param name="factory"></param>
        private void DisposeFactory(ChannelFactory factory)
        {
            if (factory == null)
            {
                return;
            }
            if (factory.State == CommunicationState.Faulted)
            {
                factory.Abort();
                return;
            }
            try
            {
                factory.Close();
            }
            catch (Exception)
            {
                factory.Abort();
                throw;
            }
        }
    }
}
