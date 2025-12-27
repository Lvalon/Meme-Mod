using LBoL.ConfigData;
using LBoLEntitySideloader;
using LBoLEntitySideloader.Entities;
using LBoLEntitySideloader.Resource;
using lvalonmeme.ImageLoader;
using lvalonmeme.Localization;
using lvalonmeme.Config;

namespace lvalonmeme.Packs
{
	public class lvalonmemepacktemplate : PackTemplate
	{
		public override IdContainer GetId()
		{
			return SampleCharacterDefaultConfig.DefaultID(this);
		}

		public override LocalizationOption LoadLocalization()
		{
			return SampleCharacterLocalization.PacksBatchLoc.AddEntity(this);
		}

		public override PackIcons LoadPackIcon()
		{
			return SampleCharacterImageLoader.LoadPackIconLoader(this);
		}

		public new PackConfig MakeConfig()
		{
			return GetDefaultPackConfig();
		}

		public static PackConfig GetDefaultPackConfig()
		{
			return SampleCharacterDefaultConfig.DefaultPackConfig();
		}
	}
}