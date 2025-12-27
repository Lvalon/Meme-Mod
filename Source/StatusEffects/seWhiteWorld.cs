using System.Collections.Generic;
using LBoL.ConfigData;
using LBoL.Core;
using LBoL.Core.Battle;
using LBoL.Base;
using LBoL.Core.StatusEffects;
using LBoL.Core.Units;
using LBoLEntitySideloader.Attributes;
using LBoL.EntityLib.StatusEffects.Marisa;
using LBoL.Core.Battle.BattleActions;

namespace lvalonmeme.StatusEffects
{
    public sealed class seWhiteWorldDef : lvalonmemesetemplate
    {
        public override StatusEffectConfig MakeConfig()
        {
            StatusEffectConfig config = GetDefaultStatusEffectConfig();
            config.Type = StatusEffectType.Special;
            config.HasCount = true;
            return config;
        }
    }

    [EntityLogic(typeof(seWhiteWorldDef))]
    public sealed class seWhiteWorld : StatusEffect
    {
        public ManaGroup Mana
        {
            get
            {
                return ManaGroup.Anys(Count);
            }
        }
        public ManaGroup nbMana
        {
            get
            {
                return ManaGroup.Anys(1);
            }
        }
        protected override void OnAdded(Unit unit)
        {
            ReactOwnerEvent(Owner.TurnEnding, new EventSequencedReactor<UnitEventArgs>(OnTurnEnding));
        }
        private IEnumerable<BattleAction> OnTurnEnding(UnitEventArgs args)
        {
            if (Battle.BattleShouldEnd)
            {
                yield break;
            }
            NotifyActivating();
            yield return new ApplyStatusEffectAction<Firepower>(Owner, Level, 0, 0, 0, 0.2f);
			yield return new ApplyStatusEffectAction<Spirit>(Owner, Level, 0, 0, 0, 0.2f);
            yield return DebuffAction<ManaFreezed>(Owner, Count, 0, 0, 0, true, 0.1f);
            yield break;
        }
    }
}