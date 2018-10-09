using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using MangaStormImporter.Contracts.KitsuService;

namespace MangaStormImporter.Interfaces
{
    public interface IKitsuService
    {
        Task<LibraryEntryResponse> FindLibraryEntry(string id, string type);
        Task UpdateChapter(int mangaStormChapter, string libraryEntryId);
    }
}
