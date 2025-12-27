using LBoL.ConfigData;
using LBoLEntitySideloader;
using LBoLEntitySideloader.Entities;
using LBoLEntitySideloader.Resource;
using UnityEngine;
using lvalonmeme.ImageLoader;
using lvalonmeme.Localization;
using lvalonmeme.Config;

namespace lvalonmeme.SampleCharacterUlt
{
    public class lvalonmemeulttemplate : UltimateSkillTemplate
    {
        public override IdContainer GetId()
        {
            return SampleCharacterDefaultConfig.DefaultID(this);
        }

        public override LocalizationOption LoadLocalization()
        {
            return SampleCharacterLocalization.UltimateSkillsBatchLoc.AddEntity(this);
        }

        public override Sprite LoadSprite()
        {
            return SampleCharacterImageLoader.LoadUltLoader(ult: this);
        }

        public override UltimateSkillConfig MakeConfig()
        {
            throw new System.NotImplementedException();
        }

        public UltimateSkillConfig GetDefaulUltConfig()
        {
            return SampleCharacterDefaultConfig.DefaultUltConfig();
        }
    }
}