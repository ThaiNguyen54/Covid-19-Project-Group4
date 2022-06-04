namespace Covid_19_WebApp.Models
{
    public class connectionstring
    {
        private string AzureconnectionString = "Add your databse connection string here";
        

        public string GetConnectionString()
        {
            return this.AzureconnectionString;
        }
    }
}
