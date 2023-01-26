using Model;

namespace BloodBankAPI
{
    public class StoreLocation
    {

        public Location storedLoc { get; set; }
        public bool isNew { get; set; }

        private static StoreLocation instance = null;
        private static readonly object padlock = new object();

        public StoreLocation()
        {
        }



        public static StoreLocation Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new StoreLocation();
                    }
                    return instance;
                }
            }
        }

        public void Store(Location loc)
        {
            storedLoc = loc;
            isNew = true;
        }
    }
}
