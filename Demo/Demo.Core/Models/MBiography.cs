using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace Demo.Core.Models
{
    /// <summary>
    /// Modelo para construir un objeto biografía.
    /// </summary>
    [DataContract]
    public class MBiography
    {
        [DataMember]
        [JsonProperty(PropertyName = "published")]
        public string Published { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "summary")]
        public string Summary { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "content")]
        public string Content { get; set; }
    }
}
