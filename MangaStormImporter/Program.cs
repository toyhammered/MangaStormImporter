using System;

namespace MangaStormImporter
{
    class Program
    {
        static void Main(string[] args)
        {
            DotNetEnv.Env.Load();
            var import = new Import();
            import.Proccess();
        }
    }
}
