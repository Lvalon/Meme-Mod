using LBoL.Base;
using LBoL.ConfigData;
using LBoLEntitySideloader.Attributes;
using System.Collections.Generic;
using lvalonmeme.Cards.Template;
using LBoL.Core.Battle;
using LBoL.Core;
using System.Linq;
using LBoL.Core.Battle.BattleActions;
using lvalonmeme.StatusEffects;
using lvalonmeme.Packs;
using LBoL.EntityLib.Cards.Character.Cirno;
using LBoL.EntityLib.Cards.Character.Reimu;
using LBoL.EntityLib.Cards.Neutral.TwoColor;
using LBoL.EntityLib.Cards.Neutral.MultiColor;
using LBoL.EntityLib.StatusEffects.Neutral.Green;
using LBoL.EntityLib.EnemyUnits.Character;

namespace lvalonmeme.Cards
{
	public sealed class cardfarmingDef : lvalonmemecardtemplate
	{
		public override CardConfig MakeConfig()
		{
			CardConfig config = GetCardDefaultConfig();
			config.IsPooled = true;
			config.HideMesuem = false;
			config.Owner = "Cirno";

			config.Value1 = 30;
			config.Value2 = 200;

			config.Colors = new List<ManaColor>() { ManaColor.Blue, ManaColor.White, ManaColor.Green };
			config.Cost = new ManaGroup { Blue = 1, White = 1, Green = 1 };
			config.Rarity = Rarity.Rare;

			config.Type = CardType.Ability;

			config.RelativeCards = new List<string>() { nameof(CallFriends), nameof(OutInGap), nameof(LanCard), nameof(PatchouliLibrary) };
			config.UpgradedRelativeCards = new List<string>() { nameof(CallFriends), nameof(OutInGap), nameof(LanCard), nameof(PatchouliLibrary) };
			config.UpgradedKeywords = Keyword.Power;
			config.RelativeEffects = new List<string>() { nameof(sememe) };
			config.UpgradedRelativeEffects = new List<string>() { nameof(sememe) };

			config.Pack = nameof(packmemeDef)[..^3];

			config.Illustrator = "Alioth Studio";

			config.Index = CardIndexGenerator.GetUniqueIndex(config);
			return config;
		}
	}

	[EntityLogic(typeof(cardfarmingDef))]
	public sealed class cardfarming : lvalonmemecard.memecard
	{
		protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
		{
			if (GameRun.BaseDeck.Any(c => c is CallFriends || c is OutInGap || c is LanCard || c is PatchouliLibrary))
			{
				yield return BuffAction<RangziFanshuSe>(999);
				GameRun.PlayedSeconds += Value1 * 60;
			}
			if (IsUpgraded && Battle.AllAliveEnemies.Any(e => e is Clownpiece))
			{
				yield return new GainPowerAction(Value2);
			}
			yield break;
		}
	}
}


