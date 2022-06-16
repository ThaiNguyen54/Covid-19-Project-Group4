using System;
using System.Runtime.Serialization;

namespace Covid_19_WebApp.Models
{
	[DataContract]
	public class DataPointForVaccine
    {
		public DataPointForVaccine(string label, int y)
		{
			this.Label = label;
			this.Y = y;
		}

		//Explicitly setting the name to be used while serializing to JSON.
		[DataMember(Name = "label")]
		public string Label = "";

		//Explicitly setting the name to be used while serializing to JSON.
		[DataMember(Name = "y")]
		public Nullable<int> Y = null;
	}
}
