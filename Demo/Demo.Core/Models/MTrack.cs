using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Demo.Core.Models
{
    /// <summary>
    /// Modelo para construir un objeto track.
    /// </summary>
    [DataContract]
    public class MTrack
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
        [JsonProperty(PropertyName = "artist")]
        public MArtist Artist { get; set; }

        //[JsonIgnore]
        [DataMember]
        public string ArtistName { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "image")]
        public List<MImage> Images { get; set; }

        [DataMember]
        //[JsonIgnore]
        public string Image { get; set; }
    }
}
