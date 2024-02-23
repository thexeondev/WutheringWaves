namespace GameServer.Systems.Event;
internal enum GameEventType
{
    Login = 1,
    EnterGame,
    PushDataDone,

    // Actions
    FormationUpdated,
    VisionSkillChanged,

    // Debug
    DebugUnlockAllRoles,
    DebugUnlockAllItems
}
