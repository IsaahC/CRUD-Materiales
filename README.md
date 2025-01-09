#Proyecto hecho por Elizalde Jaime Noe Alejandro
Descripción del Proyecto
Este proyecto es una aplicación web CRUD (Crear, Leer, Actualizar y Eliminar) para gestionar materiales de construcción. Está desarrollado utilizando ASP.NET Core con Razor Pages y está diseñado para facilitar el control de inventario y la gestión de materiales en tiendas de construcción.

Características
Gestión de materiales:
Crear nuevos materiales.
Consultar detalles de materiales existentes.
Actualizar información de materiales.
Eliminar materiales del inventario.
Interfaz amigable basada en Razor Pages.
Validación de formularios en el lado del cliente y del servidor.
Uso de Entity Framework Core para la gestión de la base de datos.
Requisitos Previos
.NET SDK: Versión 6.0 o superior.
Base de datos: SQL Server o cualquier otro proveedor compatible con Entity Framework Core.
IDE: Visual Studio 2022 (recomendado) o cualquier editor compatible con .NET.
Navegador: Google Chrome, Edge, Firefox u otro navegador moderno.
Estructura del Proyecto
graphql
Copiar código
├── Pages
│   ├── Index.cshtml         # Página principal
│   ├── Create.cshtml        # Página para crear un nuevo material
│   ├── Edit.cshtml          # Página para editar un material
│   ├── Delete.cshtml        # Página para eliminar un material
│   ├── Details.cshtml       # Página para ver detalles de un material
├── Models
│   ├── Material.cs          # Modelo de datos para materiales
├── Data
│   ├── ApplicationDbContext.cs  # Contexto de base de datos
├── wwwroot
│   ├── js                   # Archivos JavaScript opcionales
├── Program.cs               # Configuración principal de la aplicación
├── appsettings.json         # Configuración de la conexión a la base de datos
Configuración
Clonar el repositorio:


git clone https://github.com/tu-usuario/nombre-del-repositorio.git
cd nombre-del-repositorio
Configurar la conexión a la base de datos:

Editar el archivo appsettings.json y configurar la cadena de conexión:

"ConnectionStrings": {
  "DefaultConnection": "Server=tu-servidor;Database=tu-base-de-datos;Trusted_Connection=True;"
}
Instalar las dependencias necesarias:

dotnet restore
Aplicar migraciones y crear la base de datos:

dotnet ef database update
Ejecutar el proyecto:


dotnet run
Accede a la aplicación en localhost.

Tecnologías Utilizadas
ASP.NET Core: Framework principal para la aplicación.
Razor Pages: Modelo para construir la interfaz de usuario.
Entity Framework Core: ORM para la gestión de la base de datos.
SQL Server Magnament: Base de datos relacional.
