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
	public sealed class seicewingplus2Def : lvalonmemesetemplate
	{
		public override StatusEffectConfig MakeConfig()
		{
			StatusEffectConfig config = GetDefaultStatusEffectConfig();
			config.Type = StatusEffectType.Positive;
			return config;
		}
	}

	[EntityLogic(typeof(seicewingplus2Def))]
	public sealed class seicewingplus2 : StatusEffect
	{
		public ManaGroup Mana => ManaGroup.Blues(Owner != null ? Level : 2);
		protected override void OnAdded(Unit unit)
		{
			ReactOwnerEvent(Owner.TurnStarting, OnTurnStarting);
		}

		private IEnumerable<BattleAction> OnTurnStarting(UnitEventArgs args)
		{
			// 每回合获得2蓝

			yield return new GainManaAction(Mana);
		}
	}
}