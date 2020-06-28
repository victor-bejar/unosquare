using App.Model.Interface;

namespace App.Api.Contract.V1.Interface
{

    public interface IEntityService<TIEntity, TCreateRequest, TUpdateRequest>
        where TIEntity : class
        where TCreateRequest : class
        where TUpdateRequest : class
    {
        TIEntity Create(TCreateRequest createRequest);
        TIEntity Remove(int id);
        TIEntity Get(int id);
        IItemsList<TIEntity> GetList(string filter, int? pageIndex, int? pageSize);
        TIEntity Update(TUpdateRequest UpdateRequest, int id);
    }

}