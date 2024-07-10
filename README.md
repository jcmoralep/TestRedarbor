# Prueba técnica

Este proyecto es una prueba técnica usando buena practicas de programación, patrones de diseño y SOLID, basada en clean arquitectura y el patrón CQRS especialmente.

## Alcance del proyecto

1) Se contruye la aplicación en .Net 6 implementado la arquitectura (clean architecture). El proyecto tiene el nombre de PruebaRedabor
2) Se crea un proyecto Web Api "PruebaRedabor.Api" el cual contiene los controladores, Middlewares(para poder realizar las excepciones personalizadas e installación de servicios)
3) Se crea un proyecto de biblioteca de clases "PruebaRedabor.Application" el cual tiene toda la logica del negocio "midles de CQRS, y dtos"
4) Se implementa el patron CQRS para la lectura y escritura de cada metodo, los cuales se encuentran en la carpeta CQRS
5) Se implementa el patron repositorio generico en la capa "PruebaRedarbor.Infrastructure"
6) Se implementa EntityFrameworkCore para los metodos de lectura y Dapper para los metodos de escritura (desde la clase GenericRepository)
7) Se realizan pruebas unitarias cada una para un metodo GET, GETById, POST, PUT, DELETE en el proyecto PruebaRedarbor.Test
9) Se maneja dockerCompose para ejecutar en docker la solución.

## Requerimientos para ejecutar el proyecto

- Docker Desktop
- Visual Studio 2022 con los paquetes de .Net 6
- IIS
- Navegador Web
- Consola de comandos (Power Shell o consola de Windows)

## Pasos para poder usar el proyecto

1) Clonar el repositorio: https://github.com/jcmoralep/TestRedarbor
2) En la consola de comandos o en Visual Studio (Ctrl + ñ) ubicarse en la raíz del proyecto y ejecutar el comando "docker compose up -d"
3) Se debe bajar la imagen de SQL Server y configurarse para trabajar en el puerto 1433 (validar en docker la ejecución de la imagen)
4) Posteriormente abriremos Visual Studio
5) En este ubicaremos la ventana Package Manager Console y ejecutaremos el comando: database-update (Esto realizara creación de la base de datos)
6) En Visual Studio lancaremos la aplicación Inventory.Api por el IIS Express
7) Les tendrá que cargar el Swagger así: 

8) Para hacer el uso de los servicios, debemos registrarnos en el end point: "Register"
9) Obtendremos un token, que podremos usar en el AUTHORIZE, le debemos anteponer la palabra: bearer, un espacio seguido el token, ejemplo: bearer yJaaILoowksk
10) De esta manera ya estaremos autenticados, podremos hacer uso de los servicios expuestos
