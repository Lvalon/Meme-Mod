using LBoL.ConfigData;
using LBoLEntitySideloader;
using LBoLEntitySideloader.Entities;
using lvalonmeme.Config;


namespace lvalonmeme.Enemies.Template
{
    public abstract class lvalonmemeenemygrouptemplate : EnemyGroupTemplate
    {
        public override IdContainer GetId()
        {
            return SampleCharacterDefaultConfig.DefaultID(this);
        }

        public override EnemyGroupConfig MakeConfig()
        {
            return SampleCharacterDefaultConfig.EnemyGroupDefaultConfig();
        }

        public EnemyGroupConfig GetEnemyGroupDefaultConfig()
        {
            return SampleCharacterDefaultConfig.EnemyGroupDefaultConfig();
        }
    }
}