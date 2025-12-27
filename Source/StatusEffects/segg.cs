using System.Linq;
using LBoL.Base;
using LBoL.ConfigData;
using LBoL.Core;
using LBoL.Core.Cards;
using LBoL.Core.StatusEffects;
using LBoL.Core.Units;
using LBoLEntitySideloader.Attributes;

namespace lvalonmeme.StatusEffects
{
    public sealed class seggDef : lvalonmemesetemplate
    {
        public override StatusEffectConfig MakeConfig()
        {
            StatusEffectConfig config = GetDefaultStatusEffectConfig();
            return config;
        }
    }

    [EntityLogic(typeof(seggDef))]
    public sealed class segg : StatusEffect
    {
        public ManaGroup Mana
        {
            get
            {
                return ManaGroup.Colorlesses(1);
            }
        }
        public override bool ForceNotShowDownText
        {
            get
            {
                return true;
            }
        }
        protected override void OnAdded(Unit unit)
        {
            foreach (Card card in Battle.EnumerateAllCards().Where(card => !card.IsPurified && card.Cost.HasTrivialOrHybrid))
            {
                card.IsPurified = true;
            }
            NotifyActivating();
            HandleOwnerEvent(Battle.CardsAddedToDiscard, new GameEventHandler<CardsEventArgs>(OnAddCard));
            HandleOwnerEvent(Battle.CardsAddedToHand, new GameEventHandler<CardsEventArgs>(OnAddCard));
            HandleOwnerEvent(Battle.CardsAddedToExile, new GameEventHandler<CardsEventArgs>(OnAddCard));
            HandleOwnerEvent(Battle.CardsAddedToDrawZone, new GameEventHandler<CardsAddingToDrawZoneEventArgs>(OnAddCardToDraw));
            HandleOwnerEvent(Battle.CardTransformed, new GameEventHandler<CardTransformEventArgs>(OnCardTransformed));
            HandleOwnerEvent(Battle.ManaGaining, new GameEventHandler<ManaEventArgs>(OnManaGaining));
        }

        private void OnManaGaining(ManaEventArgs args)
        {
            if (args.Value.WithColorless(0) != ManaGroup.Empty)
            {
                NotifyActivating();
                args.Value = ManaGroup.Colorlesses(args.Value.Amount);
                args.AddModifier(this);
            }
        }
        private void OnAddCard(CardsEventArgs args)
        {
            Card[] cards = args.Cards.Where(card => !card.IsPurified && card.Cost.HasTrivialOrHybrid).ToArray();
            for (int i = 0; i < cards.Length; i++)
            {
                cards[i].IsPurified = true;
            }
        }
        private void OnAddCardToDraw(CardsAddingToDrawZoneEventArgs args)
        {
            Card[] cards = args.Cards.Where(card => !card.IsPurified && card.Cost.HasTrivialOrHybrid).ToArray();
            for (int i = 0; i < cards.Length; i++)
            {
                cards[i].IsPurified = true;
            }
        }
        private void OnCardTransformed(CardTransformEventArgs args)
        {
            if (args.DestinationCard.IsPurified && args.DestinationCard.Cost.HasTrivialOrHybrid)
            {
                args.DestinationCard.IsPurified = true;
            }
        }
    }
}