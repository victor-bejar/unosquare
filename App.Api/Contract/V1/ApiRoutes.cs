namespace App.Api.Contract.V1
{

    public static class ApiRoutes
    {

        public const string Root = "api";
        public const string Version = "v1";
        public const string Base = ApiRoutes.Root + "/" + ApiRoutes.Version;

        public static class Product
        {
            public const string Create = ApiRoutes.Base + "/product/";
            public const string GetList = ApiRoutes.Base + "/product/";
            public const string GetById = ApiRoutes.Base + "/product/{id}";
            public const string Update = ApiRoutes.Base + "/product/{id}";
            public const string Delete = ApiRoutes.Base + "/product/{id}";
        }

    }

}
