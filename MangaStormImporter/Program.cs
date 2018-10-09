using System;
using System.Threading.Tasks;

namespace MangaStormImporter
{
    class Program
    {
        static async Task Main(string[] args)
        {
            DotNetEnv.Env.Load();
            var import = new Import();
            await import.Proccess();
        }
    }
}
