using System.Collections.Generic;
using System.Threading.Tasks;
using MangaStormImporter.Contracts;

namespace MangaStormImporter
{
    internal interface IFileHelper
    {
        List<MangaStormResponse> GetHistoryFormatted();
        Task WriteErrors(List<MangaStormResponse> errors);
    }
}