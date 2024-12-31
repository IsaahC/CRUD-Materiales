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
        public List<ClienteInfo> ClienteList { get; set; } = new List<ClienteInfo>();

        public void OnGet()
        {
            try
            {
                string connectionString = "Server=localhost;Database=ArqCrud;Trusted_Connection=True;TrustServerCertificate=True;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT id_usuaio, primernombre, apellidos, email, telefono, direccion, notas,  createdat FROM clientes";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                if (reader.FieldCount >= 8)
                                {
                                    ClienteInfo clienteInfo = new ClienteInfo();
                                    clienteInfo.Id_cliente = reader.GetInt32(0);
                                    clienteInfo.Primernombre = reader.IsDBNull(1) ? string.Empty : reader.GetString(1); // Verifica si es NULL
                                    clienteInfo.Apellidos = reader.IsDBNull(2) ? string.Empty : reader.GetString(2); // Verifica si es NULL
                                    clienteInfo.Email = reader.IsDBNull(3) ? string.Empty : reader.GetString(3); // Verifica si es NULL
                                    clienteInfo.Telefono = reader.IsDBNull(4) ? string.Empty : reader.GetString(4); // Verifica si es NULL
                                    clienteInfo.Direccion = reader.IsDBNull(5) ? string.Empty : reader.GetString(5); // Verifica si es NULL
                                    clienteInfo.Notas = reader.IsDBNull(6) ? string.Empty : reader.GetString(6); // Verifica si es NULL                                    
                                    clienteInfo.Createdat = reader.IsDBNull(7) ? string.Empty : reader.GetDateTime(7).ToString("MM/dd/yyyy"); // Verifica si es NULL
                                    ClienteList.Add(clienteInfo);
                                }
                                else
                                {
                                    Console.WriteLine("Número de columnas no esperado.");
                                }
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hay un error" + ex.Message);
            }
        }
    }

    public class ClienteInfo
    {
        public int Id_cliente { get; set; }
        public string Primernombre { get; set; } = "";
        public string Apellidos { get; set; } = "";
        public string Email { get; set; } = "";
        public string Telefono { get; set; } = "";
        public string Direccion { get; set; } = "";
        public string Notas { get; set; } = "";
        public string Createdat { get; set; } = "";

    }
}