using System.Linq;
using LBoL.ConfigData;
using lvalonmeme.Patches;

namespace lvalonmeme.Packs
{
	public sealed class packmemeDef : lvalonmemepacktemplate
	{
		public new PackConfig MakeConfig()
		{
			PackConfig config = GetDefaultPackConfig();
			config.Id = GetId();
			config.CardList = CustomGameEventManager.GetList("meme").ToList();
			return config;
		}
	}
	public sealed class packoldDef : lvalonmemepacktemplate
	{
		public new PackConfig MakeConfig()
		{
			PackConfig config = GetDefaultPackConfig();
			config.Id = GetId();
			config.CardList = CustomGameEventManager.GetList("old").ToList();
			return config;
		}
	}
}