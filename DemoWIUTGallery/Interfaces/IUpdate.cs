using System.Threading.Tasks;

namespace DemoWIUTGallery.Interfaces
{
    public interface IUpdate<T>
    {
        public Task<bool> Update(T _object);
    }
}
