namespace FinanciarTeApi.Services
{
    public interface IServiceSecurity
    {
        string? GetUserName();
        bool CheckUserHasroles(string[] roles);
    }
}
