using System;

namespace TFG_Web.Interface
{
    public interface ITFG_DatabaseSettings
    {
        string Collection { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
