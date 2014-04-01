using AIGames.Conquest.Arena;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace AI.Games.Conquest.UnitTests.Arena
{
	[TestClass]
	public class CompetitionRunnerTest
	{
		[TestMethod]
		public void Ctor_UnitTestArena_AreEqual()
		{
            var dirs = new String[] { "Engine1.13", "Engine2", "Engine2.003", "Engine2.004", "zzz.Engine1.12" };

            var dir = new DirectoryInfo("Arena");
            if (!dir.Exists)
            {
                dir.Create();
            }
            foreach (var dr in dirs)
            {
                var child = new DirectoryInfo(Path.Combine(dir.FullName, dr));
                if (!child.Exists)
                {
                    child.Create();
                }
            }
			
			var runner = new CompetitionRunner(dir);
			Assert.AreEqual(5, runner.Bots.Count);
		}
	}
}
