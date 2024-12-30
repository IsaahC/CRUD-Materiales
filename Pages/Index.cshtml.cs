using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

public class IndexModel : PageModel
{
    // Propiedad enlazada para capturar el nombre de usuario ingresado
    [BindProperty]
    public string Correo { get; set; } = string.Empty;

    // Propiedad enlazada para capturar la contraseña ingresada
    [BindProperty]
    public string Contrasena { get; set; } = string.Empty;

    // Mensaje de error que se muestra si ocurre un problema
    public string ErrorMessage { get; set; } = string.Empty;

    // Método que se ejecuta cuando se envía el formulario
    public IActionResult OnPost()
    {
        // Verifica si los campos están vacíos
        if (string.IsNullOrWhiteSpace(Correo) || string.IsNullOrWhiteSpace(Contrasena))
        {
            ErrorMessage = "Por favor, ingrese el usuario y la contraseña.";
            return Page(); // Regresa a la misma página mostrando el mensaje de error
        }

        try
        {
            // Cadena de conexión a la base de datos
            string connectionString = "Server=DESKTOP-LOAE6OF;Database=ArqCrud;Trusted_Connection=True;TrustServerCertificate=True;";

            // Conexión a la base de datos
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open(); // Abre la conexión
                string sql = "SELECT contrasena FROM administradores WHERE correo = @Correo";

                // Prepara el comando SQL para obtener la contraseña asociada al usuario
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    // Agrega el parámetro para evitar inyección SQL
                    command.Parameters.AddWithValue("@Correo", Correo);

                    // Ejecuta el comando y obtiene el resultado
                    var result = command.ExecuteScalar();

                    // Verifica si la contraseña ingresada coincide con la almacenada
                    if (result != null && Contrasena == result.ToString())
                    {
                        // Guarda una sesión indicando que el usuario está autenticado
                        HttpContext.Session.SetString("UserAuthenticated", "true");

                        // Redirige a la página principal de productos
                        return RedirectToPage("/Clientes/Index");
                    }
                }

                // Si el usuario o la contraseña no son correctos, muestra un mensaje de error
                ErrorMessage = "Usuario o contraseña incorrectos.";
                return Page();
            }
        }
        catch (Exception ex)
        {
            // Maneja errores y muestra el mensaje correspondiente
            ErrorMessage = "Error al iniciar sesión: " + ex.Message;
            return Page();
        }
    }
}
