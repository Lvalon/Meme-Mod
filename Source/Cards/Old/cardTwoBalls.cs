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
using LBoL.EntityLib.Cards.Character.Reimu;
using lvalonmeme.Packs;

namespace lvalonmeme.Cards
{
	public sealed class cardTwoBallsDef : lvalonmemecardtemplate
	{
		public override CardConfig MakeConfig()
		{
			CardConfig config = GetCardDefaultConfig();
			config.IsPooled = true;
			config.HideMesuem = false;
			config.Owner = "Reimu";

			config.Value1 = 2;

			config.Colors = new List<ManaColor>() { ManaColor.White };
			config.Cost = new ManaGroup { Any = 1, White = 1 };
			config.UpgradedCost = new ManaGroup { White = 1 };
			config.Rarity = Rarity.Uncommon;

			config.Type = CardType.Skill;

			config.RelativeCards = new List<string>() { nameof(TwoBalls), nameof(YinyangCard) };
			config.UpgradedRelativeCards = new List<string>() { nameof(TwoBalls) + "+", nameof(YinyangCard) };
			config.RelativeEffects = new List<string>() { nameof(seold) };
			config.UpgradedRelativeEffects = new List<string>() { nameof(seold) };

			config.Pack = nameof(packoldDef)[..^3];

			config.Illustrator = "老邢";

			config.Index = CardIndexGenerator.GetUniqueIndex(config, "old");
			return config;
		}
	}

	[EntityLogic(typeof(cardTwoBallsDef))]
	public sealed class cardTwoBalls : lvalonmemecard.oldcard
	{
		protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
		{
			yield return new AddCardsToHandAction(Library.CreateCards<YinyangCard>(Value1, false).ToList(), AddCardsType.Normal);
			yield break;
		}
	}
}


