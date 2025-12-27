using System.Collections.Generic;
using LBoL.Base;
using LBoL.ConfigData;
using LBoL.Core;
using LBoL.Core.Battle;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.StatusEffects;
using LBoL.Core.Units;
using LBoL.EntityLib.StatusEffects.Others;
using LBoLEntitySideloader.Attributes;

namespace lvalonmeme.StatusEffects
{
    public sealed class se9999Def : lvalonmemesetemplate
    {
        public override StatusEffectConfig MakeConfig()
        {
            StatusEffectConfig config = GetDefaultStatusEffectConfig();
            config.Type = StatusEffectType.Negative;
            return config;
        }
    }

    [EntityLogic(typeof(se9999Def))]
    public sealed class se9999 : StatusEffect
    {
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
			yield return new ApplyStatusEffectAction<Poison>(Owner, new int?(Level), null, null, null, 0f, true);
			yield break;
		}
    }
}