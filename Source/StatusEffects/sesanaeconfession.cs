using System.Collections.Generic;
using LBoL.ConfigData;
using LBoL.Core;
using LBoL.Core.Battle;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.StatusEffects;
using LBoL.Core.Units;
using LBoLEntitySideloader.Attributes;
using LBoL.EntityLib.Cards.Enemy;

namespace lvalonmeme.StatusEffects
{
    public sealed class sesanaeconfessionDef : lvalonmemesetemplate
    {
        public override StatusEffectConfig MakeConfig()
        {
            StatusEffectConfig config = GetDefaultStatusEffectConfig();
            return config;
        }
    }

    [EntityLogic(typeof(sesanaeconfessionDef))]
    public sealed class sesanaeconfession : StatusEffect
    {
        protected override void OnAdded(Unit unit)
        {
            ReactOwnerEvent(Battle.CardUsed, new EventSequencedReactor<CardUsingEventArgs>(OnCardUsed));
        }

        private IEnumerable<BattleAction> OnCardUsed(CardUsingEventArgs args)
        {
            if (Battle.BattleShouldEnd)
			{
				yield break;
			}
            if (args.Card is LoveLetter)
            {
                Level--;
                if (Level <= 0)
                {
                    Battle.RequestDebugAction(new InstantWinAction(), "sanaeconfession win");
                    yield return new RemoveStatusEffectAction(this, true, 0.1f);
                }
            }
            yield break;
        }
    }
}