using Protocol;

namespace GameServer.Systems.Entity.Component;
internal class EntityAttributeComponent : EntityComponentBase
{
    public override EntityComponentType Type => EntityComponentType.Attribute;

    private readonly Dictionary<EAttributeType, GameplayAttributeData> _gameplayAttributes;

    public EntityAttributeComponent()
    {
        _gameplayAttributes = [];
    }

    public void SetAll(IEnumerable<GameplayAttributeData> attributes)
    {
        foreach (GameplayAttributeData attr in attributes)
        {
            SetAttribute((EAttributeType)attr.AttributeType, attr.CurrentValue, attr.BaseValue);
        }
    }

    public void SetAttribute(EAttributeType type, int currentValue, int baseValue)
    {
        if (!_gameplayAttributes.TryGetValue(type, out GameplayAttributeData? attribute))
        {
            attribute = new GameplayAttributeData
            {
                AttributeType = (int)type
            };

            _gameplayAttributes.Add(type, attribute);
        }

        attribute.CurrentValue = currentValue;
        attribute.BaseValue = baseValue;
    }

    public void SetAttribute(EAttributeType type, int currentValue)
    {
        if (!_gameplayAttributes.TryGetValue(type, out GameplayAttributeData? attribute))
        {
            SetAttribute(type, currentValue, currentValue);
            return;
        }

        attribute.CurrentValue = currentValue;
        attribute.BaseValue = currentValue;
    }

    public int GetAttribute(EAttributeType type)
    {
        if (_gameplayAttributes.TryGetValue(type, out GameplayAttributeData? attribute))
            return attribute.CurrentValue;

        return 0;
    }

    public int GetAttributeBase(EAttributeType type)
    {
        if (_gameplayAttributes.TryGetValue(type, out GameplayAttributeData? attribute))
            return attribute.BaseValue;

        return 0;
    }

    public override EntityComponentPb Pb
    {
        get
        {
            EntityComponentPb pb = new()
            {
                AttributeComponent = new()
            };

            pb.AttributeComponent.GameAttributes.AddRange(_gameplayAttributes.Values);

            return pb;
        }
    }
}
