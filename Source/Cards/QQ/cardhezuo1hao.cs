using HarmonyLib;
using LBoL.Base;
using LBoL.Base.Extensions;
using LBoL.ConfigData;
using LBoL.Core;
using LBoL.Core.Battle;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.Cards;
using LBoL.Core.Stations;
using LBoL.Core.Units;
using LBoL.EntityLib.Cards.Character.Sakuya;
using LBoL.EntityLib.Cards.Tool;
using LBoL.Presentation;
using LBoLEntitySideloader.Attributes;
using LBoLEntitySideloader.PersistentValues;
using lvalonmeme.Cards.Template;
using lvalonmeme.Packs;
using lvalonmeme.StatusEffects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace lvalonmeme.Cards
{
	public sealed class cardhezuo1haoDef : lvalonmemecardtemplate
	{
		public override CardConfig MakeConfig()
		{
			CardConfig config = GetCardDefaultConfig();
			config.FindInBattle = false;
			config.IsPooled = false;
			config.HideMesuem = false;
			config.Owner = null;

			config.Colors = new List<ManaColor>() { ManaColor.Colorless };
            config.Cost = new ManaGroup { Any = 0 };
            config.Rarity = Rarity.Rare;
            config.Mana = new ManaGroup { Philosophy = 1 };

            config.Type = CardType.Skill;

            config.Keywords = config.UpgradedKeywords = Keyword.Exile | Keyword.Retain | Keyword.Initial | Keyword.Replenish;
            config.RelativeCards = config.UpgradedRelativeCards = new List<string>() { nameof(ToolBlock), nameof(ToolAmulet), nameof(ToolFirstAid) };
            config.RelativeEffects = new List<string>() { nameof(sememe) };
			config.UpgradedRelativeEffects = new List<string>() { nameof(sememe), nameof(semodifier) };

			config.Pack = nameof(packmemeDef)[..^3];

			config.Illustrator = "from Terraria";

			config.Index = CardIndexGenerator.GetUniqueIndex(config);
			return config;
		}
	}

	[EntityLogic(typeof(cardhezuo1haoDef))]
	public sealed class cardhezuo1hao : lvalonmemecard.memecard
	{
		enum modifiers
		{
			None,
            Warding,
            Lucky,
            Menacing,
            Arcane
        }
        public static int modifier;
        private string Warding => LocalizeProperty("Warding", true, false);
        private string Lucky => LocalizeProperty("Lucky", true, false);
        private string Menacing => LocalizeProperty("Menacing", true, false);
        private string Arcane => LocalizeProperty("Arcane", true, false);
        public override string Name
        {
            get
            {
                if (!IsUpgraded)
                    return base.Name;

                StringBuilder name = new StringBuilder(base.Name);
                switch (modifier)
                {
                    case (int)modifiers.Warding:
                        name.Insert(0, Warding);
                        break;

                    case (int)modifiers.Lucky:
                        name.Insert(0, Lucky);
                        break;

                    case (int)modifiers.Menacing:
                        name.Insert(0, Menacing);
                        break;

                    case (int)modifiers.Arcane:
                        name.Insert(0, Arcane);
                        break;
                }

                return name.ToString();
            }
        }
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
        public override void Initialize()
		{
			base.Initialize();

            if (modifier == 0) modifier = 1;
        }
        public override IEnumerable<BattleAction> OnDraw()
        {
            if (Battle.Player.StatusEffects.Any(se => se.Type == StatusEffectType.Negative))
                yield return new RemoveAllNegativeStatusEffectAction(Battle.Player, 0.2f);
        }
        protected override void OnEnterBattle(BattleController battle)
        {
            ReactBattleEvent(Battle.Player.StatusEffectAdding, new EventSequencedReactor<StatusEffectApplyEventArgs>(OnStatusEffectAdding));
            HandleBattleEvent(Battle.Player.DamageReceiving, new GameEventHandler<DamageEventArgs>(OnDamageReceiving));
            HandleBattleEvent(Battle.Player.DamageDealing, OnDamageDealing);
            HandleBattleEvent(Battle.Player.DamageGiving, new GameEventHandler<DamageEventArgs>(OnDamageGiving));
            ReactBattleEvent(Battle.Player.TurnStarted, new EventSequencedReactor<UnitEventArgs>(OnOwnerStarted));
        }

        private IEnumerable<BattleAction> OnStatusEffectAdding(StatusEffectApplyEventArgs args)
        {
            if (Zone == CardZone.Hand && args.Effect.Type == StatusEffectType.Negative)
            {
                args.CancelBy(this);
                base.NotifyActivating();
                yield return PerformAction.Sfx("Amulet", 0f);
                yield return PerformAction.SePop(Battle.Player, args.Effect.Name);
            }
            yield break;
        }
        private void OnDamageReceiving(DamageEventArgs args)
        {
			if (Zone == CardZone.Hand)
			{
				DamageInfo damageInfo = args.DamageInfo;
				damageInfo.IsAccuracy = false;

                if (damageInfo.DamageType == DamageType.Attack)
				{
					args.DamageInfo = damageInfo.ReduceBy(4);

					if (IsUpgraded && modifier == (int)modifiers.Warding)
                        args.DamageInfo = args.DamageInfo.ReduceBy(4);

                    args.AddModifier(this);
				}
			}
        }
        private void OnDamageDealing(DamageDealingEventArgs args)
        {
            if (Zone == CardZone.Hand && args.DamageInfo.DamageType == DamageType.Attack && IsUpgraded && modifier == (int)modifiers.Menacing)
            {
                args.DamageInfo = args.DamageInfo.IncreaseBy(4);
                args.AddModifier(this);
            }
        }
        private void OnDamageGiving(DamageEventArgs args)
        {
            if (Zone == CardZone.Hand && args.DamageInfo.DamageType == DamageType.Attack && !args.DamageInfo.IsGrazed && IsUpgraded && modifier == (int)modifiers.Lucky && GameRun.BattleCardRng.Next(99) < 4)
            {
                var dmg = args.DamageInfo;
                args.DamageInfo = new DamageInfo(dmg.Damage * 2, dmg.DamageBlocked, dmg.DamageShielded, dmg.DamageType, dmg.IsGrazed, dmg.IsAccuracy, dmg.DontBreakPerfect);
                args.AddModifier(this);
            }
        }
        private IEnumerable<BattleAction> OnOwnerStarted(UnitEventArgs args)
        {
            if (Zone == CardZone.Hand && IsUpgraded && modifier == (int)modifiers.Arcane)
            {
                NotifyActivating();
                ManaGroup value = ManaGroup.Single(ManaColors.Colors.Sample(GameRun.BattleRng));
                yield return new GainManaAction(value);
            }
            yield break;
        }
    }

    [HarmonyPatch(typeof(GameRunController), nameof(GameRunController.AddDeckCards))]
    class GameRunController_AddDeckCards_PostPatch
	{
		static void Postfix(GameRunController __instance, IEnumerable<Card> cards, bool triggerVisual, VisualSourceData sourceData)
		{
            if (!__instance.Packs.Contains("packmeme"))
                return;

            HashSet<Type> addedTypes = new HashSet<Type>();
            List<Card> materials = new List<Card>();

            foreach (Card card in __instance.BaseDeck)
            {
                Type cardType = card.GetType();

                // 检查是否为目标类型且未添加过
                if ((card is ToolBlock || card is ToolAmulet || card is ToolFirstAid)
                    && addedTypes.Add(cardType))  // Add 返回 false 表示已存在
                {
                    materials.Add(card);
                }

				if (card is cardhezuo1hao)
					return;
            }

			if (materials.Count == 3)
			{
				__instance.RemoveDeckCards(materials);
				__instance.AddDeckCard(Library.CreateCard<cardhezuo1hao>());
                cardhezuo1hao.modifier = new System.Random((int)__instance.RootSeed).Next(1, 5);
            }
		}
	}
}
