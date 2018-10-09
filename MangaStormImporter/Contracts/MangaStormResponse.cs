using System;

namespace MangaStormImporter.Contracts
{
    public class MangaStormResponse
    {
        public string Source { get; set; }
        public string Title { get; set; }
        public int Chapter { get; set; }
        public string Unused1 { get; set; }
        public string Unused2 { get; set; }
        public string Unused3 { get; set; }
        public string Site { get; set; }
    }
}
