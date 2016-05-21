using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Demo.Core.Models
{
    /// <summary>
    /// Modelo para construir un objeto álbum.
    /// </summary>
    [DataContract]
    public class MAlbum
    {
        [DataMember]
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "artist")]
        public string Artist { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "image")]
        public List<MImage> Images { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "tracks")]
        public List<MTrack> Tracks { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "wiki")]
        public MBiography Wiki { get; set; }

        [JsonIgnore]
        public string Image { get; set; }

    }
}
