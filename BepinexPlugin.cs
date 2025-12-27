using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using LBoL.Base;
using LBoL.Core;
using LBoL.EntityLib.EnemyUnits.Character;
using LBoLEntitySideloader;
using LBoLEntitySideloader.Entities;
using LBoLEntitySideloader.Resource;
using lvalonmeme.Cards.Template;
using lvalonmeme.Config;
using System;
using System.Collections.Generic;
using LBoLEntitySideloader.CustomHandlers;
using System.Reflection;
using UnityEngine;
using lvalonmeme.Cards;
using LBoLEntitySideloader.PersistentValues;
using LBoL.Presentation;
using System.Linq;
using LBoL.EntityLib.Adventures;
using lvalonmeme.JadeBoxes;
using Yarn;
using UnityEngine.Events;
using LBoL.Presentation.UI.Panels;
using LBoL.Presentation.UI.Widgets;
using LBoL.EntityLib.JadeBoxes;


namespace lvalonmeme
{
	[BepInPlugin(lvalonmeme.PInfo.GUID, lvalonmeme.PInfo.Name, lvalonmeme.PInfo.version)]
	[BepInDependency(LBoLEntitySideloader.PluginInfo.GUID, BepInDependency.DependencyFlags.HardDependency)]
	//[BepInDependency(AddWatermark.API.GUID, BepInDependency.DependencyFlags.SoftDependency)]
	[BepInProcess("LBoL.exe")]
	public class BepinexPlugin : BaseUnityPlugin
	{
		//The Unique mod ID of the mod.
		//If defined, this is also the ID used by the Act 1 boss.
		//WARNING: It is mandatory to rename it to avoid issues.
		public static string modUniqueID = "lvalonmeme";
		//Name of the character.
		//This is also the prefix that is used before every .png file in DirResources. 
		public static string playerName = "SampleCharacter";
		//Whether to us an ingame or custom model.
		//InGame: Will load the character model of the ingame character.
		//Custom: Will load DirResource/lvalonmemeel.png 
		public static bool useInGameModel = true;
		//If InGame is selected, this is the model that will be loaded. 
		//Check LBoL.EntityLib.EnemyUnits.Character or using LBoL.EntityLib.PlayerUnits for a list of all the characters available. 
		public static string modelName = nameof(Youmu);
		//Some in-game model needs to be flipped (most notably elites).
		public static bool modelIsFlipped = true;
		//The character's off-color.
		//Used to separate cards in the card collection and put the off-color cards at the end.
		public static List<ManaColor> offColors = new List<ManaColor>() { ManaColor.Colorless };

		//Whether the Act 1 boss should be enabled.
		//The value can be customized LBoL/BepInEx/config/
		//public static ConfigEntry<bool> enableAct1Boss;

		// public static CustomConfigEntry<bool> enableAct1BossEntry = new CustomConfigEntry<bool>(
		//     value: false,
		//     section: "EnableAct1Boss",
		//     key: "EnableAct1Boss",
		//     description: "Toggle the Act 1 boss. Default: Off");
		public static ConfigEntry<bool> oldcard;
		public static CustomConfigEntry<bool> oldcardentry = new CustomConfigEntry<bool>(
			value: false,
			section: "1. 模组偏好设定 / Mod Preferences",
			key: "版本迭代者补充包支援 / Re-version Iterator Suppoprt",
			description: "在「版本迭代者」补充包被启用的游戏开始时，将一张「版本迭代者」加入牌组。 / At the start of a run where the \"Re-vision Iterator\" Pack is enabled, add a \"Re-vision Iterator\" to the library.");

		public static ConfigEntry<string> blankid;
		public static CustomConfigEntry<string> blankidentry = new CustomConfigEntry<string>(
			value: "kongbaikapai",
			section: "1. 模组偏好设定 / Mod Preferences",
			key: "玉匣「广纳百川」展品 / All-Accepting Jadebox Exhibit",
			description: "在玉匣「广纳百川」被启用的游戏开始时，将会以此展品进行换四（填写展品id，填下「none」则不会获得展品，获得时生成介面的展品不会生效）。 / At the start of a run where the jadebox \"All-Accepting\" is enabled, swap with this specified exhibit (enter exhibit id, entering \"none\" will give no exhibits, exhibits that generates an interface when obtained will not take effect)");

		public static ConfigEntry<bool> color5support;
		public static CustomConfigEntry<bool> color5supportentry = new CustomConfigEntry<bool>(
			value: true,
			section: "1. 模组偏好设定 / Mod Preferences",
			key: "玉匣「五彩斑斓」升级 / Pentachromatic Jadebox Upgrade",
			description: "在玉匣「五彩斑斓」被启用的第一场战斗开始时，升级牌组和战场内的所有卡牌。 / At the start of the first battle where the jadebox \"Pentachromatic\" is enabled, upgrade all cards in the library and battlefield.");

		public static ConfigEntry<int> balancebot;
		public static CustomConfigEntry<int> balancebotentry = new CustomConfigEntry<int>(
			value: 1,
			section: "1. 模组偏好设定 / Mod Preferences",
			key: "玉匣「守恒」费用增加的总费用门槛 / Plasma Jadebox Threshold of Total Cost for Cost Increasing",
			description: "玉匣「守恒」中，使得费用增加的总费用门槛。 / On the jadebox \"Plasma\", this sets the threshold of total cost to be qualified for cost increasing.");

		public static ConfigEntry<int> balancetop;
		public static CustomConfigEntry<int> balancetopentry = new CustomConfigEntry<int>(
			value: 4,
			section: "1. 模组偏好设定 / Mod Preferences",
			key: "玉匣「守恒」费用降低的总费用门槛 / Plasma Jadebox Threshold of Total Cost for Cost Reducing",
			description: "玉匣「守恒」中，使得费用降低的总费用门槛。 / On the jadebox \"Plasma\", this sets the threshold of total cost to be qualified for cost reducing.");

		public static ConfigEntry<string> balanceban;
		public static CustomConfigEntry<string> balancebanentry = new CustomConfigEntry<string>(
			value: "danmujade",
			section: "1. 模组偏好设定 / Mod Preferences",
			key: "玉匣「守恒」卡牌黑名单 / Plasma Jadebox Card Blacklist",
			description: "黑名单内的卡牌不受玉匣「守恒」的影响（填写卡牌id并以「,」分隔） / Cards in the blacklist are immune from the effects of jadebox \"Plasma\" (enter card id, separated by commas)");

		public static ConfigEntry<bool> favpure;
		public static CustomConfigEntry<bool> favpureentry = new CustomConfigEntry<bool>(
			value: true,
			section: "1. 模组偏好设定 / Mod Preferences",
			key: "玉匣「我的最爱」基础支援 / My Favourite Jadebox Basic Support",
			description: "在玉匣「我的最爱」被启用时，如实将 5 张射击+及结界+加入牌库。此设定关闭时，额外获得一点彩色基础费用。 / At the start of a run where the jadebox \"My Favourite\" is enabled, add 5 Shoots+ and Boundaries+ to the library as written in the description. When this setting is disabled, gain an extra Philosophy base mana.");

		public static ConfigEntry<bool> favdebug;
		public static CustomConfigEntry<bool> favdebugentry = new CustomConfigEntry<bool>(
			value: false,
			section: "1. 模组偏好设定 / Mod Preferences",
			key: "玉匣「我的最爱」显示内测 / My Favourite Jadebox Show Debug",
			description: "在玉匣「我的最爱」被启用时，显示内测卡牌（警告！！！内测卡牌含剧透，也可能含模组的剧透，开启请三思） / At the start of a run where the jadebox \"My Favourite\" is enabled, all debug cards will be shown (WARNING!!! Debug cards contain spoilers and may also contain mod spoilers, please think carefully before enabling).");

		public static ConfigEntry<bool> doublelife;
		public static CustomConfigEntry<bool> doublelifeentry = new CustomConfigEntry<bool>(
			value: true,
			section: "1. 模组偏好设定 / Mod Preferences",
			key: "玉匣「宏观宇宙」血量加倍 / Macro Cosmos Jadebox Double Life",
			description: "在玉匣「宏观宇宙」被启用的游戏开始时，玩家血量加倍。 / At the start of a run where the jadebox \"Macro Cosmos\" is enabled, double the player's life.");

		public static ConfigEntry<bool> doublepower;
		public static CustomConfigEntry<bool> doublepowerentry = new CustomConfigEntry<bool>(
			value: true,
			section: "1. 模组偏好设定 / Mod Preferences",
			key: "玉匣「宏观宇宙」能量加倍 / Macro Cosmos Jadebox Double Power",
			description: "在玉匣「宏观宇宙」被启用时，收到的所有能量加倍，上限加倍，能多次使用。 / In a run where the jadebox \"Macro Cosmos\" is enabled, double the Power gained from all sources, double the charge limit, and allows multiple uses.");

		public static ConfigEntry<bool> doublemoney;
		public static CustomConfigEntry<bool> doublemoneyentry = new CustomConfigEntry<bool>(
			value: false,
			section: "1. 模组偏好设定 / Mod Preferences",
			key: "玉匣「宏观宇宙」金钱加倍 / Macro Cosmos Jadebox Double Money",
			description: "在玉匣「宏观宇宙」被启用时，收到的所有金钱加倍。 / In a run where the jadebox \"Macro Cosmos\" is enabled, double the Money gained from all sources.");

		public static ConfigEntry<bool> doublemana;
		public static CustomConfigEntry<bool> doublemanaentry = new CustomConfigEntry<bool>(
			value: false,
			section: "1. 模组偏好设定 / Mod Preferences",
			key: "玉匣「宏观宇宙」双倍法力 / Macro Cosmos Jadebox Double Mana",
			description: "在玉匣「宏观宇宙」被启用时，玩家的回合开始额外获得基础法力。 / At the start of the player's turn in a run where the jadebox \"Macro Cosmos\" is enabled, gain extra mana equal to the player's base mana.");

		public static ConfigEntry<bool> doubleplay;
		public static CustomConfigEntry<bool> doubleplayentry = new CustomConfigEntry<bool>(
			value: false,
			section: "1. 模组偏好设定 / Mod Preferences",
			key: "玉匣「宏观宇宙」回响形态 / Macro Cosmos Jadebox Echo Form",
			description: "在玉匣「宏观宇宙」被启用时，每张打出的牌都会额外打出一次。 / In a run where the jadebox \"Macro Cosmos\" is enabled, each card played will be played an additional time.");

		public static ConfigEntry<int> mult;
		public static CustomConfigEntry<int> multentry = new CustomConfigEntry<int>(
			value: 1,
			section: "2. 内容设定 / Content Modifications",
			key: "群友卡牌战斗外比重倍率 / Meme Card Non-Battle Weight Mult",
			description: "提升群友卡牌战斗外比重倍率至设定的倍数 / Multiplies non-battle weight of meme cards by the set multiplier.");

		public static ConfigEntry<string> cardban;
		public static CustomConfigEntry<string> cardbanentry = new CustomConfigEntry<string>(
			value: "",
			section: "2. 内容设定 / Content Modifications",
			key: "卡牌禁卡表 / Card Banlist",
			description: "禁止指定的卡牌以随机选取的方式出现在游戏里（填写卡牌id并以「,」分隔） / Bans selected cards from being rolled in the game (enter card id, separated by commas)");

		public static ConfigEntry<bool> banlistoverride;
		public static CustomConfigEntry<bool> banlistoverrideentry = new CustomConfigEntry<bool>(
			value: false,
			section: "2. 内容设定 / Content Modifications",
			key: "卡牌禁卡表凌驾 / Card Banlist Override",
			description: "卡牌禁卡表里的卡牌将全面禁止在局内出现，也会主动被移除。 / Cards in the Card Banlist will not exist in the game run, and will be removed actively.");

		public static ConfigEntry<string> cardwhite;
		public static CustomConfigEntry<string> cardwhiteentry = new CustomConfigEntry<string>(
			value: "",
			section: "2. 内容设定 / Content Modifications",
			key: "卡牌白名单 / Card Whitelist",
			description: "不在卡牌白名单里的卡将包含在卡牌禁卡表里，卡牌禁卡表里原先的牌会被覆盖。（填写卡牌id并以「,」分隔）（警告！！！请填写足够的卡牌以确保商店，产牌和事件等不会因卡池太小而卡死） / Cards not in the Card Whitelist will be added to the Card Banlist, and the original cards in the Card Banlist will be overrwritten. (enter card id, separated by commas)(WARNING!!! Please fill in enough cards to ensure that the shop, card generation and event cards don't softlock due to having too small of a card pool)");

		public static ConfigEntry<int> imult;
		public static CustomConfigEntry<int> imultentry = new CustomConfigEntry<int>(
			value: 1,
			section: "2. 内容设定 / Content Modifications",
			key: "自订卡牌比重倍率 / Custom Card Weight Mult",
			description: "提升自订卡牌比重倍率至设定的倍数 / Multiplies weight of the cards in the following Custom Card List by the set multiplier.");

		public static ConfigEntry<string> imultlist;
		public static CustomConfigEntry<string> imultlistentry = new CustomConfigEntry<string>(
			value: "",
			section: "2. 内容设定 / Content Modifications",
			key: "自订卡牌表 / Custom Card List",
			description: "自订卡牌表里的卡受自订卡牌比重倍率影响（填写卡牌id并以「,」分隔） / Cards in the Custom Card List are affected by the Custom Card Weight Mult (enter card id, separated by commas)");

		private static readonly Harmony harmony = lvalonmeme.PInfo.harmony;

		internal static BepInEx.Logging.ManualLogSource log;

		internal static TemplateSequenceTable sequenceTable = new TemplateSequenceTable();

		internal static IResourceSource embeddedSource = new EmbeddedSource(Assembly.GetExecutingAssembly());

		// add this for audio loading
		internal static DirectorySource directorySource = new DirectorySource(lvalonmeme.PInfo.GUID, "");


		private void Awake()
		{
			log = Logger;
			///Load the custom config entry.
			//enableAct1Boss = Config.Bind(enableAct1BossEntry.Section, enableAct1BossEntry.Key, enableAct1BossEntry.Value, enableAct1BossEntry.Description);
			oldcard = Config.Bind(oldcardentry.Section, oldcardentry.Key, oldcardentry.Value, oldcardentry.Description);
			blankid = Config.Bind(blankidentry.Section, blankidentry.Key, blankidentry.Value, blankidentry.Description);
			color5support = Config.Bind(color5supportentry.Section, color5supportentry.Key, color5supportentry.Value, color5supportentry.Description);
			balancebot = Config.Bind(balancebotentry.Section, balancebotentry.Key, balancebotentry.Value, balancebotentry.Description);
			balancetop = Config.Bind(balancetopentry.Section, balancetopentry.Key, balancetopentry.Value, balancetopentry.Description);
			balanceban = Config.Bind(balancebanentry.Section, balancebanentry.Key, balancebanentry.Value, balancebanentry.Description);
			favpure = Config.Bind(favpureentry.Section, favpureentry.Key, favpureentry.Value, favpureentry.Description);
			favdebug = Config.Bind(favdebugentry.Section, favdebugentry.Key, favdebugentry.Value, favdebugentry.Description);
			doublelife = Config.Bind(doublelifeentry.Section, doublelifeentry.Key, doublelifeentry.Value, doublelifeentry.Description);
			doublepower = Config.Bind(doublepowerentry.Section, doublepowerentry.Key, doublepowerentry.Value, doublepowerentry.Description);
			doublemoney = Config.Bind(doublemoneyentry.Section, doublemoneyentry.Key, doublemoneyentry.Value, doublemoneyentry.Description);
			doublemana = Config.Bind(doublemanaentry.Section, doublemanaentry.Key, doublemanaentry.Value, doublemanaentry.Description);
			doubleplay = Config.Bind(doubleplayentry.Section, doubleplayentry.Key, doubleplayentry.Value, doubleplayentry.Description);


			mult = Config.Bind(multentry.Section, multentry.Key, multentry.Value, multentry.Description);
			cardban = Config.Bind(cardbanentry.Section, cardbanentry.Key, cardbanentry.Value, cardbanentry.Description);
			banlistoverride = Config.Bind(banlistoverrideentry.Section, banlistoverrideentry.Key, banlistoverrideentry.Value, banlistoverrideentry.Description);
			cardwhite = Config.Bind(cardwhiteentry.Section, cardwhiteentry.Key, cardwhiteentry.Value, cardwhiteentry.Description);
			log.LogDebug("lvalonmeme bepinex loaded all configs");
			string[] cardbanarr2 = cardban.Value.Split(',');
			string[] cardbanarr = new string[] { };
			foreach (string cardid in cardbanarr2)
			{
				cardbanarr.AddItem(cardid.ToLowerInvariant());
			}

			foreach (string cardid in cardbanarr)
			{
				if (cardid.ToLowerInvariant() != "")
				{
					log.LogDebug("lvalonmeme bepinex cardban: " + cardid.ToLowerInvariant());
				}
			}

			imult = Config.Bind(imultentry.Section, imultentry.Key, imultentry.Value, imultentry.Description);
			imultlist = Config.Bind(imultlistentry.Section, imultlistentry.Key, imultlistentry.Value, imultlistentry.Description);
			string[] imultlistarr = imultlist.Value.Split(',');
			foreach (string cardid in imultlistarr)
			{
				if (cardid.ToLowerInvariant() != "")
				{
					log.LogDebug("lvalonmeme bepinex imultlist: " + cardid.ToLowerInvariant());
				}
			}

			// very important. Without this the entry point MonoBehaviour gets destroyed
			DontDestroyOnLoad(gameObject);
			gameObject.hideFlags = HideFlags.HideAndDontSave;

			CardIndexGenerator.PromiseClearIndexSet();
			EntityManager.RegisterSelf();

			harmony.PatchAll();

			CHandlerManager.RegisterGameEventHandler(
				(GameRunController gr) => gr.DeckCardsAdding,
				CustomHandlers.OnDeckCardsAdding
				);
			CHandlerManager.RegisterGameEventHandler(
				(GameRunController gr) => gr.DeckCardsAdded,
				CustomHandlers.OnDeckCardsAdded
				);
			CHandlerManager.RegisterGameEventHandler(
				(GameRunController gr) => gr.DeckCardsRemoving,
				CustomHandlers.OnDeckCardsRemoving
				);
			CHandlerManager.RegisterGameEventHandler(
				(GameRunController gr) => gr.DeckCardsRemoved,
				CustomHandlers.OnDeckCardsRemoved
				);
			CHandlerManager.RegisterGameEventHandler(
				(GameRunController gr) => gr.StationRewardGenerating,
				CustomHandlers.StationRewardGenerating
				);
			CHandlerManager.RegisterGameEventHandler(
				(GameRunController gr) => gr.StationEntering,
				CustomHandlers.StationEntering
				);
			CHandlerManager.RegisterGameEventHandler(
				(GameRunController gr) => gr.StationEntered,
				CustomHandlers.StationEntered
				);
			CustomHandlers.addreactors();
			new lvalonmemedata().RegisterSelf(PInfo.GUID);

			//if (BepInEx.Bootstrap.Chainloader.PluginInfos.ContainsKey(AddWatermark.API.GUID))
			//    WatermarkWrapper.ActivateWatermark();

			Func<Sprite> getSprite = () => ResourceLoader.LoadSprite("BossIcon.png", directorySource);
			EnemyUnitTemplate.AddBossNodeIcon(nameof(lvalonmeme.Enemies.lvalonmeme), getSprite);
			List<string> cardart = new List<string>
			{
				nameof(cardkotk),
				nameof(cardyoumi)
			};
			foreach (string cardartid in cardart)
			{
				// ResourceLoader.LoadTexture(artid + 2 + ImageLoader.SampleCharacterImageLoader.file_extension, embeddedSource);
				CardImages cardImages = new CardImages(embeddedSource);
				cardImages.AutoLoad(cardartid + 2, ImageLoader.SampleCharacterImageLoader.file_extension, relativePath: "Resources.Cards.");
				ResourcesHelper.CardImages.AlwaysAdd(cardartid + 2, cardImages.main);
			}
		}
		//patches
		[HarmonyPatch(typeof(Debut), nameof(Debut.InitVariables))]
		private class Debut_InitVariables_Patch
		{
			private static void Postfix(Debut __instance, ref IVariableStorage storage)
			{
				if (GameMaster.Instance.CurrentGameRun.JadeBoxes.Any((JadeBox jb) => jb.Id == nameof(JadeBoxSwapBlank)))
				{
					storage.SetValue("$twoColorStart", true);
				}
			}
		}
		[HarmonyPatch(typeof(StartGamePanel), nameof(StartGamePanel.Awake))]
		class StartGamePanel_Awake_Patch
		{
			static void Postfix(ref StartGamePanel __instance)
			{
				try
				{
					// 5/7 color exclusivity
					JadeBoxToggle color5Toggle = null;
					JadeBoxToggle color7Toggle = null;
					JadeBoxToggle myfav = null;
					JadeBoxToggle best10 = null;
					foreach (var toggle in __instance._jadeBoxToggles)
					{
						//Search for the two relevant toggles
						if (toggle.Value.JadeBox.Id == nameof(JadeBox5Color))
						{
							Debug.Log("found 5colors");
							color5Toggle = toggle.Value;
						}
						if (toggle.Value.JadeBox.Id == nameof(JadeBox7Color))
						{
							Debug.Log("found 7colors");
							color7Toggle = toggle.Value;
						}
						if (toggle.Value.JadeBox.Id == nameof(JadeBoxFav))
						{
							Debug.Log("found myfav");
							myfav = toggle.Value;
						}
						if (toggle.Value.JadeBox.Id == nameof(SelectCard))
						{
							Debug.Log("found best10");
							best10 = toggle.Value;
						}
					}

					if (myfav != null && best10 != null)
					{
						//Add action to the toggles that disable the other toggle when triggered
						myfav.Toggle.onValueChanged.AddListener(new UnityAction<bool>((bool b) =>
						{
							Debug.Log("toggled myfav");
							if (best10 != null && best10.IsOn)
							{
								Debug.Log("disabling best10");
								best10.Toggle.SetIsOnWithoutNotify(false);
							}
						}));

						best10.Toggle.onValueChanged.AddListener(new UnityAction<bool>((bool b) =>
						{
							Debug.Log("toggled best10");
							if (myfav != null && myfav.IsOn)
							{
								Debug.Log("disabling myfav");
								myfav.Toggle.SetIsOnWithoutNotify(false);
							}
						}));
					}

					if (color5Toggle != null && color7Toggle != null)
					{
						//Add action to the toggles that disable the other toggle when triggered
						color5Toggle.Toggle.onValueChanged.AddListener(new UnityAction<bool>((bool b) =>
						{
							Debug.Log("toggled color5Toggle");
							if (color7Toggle != null && color7Toggle.IsOn)
							{
								Debug.Log("disabling color7Toggle");
								color7Toggle.Toggle.SetIsOnWithoutNotify(false);
							}
						}));

						color7Toggle.Toggle.onValueChanged.AddListener(new UnityAction<bool>((bool b) =>
						{
							Debug.Log("toggled color7Toggle");
							if (color5Toggle != null && color5Toggle.IsOn)
							{
								Debug.Log("disabling color5Toggle");
								color5Toggle.Toggle.SetIsOnWithoutNotify(false);
							}
						}));
					}
				}
				catch (Exception e)
				{
					Debug.Log("error when checking jadebox toggles: " + e.Message + e.StackTrace);
				}
			}
		}

		//PERSISTENT VALUES
		public class lvalonmemedata : CustomGameRunSaveData
		{
			public override void Restore(GameRunController gameRun)
			{
				//log.LogDebug("lvalonmeme bepinex restoring");
			}

			public override void Save(GameRunController gameRun)
			{
				//log.LogDebug("lvalonmeme bepinex saving");
				//youmiplayed = 1;
			}
			//public int youmiplayed;
		}

		private void OnDestroy()
		{
			if (harmony != null)
				harmony.UnpatchSelf();
		}
	}
}
