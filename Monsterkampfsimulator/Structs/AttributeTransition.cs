
namespace Monsterkampfsimulator
{
    /// <summary>
    /// A AttributeTransition Struct will be used if we apply buffs to the monster visually
    /// An AttributeTransition holds following informations:
    ///   - On which Attribute will be the buff applied?
    ///   - Whats the new Attribute Value?
    ///   - Whats the Transition called?
    ///   - What should happen after Transition? (Place for actual value overrides)
    /// </summary>
    public struct AttributeTransition
    {
        public Attribute AttributeName { get; private set; }
        public float Value { get; private set; }
        public string Message { get; private set; }
        public Action OnTransitionEnd { get; private set; }

        public AttributeTransition(Attribute attribute, float value, string message, Action OnTransitionEnd)
        {
            AttributeName = attribute;
            Value = value;
            Message = message;
            this.OnTransitionEnd = OnTransitionEnd;
        }
    }
}

