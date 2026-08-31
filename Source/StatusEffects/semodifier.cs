using LBoL.Core.StatusEffects;
using LBoLEntitySideloader.Attributes;
using UnityEngine;

namespace lvalonmeme.StatusEffects
{
    public sealed class semodifierDef : lvalonmemesetemplate
    {
        //Keywords don't have sprites.
        public override Sprite LoadSprite() => null;

    }

    [EntityLogic(typeof(semodifierDef))]
    public sealed class semodifier : StatusEffect
    {
    }
}