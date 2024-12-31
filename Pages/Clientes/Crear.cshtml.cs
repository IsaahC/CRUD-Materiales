using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace CRUD_Materiales.Pages.Clientes
{
  
    public class Crear : PageModel
    {
        [BindProperty, Required(ErrorMessage = "Se requiere el nombre")]
        public string Primernombre {get; set; } = "";
        [BindProperty, Required(ErrorMessage = "Se requiere un apellido")]
        public string Apellidos {get; set; } = "";
        [BindProperty, Required(ErrorMessage = "Se requiere un correo electrónico")]
        public string Email {get; set; } = "";
        [BindProperty, Required(ErrorMessage = "Se requiere un número de teléfono")]
        public string? Telefono {get; set; } = "";
        [BindProperty, Required(ErrorMessage = "Se requiere la dirección")]
        public string? Direccion {get; set; } = "";
        
        public string? Notas {get; set; } 
       

        public void OnGet()
        {
        }

        public string ErrorMessage { get; set; } = "";
         public void OnPost()
        {
            if(!ModelState.IsValid){
                return;
            }

            if(Telefono == null) Telefono = "";
            if(Direccion == null) Direccion = "";
            if(Notas == null) Notas = "";

            //crear nuevo cliente
            try
            {
            string connectionString = "Server=DESKTOP-LOAE6OF;Database=ArqCrud;Trusted_Connection=True;TrustServerCertificate=True;";
            using (SqlConnection connection = new SqlConnection(connectionString)){
                connection.Open();

                String sql = "INSERT INTO clientes " + 
                "(primernombre, apellidos, email, telefono, direccion, notas) VALUES" +
                "(@primernombre, @apellidos, @email, @telefono, @direccion, @notas)";
            using(SqlCommand command = new SqlCommand(sql, connection)){
                command.Parameters.AddWithValue("@primernombre", Primernombre);
                command.Parameters.AddWithValue("@apellidos", Apellidos);
                command.Parameters.AddWithValue("@email", Email);
                command.Parameters.AddWithValue("@telefono", Telefono);
                command.Parameters.AddWithValue("@direccion", Direccion);
                command.Parameters.AddWithValue("@notas", Notas);

                command.ExecuteNonQuery();
            }
            } 
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return;
            }

            Response.Redirect("/Clientes/Index");
        }
    }
}