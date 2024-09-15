using Core.Config.Attributes;
using System.Collections.Generic;

namespace Core.Config
{

    public class WorldLevelBonusType
    {
        public int Type { get; set; }
    }
    [ConfigCollection("level_entity/levelentityconfig.json")]
    public class LevelEntityConfig : IConfig
    {
        public ConfigType Type => ConfigType.LevelEntity;
        public int Identifier => Id;

        public int Id { get; set; }
        public int MapId { get; set; }
        public int EntityId { get; set; }
        public string BlueprintType { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public bool InSleep { get; set; }
        public bool IsHidden { get; set; }
        public int AreaId { get; set; }
        public Transform[] Transform { get; set; } = [];
        //public ComponentsData ComponentsData { get; set; } = new ComponentsData();
    }
    public class Transform
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
    }

    public class ComponentsData
    {
        public AiComponent AiComponent { get; set; } = new AiComponent();
        public AttributeComponent AttributeComponent { get; set; } = new AttributeComponent();

    }

    public class AiComponent
    {
        public int AiTeamLevelId { get; set; }
    }



    public class AttributeComponent
    {
        public int PropertyId { get; set; }
        public int Level { get; set; }

    }




}
