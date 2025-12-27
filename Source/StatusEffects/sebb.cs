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
    public sealed class sebbDef : lvalonmemesetemplate
    {
        public override StatusEffectConfig MakeConfig()
        {
            StatusEffectConfig config = GetDefaultStatusEffectConfig();
            config.Type = StatusEffectType.Special;
            return config;
        }
    }

    [EntityLogic(typeof(sebbDef))]
    public sealed class sebb : StatusEffect
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
            HandleOwnerEvent(Owner.DamageDealing, new GameEventHandler<DamageDealingEventArgs>(OnDamageDealing));
            ReactOwnerEvent(Owner.TurnEnded, new EventSequencedReactor<UnitEventArgs>(OnOwnerTurnEnded));
            Highlight = true;
        }
        private void OnDamageDealing(DamageDealingEventArgs args)
        {
            args.DamageInfo = args.DamageInfo.MultiplyBy(0);
            args.AddModifier(this);
            if (args.Cause != ActionCause.OnlyCalculate)
            {
                NotifyActivating();
            }
        }
        private IEnumerable<BattleAction> OnOwnerTurnEnded(UnitEventArgs args)
        {
            yield return new RemoveStatusEffectAction(this, true, 0.1f);
            yield break;
        }
    }
}