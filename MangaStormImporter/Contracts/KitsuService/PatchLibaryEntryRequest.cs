using System;
using Newtonsoft.Json;

namespace MangaStormImporter.Contracts.KitsuService
{
    public class PatchLibaryEntryRequest
    {
        [JsonProperty("data")]
        public LibraryEntryData Data { get; set; }
    }

    public class LibraryEntryData
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("attributes")]
        public LibraryEntryAttributes Attributes { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
