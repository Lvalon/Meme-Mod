using System.Collections.Generic;
using LBoL.Base;
using LBoL.ConfigData;
using LBoL.Core;
using LBoL.Core.Battle;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.StatusEffects;
using LBoL.Core.Units;
using LBoLEntitySideloader.Attributes;

namespace lvalonmeme.StatusEffects
{
	public sealed class seicewingplus1Def : lvalonmemesetemplate
	{
		public override StatusEffectConfig MakeConfig()
		{
			StatusEffectConfig config = GetDefaultStatusEffectConfig();
			config.Type = StatusEffectType.Positive;
			return config;
		}
	}

	[EntityLogic(typeof(seicewingplus1Def))]
	public sealed class seicewingplus1 : StatusEffect
	{
		protected override void OnAdded(Unit unit)
		{
			ReactOwnerEvent(Owner.TurnStarting, OnTurnStarting);
		}

		private IEnumerable<BattleAction> OnTurnStarting(UnitEventArgs args)
		{
			// 从回合开始多抽1张牌
			yield return new DrawManyCardAction(Level);
		}
	}
}