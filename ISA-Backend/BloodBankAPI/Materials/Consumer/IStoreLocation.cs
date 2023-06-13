using BloodBankAPI.Model;

namespace BloodBankAPI.Materials.Consumer
{
    public interface IStoreLocation
    {
        void Store(Location loc);
        Location Retrieve();
        bool IsEmpty();
    }
}
