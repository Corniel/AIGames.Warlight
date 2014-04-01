using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AIGames.Conquest.Arena
{
	class Program
	{
		static void Main(string[] args)
		{
            var dir = new DirectoryInfo("bots");
            if (args != null && args.Length > 0)
            {
                dir = new DirectoryInfo(args[0]);
            }

            var arena = new CompetitionRunner(dir);

            try
            {
                arena.Bots = Bots.Load(new DirectoryInfo("."));
            }
            catch { }

            while (true)
            {
                arena.RunGame();
            }
		}
	}
}
