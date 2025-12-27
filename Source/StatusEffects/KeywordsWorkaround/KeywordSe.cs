using LBoLEntitySideloader.Attributes;
using LBoL.Core.StatusEffects;
using UnityEngine;

namespace lvalonmeme.StatusEffects
{
	public sealed class sememeDef : lvalonmemesetemplate
	{
		//Keywords don't have sprites.
		public override Sprite LoadSprite() => null;

	}

	[EntityLogic(typeof(sememeDef))]
	public sealed class sememe : StatusEffect
	{
	}
	public sealed class seoldDef : lvalonmemesetemplate
	{
		//Keywords don't have sprites.
		public override Sprite LoadSprite() => null;

	}

	[EntityLogic(typeof(seoldDef))]
	public sealed class seold : StatusEffect
	{
	}
}

