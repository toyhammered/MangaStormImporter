using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MangaStormImporter.Contracts.KitsuService
{
    public class LibraryEntryResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("attributes")]
        public LibraryEntryAttributes Attributes { get; set; }
    }

    public class LibraryEntryAttributes
    {
        [JsonProperty("progress")]
        public int Progress { get; set; }
    }
}
