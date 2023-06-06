using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking;
using Windows.Networking.Connectivity;

namespace DbLibrary.DB
{
    public class MySQLConnDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {

            var hostNames = NetworkInformation.GetHostNames();
            String hostName = hostNames.FirstOrDefault(name => name.Type == HostNameType.DomainName)?.DisplayName ?? "???";

            if (hostName == "DESKTOP-F1G0I13")
            {
                optionBuilder.UseMySQL("Server=localhost;Database=medlink2;UID=root;Password=");
            }
            else
            {
                optionBuilder.UseMySQL("Server=169.254.30.133;Database=medlink;UID=root;Password=");
            }
        }
    }
}
