using System;
using System.Collections.Generic;
using System.IO;
using MangaStormImporter.Contracts;

namespace MangaStormImporter.Libs
{
    public class FileHelper
    {

        public StreamReader FileContent;

        // Constants
        private const string FILE_NAME = "history.dat";

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

        private MangaStormResponse FormattedEntry(string[] data)
        {
            return new MangaStormResponse
            {
                Source = data[0],
                Title = data[1],
                Chapter = FormatChapter(data[2]),
                Site = data[6]
            };
        }

        private int FormatChapter(string chapter)
        {
            return Convert.ToInt32(Math.Floor(Convert.ToDouble(chapter)));
        }
    }
}
