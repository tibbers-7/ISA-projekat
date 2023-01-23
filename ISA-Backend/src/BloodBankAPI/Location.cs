using System.Text.RegularExpressions;

namespace BloodBankAPI
{
    public record Location
    {
        public int Id { get; init; }
        public float Latitude { get; init; }
        public float Longitude { get; init; }

        public Location(string json)
        {
            //"{\"id\":1,\"latitude\":45.220905,\"longitude\":45.220905}"
            var str = json.Replace("\\", string.Empty);
            Regex r = new Regex("{\"id\":(\\d+),\"latitude\":(\\d+\\.?\\d+),\"longitude\":(\\d+\\.?\\d+)");
            Match m = r.Match(str);
            if (m.Success)
            {
                GroupCollection values=m.Groups;
                Id= int.Parse(values[1].Value);
                Latitude= float.Parse(values[2].Value);
                Longitude= float.Parse(values[3].Value);
                
            }
        }
        //private static readonly string regexString = "(\\d+),{(\\d+,\\d+,\\d+,\\d+,\\d+,\\d+,\\d+)}";
        //                                              INT id,INT patientId,INT room,DATEONLY date,TIMEONLY time,INT duration,INT doctorId,INT scheduledDoctorId,CHAR type,CHAR emergency,CHAR status

        /* public void FromCSV(GroupCollection csvValues)
         {
             patientId = int.Parse(csvValues[1].Value);
             String[] doctorString = csvValues[2].Value.Split(',');
             foreach (string doctor in doctorString)
             {
                 chosenDoctorIds.Add(int.Parse(doctor));
             }

         }*/
    }
}