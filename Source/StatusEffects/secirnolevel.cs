using System;
using System.Collections.Generic;
using LBoL.ConfigData;
using LBoL.Core;
using LBoL.Core.Battle;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.StatusEffects;
using LBoL.Core.Units;
using LBoLEntitySideloader.Attributes;

namespace lvalonmeme.StatusEffects
{
	public sealed class secirnolevelDef : lvalonmemesetemplate
	{
		public override StatusEffectConfig MakeConfig()
		{
			StatusEffectConfig config = GetDefaultStatusEffectConfig();
			config.HasCount = true;
			return config;
		}
	}

	[EntityLogic(typeof(secirnolevelDef))]
	public sealed class secirnolevel : StatusEffect
	{
		protected override void OnAdded(Unit unit)
		{
			Count = 0;
			ReactOwnerEvent(Owner.TurnEnding, new EventSequencedReactor<UnitEventArgs>(OnTurnEnding));
			HandleOwnerEvent(Battle.CardUsed, OnCardUsed);
		}
		public void OnCardUsed(CardUsingEventArgs args)
		{
			Count++;
			if (Count >= 10)
			{
				Highlight = true;
			}
		}

		private IEnumerable<BattleAction> OnTurnEnding(UnitEventArgs args)
		{
			int loss = Convert.ToInt32(Math.Floor((double)(Count / 10))) * Level;
			if (Battle.BattleShouldEnd || loss == 0)
			{
				yield break;
			}
			NotifyActivating();
			yield return DamageAction.LoseLife(Owner, loss, "Poison");
			Count = 0;
			Highlight = false;
			yield break;
		}
	}
}