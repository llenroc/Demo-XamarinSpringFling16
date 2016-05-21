using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Demo.Core.Models
{
    /// <summary>
    /// Modelo para construir un objeto artista.
    /// </summary>
    [DataContract]
    public class MArtist
    {
        [DataMember]
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "listeners")]
        public string Listeners { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "image")]
        public List<MImage> Images { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "bio")]
        public MBiography Biography { get; set; }

        [JsonIgnore]
        public string Image { get; set; }
    }
}
