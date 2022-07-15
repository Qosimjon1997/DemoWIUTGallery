using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemoWIUTGallery.Interfaces
{
    public interface IReadRange<T>
    {
        public Task<IEnumerable<T>> GetAllAsync();
    }
}
