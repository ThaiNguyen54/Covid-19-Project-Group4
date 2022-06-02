namespace Covid_19_WebApp.Models
{
    public class connectionstring
    {
        private string AzureconnectionString = "Server=tcp:webprojectcovid19.database.windows.net,1433;Initial Catalog=Covid-19 Project;Persist Security Info=False;User ID=thainguyen;Password=Thai******:::542001;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        

        public string GetConnectionString()
        {
            return this.AzureconnectionString;
        }
    }
}
