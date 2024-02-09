using GameServer.Handlers.Attributes;
using GameServer.Network;
using Protocol;

namespace GameServer.Handlers;
internal class WorldMessageHandler : MessageHandlerBase
{
    public WorldMessageHandler(PlayerSession session) : base(session)
    {
        // WorldMessageHandler.
    }

    [MessageHandler(MessageId.EntityActiveRequest)]
    public async Task OnEntityActiveRequest(ReadOnlyMemory<byte> _)
    {
        await Session.Rpc.ReturnAsync(MessageId.EntityActiveResponse, new EntityActiveResponse());
    }

    [MessageHandler(MessageId.EntityOnLandedRequest)]
    public async Task OnEntityOnLandedRequest(ReadOnlyMemory<byte> _)
    {
        await Session.Rpc.ReturnAsync(MessageId.EntityOnLandedResponse, new EntityOnLandedResponse());
    }

    [MessageHandler(MessageId.PlayerMotionRequest)]
    public async Task OnPlayerMotionRequest(ReadOnlyMemory<byte> _)
    {
        await Session.Rpc.ReturnAsync(MessageId.PlayerMotionResponse, new PlayerMotionResponse());
    }

    [MessageHandler(MessageId.EntityLoadCompleteRequest)]
    public async Task OnEntityLoadCompleteRequest(ReadOnlyMemory<byte> _)
    {
        await Session.Rpc.ReturnAsync(MessageId.EntityLoadCompleteResponse, new EntityLoadCompleteResponse());
    }

    [MessageHandler(MessageId.SceneLoadingFinishRequest)]
    public async Task OnSceneLoadingFinishRequest(ReadOnlyMemory<byte> _)
    {
        await Session.Rpc.ReturnAsync(MessageId.SceneLoadingFinishResponse, new SceneLoadingFinishResponse());
    }

    [MessageHandler(MessageId.UpdateSceneDateRequest)]
    public async Task OnUpdateSceneDateRequest(ReadOnlyMemory<byte> _)
    {
        await Session.Rpc.ReturnAsync(MessageId.UpdateSceneDateResponse, new UpdateSceneDateResponse());
    }
}
