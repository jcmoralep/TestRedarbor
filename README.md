# Prueba Técnica

Este proyecto es una prueba técnica que utiliza buenas prácticas de programación, patrones de diseño y principios SOLID, basado en la arquitectura limpia y el patrón CQRS.

## Tecnologías
- .Net 6
- Entity Framework Core
- Identity Framework
- Dapper
- Swagger
- API Rest
- Unit Test de MS
- JWT

## Alcance del Proyecto

1. **Arquitectura**
   - El proyecto está construido en .Net 6 utilizando la arquitectura limpia.
   - El nombre del proyecto es `PruebaRedabor`.

2. **Proyecto Web API**
   - Se crea un proyecto Web API denominado `PruebaRedabor.Api`.
   - Este proyecto contiene los controladores y Middlewares para gestionar excepciones personalizadas e instalación de servicios.

3. **Biblioteca de Clases**
   - Se desarrolla un proyecto de biblioteca de clases llamado `PruebaRedabor.Application`.
   - Este proyecto contiene toda la lógica de negocio, incluyendo los manejadores CQRS y DTOs.

4. **Patrón CQRS**
   - Se implementa el patrón CQRS para separar las operaciones de lectura y escritura.
   - Los métodos de lectura y escritura están organizados en la carpeta CQRS.

5. **Patrón Repositorio Genérico**
   - Se utiliza el patrón de repositorio genérico en la capa `PruebaRedabor.Infrastructure`.

6. **Entity Framework Core y Dapper**
   - Se implementa Entity Framework Core para los métodos de lectura.
   - Se utiliza Dapper para los métodos de escritura dentro de la clase `GenericRepository`.

7. **Identity Framework**
   - Para la administración de los usuarios

9. **Pruebas Unitarias**
   - Se realizan pruebas unitarias para cada método (GET, GETById, POST, PUT, DELETE) en el proyecto `PruebaRedabor.Test`.

10. **Excepción Hanlder**
    - Para para tolerancia a fallos y controlar las excepciones

11. **Centralizador de servicios IInstaller**
    - Moviendo la lógica de configuración de servicios a métodos más organizados y modulares.
    - Esto mejora la legibilidad y el mantenimiento del código.

12. **Docker:**
   - Se utiliza Docker para ejecutar la solución en un entorno contenerizado.

## Requerimientos para Ejecutar el Proyecto con IIS

- Docker
- Visual Studio 2022 con los paquetes de .Net 6
- IIS
- Navegador Web
- Consola de comandos (PowerShell o consola de Windows)
- SQL Server

## Pasos para configurar el Proyecto

1. **Clonar el Repositorio:**
   ```bash
   git clone https://github.com/jcmoralep/TestRedarbor
2) Se debe cambiar la cadena de conexión en el proyecto de Inventory.Api: "appsettings.json", de la solución clonada
   ```bash
   Data Source=localhost,1433;Initial Catalog=BDRedarbor;User Id=sa;Password=Zaq2024sql24*;
3) En la consola de comandos o en Visual Studio (Ctrl + ñ) ubicarse en la raíz del proyecto y ejecutar el comando: 
   ```bash"
   docker compose up -d"
4) El paso anterior instala la imagen de SQL Server configurado para trabajar por el puerto 1433 (validar en docker la ejecución de la imagen)
5) En este paso ejecutaremos el script de la base de datos:
   - Abrir el MS SQL Server localmente
   - Iniciar sesión con las siguientes credenciales:
      - Server: localhost,1433
      - User: sa
      - Password: Zaq2024sql24*
   - Ejecutar el script que está en la raiz del proyecto: Script-Bb/script-inventory.sql, validando que se ejecute correctamente
6) En Visual Studio lancaremos la aplicación Inventory.Api por el IIS Express
   ![image](https://github.com/jcmoralep/TestRedarbor/assets/152304974/c12ce24f-6f21-446a-905b-cf1c51dedb4f)
7) Tendrá que cargar el Swagger así: 
   ![image](https://github.com/jcmoralep/TestRedarbor/assets/152304974/ab7f1246-d9e0-4042-82fb-631fad4fe494)


## Requerimientos para Ejecutar el Proyecto con DOCKER

- Docker Desktop
- Sql Server
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
5) Abrir la siguiente URL: http://localhost:8005/swagger/index.html 
   ![image](https://github.com/jcmoralep/TestRedarbor/assets/152304974/ab7f1246-d9e0-4042-82fb-631fad4fe494)

## Uso de los servicios: 
1) Para hacer el uso de los servicios, debemos registrarnos en el end point: "Register"
2) Obtendremos un token, que podremos usar en el AUTHORIZE, le debemos anteponer la palabra: bearer, un espacio seguido el token, ejemplo: bearer yJaaILoowksk
3) De esta manera ya estaremos autenticados, podremos hacer uso de los servicios expuestos con la ayuda de swagger
4) En caso de expirar el token, en el endpoint Login, ingresar los datos cuando te registraste para obtener un nuevo token y hacer el paso 2.
