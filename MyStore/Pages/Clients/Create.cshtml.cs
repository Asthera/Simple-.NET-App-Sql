using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;

namespace MyStore.Pages.Clients
{

    public class CreateModel : PageModel
    {
        public string user = "root";
        public string password = "lovemother79";

        public ClientInfo clientInfo = new ClientInfo();

        public String errorMessage = "";
        public String succesMessage = "";

        public void OnGet()
        {

        }

        public void OnPost()
        {
            clientInfo.name = Request.Form["name"];
            clientInfo.email = Request.Form["email"];
            clientInfo.address = Request.Form["address"];
            clientInfo.phone = Request.Form["phone"];

            if(clientInfo.name.Length == 0 || clientInfo.email.Length == 0 || 
               clientInfo.address.Length == 0 || clientInfo.phone.Length == 0 )
            {
                errorMessage = "All fields are requared";
                return;
            }

            // save to database


            try
            {
                String connectionString = "server = localhost; User Id = " + user + "; " +
                    "Persist Security Info = True; database = PAK; Password = " + password;
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO clients " +
                        "(name, email, phone, address) VALUES " +
                        "(@name, @email, @phone, @address);";


      
                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name",clientInfo.name);
                        command.Parameters.AddWithValue("@phone", clientInfo.phone);
                        command.Parameters.AddWithValue("@address", clientInfo.address);
                        command.Parameters.AddWithValue("@email", clientInfo.email);

                        command.ExecuteNonQuery();
                    }
                 
                }
            }

            catch (Exception e)
            {
                errorMessage = e.Message;
                return;
            }


            clientInfo.name = "";
            clientInfo.email = "";
            clientInfo.address = "";
            clientInfo.phone = "";

            succesMessage = "New client added succesful";

            Response.Redirect("/Clients/Client");
        }

    }
}
