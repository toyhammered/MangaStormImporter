using System;
using System.Threading.Tasks;
using Algolia.Search;
using MangaStormImporter.Interfaces;
using Newtonsoft.Json.Linq;

namespace MangaStormImporter.Services
{
    public class AlgoliaService : IAlgoliaService
    {
        private readonly AlgoliaClient client;
        private readonly Index mediaIndex;

        public AlgoliaService()
        {
            client = new AlgoliaClient(
                DotNetEnv.Env.GetString("ALGOLIA_APPLICATION_ID"),
                DotNetEnv.Env.GetString("ALGOLIA_MEDIA_KEY")
            );

            mediaIndex = client.InitIndex(DotNetEnv.Env.GetString("ALGOLIA_MEDIA_INDEX"));
        }

        public async Task<string> FindMedia(string query)
        {
            JObject result = await mediaIndex.SearchAsync(new Query(query)
                                                      .SetFilters("kind:manga AND NOT subtype:novel")
                                                      .SetAttributesToRetrieve(new string[] { "id" })
                                                      .SetNbHitsPerPage(1));

            return result["hits"].First["id"].ToString();
        }
    }
}
