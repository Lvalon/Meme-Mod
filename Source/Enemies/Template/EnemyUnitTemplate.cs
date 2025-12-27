using System;
using LBoL.ConfigData;
using LBoLEntitySideloader;
using LBoLEntitySideloader.Entities;
using LBoLEntitySideloader.Resource;
using lvalonmeme.Config;
using lvalonmeme.Localization;


namespace lvalonmeme.Enemies.Template
{
    public class lvalonmemeenemyunittemplate : EnemyUnitTemplate
    {
        public override IdContainer GetId()
        {
            return SampleCharacterDefaultConfig.DefaultID(this);
        }

        public override EnemyUnitConfig MakeConfig()
        {
            return SampleCharacterDefaultConfig.EnemyUnitDefaultConfig();
        }

        public override LocalizationOption LoadLocalization()
        {
            return SampleCharacterLocalization.EnemiesUnitBatchLoc.AddEntity(this);
        }

        public override Type TemplateType()
        {
            return typeof(EnemyUnitTemplate);
        }

        public EnemyUnitConfig GetEnemyUnitDefaultConfig()
        {
            return SampleCharacterDefaultConfig.EnemyUnitDefaultConfig();
        }


    }
}