using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace Demo.Core.Models
{
    /// <summary>
    /// Modelo para construir un objeto imagen.
    /// </summary>
    [DataContract]
    public class MImage
    {
        [DataMember]
        [JsonProperty(PropertyName = "#text")]
        public string Url { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "size")]
        public string Size { get; set; }
    }
}
