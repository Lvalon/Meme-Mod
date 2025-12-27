using LBoL.Base;
using LBoL.ConfigData;
using LBoL.Core;
using LBoL.Core.Battle;
using LBoL.Core.Units;
using LBoLEntitySideloader.Attributes;

namespace lvalonmeme.Exhibits
{
	public sealed class exmathexhibitDef : lvalonmemeexhibittemplate
	{
		public override ExhibitConfig MakeConfig()
		{
			ExhibitConfig exhibitConfig = GetDefaultExhibitConfig();

			exhibitConfig.Order = 1;
			exhibitConfig.Value1 = 2;
			exhibitConfig.IsPooled = false;
			exhibitConfig.IsSentinel = false;
			exhibitConfig.Revealable = true;
			exhibitConfig.Appearance = AppearanceType.Nowhere;
			exhibitConfig.Owner = null;
			exhibitConfig.HasCounter = true;
			exhibitConfig.InitialCounter = 3;
			exhibitConfig.LosableType = ExhibitLosableType.CantLose;
			exhibitConfig.Rarity = Rarity.Rare;
			exhibitConfig.Keywords = Keyword.Accuracy;

			return exhibitConfig;
		}
	}

	[EntityLogic(typeof(exmathexhibitDef))]
	public sealed class exmathexhibit : Exhibit
	{

		protected override void OnAdded(PlayerUnit player)
		{
			HandleGameRunEvent(Owner.DamageDealing, OnDamageDealing);
		}

		private void OnDamageDealing(DamageDealingEventArgs args)
		{
			if (args.DamageInfo.DamageType == DamageType.Attack)
			{
				DamageInfo damageInfo = args.DamageInfo;
				if (damageInfo.IsAccuracy)
				{
					damageInfo = damageInfo.IncreaseBy(Counter);
				}
				damageInfo.IsAccuracy = true;
				args.DamageInfo = damageInfo;
				args.AddModifier(this);
				if (args.Cause != ActionCause.OnlyCalculate)
				{
					NotifyActivating();
				}
			}
		}
	}
}