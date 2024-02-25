using Protocol;

namespace GameServer.Extensions.Logic;
internal static class MathExtensions
{
    public static float GetDistance(this Vector self, Vector other)
    {
        float x = self.X - other.X;
        float y = self.Y - other.Y;

        return (float)Math.Sqrt(x * x + y * y);
    }
}
