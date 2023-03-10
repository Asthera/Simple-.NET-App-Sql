using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;

namespace MyStore.Pages.Clients
{
    public class ClientModel : PageModel
    {
        public List<ClientInfo> listClients = new List<ClientInfo>();
        public static MySqlConnection connection;

        public string username = "";
        public string password = "";
        public string database = "";

        public void OnGet()
        {
            connection = new MySqlConnection();

            try
            {
                connection.ConnectionString = "server = localhost; User Id = " + username + "; " +
                    "Persist Security Info = True; database = " + database + "; Password = " + password;
                connection.Open();

                String sql = "SELECT * FROM clients";
                Console.WriteLine("Selected");
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ClientInfo clientInfo = new ClientInfo();
                            
                            clientInfo.id = "" + reader.GetInt32(0);
                            clientInfo.name = reader.GetString(1);
                            clientInfo.email = reader.GetString(2);
                            clientInfo.phone = reader.GetString(3);
                            clientInfo.address = reader.GetString(4);
                            clientInfo.created_at = reader.GetDateTime(5).ToString();
                            Console.WriteLine(clientInfo.name);
                            listClients.Add(clientInfo);
                        }
                    }

                }
                Console.WriteLine("Succesfully connected!  ");

            }

            catch (Exception e)
            {
                Console.WriteLine("Not Successful! due to " + e.ToString());
            }
        }

    }


    public class ClientInfo
    {
        public String id;
        public String name;
        public String email;
        public String phone;
        public String address;
        public String created_at;
    }

}
