using AIGames.Conquest.Arena;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AI.Games.Conquest.UnitTests.Arena
{
	[TestClass]
	public class BotInfoTest
	{
		[TestMethod]
		public void TryParse_Null_IsFalse()
		{
			BotInfo act;
			Assert.IsFalse(BotInfo.TryParse(null, out act), "TryParse");
		}
		[TestMethod]
		public void TryParse_StringEmpty_IsFalse()
		{
			BotInfo act;
			Assert.IsFalse(BotInfo.TryParse(String.Empty, out act), "TryParse");
		}

		[TestMethod]
		public void TryParse_Engine1_AreEqual()
		{
			BotInfo act;
			Assert.IsTrue(BotInfo.TryParse("Engine1", out act), "TryParse");
			Assert.AreEqual("Engine1", act.Name, "act.Name");
			Assert.AreEqual(0, act.Version, "act.Version");
			Assert.AreEqual(false, act.Inactive, "act.Inactive");
			Assert.AreEqual("Bot: Engine1, Version: 0", act.DebugToString(), "act.DebugToString()");
		}

		[TestMethod]
		public void TryParse_zDotEngine1_AreEqual()
		{
			BotInfo act;
			Assert.IsTrue(BotInfo.TryParse("z.Engine1", out act), "TryParse");
			Assert.AreEqual("Engine1", act.Name, "act.Name");
			Assert.AreEqual(0, act.Version, "act.Version");
			Assert.AreEqual(true, act.Inactive, "act.Inactive");
			Assert.AreEqual("Bot: Engine1, Version: 0, Inactive", act.DebugToString(), "act.DebugToString()");
		}

		[TestMethod]
		public void TryParse_zzzDotEngine1_AreEqual()
		{
			BotInfo act;
			Assert.IsTrue(BotInfo.TryParse("zzz.Engine1", out act), "TryParse");
			Assert.AreEqual("Engine1", act.Name, "act.Name");
			Assert.AreEqual(0, act.Version, "act.Version");
			Assert.AreEqual(true, act.Inactive, "act.Inactive");
			Assert.AreEqual("Bot: Engine1, Version: 0, Inactive", act.DebugToString(), "act.DebugToString()");
		}


		[TestMethod]
		public void TryParse_Engine1V123_AreEqual()
		{
			BotInfo act;
			Assert.IsTrue(BotInfo.TryParse("Engine1.123", out act), "TryParse");
			Assert.AreEqual("Engine1", act.Name, "act.Name");
			Assert.AreEqual(123, act.Version, "act.Version");
			Assert.AreEqual(false, act.Inactive, "act.Inactive");
			Assert.AreEqual("Bot: Engine1, Version: 123", act.DebugToString(), "act.DebugToString()");
		}

		[TestMethod]
		public void TryParse_zDotEngine1V123_AreEqual()
		{
			BotInfo act;
			Assert.IsTrue(BotInfo.TryParse("z.Engine1.123", out act), "TryParse");
			Assert.AreEqual("Engine1", act.Name, "act.Name");
			Assert.AreEqual(123, act.Version, "act.Version");
			Assert.AreEqual(true, act.Inactive, "act.Inactive");
			Assert.AreEqual("Bot: Engine1, Version: 123, Inactive", act.DebugToString(), "act.DebugToString()");
		}

		[TestMethod]
		public void TryParse_zzzDotEngine1V123_AreEqual()
		{
			BotInfo act;
			Assert.IsTrue(BotInfo.TryParse("zzz.Engine1.123", out act), "TryParse");
			Assert.AreEqual("Engine1", act.Name, "act.Name");
			Assert.AreEqual(123, act.Version, "act.Version");
			Assert.AreEqual(true, act.Inactive, "act.Inactive");
			Assert.AreEqual("Bot: Engine1, Version: 123, Inactive", act.DebugToString(), "act.DebugToString()");
		}
	}
}
