using System;
using System.Threading.Tasks;

namespace MangaStormImporter.Interfaces
{
    public interface IAlgoliaService
    {
        Task<string> FindMedia(string query);
    }
}
