﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace AIGames.Conquest.Arena
{
	public class CompetitionRunner
	{
		public CompetitionRunner(DirectoryInfo dir) : 
			this(dir, Environment.TickCount) { }

		public CompetitionRunner(DirectoryInfo dir, int seed)
		{
			this.RootDirectory = dir;
			this.Bots = new Bots();
			this.Rnd = new Random(seed);
            this.BotLocations = new Dictionary<BotInfo, DirectoryInfo>();

			ScanDirectory();
		}

		public DirectoryInfo RootDirectory { get; protected set; }
		public Bots Bots { get; set; }
		public Random Rnd { get; set; }

        protected Dictionary<BotInfo, DirectoryInfo> BotLocations { get; set; }

		public void RunGame()
		{
			ScanDirectory();

			if (Bots.Count(bot => !bot.Info.Inactive) < 2) { return; }

			Bot player1 = Bots.GetRandom(Rnd);
			Bot player2 = Bots.GetRandom(Rnd);
			
			while(player1.Equals(player2))
			{
				player2 = Bots.GetRandom(Rnd);
			}

            var main1 = GetMainMethod(player1.Info);
            var main2 = GetMainMethod(player2.Info);
            if (main1 == null || main2 == null) { return; }

            var elo1 = player1.Rating;
            var elo2 = player2.Rating;
            var k1 = player1.K;
            var k2 = player2.K;

            // TODO: run an actual game.
            var rnd1 = Rnd.Next(6);
            var rnd2 = Rnd.Next(6);
            var score1 = 0.0;

            if (rnd1 == rnd2)
            {
                player1.Draws++;
                player2.Draws++;
                score1 = 0.5;
            }
            else if (rnd1 > rnd2)
            {
                player1.Wins++;
                player2.Losses++;
                score1 = 1.0;
            }
            else
            {
                player1.Losses++;
                player2.Wins++;
            }
            var score2 = 1.0 - score1;

            player1.Rating = player1.Rating.GetNew(elo2, score1, k1);
            player2.Rating = player2.Rating.GetNew(elo1, score2, k2);

            player1.K = NewK(k1, k2);
            player2.K = NewK(k2, k1);

            Bots.Save(new DirectoryInfo("."));
		}

        private MethodInfo GetMainMethod(BotInfo info)
        {
            var exe = BotLocations[info].GetFiles("*.exe").FirstOrDefault();
            if(exe != null)
            {
                var assembly = Assembly.LoadFile(exe.FullName);

                var program = assembly.GetTypes().FirstOrDefault(tp => tp.GetMethod("Main", BindingFlags.Public|BindingFlags.NonPublic|BindingFlags.Static) != null);

                if(program != null)
                {
                    var main = program.GetMethod("Main", BindingFlags.Public|BindingFlags.NonPublic|BindingFlags.Static);
                    return main;
                }
            }
            return null;
        }

		private void ScanDirectory()
		{
            BotLocations.Clear();
            // Disable all.
            foreach (var bot in Bots)
            {
                bot.Info = bot.Info.SetIsActive(true);
            }

            foreach (var dir in RootDirectory.GetDirectories())
            {
                BotInfo info;
                if (BotInfo.TryCreate(dir, out info))
                {
                    var bot = Bots.GetOrCreate(info);
                    if (!info.Inactive)
                    {
                        BotLocations[bot.Info] = dir;
                    }
                }
            }
		}

        public static double NewK(double kOwn, double kOther)
        {
            if (kOwn <= kOther)
            {
                kOwn *= 0.99;
                return Math.Max(12.0, kOwn);
            }
            kOwn = (kOwn - kOther) * 0.90 + kOther;

            return kOwn;
        }
    }
}
