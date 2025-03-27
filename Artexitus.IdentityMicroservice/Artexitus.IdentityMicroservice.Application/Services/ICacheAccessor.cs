namespace Artexitus.IdentityMicroservice.Application.Services
{
    public interface ICacheAccessor
    {
        public TEntity? Get<TEntity>(string key);
        public void Set(string key, object payload, TimeSpan lifetime);
        public void Remove(string key);
    }
}
