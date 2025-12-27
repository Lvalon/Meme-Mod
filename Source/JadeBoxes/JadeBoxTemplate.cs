using LBoL.ConfigData;
using LBoLEntitySideloader;
using LBoLEntitySideloader.Entities;
using LBoLEntitySideloader.Resource;
using lvalonmeme.Config;
using lvalonmeme.Localization;

namespace lvalonmeme.JadeBoxes
{
	public class lvalonmemejadeboxtemplate : JadeBoxTemplate
	{
		public override IdContainer GetId()
		{
			return SampleCharacterDefaultConfig.DefaultID(this);
		}

		public override LocalizationOption LoadLocalization()
		{
			return SampleCharacterLocalization.JadeBoxBatchLoc.AddEntity(this);
		}

		public override JadeBoxConfig MakeConfig()
		{
			return GetDefaultJadeBoxConfig();
		}

		public JadeBoxConfig GetDefaultJadeBoxConfig()
		{
			return SampleCharacterDefaultConfig.DefaultJadeBoxConfig();
		}

	}
}