--Se crea la base de datos
create database [Gestion_Analisis_Proyectos]
go 

DROP DATABASE [Gestion_Analisis_Proyectos]
---Usamos la BD
use [Gestion_Analisis_Proyectos]
go

---Creación de la tabla usuarios
IF EXISTS(SELECT name FROM dbo.sysobjects WHERE name='USP_GES_AP_USERS')
DROP TABLE [USP_GES_AP_USERS]
GO


CREATE TABLE [USP_GES_AP_USERS](
userMail VARCHAR(300) NOT NULL,
password VARCHAR(max),
rolUser VARCHAR(300),
urlImage VARCHAR(MAX),
codeValidation VARCHAR(MAX),
phoneNumber int,
state int
PRIMARY KEY(userMail))
GO


SELECT* from [USP_GES_AP_USERS];
SELECT* from pr