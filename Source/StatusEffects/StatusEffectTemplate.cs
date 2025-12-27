using LBoL.ConfigData;
using LBoLEntitySideloader;
using LBoLEntitySideloader.Entities;
using LBoLEntitySideloader.Resource;
using UnityEngine;
using lvalonmeme.ImageLoader;
using lvalonmeme.Localization;
using lvalonmeme.Config;

namespace lvalonmeme.StatusEffects
{
    public class lvalonmemesetemplate : StatusEffectTemplate
    {
        public override IdContainer GetId()
        {
            return SampleCharacterDefaultConfig.DefaultID(this);
        }

        public override LocalizationOption LoadLocalization()
        {
            return SampleCharacterLocalization.StatusEffectsBatchLoc.AddEntity(this);
        }

        public override Sprite LoadSprite()
        {
            return SampleCharacterImageLoader.LoadStatusEffectLoader(status: this);
        }

        public override StatusEffectConfig MakeConfig()
        {
            return GetDefaultStatusEffectConfig();
        }

        public static StatusEffectConfig GetDefaultStatusEffectConfig()
        {
            return SampleCharacterDefaultConfig.DefaultStatusEffectConfig();
        }        
    }
}