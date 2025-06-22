## Proyecto grupal del curso SC-601 - Programación Avanzada

### Integrantes:
1. MARIA LAURA AZOFEIFA BARRANTES 
2. PRISCILLA DE LA TRINIDAD CHACON CONEJO 
3. HARLYN JOSUE LUNA BRENES 
4. PAULINO JOSE DELGADO FLORES 

### Profesor:
- JORGE PABLO RODRIGUEZ GUZMAN 

## Descripción del proyecto.

### SINPE Empresarial

Este proyecto está desarrollado en **ASP.NET MVC** utilizando el enfoque **Code First** de Entity Framework. Se aplica una arquitectura basada en **principios SOLID** y una organización por capas que permite separar responsabilidades, mejorar el mantenimiento del código y facilitar su escalabilidad.

## 🛠️ Tecnologías y Lenguajes Utilizados

| Lenguaje / Tecnología                 | Propósito en el Proyecto                                                                 
|--------------------------------------|------------------------------------------------------------------------------------------|
| 🧠 **C# (.NET Framework)**           | Lenguaje principal del backend (controladores, entidades, lógica de negocio).            |
| 🎯 **ASP.NET MVC**                   | Framework para estructurar el proyecto en arquitectura Model-View-Controller.            |
| 🧩 **Entity Framework (Code First)** | ORM usado para mapear clases a la base de datos con migraciones.                         |
| 💻 **Razor (cshtml)**                | Motor de vistas usado para construir las vistas en C#.                                   |
| 🧾 **HTML5**                         | Lenguaje de hipertexto para el Frontend.                                                 |
| 🎨 **CSS3**                          | Estilos para las vistas.                                                                 |
| 🧠 **JavaScript**                    | Scripts para la interacción con el usuario, validaciones.                                |
| ✨ **SweetAlert2 (CDN)**             | Alertas elegantes y personalizadas para acciones del usuario.                            |
| 🗄️ **SQL Server**                    | Base de datos relacional utilizada por medio de EF.                                      |
| 🧱 **Bootstrap**                     | Sistema de diseño responsive para estilos rápidos y adaptables.                          |

## 🔁 Flujo General de Desarrollo para los módulos

El desarrollo del primer modulo de **Comercio** fue desarrollado en el siguiente orden:

<img src="./SINPE%20Empresarial/Docs/img/Flujo%20del%20proyecto.png" alt="Flujo del Proyecto" width="500"/>

#### Detalle:

1. **Diseño de la Entidad**
   - Creación de la clase `Comercio` en `Domain.ComercioDomain.Entities`
   - Definición de relaciones con catálogos 'Tablas misceláneas' (`TipoDeComercio`, `TipoDeIdentificacion`)

2. **Interfaz del Repositorio**
   - `ComercioInterface.cs` en `Domain.ComercioDomain.Interfaces`
   - Declaración de métodos CRUD

3. **Repositorio**
   - `ComercioRepository.cs` en `Infrastructure.ComercioInfrastructure.Repositories`
   - Implementación de los métodos usando `DbContext`

4. **Servicio**
   - `ComercioService.cs` en `Services`
   - Lógica de negocio y validaciones

5. **Controlador MVC**
   - `ComercioController.cs` en `Controllers`
   - Métodos:
     - `Index` (listar)
     - `Register` (crear)
     - `Editar` (editar)
     - `Eliminar` (borrar)
     - `Detalles` (ver)

6. **Vistas Razor**
   - `Index.cshtml` para listado
   - `Register.cshtml` para el registro
   - `Edit.cshtml` para la edición del registro
   - `Details.cshtml` solo vista

7. **Scripts y Estilos**
   - Uso de SweetAlert
   - Archivos `.js`  ubicados en `Scripts/ComercioScripts/` 
   - Archivos `.css` ubicados en `Content/css/ComercioStyles/`

8. **Configuración de EF y Migraciones**
   - Se crea la carpeta `Migrations` desde cero mediante los comandos:
     ```bash
     Enable-Migrations
     Add-Migration InitialCreate
     Update-Database
     ```

---

