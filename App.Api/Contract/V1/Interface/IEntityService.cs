using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Api.Contract.V1.Interface
{

    public interface IEntityService<TIEntity, TCreateRequest, TUpdateRequest>
    {
        TIEntity Create(TCreateRequest createRequest);
        TIEntity Remove(int id);
        TIEntity Get(int id);
        IEnumerable<TIEntity> GetList(string filter, int? pageIndex, int? pageSize);
        TIEntity Update(TUpdateRequest UpdateRequest, int id);
    }

}