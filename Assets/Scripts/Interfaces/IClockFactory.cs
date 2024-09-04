namespace Factories
{
    public interface IClockFactory<T> : IFactory<T> where T : class
    {
    }
}