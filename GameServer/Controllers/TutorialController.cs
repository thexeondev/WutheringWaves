using GameServer.Controllers.Attributes;
using GameServer.Network;
using GameServer.Network.Messages;
using Protocol;

namespace GameServer.Controllers;
internal class TutorialController : Controller
{
    public TutorialController(PlayerSession session) : base(session)
    {
        // TutorialController.
    }

    [NetEvent(MessageId.TutorialInfoRequest)]
    public RpcResult OnTutorialInfoRequest()
    {
        int[] tutorials = [30001, 30002, 30003, 30004, 30005, 30006, 30007, 30011, 30012, 30008, 30009, 30010, 30013, 30014, 30015, 30016, 30017, 30018, 30019, 30020, 30021, 30022, 30023, 30024, 40001, 30025, 30026, 30027, 30028, 30029, 30030, 30031, 30032, 30033, 30034, 30035, 30036, 50001, 50002, 50003, 50004, 50005, 50006, 50007, 50008, 50009, 50010, 50011, 33001, 34017, 34018, 32001, 32002, 32003, 32004, 32005, 32006, 32007, 32008, 32009, 32010, 32011, 32012, 32013, 32014, 32015, 32016, 32017, 32018, 32019, 32020, 32021, 33002, 33003, 33004, 33005, 34001, 34002, 34003, 34004, 34005, 34006, 34007, 34008, 34009, 34010, 34011, 34012, 34013, 34014, 34015, 34016, 34019, 34020, 34021, 34022, 34023, 34024, 34025, 34027, 34028, 34029, 34030, 34031, 34032, 34033];
        TutorialInfoResponse rsp = new();
        foreach (int id in tutorials)
        {
            rsp.UnLockList.Add(new TutorialInfo
            {
                Id = id,
                GetAward = true,
                CreateTime = 1337
            });
        }

        return Response(MessageId.TutorialInfoResponse, rsp);
    }

    [NetEvent(MessageId.GetDetectionLabelInfoRequest)]
    public RpcResult OnGetDetectionLabelInfoRequest()
    {
        int[] guides = [0, 1, 2, 3, 14, 15, 16, 4, 21, 22, 7, 5, 18, 6, 61, 8, 9, 10, 11, 12, 13, 17, 19];
        int[] detectionTexts = [1, 2, 3, 4, 5, 6, 7, 0, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 21, 22, 61];

        GetDetectionLabelInfoResponse rsp = new() { UnlockLabelInfo = new() };
        rsp.UnlockLabelInfo.UnlockedGuideIds.AddRange(guides);
        rsp.UnlockLabelInfo.UnlockedDetectionTextIds.AddRange(detectionTexts);

        return Response(MessageId.GetDetectionLabelInfoResponse, rsp);
    }

    [NetEvent(MessageId.GuideInfoRequest)]
    public RpcResult OnGuideInfoRequest() => Response(MessageId.GuideInfoResponse, new GuideInfoResponse()
    {
        GuideGroupFinishList = { 60001, 60002, 60003, 60004, 60005, 60006, 60007, 60008, 60009, 60010, 60011, 60012, 60013, 60014, 60015, 60016, 60017, 60018, 60019, 60020, 60021, 60101, 60102, 60103, 62002, 62004, 62005, 62006, 62007, 62009, 62010, 62011, 62012, 62013, 62014, 62015, 62016, 62017, 62022, 62027, 62028, 62029, 62030, 62031, 62032, 62033, 62034, 62036, 65001, 67001, 67002, 67003, 67004, 67005, 67006, 67007, 67008, 67009, 67010, 67011, 67012, 67013, 67014, 67015, 67016, 67017, 67018, 67019, 67022, 62001, 62008, 62018, 62019, 62020, 62021, 62023, 62024, 62025, 62026, 62035, 65002, 65003, 65004, 65005 }
    });
}
