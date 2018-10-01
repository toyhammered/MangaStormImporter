using System;
using System.Collections.Generic;
using MangaStormImporter.Contracts;
using MangaStormImporter.Libs;

namespace MangaStormImporter
{
    public class Import
    {
        //private readonly IKitsuService _kitsuService;

        public List<MangaStormResponse> MangaCollection;

        public Import()
        {
            //_kitsuService = new KitsuService();
            MangaCollection = new FileHelper().GetHistoryFormatted();
        }

        public void Proccess()
        {
            foreach (var manga in MangaCollection)
            {

            }
        }
    }
}
