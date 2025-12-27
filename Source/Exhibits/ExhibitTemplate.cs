using LBoL.ConfigData;
using LBoLEntitySideloader;
using LBoLEntitySideloader.Entities;
using LBoLEntitySideloader.Resource;
using lvalonmeme.Config;
using lvalonmeme.ImageLoader;
using lvalonmeme.Localization;

namespace lvalonmeme.Exhibits
{
    public class lvalonmemeexhibittemplate : ExhibitTemplate
    {
        public override IdContainer GetId()
        {
            return SampleCharacterDefaultConfig.DefaultID(this);
        }

        public override LocalizationOption LoadLocalization()
        {
            return SampleCharacterLocalization.ExhibitsBatchLoc.AddEntity(this);
        }

        public override ExhibitSprites LoadSprite()
        {
            return SampleCharacterImageLoader.LoadExhibitSprite(exhibit: this);
        }

        public override ExhibitConfig MakeConfig()
        {
            return GetDefaultExhibitConfig();
        }

        public ExhibitConfig GetDefaultExhibitConfig()
        {
            return SampleCharacterDefaultConfig.DefaultExhibitConfig();
        }

    }
}