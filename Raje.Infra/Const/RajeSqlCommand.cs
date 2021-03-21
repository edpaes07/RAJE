namespace Raje.Infra.Const
{
    public static class RajeSqlCommand
    {
        public const string SP_SearchUsers = "exec [dbo].[SP_SearchUsers] @Ids, @FranchiseeGroupIds, @StoreIds, @Cpf, @UserRoleId, @FlagActive, @ExcludeUserRoleId, @Offset, @Next";
        public const string SP_SearchUsersCount = "exec [dbo].[SP_SearchUsersCount] @Ids, @FranchiseeGroupIds, @StoreIds, @Cpf, @UserRoleId, @FlagActive, @ExcludeUserRoleId";

        public const string SP_SearchLog = "exec [dbo].[SP_SearchLog] @Id, @Author, @Api, @Code, @Method, @UrlQuery, @Request, @Response, @DateBegin, @DateEnd, @Input, @Offset, @Next";
        public const string SP_SearchLogCount = "exec [dbo].[SP_SearchLogCount] @Id, @Author, @Api, @Code, @Method, @UrlQuery, @Request, @Response, @DateBegin, @DateEnd, @Input";
        public const string SP_SearchLogHealthCheck = "exec [dbo].[SP_SearchLogHealthCheck] @DateWeekAgo";

    }
}
