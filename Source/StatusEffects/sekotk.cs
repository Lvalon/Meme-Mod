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
using LBoL.EntityLib.StatusEffects.Neutral.Black;
using LBoLEntitySideloader.Attributes;
using lvalonmeme.Cards;

namespace lvalonmeme.StatusEffects
{
    public sealed class sekotkDef : lvalonmemesetemplate
    {
        public override StatusEffectConfig MakeConfig()
        {
            StatusEffectConfig config = GetDefaultStatusEffectConfig();
            config.Type = StatusEffectType.Special;
            config.Order = 20;
            return config;
        }
    }

    [EntityLogic(typeof(sekotkDef))]
    public sealed class sekotk : StatusEffect
    {
        public int Value1 
        {
            get {
                if (GameRun.Battle != null)
                {
                    return Level+1;
                }
                else
                {
                    return 2;
                }
            }
        }

		protected override void OnAdded(Unit unit)
		{
            ReactOwnerEvent(Owner.DamageReceived, new EventSequencedReactor<DamageEventArgs>(OnDamageReceived));
            HandleOwnerEvent(Owner.DamageDealing, new GameEventHandler<DamageDealingEventArgs>(OnDamageDealing));
		}

		private IEnumerable<BattleAction> OnDamageReceived(DamageEventArgs args)
		{
            if (Battle.BattleShouldEnd)
			{
				yield break;
			}
            if (args.DamageInfo.DamageType == DamageType.Attack && args.DamageInfo.Damage > 0) {
                if (GameRun.BaseDeck.Any(c => c is cardyoumi)) {
                    yield return BuffAction<NextTurnLoseGame>(0, 0, 0, 0, 0.2f);
                }
                else {
                    yield return new DamageAction(Owner, Owner, DamageInfo.Reaction(9999f, false), "Instant", GunType.Single);
                }
            }
			yield break;
		}

        private void OnDamageDealing(DamageDealingEventArgs args)
		{
			if (args.DamageInfo.DamageType == DamageType.Attack)
			{
				args.DamageInfo = args.DamageInfo.MultiplyBy(Level+1);
				args.AddModifier(this);
				if (args.Cause != ActionCause.OnlyCalculate)
				{
					NotifyActivating();
				}
			}
		}
    }
}