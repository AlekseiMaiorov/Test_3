using Cysharp.Threading.Tasks;

namespace Factories
{
    public interface IFactory<T> where T: class
    {
        UniTask<T> Create();
    }
}