using Project2025.Data.Entity;

namespace Project2025.Components.Pages.PropertyOwnerComponents
{
    public class PropertyOwnerDesignServices
    {
        public List<PropertyOwner> GetPropertyOwners()
        {
            return new List<PropertyOwner>
                {
                    new PropertyOwner
                    {
                        ownerId = Guid.NewGuid(),
                        ownerName = "ownerName 1",
                        email = "email for PropertyOwner 1"
                    },
                    new PropertyOwner
                    {
                        ownerId = Guid.NewGuid(),
                        ownerName = "ownerName 2",
                        email = "email for PropertyOwner 2"
                    }
                };
        }
    }
}