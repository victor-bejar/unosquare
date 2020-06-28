namespace App.Api.Contract.V1.Interface
{

    public interface IEntityMapper<TIEntity, TCreateReq, TUpdateReq, TResp>
    {
        TIEntity CreateRequestToModel(TCreateReq entityRequest);
        TIEntity UpdateRequestToModel(TUpdateReq entityRequest, int entityId);
        TResp ModelToResponse(TIEntity entityModel);
    }

}