using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AIGames.Conquest.Arena;

namespace AI.Games.Conquest.UnitTests.Arena
{
	[TestClass]
	public class BotTest
	{
		[TestMethod]
		public void DebugToString_ActiveBot_AreEqual()
		{
			var bot = new Bot() { Info = new BotInfo( "Engine1", 3, false) };

			var exp = "Bot: Engine1, Version: 3, Elo: 1400, K: 32,0, W: 0, D: 0, L: 0";
			var act = bot.DebugToString();

			Assert.AreEqual(exp, act);
		}

		[TestMethod]
		public void DebugToString_InactiveBot_AreEqual()
		{
            var bot = new Bot() { Info = new BotInfo("Engine1", 3, true) };

			var exp = "Bot: Engine1, Version: 3, Inactive, Elo: 1400, K: 32,0, W: 0, D: 0, L: 0";
			var act = bot.DebugToString();

			Assert.AreEqual(exp, act);
		}
	}
}
