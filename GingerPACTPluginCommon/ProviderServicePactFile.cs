using Newtonsoft.Json;
using PactNet.Mocks.MockHttpService.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GingerPACTPluginCommon
{
    public class ProviderServicePactFile
    {
        [JsonProperty(PropertyName = "interactions")]
        public IEnumerable<ProviderServiceInteraction> Interactions { get; set; }
    }
}
