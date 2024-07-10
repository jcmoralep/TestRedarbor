# Prueba Técnica

Este proyecto es una prueba técnica que utiliza buenas prácticas de programación, patrones de diseño y principios SOLID, basado en la arquitectura limpia y el patrón CQRS.

## Alcance del Proyecto

1. **Arquitectura:**
   - El proyecto está construido en .Net 6 utilizando la arquitectura limpia.
   - El nombre del proyecto es `PruebaRedabor`.

2. **Proyecto Web API:**
   - Se crea un proyecto Web API denominado `PruebaRedabor.Api`.
   - Este proyecto contiene los controladores y Middlewares para gestionar excepciones personalizadas e instalación de servicios.

3. **Biblioteca de Clases:**
   - Se desarrolla un proyecto de biblioteca de clases llamado `PruebaRedabor.Application`.
   - Este proyecto contiene toda la lógica de negocio, incluyendo los manejadores CQRS y DTOs.

4. **Patrón CQRS:**
   - Se implementa el patrón CQRS para separar las operaciones de lectura y escritura.
   - Los métodos de lectura y escritura están organizados en la carpeta CQRS.

5. **Patrón Repositorio Genérico:**
   - Se utiliza el patrón de repositorio genérico en la capa `PruebaRedabor.Infrastructure`.

6. **Entity Framework Core y Dapper:**
   - Se implementa Entity Framework Core para los métodos de lectura.
   - Se utiliza Dapper para los métodos de escritura dentro de la clase `GenericRepository`.

7. **Identity Framework para el manejo de usuarios**

9. **Pruebas Unitarias:**
   - Se realizan pruebas unitarias para cada método (GET, GETById, POST, PUT, DELETE) en el proyecto `PruebaRedabor.Test`.

10. **Docker:**
   - Se utiliza Docker Compose para ejecutar la solución en un entorno Docker.

## Requerimientos para Ejecutar el Proyecto con IIS

- Docker
- Visual Studio 2022 con los paquetes de .Net 6
- IIS
- Navegador Web
- Consola de comandos (PowerShell o consola de Windows)
- SQL Server Express

## Pasos para configurar el Proyecto

1. **Clonar el Repositorio:**
   ```bash
   git clone https://github.com/jcmoralep/TestRedarbor
2) En la consola de comandos o en Visual Studio (Ctrl + ñ) ubicarse en la raíz del proyecto y ejecutar el comando: 
   ```bash"
   docker compose up -d"
3) Se debe bajar la imagen de SQL Server y configurarse para trabajar en el puerto 1433 (validar en docker la ejecución de la imagen)
4) Posteriormente abriremos Visual Studio
5) En este paso ejecutaremos el script de la base de datos:
   - Abrir el MS SQL Server
   - Iniciar sesión con las siguientes credenciales:
      - Server: localhost,1433
      - User: sa
      - Password: Zaq2024sql24*
   - Ejecutar el script que está en la raiz del proyecto: Script-Bb/script-inventory.sql, validando que se ejecute correctamente
7) En Visual Studio lancaremos la aplicación Inventory.Api por el IIS Express
   ![image](https://github.com/jcmoralep/TestRedarbor/assets/152304974/c12ce24f-6f21-446a-905b-cf1c51dedb4f)
8) Les tendrá que cargar el Swagger así: 
   ![image](https://github.com/jcmoralep/TestRedarbor/assets/152304974/ab7f1246-d9e0-4042-82fb-631fad4fe494)


## Requerimientos para Ejecutar el Proyecto con DOCKER

- Docker Desktop
- Navegador web

1. **Clonar el Repositorio:**
   ```bash
   git clone https://github.com/jcmoralep/TestRedarbor
2) En la consola de comandos o en Visual Studio (Ctrl + ñ) ubicarse en la raíz del proyecto y ejecutar el comando: 
   ```bash"
   docker compose up -d"
3) Cuando se monte el contendor, hacer los siguientes pasos:
   En este paso ejecutaremos el script de la base de datos:
   - Abrir el MS SQL Server
   - Iniciar sesión con las siguientes credenciales:
      - Server: localhost,1433
      - User: sa
      - Password: Zaq2024sql24*
   - Ejecutar el script que está en la raiz del proyecto: Script-Bb/script-inventory.sql, validando que se ejecute correctamente
   - Abrir la siguiente URL: http://localhost:8005/swagger/index.html 
5) Se abrira el swagger y se debe visualizar así:
   ![image](https://github.com/jcmoralep/TestRedarbor/assets/152304974/ab7f1246-d9e0-4042-82fb-631fad4fe494)

## Uso de los servicios: 
1) Para hacer el uso de los servicios, debemos registrarnos en el end point: "Register"
2) Obtendremos un token, que podremos usar en el AUTHORIZE, le debemos anteponer la palabra: bearer, un espacio seguido el token, ejemplo: bearer yJaaILoowksk
3) De esta manera ya estaremos autenticados, podremos hacer uso de los servicios expuestos con la ayuda de swagger
4) En caso de expirar el token, en el endpoint Login, ingresar los datos cuando te registraste para obtener un nuevo token y hacer el paso 2.
