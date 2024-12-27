using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace CRUD_Materiales.Pages.Clientes
{
    public class Index : PageModel
    {
       public List<ClienteInfo> ClienteList {get; set;} = [];
        public void OnGet()
        {
            try{
                string connectionString = "Server=localhost\\SQLEXPRESS;Database=ArqCrud;Trusted_Connection=True;TrustServerCertificate=True;";
                using (SqlConnection connection = new SqlConnection(connectionString)){
                connection.Open();
                string sql ="SELECT * FROM clientes";
                using(SqlCommand command = new SqlCommand(sql, connection)){
                    using (SqlDataReader reader = command.ExecuteReader()){
                        while (reader.Read()){
                            ClienteInfo clienteInfo = new ClienteInfo();
                            
                            clienteInfo.Id_cliente = reader.GetInt32(0);
                            clienteInfo.Primernombre = reader.GetString(1);
                            clienteInfo.Apellidos = reader.GetString(2);
                            clienteInfo.Email = reader.GetString(3);
                            clienteInfo.Telefono = reader.GetString(4);
                            clienteInfo.Direccion = reader.GetString(5);
                            clienteInfo.Notas = reader.GetString(6);
                            clienteInfo.Createdat = reader.GetDateTime(7).ToString("MM/dd/yyyy");
                            ClienteList.Add(clienteInfo);
                        }
                    }
                }    
            }
            } catch (Exception ex){
                Console.WriteLine("Hay un error" + ex.Message);
            }
        }
    }

    public class ClienteInfo{
        public int Id_cliente { get; set; }
        public string Primernombre {get; set; } = "";
        public string Apellidos {get; set; } = "";
        public string Email {get; set; } = "";
        public string Telefono {get; set; } = "";
        public string Direccion {get; set; } = "";
        public string Notas {get; set; } = "";
        public string Createdat {get; set; } = "";

    }
}