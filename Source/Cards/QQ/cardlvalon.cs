using LBoL.Base;
using LBoL.ConfigData;
using LBoLEntitySideloader.Attributes;
using System.Collections.Generic;
using lvalonmeme.Cards.Template;
using LBoL.Core.Battle;
using LBoL.Core;
using LBoL.Core.Cards;
using System.Linq;
using LBoL.Core.Stations;
using LBoLEntitySideloader.Resource;
using lvalonmeme.StatusEffects;
using lvalonmeme.Packs;

namespace lvalonmeme.Cards
{
	public sealed class cardlvalonDef : lvalonmemecardtemplate
	{
		public override CardImages LoadCardImages()
		{
			var imgs = new CardImages(BepinexPlugin.embeddedSource);
			imgs.AutoLoad(this, ".png", hasUpgradeImage: true);
			return imgs;
		}
		public override CardConfig MakeConfig()
		{
			CardConfig config = GetCardDefaultConfig();
			config.IsPooled = true;
			config.HideMesuem = false;
			config.Owner = null;
			config.FindInBattle = false;

			config.Colors = new List<ManaColor>() { ManaColor.Blue };
			config.Rarity = Rarity.Rare;
			config.Mana = new ManaGroup { Philosophy = 1 };

			config.Type = CardType.Skill;
			config.Keywords = Keyword.Forbidden | Keyword.Ethereal;
			config.UpgradedKeywords = Keyword.Forbidden | Keyword.Replenish;

			config.RelativeEffects = new List<string>() { nameof(sememe) };
			config.UpgradedRelativeEffects = new List<string>() { nameof(sememe) };

			config.Pack = nameof(packmemeDef)[..^3];

			config.Illustrator = "ジェット虚無僧 / minusT";

			config.UpgradeImageId = $"{GetId()}{CardImages.upgradeString}";

			config.Index = CardIndexGenerator.GetUniqueIndex(config);
			return config;
		}
	}

	[EntityLogic(typeof(cardlvalonDef))]
	public sealed class cardlvalon : lvalonmemecard.memecard
	{
		public override ManaGroup? PlentifulMana
		{
			get
			{
				if (IsUpgraded)
				{
					return new ManaGroup?(Mana);
				}
				else
				{
					return new ManaGroup { Philosophy = 0 };
				}
			}
		}
		protected override void OnEnterBattle(BattleController battle)
		{
			ReactBattleEvent(Battle.BattleEnding, new EventSequencedReactor<GameEventArgs>(OnBattleEnding));
		}

		private IEnumerable<BattleAction> OnBattleEnding(GameEventArgs args)
		{
			EnemyType enemyType = Battle.EnemyGroup.EnemyType;
			List<Card> list = (from card in Battle.EnumerateAllCardsButExile()
							   where card == this
							   select card).ToList();
			if (list.Count > 0)
			{
				switch (GameRun.CurrentStation.Type)
				{
					case StationType.Boss:
						NotifyActivating();
						GameRun.CurrentStation.AddReward(GameRun.CurrentStage.GetBossCardReward());
						break;
					case StationType.EliteEnemy:
						NotifyActivating();
						GameRun.CurrentStation.AddReward(GameRun.CurrentStage.GetEliteEnemyCardReward());
						break;
					case StationType.Enemy:
						NotifyActivating();
						GameRun.CurrentStation.AddReward(GameRun.CurrentStage.GetEnemyCardReward());
						break;
					default:
						break;
				}
			}
			yield break;
		}
	}
}


