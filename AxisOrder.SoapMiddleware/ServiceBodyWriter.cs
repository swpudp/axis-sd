using System.Runtime.Serialization;
using System.ServiceModel.Channels;
using System.Xml;

namespace AxisOrder.SoapMiddleware
{
    public class ServiceBodyWriter : BodyWriter
    {
        readonly string _serviceNamespace;
        readonly string _envelopeName;
        readonly string _resultName;
        object _result;

        public ServiceBodyWriter(string serviceNamespace, string envelopeName, string resultName, object result) : base(isBuffered: true)
        {
            _serviceNamespace = serviceNamespace;
            _envelopeName = envelopeName;
            _resultName = resultName;
            _result = result;
        }

        protected override void OnWriteBodyContents(XmlDictionaryWriter writer)
        {
            writer.WriteStartElement(_envelopeName, _serviceNamespace);
            var serializer = new DataContractSerializer(_result.GetType(), _resultName, _serviceNamespace);
            serializer.WriteObject(writer, _result);
            writer.WriteEndElement();
        }

    }
}
