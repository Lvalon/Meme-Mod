using LBoL.Base;
using LBoL.Core.Cards;
using LBoLEntitySideloader.CustomKeywords;

namespace lvalonmeme.Cards.Template
{
    public class lvalonmemecard : Card
    {
        public class oldcard : lvalonmemecard
        {
            protected override bool isold { get; set; } = true;
        }
        public class memecard : lvalonmemecard
        {
            protected override bool ismeme { get; set; } = true;
        }
        //lvalonmemecard can be used to give additional properties to all the cards.
        //For instance, this can be used to give every card a new custom parameter called Value4. 
        //Custom value for display purposes.
        protected virtual int BaseValue3 { get; set; } = 0;
        protected virtual int BaseUpgradedValue3 { get; set; } = 0;
        public int Value3
        {
            get
            {
                if (this.IsUpgraded)
                {
                    return BaseUpgradedValue3;
                }
                return BaseValue3;
            }
        }
        protected virtual int BaseValue4 { get; set; } = 0;
        protected virtual int BaseUpgradedValue4 { get; set; } = 0;
        public int Value4
        {
            get
            {
                if (this.IsUpgraded)
                {
                    return BaseUpgradedValue4;
                }
                return BaseValue4;
            }
        }
        protected virtual ManaGroup vMana2 { get; set; } = new ManaGroup();
        protected virtual ManaGroup vUpgradedMana2 { get; set; } = new ManaGroup();
        public ManaGroup Mana2
        {
            get
            {
                if (this.IsUpgraded)
                {
                    return vUpgradedMana2;
                }
                return vMana2;
            }
        }
        protected virtual bool ismeme { get; set; } = false;
        protected virtual bool isold { get; set; } = false;
        public override void Initialize()
        {
            base.Initialize();
            if (ismeme)
            {
                this.AddCustomKeyword(lvalonmemekeyword.Meme);
            }
            if (isold) {
                this.AddCustomKeyword(lvalonmemekeyword.Old);
            }
        }
    }
}