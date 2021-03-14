using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inori.Models.randomc
{
    public class RandomCPostGuid
    {
        [JsonProperty("rendered")]
        public string Rendered { get; set; }
    }
}
