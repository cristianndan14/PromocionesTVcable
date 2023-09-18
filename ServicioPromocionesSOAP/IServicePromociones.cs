using System.Runtime.Serialization;
using System.ServiceModel;
using Negocio;

namespace ServicioPromocionesSOAP
{
    [ServiceContract]
    public interface IServicePromociones
    {
        [OperationContract]
        string ObtenerControlname();
        [OperationContract]
        string ObtenerPromociones(string peticion);
    }

    // Utilice un contrato de datos, como se ilustra en el ejemplo siguiente, para agregar tipos compuestos a las operaciones de servicio.
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";
        Request request = new Request();

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
        [DataMember]
        public Request Request
        {
            get { return request; }
            set { request = value; }
        }
        public object Object
        {
            get { return Object; }
            set { Object = value; }
        }

    }

}
