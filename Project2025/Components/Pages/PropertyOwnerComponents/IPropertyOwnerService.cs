using Project2025.Data.Entity;

namespace Project2025.Components.Pages.PropertyOwnerComponents
{
    public interface IPropertyOwnerService
    {
        Task DeleteAsync(PropertyOwner propertyOwner);
        Task<PropertyOwner?> GetPropertyOwnerByownerId(Guid ownerId);
        Task<List<PropertyOwner>> GetPropertyOwners();
        Task<PropertyOwner> Upsert(PropertyOwner propertyOwner);
    }
}