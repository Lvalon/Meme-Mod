using System.Collections.Generic;
using System.Linq;
using LBoL.Base;
using LBoL.ConfigData;
using LBoL.Core;
using LBoL.Core.Battle;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.Cards;
using LBoL.Core.StatusEffects;
using LBoL.Core.Units;
using LBoLEntitySideloader.Attributes;

namespace lvalonmeme.StatusEffects
{
	public sealed class seFairyTreeDef : lvalonmemesetemplate
	{
		public override StatusEffectConfig MakeConfig()
		{
			StatusEffectConfig config = GetDefaultStatusEffectConfig();
			config.HasCount = true;
			return config;
		}
	}

	[EntityLogic(typeof(seFairyTreeDef))]
	public sealed class seFairyTree : StatusEffect
	{
		public ManaGroup Mana
		{
			get
			{
				return ManaGroup.Philosophies(1);
			}
		}
		protected override void OnAdded(Unit unit)
		{
			ReactOwnerEvent(Battle.CardMoved, new EventSequencedReactor<CardMovingEventArgs>(OnCardMoved));
			ReactOwnerEvent(Owner.TurnEnding, new EventSequencedReactor<UnitEventArgs>(OnTurnEnding));
			ReactOwnerEvent(Battle.CardsAddedToHand, new EventSequencedReactor<CardsEventArgs>(OnCardsAddedToHand));
			ReactOwnerEvent(Battle.CardDrawn, new EventSequencedReactor<CardEventArgs>(OnCardDrawn));
			if (Count > 0)
			{
				Highlight = true;
			}
		}

		private IEnumerable<BattleAction> OnCardMoved(CardMovingEventArgs args)
		{
			if (args.ActionSource == this || Battle.BattleShouldEnd || Count <= 0 || args.DestinationZone != CardZone.Hand || (args.Card.CardType != CardType.Skill && args.Card.Config.Type != CardType.Friend))
			{
				yield break;
			}
			NotifyActivating();
			if (Count > 0)
			{
				yield return new GainManaAction(Mana);
				yield return new DrawManyCardAction(1);
				Count = System.Math.Max(0, Count - 1);
			}
			if (Count <= 0)
			{
				Highlight = false;
			}
			yield break;
		}
		private IEnumerable<BattleAction> OnCardDrawn(CardEventArgs args)
		{
			if (args.ActionSource == this || Battle.BattleShouldEnd || Count <= 0 || (args.Card.CardType != CardType.Skill && args.Card.Config.Type != CardType.Friend))
			{
				yield break;
			}
			NotifyActivating();
			if (Count > 0)
			{
				yield return new GainManaAction(Mana);
				yield return new DrawManyCardAction(1);
				Count = System.Math.Max(0, Count - 1);
			}
			if (Count <= 0)
			{
				Highlight = false;
			}
			yield break;
		}
		private IEnumerable<BattleAction> OnCardsAddedToHand(CardsEventArgs args)
		{
			if (args.ActionSource == this || Battle.BattleShouldEnd || Count <= 0 || !args.Cards.Any(card => card.CardType == CardType.Friend || card.Config.Type == CardType.Skill))
			{
				yield break;
			}
			NotifyActivating();
			if (Count > 0)
			{
				yield return new GainManaAction(Mana);
				yield return new DrawManyCardAction(1);
				Count = System.Math.Max(0, Count - 1);
			}
			if (Count <= 0)
			{
				Highlight = false;
			}
			yield break;
		}
		private IEnumerable<BattleAction> OnTurnEnding(UnitEventArgs args)
		{
			if (Battle.BattleShouldEnd)
			{
				yield break;
			}
			Count = Level;
			Highlight = true;
			yield break;
		}
	}
}