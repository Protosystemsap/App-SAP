--------------Crear Tabla de proyectos

CREATE TABLE [USP_GES_AP_PROJECTS](
projectCode INT NOT NULL,
projectName VARCHAR(max),
description VARCHAR(300),
startDate datetime,
endDate datetime,
PRIMARY KEY(projectCode))
GO
--R
presupuesto= 200000

actividads= 10

Rango >5000 = Viable
Rango<5000? No viable


----M

--J------
Lista actividades =10
acvidades> 10 = rentable
activdade<10= No rentable


-------PROCEDIMIENTOS

------------INSRTAR
create procedure [USP_GES_AP_INSERTPROJECTS](
@projectCode int,
@projectName varchar(max),
@description  varchar(max),
@startDate datetime,
@endDate datetime)
as
insert into [USP_GES_AP_PROJECTS] values (@projectCode,@projectName,@description,@startDate,@endDate)
go

------------ACTUALIZAR
create procedure [USP_GES_AP_UPDATEPROJECTS](
@projectCode int,
@projectName varchar(max),
@description  varchar(max),
@startDate datetime,
@endDate datetime)
as
update [USP_GES_AP_PROJECTS] set projectName = @projectName, description = @description, startDate = @startDate, endDate = @endDate where projectCode = @projectCode
go

------------ELIMINAR
create procedure [USP_GES_AP_DELETEPROJECTS](
@projectCode int)
as
delete from [USP_GES_AP_PROJECTS]
where projectCode=@projectCode;
go

-----------VERIFICAR CODIGO
--Procedimiento para buscar un proyecto por medio de su codigo 
CREATE PROCEDURE [USP_GES_AP_SEARCHCODEPROJECT](
@code varchar(50))
AS
---Se realiza la búsqueda por código
SELECT projectCode,projectName,description,startDate,endDate
FROM [USP_GES_AP_PROJECTS]
WHERE projectCode like '%'+@code+'%';
GO

SELECT* from [USP_GES_AP_USERS]
SELECT* from [USP_GES_AP_PROJECTS]





