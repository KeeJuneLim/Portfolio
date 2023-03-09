namespace Proj.Enum {
    class Idspace {
        public const string PC = "PC";
        public const string NPC = "NPC";
        public const string Skill = "Skill";
        public const string SkillObject = "SkillObject";
        public const string Map = "Map";
        public const string NPCGenerateInfo = "NPCGenerateInfo";
        public const string Faction = "Faction";
    }

    class Category {
        public const string Field = "Field";
        public const string Dungeon = "Dungeon";

        public const string Normal = "Normal";
        public const string Boss = "Boss";

    }



    class PropName {
        public const string ClassName = "ClassName";
        public const string ClassId = "ClassId";
        public const string NPCName = "NPCName";
        public const string Level = "Level";
        public const string MaxCount = "MaxCount";
        public const string GenerateInterval = "GenerateInterval";
        public const string Position = "Position";
        public const string PositionOffset= "PositionOffset";

        public const string BaseHP = "BaseHP";
        public const string BaseAttackPower = "BaseAttackPower";
        public const string BaseDefense = "BaseDefense";

        public const string HPPerLevel = "HPPerLevel";
        public const string AttackPowerPerLevel = "AttackPowerPerLevel";
        public const string DefensePerLevel = "DefensePerLevel";

        public const string SkillList = "SkillList";

        // Skill
        public const string SkillObjectName = "SkillObjectName";
        public const string DamageMultiplier = "DamageMultiplier";
        public const string BaseCooldown = "BaseCooldown";

        // SkillObject
        public const string Radius = "BaseRadiusCooldown";
        public const string Shape = "Shape";
        public const string UpdateInterval = "UpdateInterval";
        public const string LifeTime = "LifeTime";

    }

    class Faction {
        public const string Player = "Player";
        public const string Monster = "Monster";
        public const string Villager = "Villager";
        public const string VillageSoldier = "VillageSoldier";

    }

    class MessageType {
        public const string Heal = "Heal";
        public const string ReduceHp = "ReduceHp";
        public const string AddBuff = "AddBuff";
        public const string RemoveBuff = "RemoveBuff";
        public const string Attack = "Attack";
    }
}
