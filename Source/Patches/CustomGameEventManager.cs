using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using HarmonyLib;
using LBoL.Base;
using LBoL.ConfigData;
using LBoL.Core;
using LBoL.Core.Cards;
using LBoL.Core.Randoms;
using LBoL.EntityLib.Cards.Character.Cirno;
using LBoL.EntityLib.Cards.Character.Reimu;
using LBoL.EntityLib.Cards.Character.Sakuya;
using LBoL.EntityLib.Cards.Neutral.Black;
using LBoL.EntityLib.Cards.Neutral.Blue;
using LBoL.EntityLib.Cards.Neutral.MultiColor;
using LBoL.EntityLib.Cards.Neutral.NoColor;
using LBoL.EntityLib.Cards.Neutral.Red;
using LBoL.EntityLib.Cards.Neutral.White;
using LBoL.Presentation;
using LBoL.Presentation.UI;
using LBoL.Presentation.Units;
using lvalonmeme.Cards;
using lvalonmeme.JadeBoxes;
using lvalonmeme.Packs;

namespace lvalonmeme.Patches
{
	[HarmonyPatch]
	class CustomGameEventManager
	{
		[HarmonyPatch(typeof(UiManager), "EnterGameRun")]
		private class UiManager_EnterGameRun_Patch
		{
			private static void Postfix(UiManager __instance)
			{
				if (GameMaster.Instance.CurrentGameRun != null)
				{
					if (GameMaster.Instance.CurrentGameRun.JadeBoxes.Any(x => x.Id == nameof(JadeBoxDouble) && BepinexPlugin.doublelife.Value))
					{
						TweenExtensions.WaitForCompletion((Tween)(object)TweenSettingsExtensions.SetAutoKill(TweenSettingsExtensions.SetUpdate(TweenSettingsExtensions.SetEase(ShortcutExtensions.DOScale(Singleton<GameMaster>.Instance.CurrentGameRun.Player.GetView<UnitView>().transform, new UnityEngine.Vector3(1.2f, 1.2f, 1.2f), 1f), (Ease)1), true), true));
					}
				}
				// if (BepinexPlugin.doublelife.Value)
				// {
				//     TweenExtensions.WaitForCompletion((Tween)(object)TweenSettingsExtensions.SetAutoKill(TweenSettingsExtensions.SetUpdate(TweenSettingsExtensions.SetEase(ShortcutExtensions.DOScale(Singleton<GameMaster>.Instance.CurrentGameRun.Player.GetView<UnitView>().transform, new UnityEngine.Vector3(2f, 2f, 2f), 1f), (Ease)1), true), true));
				// }
				//TweenExtensions.WaitForCompletion((Tween)(object)TweenSettingsExtensions.SetAutoKill(TweenSettingsExtensions.SetUpdate(TweenSettingsExtensions.SetEase(ShortcutExtensions.DOScale(Singleton<GameMaster>.Instance.CurrentGameRun.Player.GetView<UnitView>().transform, new UnityEngine.Vector3(2f, 2f, 2f), 1f), (Ease)1), true), true));
			}
		}
		[HarmonyPatch(typeof(GameRunController), "InternalGainMoney")]
		private class GameRunController_InternalGainMoney_Patch
		{
			private static void Prefix(GameRunController __instance, ref int money)
			{
				if (GameMaster.Instance.CurrentGameRun.JadeBoxes.Any(x => x.Id == nameof(JadeBoxDouble)) && BepinexPlugin.doublemoney.Value)
				{
					money *= 2;
				}
			}
		}
		[HarmonyPatch(typeof(GameRunController), "InternalGainPower")]
		private class GameRunController_InternalGainPower_Patch
		{

			private static void Prefix(GameRunController __instance, ref int power)
			{
				if (GameMaster.Instance.CurrentGameRun.JadeBoxes.Any(x => x.Id == nameof(JadeBoxDouble)) && BepinexPlugin.doublepower.Value)
				{
					power *= 2;
				}
			}
		}
		[HarmonyPatch(typeof(GameRunController), nameof(GameRunController.RollTransformCard), typeof(RandomGen), typeof(CardWeightTable), typeof(bool), typeof(bool), typeof(Predicate<CardConfig>)), HarmonyPostfix]
		private static void Postfix(GameRunController __instance, ref Card __result, RandomGen rng, CardWeightTable weightTable, bool applyFactors, bool battleRolling, Predicate<CardConfig> filter)
		{
			if (__result == null)
			{
				__result = __instance.RollCard(rng, weightTable, applyFactors, battleRolling, null);
				__result ??= Library.CreateCard<Shoot>();
			}
		}

		// static public GameEvent<BuffAttackEventArgs> PreCustomEvent { get; set; }
		// static public GameEvent<BuffAttackEventArgs> PostCustomEvent { get; set; }

		// [HarmonyPatch(typeof(GameRunController), nameof(GameRunController.EnterBattle))]
		// private static bool Prefix(GameRunController __instance)
		// {
		//     PreCustomEvent = new GameEvent<BuffAttackEventArgs>();
		//     PostCustomEvent = new GameEvent<BuffAttackEventArgs>();
		//     return true;
		// }
		public static IEnumerable<string> GetList(string type)
		{
			switch (type)
			{
				case "meme":
					return memecardids;
				case "old":
					return oldcardids;
				case "oldban":
					return oldcardbanids;
				default:
					break;
			}
			return new List<string>();
		}
		static List<string> memecardids = new List<string>
			{
                //day1
                nameof(cardlvalon),
				nameof(cardkotk),
				nameof(card9999),
				nameof(cardanlemi),
				nameof(cardaleph),
				nameof(cardedoras),
				nameof(cardyanling),
				nameof(cardshouchang),
                //day2
                nameof(cardallfriends),
				nameof(cardprimitive),
				nameof(cardbb),
				nameof(cardsanaeseduction),
                //day3
                nameof(cardgg),
				nameof(cardsanaeconfession),
				nameof(cardyoumi),
				nameof(cardarashi),
				nameof(cardbluepoint),
				nameof(cardcirnolevel),

				nameof(cardicewingplus),

				nameof(cardfarming),
				nameof(cardarashidad),
				nameof(cardmathexhibit),
				nameof(cardtfm),
				nameof(cardperfectfumo),
			};
		static List<string> oldcardids = new List<string>
			{
                //day4
                nameof(cardold),
				nameof(cardDoremyDuplicate),
				nameof(cardYonglinCard),
                //day5
                nameof(cardMystiaSing),
				nameof(cardMeilingBlock),
				nameof(cardTwoBalls),
				nameof(cardIceLance),
				nameof(cardIceMatrix),
				nameof(cardLarvaDefense),
                //day6
                nameof(cardSummerParty),
				nameof(cardWhiteWorld),
				nameof(cardFairyTree),
				nameof(cardDeepFreeze),
				nameof(cardBladePower),
                //day7
                nameof(cardFairyWrath),
				nameof(cardColdChain),
				nameof(cardJinziDoppelganger),
				nameof(cardTaijiLiangyi),
				nameof(cardYaoguaiBuster),
				nameof(cardCleanRoom),
				nameof(cardSakuyaSleep),
				nameof(cardDropToKnife),
				nameof(cardChangzhizhen),
				nameof(cardHuanxiangBlock),
				nameof(cardShenziUpgradeAll),
			};
		static List<string> oldcardbanids = new List<string>
			{
                //day4
                nameof(DoremyDuplicate),
				nameof(YonglinCard),
                //day5
                nameof(MystiaSing),
				nameof(MeilingBlock),
				nameof(TwoBalls),
				nameof(IceLance),
				nameof(IceMatrix),
				nameof(LarvaDefense),
                //day6
                nameof(SummerParty),
				nameof(WhiteWorld),
				nameof(FairyTree),
				nameof(DeepFreeze),
				nameof(BladePower),
                //day7
                nameof(FairyWrath),
				nameof(ColdChain),
				nameof(JinziDoppelganger),
				nameof(TaijiLiangyi),
				nameof(YaoguaiBuster),
				nameof(CleanRoom),
				nameof(SakuyaSleep),
				nameof(DropToKnife),
				nameof(Changzhizhen),
				nameof(HuanxiangBlock),
				nameof(ShenziUpgradeAll),
			};
		[HarmonyPatch(typeof(CardWeightTable), nameof(CardWeightTable.WeightFor), typeof(CardConfig), typeof(string), typeof(ISet<string>)), HarmonyPostfix]
		public static void OverrideWeightFor(CardWeightTable __instance, CardConfig cardConfig, string playerId, ISet<string> exhibitOwnerIds, ref float __result)
		{
			// if (GameMaster.Instance.CurrentGameRun != null)
			// {
			// banlist
			string[] cardbanarr = toolbox.banlistgetter();
			foreach (string id in cardbanarr)
			{
				if (id.ToLowerInvariant() == cardConfig.Id.ToLowerInvariant())
				{
					__result = 0f;
				}
			}
			// custom mult
			float imult = 1f * BepinexPlugin.imult.Value;
			string[] icardarr = BepinexPlugin.imultlist.Value.Split(',');
			foreach (string id in icardarr)
			{
				if (id.ToLowerInvariant() == cardConfig.Id.ToLowerInvariant())
				{
					__result *= imult;
				}
			}
			// meme mult
			if (memecardids.Contains(cardConfig.Id) && GameMaster.Instance?.CurrentGameRun?.Battle == null)
			{
				__result *= 1f * BepinexPlugin.mult.Value;
			}

			// meme cards
			// if (GameMaster.Instance.CurrentGameRun.JadeBoxes.Any((JadeBox jb) => jb.Id == nameof(JadeBoxEnableMemes)))
			// {
			// 	float mult = 1f * BepinexPlugin.mult.Value;
			// 	// fallback no gamerun
			// 	if (GameMaster.Instance.CurrentGameRun == null)
			// 	{
			// 		if (memecardids.Contains(cardConfig.Id))
			// 		{
			// 			__result *= mult;
			// 		}
			// 	}
			// 	// in gamerun
			// 	else
			// 	{
			// 		// no copy, and not in battle
			// 		if (GameMaster.Instance.CurrentGameRun.Battle == null && memecardids.Contains(cardConfig.Id) && !GameMaster.Instance.CurrentGameRun.BaseDeck.Where(x => memecardids.Contains(x.Id)).Select(x => x.Id).ToList().Contains(cardConfig.Id)) // no copy in deck
			// 		{
			// 			__result *= mult;
			// 		}
			// 	}
			// }
			// else if (memecardids.Contains(cardConfig.Id))
			// {
			// 	__result = 0f;
			// }

			// no old cards
			if ((GameMaster.Instance?.CurrentGameRun?.Packs.Contains(nameof(packoldDef)[..^3]) ?? false) && oldcardbanids.Contains(cardConfig.Id))
			{
				__result = 0f;
			}
			// }
		}

		// [HarmonyPatch(typeof(GameRunController), nameof(GameRunController.CreateValidCardsPool), typeof(CardWeightTable), typeof(ManaGroup?), typeof(bool), typeof(bool), typeof(bool), typeof(Predicate<CardConfig>)), HarmonyPostfix]
		// public static void AlterWeights(GameRunController __instance, CardWeightTable weightTable, ManaGroup? manaLimit, bool colorLimit, bool applyFactors, bool battleRolling, Predicate<CardConfig> filter, ref UniqueRandomPool<Type> __result)
		// {
		//     if (!battleRolling)
		//     {
		//         __result = __result.WithAlteredWeights(rollableCard => cards.Contains(rollableCard.Elem.Name) ? 100000000f * rollableCard.Weight : rollableCard.Weight);
		//     }
		// }
	}
	// public static class UniqueRandomPoolExtensions
	// {
	//     public static UniqueRandomPool<T> WithAlteredWeights<T>(this UniqueRandomPool<T> pool, Func<RandomPoolEntry<T>, float> weightFunc)
	//     {
	//         UniqueRandomPool<T> newPool = new UniqueRandomPool<T>(pool._fallback);

	//         foreach (var entry in pool)
	//         {
	//             newPool.Add(entry.Elem, weightFunc(entry));
	//         }

	//         return newPool;
	//     }
	// }
}