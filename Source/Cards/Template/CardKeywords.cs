using LBoLEntitySideloader.CustomKeywords;
using lvalonmeme.StatusEffects;

namespace lvalonmeme.Cards.Template
{
    public static class lvalonmemekeyword
    {
        public static CardKeyword Meme = new CardKeyword(nameof(sememe)) { descPos = KwDescPos.First };
        public static CardKeyword Old = new CardKeyword(nameof(seold)) { descPos = KwDescPos.First };
    }
}
