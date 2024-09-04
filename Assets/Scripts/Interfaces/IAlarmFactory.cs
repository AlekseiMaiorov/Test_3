namespace Factories
{
    public interface IAlarmFactory<T> : IFactory<T> where T : class
    {
    }
}