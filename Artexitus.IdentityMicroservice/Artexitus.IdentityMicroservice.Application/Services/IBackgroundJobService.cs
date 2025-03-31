namespace Artexitus.IdentityMicroservice.Application.Services
{
    public interface IBackgroundJobService
    {
        void ClearNonActivatedAccounts();
        void DeactivateStaleAccounts();
    }
}
