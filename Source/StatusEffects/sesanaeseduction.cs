using System.Collections.Generic;
using LBoL.Base;
using LBoL.ConfigData;
using LBoL.Core;
using LBoL.Core.Battle;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.StatusEffects;
using LBoL.Core.Units;
using LBoL.EntityLib.Cards.Adventure;
using LBoLEntitySideloader.Attributes;
using lvalonmeme.Cards;

namespace lvalonmeme.StatusEffects
{
    public sealed class sesanaeseductionDef : lvalonmemesetemplate
    {
        public override StatusEffectConfig MakeConfig()
        {
            StatusEffectConfig config = GetDefaultStatusEffectConfig();
            config.HasCount = true;
            config.Type = StatusEffectType.Negative;
            return config;
        }
    }

    [EntityLogic(typeof(sesanaeseductionDef))]
    public sealed class sesanaeseduction : StatusEffect
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
            Count = 2;
            ReactOwnerEvent(Battle.CardUsed, OnCardUsed);
        }
        private IEnumerable<BattleAction> OnCardUsed(CardUsingEventArgs args)
        {
            if (args.Card is cardsanaeseduction)
            {
                if (Count > 0)
                {
                    Count--;
                }
                if (Count == 0)
                {
                    Count = 2;
                    NotifyActivating();
                    yield return new AddCardsToDeckAction(Library.CreateCards<NewsNegative>(1, false));
                }
            }
            yield break;
        }
    }
}