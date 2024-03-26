using System;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRPExtractor
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=============================");
            Console.WriteLine("DRPExtractor v0.8");
            Console.WriteLine("Copyright(c) 2018 Sammi Husky");
            Console.WriteLine("Licensed under MIT License");
            Console.WriteLine("=============================\n");
            if (args.Length <= 0 || args.Length > 1)
            {
                Console.WriteLine("Usage:\n\tDRPE <drp file>");
                return;
            }

            var drp = new DRPFile(args[0]);
            int loopcount = 0;

            Directory.CreateDirectory(Path.GetFileNameWithoutExtension(args[0]));
            foreach(var entry in drp.ExtractFiles())
            {
                if (drp.Entries.Count > loopcount)
                {
                    var specificEntry = drp.Entries.ElementAt(loopcount).Value;
                    if (specificEntry.Unk == 0)
                    {
                        int isCompressed = specificEntry.Unk;
                        var data = entry.Value;
                        var path = Path.Combine(Path.GetFileNameWithoutExtension(args[0]), entry.Key);
                        File.WriteAllBytes(path, data);
                    }
                    else
                    {
                        var data = Util.DeCompress(entry.Value);
                        var path = Path.Combine(Path.GetFileNameWithoutExtension(args[0]), entry.Key);
                        File.WriteAllBytes(path, data);
                    }
                }
                loopcount++;
            }
        }
    }
}
