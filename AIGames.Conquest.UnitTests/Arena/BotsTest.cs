using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AIGames.Conquest.Arena;

namespace AI.Games.Conquest.UnitTests.Arena
{
	[TestClass]
	public class BotsTest
	{
		[TestMethod]
		public void GetRandom_NoActive_IsNull()
		{
			var bots = new Bots();
            bots.Add(new Bot() { Info = new BotInfo("Engine1", 0, true) });

			var rnd = new Random(17);

			var act = bots.GetRandom(rnd);

			Assert.IsNull(act);
		}

		[TestMethod]
		public void GetRandom_Seed17_AreEqual()
		{
            var exp = new Bot() { Info = new BotInfo("Engine4") };

			var bots = new Bots();
            bots.Add(new Bot() { Info = new BotInfo("Engine1", 0, true) });
            bots.Add(new Bot() { Info = new BotInfo("Engine2", 0, true) });
            bots.Add(new Bot() { Info = new BotInfo("Engine3", 0, true) });
			bots.Add(exp);

			var rnd = new Random(17);

			var act = bots.GetRandom(rnd);

			Assert.AreEqual(exp, act);
		}

		[TestMethod]
		public void GetOrCreate_Existing_AreEqual()
		{
			var info = new BotInfo("Engine1", 0);

			var bots = new Bots();
            bots.Add(new Bot() { Info = new BotInfo("Engine1", 0, true) });

			var act = bots.GetOrCreate(info);

			Assert.AreEqual(1, bots.Count, "bots.Count");
			Assert.AreEqual("Engine1", act.Info.Name, "Name");
			Assert.AreEqual(false, act.Info.Inactive, "Inactive");
		}
	}
}
