# 更新日志 / Changelog

## [0.0.29] - 2025-7-2

### 修复 / Fixed

- 一些 bug。
- Some bugs.

### 改动 / Changes

- 玉匣「宏观宇宙」的结界/防御改为各给 5 张。
- Jadebox "Macro Cosmos" now adds 5 Shoot/Boundary.

## [0.0.28] - 2025-5-24

### 新增 / Added

- 玉匣「宏观宇宙」下，血量翻倍的角色体型也会变大。
- Under Jadebox "Macro Cosmos", units with double life will also be enlarged.

## [0.0.27] - 2025-5-23

### 修复 / Fixed

- 玉匣「宏观宇宙」下召唤物获得四倍血量的 bug。
- Summons having 4 times the original life under the influence of Jadebox "Macro Cosmos".

## [0.0.26] - 2025-5-23

### 修复 / Fixed

- 玉匣「宏观宇宙」下幽幽子召唤物获得四倍血量的 bug。
- Yuyuko's Summons having 4 times the original life under the influence of Jadebox "Macro Cosmos".

## [0.0.25] - 2025-5-23

### 修复 / Fixed

- 以额外展品真正修复玉匣「宏观宇宙」的 p 点问题。
- 金字塔每回合叠一次的问题。
- Truly fixed the power problem with an extra exhibit.
- Jadebox "Macro Cosmos" Pyramid stacking once per turn.

## [0.0.24] - 2025-5-22

### 修复 / Fixed

- 玉匣「宏观宇宙」现在不会因重开而失去 p 上限和重复发动特性。
- Jadebox "Macro Cosmos" now will not lose max charges and repeated use on restart.

## [0.0.23] - 2025-5-22

### 改动 / Changes

- 玉匣「宏观宇宙」新增了金字塔形状的积木效果。
- Jadebox "Macro Cosmos" now has the Pyramid-Shaped Puzzle effect.

## [0.0.22] - 2025-5-22

### 新增 / Added

- 玉匣「宏观宇宙」。
- Jadebox "Macro Cosmos".

## [0.0.21] - 2025-5-19

### 修复 / Fixed

- 模组给予展品的代码现在能正常运作。
- Modded code that gives exhibits can function properly now.

## [0.0.20] - 2025-5-16

### 修复 / Fixed

- 妖梦事件现在不会因只有一种颜色的基础法力而卡死。
- Youmu encounter will no longer softlock because of only having a monocolor mana base.

## [0.0.19] - 2025-5-15

### 修复 / Fixed

- 新增了防 bug 对策：没有五色基础法力时获得空白卡牌。
- Added a bug safeguard: Obtain Blank Card when there are no monochromatic base mana.

## [0.0.18] - 2025-5-15

### 修复 / Fixed

- 真的修复了玉匣「我的最爱」不能显示游戏里所有的牌。
- Fixed jadebox My Favourite not showing all cards in the game fr.

## [0.0.17] - 2025-5-15

### 修复 / Fixed

- 玉匣「我的最爱」不能显示游戏里所有的牌。
- Jadebox My Favourite not showing all cards in the game.

## [0.0.16] - 2025-5-15

### 改动 / Changes

- 玉匣「我的最爱」现在不能与「精挑细选」一起被启用。
- Jadebox My Favourite and Best Collection are now mutually exclusive.

## [0.0.15] - 2025-5-15

### 新增 / Added

- 玉匣「我的最爱」（启动会卡半分钟）：移除初始套牌及基础法力。从游戏里选择一张牌加入牌库，并以|该牌的费用|获得基础法力（数字部份及混血以彩色代替）。若基础法力不足 5 ，则以彩色补上。将 4 张射击+及结界+加入牌库。（可于设定文件内取消基础牌支援，以加基础彩费代替）
- 设定文件内的「卡牌白名单」：不在卡牌白名单里的卡将包含在卡牌禁卡表里，卡牌禁卡表里原先的牌会被覆盖。（警告！！！请填写足够的卡牌以确保商店，产牌和事件等不会因卡池太小而卡死）
- Jadebox My Favourite (loads for half a minute on start): Remove the base deck and base mana. Choose a card from the game to add to the library, then gain base mana equal to its cost (generic mana cost component and hybrid mana are replaced with Philosophy mana). If total base mana is less than 5, fill with Philosophy mana. Add 4 Shoots+ and Boundaries+ to the library. (can cancel the basic card support and gain a Philosophy base mana instead in the config file)
- Card Whitelist in config file: Cards not in the Card Whitelist will be added to the Card Banlist, and the original cards in the Card Banlist will be overrwritten. (WARNING!!! Please fill in enough cards to ensure that the shop, card generation and event cards don't softlock due to having too small of a card pool)

## [0.0.14] - 2025-5-11

### 修复 / Fixed

- 玉匣「五彩斑斓」的升级设定被启用时，现在不会在退出重登后在不正确的时机升级所有卡牌。
- When the Pentachromatic Jadebox Upgrade is enabled, it will no longer upgrade all the cards at the wrong timing after quitting and rejoining the game run.

## [0.0.13] - 2025-5-10

### 修复 / Fixed

- 英文版的玉匣现在会正确的被加载。
- English Jadebox names and descriptions will now load properly.

## [0.0.12] - 2025-5-9

### 修复 / Fixed

- 群友安勒米现在现在不会突然给牌加上放逐和虚无。
- Meme 安勒米 will not add Exile and Ethereal on the card now.

## [0.0.11] - 2025-5-9

### 修复 / Fixed

- 妖精之家现在不会因自己抽的牌触发自己。
- Fairy's Abode will not trigger by its own draw now.

## [0.0.10] - 2025-5-9

### 修复 / Fixed

- 深度冻结现在能正确地使受到的所有伤害翻倍。
- Deep Freeze now doubles all damage taken correctly.

## [0.0.9] - 2025-5-9

### 改动 / Changes

- 设定文件里卡牌禁卡的凌驾选项：启用时，卡牌禁卡表里的卡牌将全面禁止在局内出现，也会主动被移除。
- Card Banlist Override option in config: When enabled, cards in the Card Banlist will not exist in the game run, and will be removed actively.

## [0.0.8] - 2025-5-8

### 修复 / Fixed

- 在玉匣「广纳百川」与「五彩斑斓」或「七彩缤纷」一起启用的时候，现在会正确地在最后移除原本展品的基础法力。
- 在玉匣「版本迭代者」启用时，现在将在新版卡牌出现后以旧版卡牌替代。
- When jadebox All-Accepting and Pentachromatic/Prismatic are both enabled, it will now correctly remove the original exhibit's base mana at last.
- When jadebox Re-version Iterator is enabled, it will now replace the modern versions of the cards after they have appeared.

## [0.0.7] - 2025-5-8

### 修复 / Fixed

- 妖精之家 bug 修复
- Fairy's Abode bugfix

## [0.0.6] - 2025-5-7

### 改动 / Changes

- 版本迭代者现在会将初始牌组的卡替换为对应的旧版卡。
- 全面支援繁中。
- Re-version Iterator will now replace cards in the starting deck with their respective Historic cards.
- Full traditional chinese language support

## [0.0.5] - 2025-5-7

### 新增 / Added

- 初代旧版卡牌完成（24 张 + 版本迭代者）。
- 开启群友，旧版模式的玉匣。旧版模式下所有对应的新版卡不会被随机获得。
- 全面支援英语。
- 设定文件内新增了卡牌禁卡表，自订卡牌表及对应的自订卡牌比重倍率。
- First generation of Historic cards completed (24 cards + Re-version Iterator).
- Jadeboxes that enables the Meme and Historic gamemodes. When the Historic gamemode is active, all respective modern cards will not be ranndomly generated.
- Full english language support.
- Added Card Banlist, Custom Card List and its corresponding Custom Card Weight Mult in the config file.

### 改动 / Changes

- 优化了卡牌文本。
- 优化了设定文件。建议将现有的设定文件以生成一个新的设定文件。
- Improved card text.
- Improved config file. Recommended to delete the old config file to generate a new one.

## [0.0.4] - 2025-4-28

### 修复 / Fixed

- 把 cardcirnolevel 加到群友名单里，现在没开群友启动！不会擅自出现了。
- Added cardcirnolevel to community member list, now it won't appear without enabling the specific jadebox.

## [0.0.3] - 2025-4-28

### 修复 / Fixed

- 速修诈骗兔子 bug
- Fraud rabbit bug

## [0.0.2] - 2025-4-28

### 新增 / Added

- QQ 群友牌数量提升至 18 张。
- 设定文件：mod 卡牌战斗外比重倍率及卡牌禁卡表。
- Number of QQ community member cards increased to 18.
- Config file: Modded Card Non-Battle Weight Multiplier and the Card Banlist.

## [0.0.1] - 2025-4-19

### 初版 / Initial version

- 初代 QQ 群友牌完成（8 张）。
- First generation of QQ community member (meme) cards completed (8 cards).

[0.0.30]: https://thunderstore.io/package/download/Lvalon/Everyone_Is_Here/0.0.30/
[0.0.29]: https://thunderstore.io/package/download/Lvalon/Everyone_Is_Here/0.0.29/
[0.0.28]: https://thunderstore.io/package/download/Lvalon/Everyone_Is_Here/0.0.28/
[0.0.27]: https://thunderstore.io/package/download/Lvalon/Everyone_Is_Here/0.0.27/
[0.0.26]: https://thunderstore.io/package/download/Lvalon/Everyone_Is_Here/0.0.26/
[0.0.25]: https://thunderstore.io/package/download/Lvalon/Everyone_Is_Here/0.0.25/
[0.0.24]: https://thunderstore.io/package/download/Lvalon/Everyone_Is_Here/0.0.24/
[0.0.23]: https://thunderstore.io/package/download/Lvalon/Everyone_Is_Here/0.0.23/
[0.0.22]: https://thunderstore.io/package/download/Lvalon/Everyone_Is_Here/0.0.22/
[0.0.21]: https://thunderstore.io/package/download/Lvalon/Everyone_Is_Here/0.0.21/
[0.0.20]: https://thunderstore.io/package/download/Lvalon/Everyone_Is_Here/0.0.20/
[0.0.19]: https://thunderstore.io/package/download/Lvalon/Everyone_Is_Here/0.0.19/
[0.0.18]: https://thunderstore.io/package/download/Lvalon/Everyone_Is_Here/0.0.18/
[0.0.17]: https://thunderstore.io/package/download/Lvalon/Everyone_Is_Here/0.0.17/
[0.0.16]: https://thunderstore.io/package/download/Lvalon/Everyone_Is_Here/0.0.16/
[0.0.15]: https://thunderstore.io/package/download/Lvalon/Everyone_Is_Here/0.0.15/
[0.0.14]: https://thunderstore.io/package/download/Lvalon/Everyone_Is_Here/0.0.14/
[0.0.13]: https://thunderstore.io/package/download/Lvalon/Everyone_Is_Here/0.0.13/
[0.0.12]: https://thunderstore.io/package/download/Lvalon/Everyone_Is_Here/0.0.12/
[0.0.11]: https://thunderstore.io/package/download/Lvalon/Everyone_Is_Here/0.0.11/
[0.0.10]: https://thunderstore.io/package/download/Lvalon/Everyone_Is_Here/0.0.10/
[0.0.9]: https://thunderstore.io/package/download/Lvalon/Everyone_Is_Here/0.0.9/
[0.0.8]: https://thunderstore.io/package/download/Lvalon/Everyone_Is_Here/0.0.8/
[0.0.7]: https://thunderstore.io/package/download/Lvalon/Everyone_Is_Here/0.0.7/
[0.0.6]: https://thunderstore.io/package/download/Lvalon/Everyone_Is_Here/0.0.6/
[0.0.5]: https://thunderstore.io/package/download/Lvalon/Everyone_Is_Here/0.0.5/
[0.0.4]: https://thunderstore.io/package/download/Lvalon/Everyone_Is_Here/0.0.4/
[0.0.3]: https://thunderstore.io/package/download/Lvalon/Everyone_Is_Here/0.0.3/
[0.0.2]: https://thunderstore.io/package/download/Lvalon/Everyone_Is_Here/0.0.2/
[0.0.1]: https://thunderstore.io/package/download/Lvalon/Everyone_Is_Here/0.0.1/
