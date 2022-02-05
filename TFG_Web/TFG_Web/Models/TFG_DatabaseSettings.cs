using TFG_Web.Interface;

namespace TFG_Web.Models
{
    public class TFG_DatabaseSettings : ITFG_DatabaseSettings
    {
        public string Collection { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
