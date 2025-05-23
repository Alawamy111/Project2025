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
        }//جيب جميع مالك   //

        public PropertyOwner GetPropertyOwnerByownerId(Guid ownerId)//جيب مالك باستحدام ID //
        {

            return new PropertyOwner

            {
                ownerId = ownerId,
                ownerName = "ownerName 1",
                email = "email for PropertyOwner 1"
            };
        }

        public PropertyOwner Save(PropertyOwner propertyOwner)
        {
            return propertyOwner;

        }// احفظ مالك//

        public void Delete(PropertyOwner propertyOwner)  //// احذف مالك//

        {

        }

        public PropertyOwner Update(PropertyOwner propertyOwner)
        {
            return propertyOwner;
        }//// تحديث مالك//
    }
}


