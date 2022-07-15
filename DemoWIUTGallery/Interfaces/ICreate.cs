using System.Threading.Tasks;

namespace DemoWIUTGallery.Interfaces
{
    public interface ICreate<T>
    {
        public Task<bool> CreateAsync(T _object);
    }
}
