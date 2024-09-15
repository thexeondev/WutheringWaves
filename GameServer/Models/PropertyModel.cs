using Core.Config;
using Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Models;
internal class PropertyModel
{

    public class MonsterPropertyGrowth
    { 
    public int Id { get; set; }
        public int Level { get; set; }
        public int LifeMaxRatio { get; set; }
        public int AtkRatio { get; set; }
        public int DefRatio { get; set; }
        public int HardnessMaxRatio { get; set; }
        public int HardnessRatio { get; set; }
        public int HardnessRecoverRatio { get; set; }
        public int RageMaxRatio { get; set; }
        public int RageRatio { get; set; }
        public int RageRecoverRatio { get; set; }  
    }
    public class RolePropertyGrowth
    {
        public int Id { get; set; }
        public int Level { get; set; }
        public int BreachLevel { get; set; }
        public int LifeMaxRatio { get; set; }
        public int AtkRatio { get; set; }
        public int DefRatio { get; set; }

    }
    public List<MonsterPropertyGrowth> MonsterPropertyGrowthList { get; } = [];
    public List<RolePropertyGrowth> RolePropertyGrowthList { get; } = [];




    public void AddMonsterPropertyGrowth(MonsterPropertyGrowthConfig growth)
    {
        MonsterPropertyGrowth MonsterPropertyGrowth = new()
        {
            Id = growth.Id,
            Level = growth.Level,
            LifeMaxRatio = growth.LifeMaxRatio,
            AtkRatio = growth.AtkRatio,
            DefRatio = growth.DefRatio,
            HardnessMaxRatio = growth.HardnessMaxRatio,
            HardnessRatio = growth.HardnessRatio,
            HardnessRecoverRatio = growth.HardnessRecoverRatio,
            RageMaxRatio = growth.RageMaxRatio,
            RageRatio = growth.RageRatio,
            RageRecoverRatio = growth.RageRecoverRatio
        };
        MonsterPropertyGrowthList.Add(MonsterPropertyGrowth);
    }
    public void ApplyMonsterPropertyGrowth(int lv, BasePropertyConfig baseProperty)
    {
        baseProperty.Lv = lv;
        float LifeMaxRatio = MonsterPropertyGrowthList[lv - 1].LifeMaxRatio / 10000;
        baseProperty.LifeMax = (int)(baseProperty.LifeMax* LifeMaxRatio);
        float AtkRatio = MonsterPropertyGrowthList[lv - 1].AtkRatio / 10000;
        baseProperty.Atk = (int)(baseProperty.Atk * AtkRatio);
        float DefRatio = MonsterPropertyGrowthList[lv - 1].DefRatio / 10000;
        baseProperty.Def = (int)(baseProperty.Def * DefRatio);
        float HardnessMaxRatio = MonsterPropertyGrowthList[lv - 1].HardnessMaxRatio / 10000;
        baseProperty.HardnessMax = (int)(baseProperty.HardnessMax * HardnessMaxRatio);
        float HardnessRatio = MonsterPropertyGrowthList[lv - 1].HardnessRatio / 10000;
        baseProperty.Hardness = (int)(baseProperty.Hardness * HardnessRatio);
        float HardnessRecoverRatio = MonsterPropertyGrowthList[lv - 1].HardnessRecoverRatio / 10000;
        baseProperty.HardnessRecover = (int)(baseProperty.HardnessRecover * HardnessRecoverRatio);
        float RageMaxRatio = MonsterPropertyGrowthList[lv - 1].RageMaxRatio / 10000;
        baseProperty.RageMax = (int)(baseProperty.RageMax * RageMaxRatio);
        float RageRatio = MonsterPropertyGrowthList[lv - 1].RageRatio / 10000;
        baseProperty.Rage = (int)(baseProperty.Rage * RageRatio);
        float RageRecoverRatio = MonsterPropertyGrowthList[lv - 1].RageRecoverRatio / 10000;
        baseProperty.RageRecover = (int)(baseProperty.RageRecover * RageRecoverRatio);

    }





}
