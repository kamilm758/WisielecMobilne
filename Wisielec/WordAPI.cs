using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;

namespace Wisielec
{
    public class WordAPI
    {
        [JsonProperty("word")]
        public string Word { get; set; }
        [JsonProperty("results")]
        public List<Results> Results { get; set; }
    }

    public class Results
    {
        [JsonProperty("definition")]
        public string Definition { get; set; }

        [JsonProperty("partOfSpeech")]
        public string PartOfSpeech { get; set; }

        [JsonProperty("typeOf")]
        public List<string> TypeOf { get; set; }

        [JsonProperty("derivation")]
        public List<string> Derivation { get; set; }
    }
}