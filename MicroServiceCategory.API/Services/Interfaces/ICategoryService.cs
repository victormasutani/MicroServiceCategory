using MicroServiceCategory.API.Models;
using System.Collections.Generic;

namespace MicroServiceCategory.API.Services.Interfaces
{
    public interface ICategoryService
    {
        IList<Category> List(string filter = null);
        Category Get(long id);
        long Post(string name);
        void Put(long id, string name);
        void Delete(long id);
    }
}
