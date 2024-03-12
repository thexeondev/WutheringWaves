namespace GameServer.Controllers.Gacha;
internal class GachaPool
{
    public int Id { get; }
    public string Name { get; }
    public int SoftPityStart { get; }
    public int HardPityFour { get; }
    public int HardPityFive { get; }
    public float[] Rates { get; }
    public readonly List<List<int>> Items;
    private int _pullCount;
    private int _pityFour;
    private readonly Random _random = new(Environment.TickCount);

    public GachaPool(int id, string name, int softPityStart, int hardPityFour, int hardPityFive, float[] rates)
    {
        Id = id;
        Name = name;
        SoftPityStart = softPityStart;
        HardPityFour = hardPityFour;
        HardPityFive = hardPityFive;
        Rates = rates;
        Items = [
            [], // 3*
            [], // 4*
            [], // 5*
        ];
    }
    public void AddItemsThree(int[] items)
    {
        Items[0].AddRange(items);
    }

    public void AddItemsFour(int[] items)
    {
        Items[1].AddRange(items);
    }

    public void AddItemsFive(int[] items)
    {
        Items[2].AddRange(items);
    }

    public (int, int) DoPull()
    {
        float[] prob = (float[])Rates.Clone();
        if (_pullCount >= SoftPityStart)
        {
            prob[0] = 100 - (0.8f + 8 * (_pullCount - SoftPityStart - 1));
            prob[2] = 0.8f + 8 * (_pullCount - SoftPityStart - 1);
        }
        if (_pityFour + 1 >= HardPityFour)
        {
            prob = [0, 100, 0];
        }
        if (_pullCount + 1 >= HardPityFive)
        {
            prob = [0, 0, 100];
        }

        int roll = _random.Next(1000) / 10;
        int rarityResult = 3;
        int itemResult = 1;

        for (int i = 0; i < prob.Length; i++)
        {
            if (prob[i] > roll)
            {
                rarityResult = i + 3;
                break;
            }
        }
        itemResult = rarityResult switch
        {
            3 => Items[0][_random.Next(Items[0].Count)],
            4 => Items[1][_random.Next(Items[1].Count)],
            5 => Items[2][_random.Next(Items[2].Count)],
            _ => Items[0][_random.Next(Items[0].Count)],
        };
        _pityFour = rarityResult == 4 || _pityFour == 10 ? 0 : _pityFour + 1;
        _pullCount = rarityResult == 5 || _pullCount == 80 ? 0 : _pullCount + 1;
        return (itemResult, rarityResult);
    }
}
