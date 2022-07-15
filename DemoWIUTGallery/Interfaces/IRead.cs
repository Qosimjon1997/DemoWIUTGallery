using System;
using System.Threading.Tasks;

namespace DemoWIUTGallery.Interfaces
{
    public interface IRead<T>
    {
        public Task<T> GetByIdAsync(Guid? id);
    }
}
