using LBoL.Base;
using LBoL.ConfigData;
using LBoL.Core.Units;
using LBoL.EntityLib.Exhibits;
using LBoLEntitySideloader.Attributes;

namespace lvalonmeme.Exhibits
{
    public sealed class exmacrosDef : lvalonmemeexhibittemplate
    {
        public override ExhibitConfig MakeConfig()
        {
            ExhibitConfig exhibitConfig = GetDefaultExhibitConfig();

            exhibitConfig.Order = 1;
            exhibitConfig.Value1 = 2;
            exhibitConfig.IsPooled = false;
            exhibitConfig.IsSentinel = false;
            exhibitConfig.Revealable = true;
            exhibitConfig.Appearance = AppearanceType.Anywhere;
            exhibitConfig.Owner = null;
            exhibitConfig.LosableType = ExhibitLosableType.CantLose;
            exhibitConfig.Rarity = Rarity.Mythic;
            exhibitConfig.Keywords = Keyword.Power;
            exhibitConfig.BaseManaAmount = 0;

            return exhibitConfig;
        }
    }

    [EntityLogic(typeof(exmacrosDef))]
    public sealed class exmacros : ShiningExhibit
    {
		protected override void OnAdded(PlayerUnit player)
		{
			player.Us.MaxPowerLevel *= 2;
			player.Us.UsRepeatableType = UsRepeatableType.FreeToUse;
		}
    }
}