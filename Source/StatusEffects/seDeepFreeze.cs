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
	public sealed class seDeepFreezeDef : lvalonmemesetemplate
	{
		public override StatusEffectConfig MakeConfig()
		{
			StatusEffectConfig config = GetDefaultStatusEffectConfig();
			config.Type = StatusEffectType.Negative;
			config.Order = 8;
			return config;
		}
	}

	[EntityLogic(typeof(seDeepFreezeDef))]
	public sealed class seDeepFreeze : StatusEffect
	{
		public override bool ForceNotShowDownText
		{
			get
			{
				return true;
			}
		}
		protected override void OnAdded(Unit unit)
		{
			//ReactOwnerEvent(Owner.DamageReceiving, new EventSequencedReactor<DamageEventArgs>(OnDamageReceiving));
			HandleOwnerEvent(unit.DamageReceiving, new GameEventHandler<DamageEventArgs>(OnDamageReceiving));
			ReactOwnerEvent(Battle.RoundEnding, new EventSequencedReactor<GameEventArgs>(OnRoundEnding));
		}
		private void OnDamageReceiving(DamageEventArgs args)
		{
			DamageInfo damageInfo = args.DamageInfo;
			// if (damageInfo.DamageType == DamageType.Attack)
			// {
			damageInfo.Damage = damageInfo.Amount * 2f;
			args.DamageInfo = damageInfo;
			args.AddModifier(this);
			// }
		}

		// private IEnumerable<BattleAction> OnDamageReceiving(DamageEventArgs args)
		// {
		//     args.DamageInfo = args.DamageInfo.MultiplyBy(2);
		//     args.AddModifier(this);
		//     if (args.Cause != ActionCause.OnlyCalculate)
		//     {
		//         NotifyActivating();
		//     }
		//     yield break;
		// }
		private IEnumerable<BattleAction> OnRoundEnding(GameEventArgs args)
		{
			if (Battle.BattleShouldEnd)
			{
				yield break;
			}
			yield return new RemoveStatusEffectAction(this, true, 0.1f);
			yield break;
		}
	}
}