using System.Collections.Generic;
using LBoL.Base;
using LBoL.ConfigData;
using LBoL.Core;
using LBoL.Core.Battle;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.Cards;
using LBoL.Core.StatusEffects;
using LBoL.Core.Units;
using LBoLEntitySideloader.Attributes;

namespace lvalonmeme.StatusEffects
{
    public sealed class semanaunfreezeDef : lvalonmemesetemplate
    {
        public override StatusEffectConfig MakeConfig()
        {
            StatusEffectConfig config = GetDefaultStatusEffectConfig();
            return config;
        }
    }

    [EntityLogic(typeof(semanaunfreezeDef))]
    public sealed class semanaunfreeze : StatusEffect
    {
        public ManaGroup Mana
        {
            get
            {
                return ManaGroup.Anys(1);
            }
        }
        protected override void OnAdded(Unit unit)
        {
            foreach (Card card in unit.Battle.EnumerateAllCards())
            {
                card.AuraCost -= Mana;
            }
            HandleOwnerEvent(Battle.CardsAddedToDiscard, new GameEventHandler<CardsEventArgs>(OnAddCard));
            HandleOwnerEvent(Battle.CardsAddedToHand, new GameEventHandler<CardsEventArgs>(OnAddCard));
            HandleOwnerEvent(Battle.CardsAddedToExile, new GameEventHandler<CardsEventArgs>(OnAddCard));
            HandleOwnerEvent(Battle.CardsAddedToDrawZone, new GameEventHandler<CardsAddingToDrawZoneEventArgs>(OnAddCardToDraw));
            HandleOwnerEvent(Battle.CardTransformed, new GameEventHandler<CardTransformEventArgs>(OnCardTransformed));
            ReactOwnerEvent(Battle.CardUsed, new EventSequencedReactor<CardUsingEventArgs>(OnCardUsed));
        }
        private void OnAddCard(CardsEventArgs args)
        {
            Card[] cards = args.Cards;
            for (int i = 0; i < cards.Length; i++)
            {
                cards[i].AuraCost -= Mana;
            }
            NotifyActivating();
        }
        private void OnAddCardToDraw(CardsAddingToDrawZoneEventArgs args)
        {
            Card[] cards = args.Cards;
            for (int i = 0; i < cards.Length; i++)
            {
                cards[i].AuraCost -= Mana;
            }
            NotifyActivating();
        }
        private void OnCardTransformed(CardTransformEventArgs args)
        {
            args.DestinationCard.AuraCost -= Mana;
            NotifyActivating();
        }
        private IEnumerable<BattleAction> OnCardUsed(CardUsingEventArgs args)
        {
            Level--;
            if (Level <= 0)
            {
                yield return new RemoveStatusEffectAction(this, true, 0.1f);
            }
            yield break;
        }

        protected override void OnRemoved(Unit unit)
        {
            foreach (Card card in Battle.EnumerateAllCards())
            {
                card.AuraCost += Mana;
            }
        }
    }
}