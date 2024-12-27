using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace CRUD_Materiales.Pages.Clientes
{
    public class EditarModel : PageModel
    {
        [BindProperty]
        public int Id { get; set; }

        [BindProperty, Required(ErrorMessage = "Se requiere el nombre")]
        public string Primernombre { get; set; } = "";

        [BindProperty, Required(ErrorMessage = "Se requiere un apellido")]
        public string Apellidos { get; set; } = "";

        [BindProperty, Required(ErrorMessage = "Se requiere un correo electrónico")]
        public string Email { get; set; } = "";

        [BindProperty, Required(ErrorMessage = "Se requiere un número de teléfono")]
        public string? Telefono { get; set; } = "";

        [BindProperty, Required(ErrorMessage = "Se requiere la dirección")]
        public string? Direccion { get; set; } = "";

        [BindProperty]
        public string? Notas { get; set; }

        public string ErrorMessage { get; set; } = "";

        public void OnGet(int id_usuaio)
        {
            try
            {
                string connectionString = "Server=localhost\\SQLEXPRESS;Database=ArqCrud;Trusted_Connection=True;TrustServerCertificate=True;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sql = "SELECT * FROM clientes WHERE id_usuaio=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id_usuaio);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Id = reader.GetInt32(0); // id_usuaio
                                Primernombre = reader.GetString(1);
                                Apellidos = reader.GetString(2);
                                Email = reader.GetString(3);
                                Telefono = reader.GetString(4);
                                Direccion = reader.GetString(5);
                                Notas = reader.IsDBNull(6) ? null : reader.GetString(6);
                            }
                            else
                            {
                                Response.Redirect("/Clientes/Index");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error al obtener los datos: {ex.Message}";
            }
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                string connectionString = "Server=localhost\\SQLEXPRESS;Database=ArqCrud;Trusted_Connection=True;TrustServerCertificate=True;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sql = @"
                        UPDATE clientes
                        SET 
                            primernombre = @Primernombre,
                            apellidos = @Apellidos,
                            email = @Email,
                            telefono = @Telefono,
                            direccion = @Direccion,
                            notas = @Notas
                        WHERE id_usuaio = @Id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Id", Id);
                        command.Parameters.AddWithValue("@Primernombre", Primernombre);
                        command.Parameters.AddWithValue("@Apellidos", Apellidos);
                        command.Parameters.AddWithValue("@Email", Email);
                        command.Parameters.AddWithValue("@Telefono", Telefono);
                        command.Parameters.AddWithValue("@Direccion", Direccion);
                        command.Parameters.AddWithValue("@Notas", Notas);

                        command.ExecuteNonQuery();
                    }
                }

                return RedirectToPage("/Clientes/Index");
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Ocurrió un error al actualizar los datos: {ex.Message}";
                return Page();
            }
        }
    }
}
