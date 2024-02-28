using Core.Config;
using Protocol;

namespace GameServer.Models;
//remember
//{QuestStatus Locked = 0, CanAccept = 1, Accepted 2, Completed = 3},
//{ItemStatus ItemLocked = 0, ItemCanUnLock = 1, ItemUnLocked = 2},
//{ItemType Word = 0, Story = 1, Goods = 2}
internal class FavorModel
{
    public List<RoleFavor> FavorList { get; } = [];
    public List<FavorItem> FavorWords { get; } = [];
    public List<FavorItem> FavorStory { get; } = [];
    public List<FavorItem> FavorGoods { get; } = [];



    public FavorItem AddFavor(int id, int choose)
    {
        FavorItem item = new()
        {
            Id = id,
            Status = 2,
        };
        switch (choose)
        {
            case 0:
                FavorWords.Add(item); //word
                break;
            case 1:
                FavorStory.Add(item); //story
                break;
            case 2:
                FavorGoods.Add(item); //goods
                break;
            default:
                break;
        }
        FavorWords.Add(item);
        return item;
    }
    public void cleanFavor()
    {
        FavorWords.Clear();
        FavorStory.Clear();
        FavorGoods.Clear();
    }


    public RoleFavor AddFavorRole(int roleid, List<FavorItem> goods, List<FavorItem> story, List<FavorItem> words) //FavorQuest not used in roleFavor
    {
        RoleFavor favor = new()
        {
            Exp = 0,
            Level = 6, //idk this max?
            RoleId = roleid,
            GoodsIds = { words },
            StoryIds = { story },
            WordIds = { goods }
        };
        FavorList.Add(favor);
        return favor;
    }
}
