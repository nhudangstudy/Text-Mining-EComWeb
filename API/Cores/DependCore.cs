//using AutoMapper.Extensions.ExpressionMapping;

//using GoogleApi.Drive;

//using Microsoft.Extensions.DependencyInjection;

//namespace API.Cores
//{
//    public static class DependCore
//    {
//        public static IServiceCollection AddDependencies(this IServiceCollection service)
//        {
//            return service
//                .AddSingleton<IHttpContextAccessor, HttpContextAccessor>()
//                .AddAutoMapper(options =>
//                {
//                    options.AddExpressionMapping();
//                }, AppDomain.CurrentDomain.GetAssemblies())
//                .AddScoped<IUnitOfWork, UnitOfWork>()
//                .AddSingleton<GoogleDriveApi>();
//            //.AddSignalR(options =>
//            //{
//            //    options.ClientTimeoutInterval = TimeSpan.FromSeconds(15);
//            //    options.MaximumReceiveMessageSize = 12;
//            //}).Services;
//        }
//    }
//}
