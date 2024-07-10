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

7. **Pruebas Unitarias:**
   - Se realizan pruebas unitarias para cada método (GET, GETById, POST, PUT, DELETE) en el proyecto `PruebaRedabor.Test`.

8. **Docker:**
   - Se utiliza Docker Compose para ejecutar la solución en un entorno Docker.

## Requerimientos para Ejecutar el Proyecto

- Docker Desktop
- Visual Studio 2022 con los paquetes de .Net 6
- IIS
- Navegador Web
- Consola de comandos (PowerShell o consola de Windows)

## Pasos para Usar el Proyecto

1. **Clonar el Repositorio:**
   ```bash
   git clone https://github.com/jcmoralep/TestRedarbor
2) En la consola de comandos o en Visual Studio (Ctrl + ñ) ubicarse en la raíz del proyecto y ejecutar el comando "docker compose up -d"
3) Se debe bajar la imagen de SQL Server y configurarse para trabajar en el puerto 1433 (validar en docker la ejecución de la imagen)
4) Posteriormente abriremos Visual Studio
5) En este ubicaremos la ventana Package Manager Console y ejecutaremos el comando: database-update (Esto realizara creación de la base de datos)
6) En Visual Studio lancaremos la aplicación Inventory.Api por el IIS Express
7) Les tendrá que cargar el Swagger así: 

8) Para hacer el uso de los servicios, debemos registrarnos en el end point: "Register"
9) Obtendremos un token, que podremos usar en el AUTHORIZE, le debemos anteponer la palabra: bearer, un espacio seguido el token, ejemplo: bearer yJaaILoowksk
10) De esta manera ya estaremos autenticados, podremos hacer uso de los servicios expuestos
