namespace Covid_19_WebApp.Models
{
    public class connectionstring
    {
        private string SqlServer = @"Data Source=DESKTOP-S89DF0S;Initial Catalog=Covid_19;Integrated Security=True";
        private string AzureconnectionString = "Server=tcp:webprojectcovid19.database.windows.net,1433;Initial Catalog=Covid_19_Dose;Persist Security Info=False;User ID=thainguyen;Password=Thai******:::542001;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        


        public string GetConnectionString()
        {
            //return this.AzureconnectionString;
            return this.SqlServer;
        }
    }
}
