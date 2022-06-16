using System;
using System.Runtime.Serialization;

namespace Covid_19_WebApp.Models
{
    [DataContract]
    public class DataPoint
    {
        public DataPoint(int month, int numofCitizen)
        {
            this.Month = month;
            this.NumOfCitizen = numofCitizen;
        }

        //Explicitly setting the Month to be used while serializing to JSON.
        [DataMember(Name = "label")]
        public int Month;

        //Explicitly setting the name to be used while serializing to JSON.
        [DataMember(Name = "y")]
        public Nullable<int> NumOfCitizen = null;


    }
}
