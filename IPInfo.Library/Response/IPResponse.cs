using IPInfo.Library.Interfaces;
using System.Runtime.Serialization;

namespace IPInfo.Library.Response
{
    [DataContract]
    public class IPResponse : IPDetails
    {
        [DataMember(Name = "city")]
        public string City { get; set; }

        [DataMember(Name = "country_name")]
        public string Country { get; set; }

        [DataMember(Name = "continent_name")]
        public string Continent { get; set; }

        [DataMember(Name = "latitude")]
        public double Latitude { get; set; }

        [DataMember(Name = "longitude")]
        public double Longitude { get; set; }
    }
}
