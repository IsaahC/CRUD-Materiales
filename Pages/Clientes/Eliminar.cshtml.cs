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
    public class Eliminar : PageModel
    {
        

        public void OnGet()
        {
        }

        public void OnPost(int id){
            eliminarCliente(id);
            Response.Redirect("/Clientes/Index");
        }

        private void eliminarCliente(int id){
            try
            {
                string connectionString = "Server=localhost\\SQLEXPRESS;Database=ArqCrud;Trusted_Connection=True;TrustServerCertificate=True;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    //Eliminar clientes de la base
                    string sql = "DELETE FROM clientes WHERE id_usuaio=@id_usuaio";
                    using(SqlCommand command = new SqlCommand(sql, connection)){
                        command.Parameters.AddWithValue("@id_usuaio", id);
                        command.ExecuteNonQuery();
                    }
                }
            } catch(Exception ex){
                Console.WriteLine("No se puede eliminar al usuario:" + ex.Message);
            }
        }
    }
}