using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using MangaStormImporter.Contracts.KitsuService;
using MangaStormImporter.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MangaStormImporter.Libs
{
    public class KitsuService : IKitsuService
    {
        private static readonly HttpClient _client = new HttpClient();
        private const string AUTH_URL = "https://kitsu.io/api/oauth/token";
        private const string BASE_PATH = "https://kitsu.io/api/edge/";

        private readonly string username;
        private readonly string password;
        public string authToken;

        public KitsuService()
        {
            username = DotNetEnv.Env.GetString("KITSU_USERNAME");
            password = DotNetEnv.Env.GetString("KITSU_PASSWORD");
            authToken = Authorization();
        }

        public async Task<LibraryEntryResponse> FindLibraryEntry(string id, string type)
        {
            string filterType = ChooseFilterType(type);

            string path = $"{BASE_PATH}library-entries?filter[userId]=4195&filter[{filterType}]={id}";
            var response = await _client.GetAsync(new Uri(path));
            var responseBody = response.Content.ReadAsStringAsync().Result;
            return FormattedLibraryEntry(responseBody);
        }

        public async Task UpdateChapter(int mangaStormChapter, string libraryEntryId)
        {
            string path = $"{BASE_PATH}library-entries/{libraryEntryId}";
            SetHeaders();
            var response = await _client.PatchAsync(
                new Uri(path),
                UpdateChapterBody(mangaStormChapter, libraryEntryId)
            );

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception();
            }
        }

        private void SetHeaders()
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authToken);
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.api+json"));
        }

        private string Authorization()
        {
            var response = _client.PostAsync(AUTH_URL, AuthBody()).Result;
            var responseBody = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<AuthorizationResponse>(responseBody).AccessToken;
        }

        private FormUrlEncodedContent AuthBody()
        {
            return new FormUrlEncodedContent(
                new Dictionary<string, string>
                {
                    { "grant_type", "password" },
                    { "username", username },
                    { "password", password }
                }
            );
        }

        private string ChooseFilterType(string type)
        {
            string filterType;

            switch (type)
            {
                case "manga":
                    filterType = "mangaId";
                    break;
                case "anime":
                    filterType = "animeId";
                    break;
                default:
                    filterType = "";
                    break;
            }

            return filterType;
        }

        private LibraryEntryResponse FormattedLibraryEntry(string body)
        {
            var jsonBody = JsonConvert.DeserializeObject<JObject>(body)["data"].First;
            return JsonConvert.DeserializeObject<LibraryEntryResponse>(
                JsonConvert.SerializeObject(jsonBody)
            );
        }

        private StringContent UpdateChapterBody(int progress, string libraryEntryId)
        {
            var body = new PatchLibaryEntryRequest
            {
                Data = new LibraryEntryData
                {
                    Id = libraryEntryId,
                    Attributes = new LibraryEntryAttributes
                    {
                        Progress = progress
                    },
                    Type = "library-entries"
                }
            };

            var formattedBody = new StringContent(JsonConvert.SerializeObject(body));
            formattedBody.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.api+json");

            return formattedBody;
        }
    }
}
