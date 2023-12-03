namespace Uqs.Weather.Wrappers
{
    public class RandomWrapper : IRandomWrapper
    {
        private readonly Random _random = Random.Shared;
        public int Next(int min, int max) => _random.Next(min, max);
    }
}
