namespace Covid_19_WebApp.Models
{
    public class connectionstring
    {
        private string SqlServer = @""


        public string GetConnectionString()
        {

            return this.SqlServer;
        }
    }
}
