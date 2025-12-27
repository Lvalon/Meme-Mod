using System.Collections.Generic;
using LBoL.Base;
using LBoL.ConfigData;
using LBoL.Core;
using LBoL.Core.Battle;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.StatusEffects;
using LBoL.Core.Units;
using LBoL.EntityLib.StatusEffects.Enemy;
using LBoLEntitySideloader.Attributes;

namespace lvalonmeme.StatusEffects
{
    public sealed class seedgingDef : lvalonmemesetemplate
    {
        public override StatusEffectConfig MakeConfig()
        {
            StatusEffectConfig config = GetDefaultStatusEffectConfig();
            config.HasCount = true;
            config.Type = StatusEffectType.Negative;
            return config;
        }
    }

    [EntityLogic(typeof(seedgingDef))]
    public sealed class seedging : StatusEffect
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
            Count = Level;
            ReactOwnerEvent(Owner.TurnEnding, new EventSequencedReactor<UnitEventArgs>(OnTurnEnding));
            HandleOwnerEvent(Battle.CardUsed, OnCardUsed);
        }
        public void OnCardUsed(CardUsingEventArgs args)
        {
            if (Count > 0)
            {
                Count--;
            }
            if (Count == 0) {
                Highlight = true;
            }
        }

        private IEnumerable<BattleAction> OnTurnEnding(UnitEventArgs args)
        {
            if (Battle.BattleShouldEnd)
            {
                yield break;
            }
            NotifyActivating();
            if (Count == 0 && !Owner.HasStatusEffect<FoxCharm>())
            {
                yield return new ApplyStatusEffectAction<FoxCharm>(Owner, null, null, null, 3, 0f, true);
                yield return new RemoveStatusEffectAction(this, true, 0.1f);
            }
            Count = Level;
            Highlight = false;
            yield break;
        }
    }
}