// using System;
// using System.Collections.Generic;
// using HarmonyLib;
// using LBoL.Base;
// using LBoL.ConfigData;
// using LBoL.Core;
// using LBoL.Core.Cards;
// using LBoL.Core.Randoms;
// using lvalonmeme.BattleActions;
// using lvalonmeme.Cards;

// namespace lvalonmeme.Patches
// {
//     [HarmonyPatch]
//     class CustomGameEventManager
//     {
//         // prefix omitted for brevity

//         static List<string> cards = new List<string>
//             {
//                 nameof(cardlvalon),
//                 nameof(cardkotk),
//                 nameof(card9999),
//                 nameof(cardanlemi),
//                 nameof(cardaleph),
//                 nameof(cardedoras),
//                 nameof(cardyanling),
//                 nameof(cardshouchang)
//             };

//         [HarmonyPatch(typeof(GameRunController), nameof(GameRunController.CreateValidCardsPool), typeof(CardWeightTable), typeof(ManaGroup?), typeof(bool), typeof(bool), typeof(bool), typeof(Predicate<CardConfig>)), HarmonyPostfix]
//         public static void AlterWeights(GameRunController __instance, CardWeightTable weightTable, ManaGroup? manaLimit, bool colorLimit, bool applyFactors, bool battleRolling, Predicate<CardConfig> filter, ref UniqueRandomPool<Type> __result)
//         {
//             if (!battleRolling)
//             {
//                 __result = __result.WithAlteredWeights(rollableCard => cards.Contains(rollableCard.Elem.Name) ? 100000000f * rollableCard.Weight : rollableCard.Weight);
//             }
//         }
//     }

//     public static class UniqueRandomPoolExtensions
//     {
//         public static UniqueRandomPool<T> WithAlteredWeights<T>(this UniqueRandomPool<T> pool, Func<RandomPoolEntry<T>, float> weightFunc)
//         {
//             UniqueRandomPool<T> newPool = new UniqueRandomPool<T>(pool._fallback);

//             foreach (var entry in pool)
//             {
//                 newPool.Add(entry.Elem, weightFunc(entry));
//             }

//             return newPool;
//         }
//     }
// }