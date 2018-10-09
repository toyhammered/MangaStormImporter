using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using MangaStormImporter.Contracts;

namespace MangaStormImporter.Libs
{
    public class FileHelper : IFileHelper
    {

        public StreamReader FileContent;

        // Constants
        private const string FILE_NAME = "history.dat";
        private const string UPDATED_FILE_NAME = "updated_history.dat";

        public FileHelper()
        {
            FileContent = File.OpenText(FILE_NAME);
        }

        public List<MangaStormResponse> GetHistoryFormatted()
        {
            string line;
            var formatted = new List<MangaStormResponse>();

            while ((line = FileContent.ReadLine()) != null)
            {
                var parsed_line = line.Split('\t');
                formatted.Add(FormattedEntry(parsed_line));
            }

            return formatted;
        }

        public async Task WriteErrors(List<MangaStormResponse> errors)
        {
            StreamWriter file = new StreamWriter(UPDATED_FILE_NAME, false);
            string formatted;

            foreach (var manga in errors)
            {
                formatted = FormattedEntry(manga);
                await file.WriteAsync(formatted);
            }

            file.Close();
        }

        private MangaStormResponse FormattedEntry(string[] data)
        {
            return new MangaStormResponse
            {
                Source = data[0],
                Title = data[1],
                Chapter = FormatChapter(data[2]),
                Unused1 = data[3],
                Unused2 = data[4],
                Unused3 = data[5],
                Site = data[6]
            };
        }

        private string FormattedEntry(MangaStormResponse data)
        {
            string formatted = "";

            formatted += data.Source + "\t";
            formatted += data.Title + "\t";
            formatted += data.Chapter + "\t";
            formatted += data.Unused1 + "\t";
            formatted += data.Unused2 + "\t";
            formatted += data.Unused3 + "\t";
            formatted += data.Site + "\n";

            return formatted;
        }

        private int FormatChapter(string chapter)
        {
            return Convert.ToInt32(Math.Floor(Convert.ToDouble(chapter)));
        }
    }
}
