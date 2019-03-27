using Newtonsoft.Json;
namespace CMS_DTO.CMSCentrifugo
{
    public class CentrifugoModel
    {
        public string method { get; set; }
        [JsonProperty(PropertyName = "params")]
        public CentrifugoParamModel param { get; set; }
    }
}