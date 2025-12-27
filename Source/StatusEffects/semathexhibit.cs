using LBoL.Base;
using LBoL.ConfigData;
using LBoL.Core;
using LBoL.Core.StatusEffects;
using LBoL.Core.Units;
using LBoL.Presentation;
using LBoLEntitySideloader.Attributes;
using lvalonmeme.Exhibits;

namespace lvalonmeme.StatusEffects
{
	public sealed class semathexhibitDef : lvalonmemesetemplate
	{
		public override StatusEffectConfig MakeConfig()
		{
			StatusEffectConfig config = GetDefaultStatusEffectConfig();
			config.Type = StatusEffectType.Positive;
			config.Order = 3;
			return config;
		}
	}

	[EntityLogic(typeof(semathexhibitDef))]
	public sealed class semathexhibit : StatusEffect
	{
		public override bool ForceNotShowDownText => true;
		protected override void OnAdded(Unit unit)
		{
			HandleOwnerEvent(Battle.BattleEnding, OnBattleEnding);
		}

		private void OnBattleEnding(GameEventArgs args)
		{
			GameMaster.DebugGainExhibit(Library.CreateExhibit<exmathexhibit>());
		}
	}
}