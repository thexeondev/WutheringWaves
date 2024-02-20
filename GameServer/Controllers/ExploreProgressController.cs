using GameServer.Controllers.Attributes;
using GameServer.Network;
using GameServer.Network.Messages;
using Protocol;

namespace GameServer.Controllers;
internal class ExploreProgressController : Controller
{
    private static readonly int[] s_areaIds = [1, 2, 3, 4, 5, 6, 7, 8, 10, 12, 100, 101, 102, 103, 107, 110, 113, 124, 122, 199, 301, 302, 303, 401, 402, 403, 404, 405, 406, 407, 408, 708, 601, 602, 603, 606, 607, 608, 609, 202, 203, 204, 501, 502, 503, 504, 508, 802, 803, 805, 807, 702, 703, 704, 705, 706, 709, 1201, 1202, 1203, 1204, 1001, 1002, 1003, 1004, 1005, 1006, 1007, 1301, 119, 120, 10001, 10002, 10003, 10004, 10005, 11001, 12001, 12002, 12003, 1500001, 1500002, 14001, 14002, 14003, 14004, 14005, 14006, 14007, 14008, 14011, 14012, 14013, 14021, 14022, 123, 125, 51, 804];
    private static readonly (int, int)[] s_exploreIds = [(1, 2), (2, 2), (3, 2), (11, 4), (12, 4), (13, 4), (21, 6), (22, 6), (23, 6), (24, 6), (31, 8), (32, 8), (33, 8), (34, 8), (41, 10), (42, 10), (43, 10), (44, 10), (51, 12), (52, 12), (53, 12), (54, 12), (6, 3), (7, 3), (8, 3), (9, 3), (14, 4), (15, 4), (16, 5), (17, 5), (18, 5), (19, 5), (25, 6), (26, 7), (27, 7), (28, 7), (29, 7), (45, 10), (55, 12), (4, 2), (5, 2), (20, 5), (30, 7), (35, 8)];
    
    public ExploreProgressController(PlayerSession session) : base(session)
    {
        // ExploreProgressController.
    }

    [NetEvent(MessageId.ExploreProgressRequest)]
    public RpcResult OnExploreProgressRequest()
    {
        return Response(MessageId.ExploreProgressResponse, new ExploreProgressResponse
        {
            AreaProgress = 
            {
                s_areaIds.Select(id => new AreaExploreInfo
                {
                    AreaId = id,
                    ExplorePercent = 100,
                    ExploreProgress =
                    {
                        s_exploreIds.Where(pair => pair.Item2 == id).Select(pair => new OneExploreItem
                        {
                            ExplorePercent = 100,
                            ExploreProgressId = pair.Item1
                        })
                    }
                })
            }
        });
    }
}
