using LBoL.Base;
using LBoL.ConfigData;
using LBoLEntitySideloader.Attributes;
using System.Collections.Generic;
using lvalonmeme.Cards.Template;
using LBoL.Core.Battle;
using LBoL.Core;
using LBoL.Core.Cards;
using System.Linq;
using LBoL.Core.Battle.BattleActions;
using LBoL.EntityLib.Cards.Character.Marisa;
using lvalonmeme.StatusEffects;
using LBoL.EntityLib.StatusEffects.Others;
using lvalonmeme.Packs;

namespace lvalonmeme.Cards
{
	public sealed class card9999Def : lvalonmemecardtemplate
	{
		public override CardConfig MakeConfig()
		{
			CardConfig config = GetCardDefaultConfig();
			config.IsPooled = true;
			config.HideMesuem = false;
			config.Owner = null;

			config.Value1 = 3;
			config.Value2 = 6;

			config.Colors = new List<ManaColor>() { ManaColor.Black };
			config.Cost = new ManaGroup { Any = 2, Black = 3 };
			config.Rarity = Rarity.Rare;

			config.Type = CardType.Ability;

			config.RelativeCards = new List<string>() { nameof(MarisaSteal), nameof(PotionDefense) };
			config.UpgradedRelativeCards = new List<string>() { nameof(MarisaSteal), nameof(PotionDefense) };
			config.RelativeEffects = new List<string>() { nameof(Poison), nameof(sememe) };
			config.UpgradedRelativeEffects = new List<string>() { nameof(Poison), nameof(sememe) };

			config.Pack = nameof(packmemeDef)[..^3];

			config.Illustrator = "Alioth Studio";

			config.Index = CardIndexGenerator.GetUniqueIndex(config);
			return config;
		}
	}

	[EntityLogic(typeof(card9999Def))]
	public sealed class card9999 : lvalonmemecard.memecard
	{
		protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
		{
			List<Card> cards = new List<Card>();
			foreach (Card card in Battle.EnumerateAllCardsButExile().Where(c => c.IsBasic))
			{
				cards.Add(card);
			}
			yield return new ExileManyCardAction(cards);
			yield return new AddCardsToDrawZoneAction(Library.CreateCards<MarisaSteal>(1, false), DrawZoneTarget.Random, AddCardsType.Normal);
			yield return new AddCardsToDrawZoneAction(Library.CreateCards<PotionDefense>(1, false), DrawZoneTarget.Random, AddCardsType.Normal);
			yield return new ApplyStatusEffectAction<se9999>(Battle.Player, Value2, null, null, null, 0f, true);
			if (IsUpgraded)
			{
				yield return new DrawManyCardAction(Value1);
			}
			yield break;
		}
	}
}


