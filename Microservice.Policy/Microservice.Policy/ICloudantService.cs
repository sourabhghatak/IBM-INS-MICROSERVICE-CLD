namespace Microservice.Policy
{
    public interface ICloudantService<T> where T:class
    {
        Task<T?> CreateAsync(T item);
        Task DeleteAsync(T item);
        Task<CloudantResponse?> GetAllAsync();
        Task<T?> UpdateAsync(T item);
        Task<T?> GetAsync(string id);
    }
}
