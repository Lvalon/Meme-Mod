using LBoL.ConfigData;
using LBoLEntitySideloader;
using LBoLEntitySideloader.Entities;
using LBoLEntitySideloader.Resource;
using lvalonmeme.Config;
using lvalonmeme.ImageLoader;
using lvalonmeme.Localization;


namespace lvalonmeme.Cards.Template
{
    public abstract class lvalonmemecardtemplate : CardTemplate
    {
        public override IdContainer GetId()
        {
            return SampleCharacterDefaultConfig.DefaultID(this);
        }

        public override CardImages LoadCardImages()
        {
            return SampleCharacterImageLoader.LoadCardImages(this);
        }

        public override LocalizationOption LoadLocalization()
        {
            return SampleCharacterLocalization.CardsBatchLoc.AddEntity(this);
        }

        public CardConfig GetCardDefaultConfig()
        {
            return SampleCharacterDefaultConfig.CardDefaultConfig();
        }
    }


}


