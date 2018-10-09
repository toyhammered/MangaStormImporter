using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MangaStormImporter.Contracts;
using MangaStormImporter.Contracts.KitsuService;
using MangaStormImporter.Interfaces;
using MangaStormImporter.Libs;
using MangaStormImporter.Services;

namespace MangaStormImporter
{
    public class Import
    {
        private readonly IKitsuService _kitsuService;
        private readonly IAlgoliaService _algoliaService;
        private readonly IFileHelper _fileHelper;

        public List<MangaStormResponse> MangaCollection;
        public List<MangaStormResponse> Errors = new List<MangaStormResponse>();

        public Import()
        {
            _kitsuService = new KitsuService();
            _algoliaService = new AlgoliaService();
            _fileHelper = new FileHelper();
            MangaCollection = _fileHelper.GetHistoryFormatted();
        }

        public async Task Proccess()
        {
            string mangaId;
            LibraryEntryResponse libraryEntry;

            foreach (var manga in MangaCollection)
            {
                try
                {
                    mangaId = await _algoliaService.FindMedia(manga.Title);
                    libraryEntry = await _kitsuService.FindLibraryEntry(mangaId, "manga");
                    if (manga.Chapter > libraryEntry.Attributes.Progress)
                    {
                        await _kitsuService.UpdateChapter(manga.Chapter, libraryEntry.Id);
                    }
                }
                catch (Exception)
                {
                    Errors.Add(manga);
                }
            }

            await _fileHelper.WriteErrors(Errors);
        }
    }
}
