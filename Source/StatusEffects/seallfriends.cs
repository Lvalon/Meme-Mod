using System.Collections.Generic;
using LBoL.ConfigData;
using LBoL.Core;
using LBoL.Core.Battle;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.StatusEffects;
using LBoL.Core.Units;
using LBoL.EntityLib.Cards.Character.Cirno;
using LBoLEntitySideloader.Attributes;

namespace lvalonmeme.StatusEffects
{
    public sealed class seallfriendsDef : lvalonmemesetemplate
    {
        public override StatusEffectConfig MakeConfig()
        {
            StatusEffectConfig config = GetDefaultStatusEffectConfig();
            return config;
        }
    }

    [EntityLogic(typeof(seallfriendsDef))]
    public sealed class seallfriends : StatusEffect
    {
        protected override void OnAdded(Unit unit)
		{
            ReactOwnerEvent(Owner.TurnStarted, new EventSequencedReactor<UnitEventArgs>(OnTurnStarting));
		}

		private IEnumerable<BattleAction> OnTurnStarting(UnitEventArgs args)
		{
			if (Battle.BattleShouldEnd)
			{
				yield break;
			}
			NotifyActivating();
            yield return new AddCardsToDrawZoneAction(Library.CreateCards<GatherFairy>(Level, false), DrawZoneTarget.Random, AddCardsType.Normal);
			yield return new RemoveStatusEffectAction(this, true, 0.1f);
			yield break;
		}
    }
}