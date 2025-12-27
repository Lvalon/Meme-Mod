using LBoL.Base;
using LBoL.ConfigData;
using LBoLEntitySideloader.Attributes;
using System.Collections.Generic;
using lvalonmeme.Cards.Template;
using LBoL.Core.Battle;
using LBoL.Core;
using lvalonmeme.StatusEffects;
using LBoL.EntityLib.Cards.Character.Reimu;
using LBoL.Core.StatusEffects;
using LBoL.Core.Units;
using LBoL.Core.Cards;
using lvalonmeme.Packs;

namespace lvalonmeme.Cards
{
	public sealed class cardYaoguaiBusterDef : lvalonmemecardtemplate
	{
		public override CardConfig MakeConfig()
		{
			CardConfig config = GetCardDefaultConfig();
			config.IsPooled = true;
			config.HideMesuem = false;
			config.Owner = "Reimu";

			config.TargetType = TargetType.SingleEnemy;
			config.Damage = 7;
			config.Value1 = 1;
			config.UpgradedValue1 = 2;
			config.Value2 = 2;
			config.UpgradedValue2 = 3;

			config.Colors = new List<ManaColor>() { ManaColor.Red };
			config.Cost = new ManaGroup { Red = 2 };
			config.Rarity = Rarity.Common;

			config.Type = CardType.Attack;

			config.RelativeCards = new List<string>() { nameof(YaoguaiBuster) };
			config.UpgradedRelativeCards = new List<string>() { nameof(YaoguaiBuster) + "+" };
			config.RelativeEffects = new List<string>() { nameof(seold), nameof(Weak) };
			config.UpgradedRelativeEffects = new List<string>() { nameof(seold), nameof(Weak) };

			config.Pack = nameof(packoldDef)[..^3];

			config.Illustrator = "三折塔";

			config.Index = CardIndexGenerator.GetUniqueIndex(config, "old");
			return config;
		}
	}

	[EntityLogic(typeof(cardYaoguaiBusterDef))]
	public sealed class cardYaoguaiBuster : lvalonmemecard.oldcard
	{
		protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
		{
			if (IsUpgraded)
			{
				CardGuns = new Guns(new string[3]
				{
				Config.GunNameBurst,
				Config.GunName,
				Config.GunNameBurst
				});
			}
			else
			{
				CardGuns = new Guns(new string[2]
				{
				Config.GunName,
				Config.GunNameBurst
				});
			}

			foreach (GunPair gunPair in CardGuns.GunPairs)
			{
				yield return AttackAction(selector, gunPair);
			}

			EnemyUnit selectedEnemy = selector.SelectedEnemy;
			if (selectedEnemy.IsAlive)
			{
				yield return DebuffAction<Weak>(selectedEnemy, 0, Value1);
			}
			yield break;
		}
	}
}


