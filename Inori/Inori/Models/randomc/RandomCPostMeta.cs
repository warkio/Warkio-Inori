using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Inori.Models.randomc
{
    public class RandomCPostMeta
    {
        [JsonProperty("spay_email")]
        public string SpayEmail { get; set; }
    }
}
