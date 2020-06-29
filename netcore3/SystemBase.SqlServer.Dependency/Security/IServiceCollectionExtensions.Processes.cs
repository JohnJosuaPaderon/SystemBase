using Microsoft.Extensions.DependencyInjection;
using SystemBase.Security.Processes;

namespace SystemBase.Security
{
    partial class IServiceCollectionExtensions
    {
        private static IServiceCollection AddProcesses(this IServiceCollection instance) => instance
            .AddTransient<IDeleteApplication, DeleteApplication>()
            .AddTransient<IDeleteModule, DeleteModule>()
            .AddTransient<IDeletePermission, DeletePermission>()
            .AddTransient<IDeletePermissionScope, DeletePermissionScope>()
            .AddTransient<IDeletePlatform, DeletePlatform>()
            .AddTransient<IDeleteUser, DeleteUser>()
            .AddTransient<IGetApplication, GetApplication>()
            .AddTransient<IGetModule, GetModule>()
            .AddTransient<IGetPermission, GetPermission>()
            .AddTransient<IGetPermissionScope, GetPermissionScope>()
            .AddTransient<IGetPlatform, GetPlatform>()
            .AddTransient<IGetUser, GetUser>()
            .AddTransient<ISaveApplication, SaveApplication>()
            .AddTransient<ISaveApplicationPermission, SaveApplicationPermission>()
            .AddTransient<ISaveModule, SaveModule>()
            .AddTransient<ISaveModulePermission, SaveModulePermission>()
            .AddTransient<ISavePermission, SavePermission>()
            .AddTransient<ISavePermissionScope, SavePermissionScope>()
            .AddTransient<ISavePlatform, SavePlatform>()
            .AddTransient<ISaveUser, SaveUser>()
            .AddTransient<ISearchApplication, SearchApplication>()
            .AddTransient<ISearchModule, SearchModule>()
            .AddTransient<ISearchPermission, SearchPermission>()
            .AddTransient<ISearchPermissionScope, SearchPermissionScope>()
            .AddTransient<ISearchPlatform, SearchPlatform>()
            .AddTransient<ISearchUser, ISearchUser>();
    }
}
