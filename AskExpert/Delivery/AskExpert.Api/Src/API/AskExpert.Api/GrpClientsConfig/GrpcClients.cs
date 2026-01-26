namespace _4EPlatform_AskExpert.GrpClientsConfig
{

    public static class GrpcClients
    {
        #region UserManagement
        public static void AddUserManagementGrpcClients(
            this IServiceCollection services,
            string UserManagmentUrl)
        {
            //services.AddGrpcServiceClient<PermissionService.PermissionServiceClient>(UserManagmentUrl);
            services.AddGrpcServiceClient<PermissionGroupingService.PermissionGroupingServiceClient>(UserManagmentUrl);
        }
        #endregion

    }
}
