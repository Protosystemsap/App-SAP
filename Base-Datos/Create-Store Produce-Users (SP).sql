

--**************Inicio para procedimientos almacenados para logincontroler********************

--Procedimiento para  selecionar todos los usuarios
IF EXISTS(SELECT NAME FROM dbo.sysobjects WHERE name ='USP_GES_AP_GETALLUSERS')
DROP PROCEDURE [USP_GES_AP_GETALLUSERS]
GO
CREATE PROCEDURE [USP_GES_AP_GETALLUSERS]
as

 SELECT* from USP_GES_AP_USERS;
GO


--Procedimiento para  buscar usuario por userMail
IF EXISTS(SELECT NAME FROM dbo.sysobjects WHERE name ='USP_GES_AP_SEARCH_USERMAIL')
DROP PROCEDURE [USP_GES_AP_SEARCH_USERMAIL]
GO
CREATE PROCEDURE [USP_GES_AP_SEARCH_USERMAIL](
 @userMail VARCHAR(300)
)
as

 SELECT userMail,password,rolUser,urlImage,codeValidation,phoneNumber,state from USP_GES_AP_USERS where userMail=@userMail;
GO

--Procedimiento encargado de actualizar contraseña y estado , la primera vez que un usuario se loguea

IF EXISTS(SELECT NAME FROM dbo.sysobjects WHERE name ='USP_GES_AP_UPDATEPASSWORDSTATE')
DROP PROCEDURE [USP_GES_AP_UPDATEPASSWORDSTATE]
GO
CREATE PROCEDURE [USP_GES_AP_UPDATEPASSWORDSTATE](
 @userMail VARCHAR(300),
 @password VARCHAR(MAX)
)
as
  UPDATE USP_GES_AP_USERS set password=@password,state=1
  where userMail=@userMail;
  
GO


---Procedimiento para autenticar un usuario
IF EXISTS(SELECT NAME FROM dbo.sysobjects WHERE name ='USP_GES_AUTHENTICATION_USERS')
DROP PROCEDURE [autenticar_usuario]
GO
CREATE PROCEDURE [USP_GES_AUTHENTICATION_USERS](
@userMail varchar(50),
@password varchar (50))
AS
---Se consulta por medio del login y password
SELECT userMail,password FROM USP_GES_AP_USERS
WHERE userMail=ltrim(rtrim(@userMail)) COLLATE SQL_Latin1_General_CP1_CS_AS  AND password=ltrim(rtrim(@password)) COLLATE SQL_Latin1_General_CP1_CS_AS ;
GO

INSERT INTO USP_GES_AP_USERS (userMail,password,rolUser) values('allanmongec16@gmail.com','1234','Admin');

--**************Fin  para procedimientos almacenados para logincontroler*************************



--**************Inicio de procedimientos para el admiControler**************************


--Procedimiento para insertar usuarios por parte del Administrador
IF EXISTS(SELECT NAME FROM dbo.sysobjects WHERE name ='USP_GES_AP_INSERTUSERS')
DROP PROCEDURE [USP_GES_AP_INSERTUSERS]
GO
CREATE PROCEDURE [USP_GES_AP_INSERTUSERS](
	@userMail VARCHAR(300),
	@password VARCHAR(max),
	@phoneNumber int,
	@rolUser varchar(40)
)
as
 Insert Into USP_GES_AP_USERS (userMail,password,rolUser,phoneNumber,state)values(@userMail,@password,@rolUser,@phoneNumber,0)
GO

---Porcedimeinto encargado de vereficar si existe el correo
	IF EXISTS(SELECT NAME FROM dbo.sysobjects WHERE name ='USP_GES_AP_VALIDATEMAIL')
	DROP PROCEDURE [USP_GES_AP_VALIDATEMAIL]
	go
	
	CREATE PROCEDURE [USP_GES_AP_VALIDATEMAIL](
		@userMail VARCHAR(300)
	)
	as
		Select userMail from USP_GES_AP_USERS
		where userMail= ltrim(rtrim(@userMail)) ;
	go

--Procedimiendo para eliminar users
CREATE PROCEDURE [USP_GES_AP_DELETEUSER](
 @userMail VARCHAR(300)
 )
 as
delete from  [USP_GES_AP_USERS] 
where usermail=@userMail;

--Procedimiento para buscar usuario

CREATE PROCEDURE [USP_GES_AP_USERSEARCH](
 @userMail VARCHAR(300)
)
as
select phoneNumber,rolUser  from [USP_GES_AP_USERS]
where userMail=@userMail;


--Procedimiento para traer todos los usarios
CREATE PROCEDURE[USP_GES_AP_ALTERUSER](
@phoneNumber int,
@rolUser varchar(40)
)
as
Update [USP_GES_AP_USERS]
set phoneNumber=@phoneNumber,
rolUser= @rolUser;

--**************Fin  para procedimientos almacenados para admiControler*************************



--**************Inicio de Procedimientos almacenados para forgotPasswordControler***************
	
---Procedimiento encargado de actualizar codigo de validacion  si al usuario se le olvida la contraseña

	IF EXISTS(SELECT NAME FROM dbo.sysobjects WHERE name ='USP_GES_AP_UPDATECODEVALIDATION')
	DROP PROCEDURE [USP_GES_AP_UPDATECODEVALIDATION]
	GO
	CREATE PROCEDURE [USP_GES_AP_UPDATECODEVALIDATION](
		@userMail VARCHAR(300),
		@codeValidation VARCHAR(MAX)
	)
	as
		UPDATE USP_GES_AP_USERS set codeValidation=@codeValidation
		where userMail=@userMail;

 

	GO
	

---Porcedimeinto encargado de crear una nueva contraseña para el usuario
	
	IF EXISTS(SELECT NAME FROM dbo.sysobjects WHERE name ='USP_GES_AP_CREATEENEWPASSWORD')
	DROP PROCEDURE [USP_GES_AP_CREATEENEWPASSWORD]
	GO
	CREATE PROCEDURE [USP_GES_AP_CREATEENEWPASSWORD](
		@userMail VARCHAR(300),
		@password VARCHAR(MAX)
	)
	as
		UPDATE USP_GES_AP_USERS set password=@password
		where userMail=@userMail;
  
	GO


---Porcedimeinto encargado de crear de validar si el codigo es el mismo al que se ingresa
	IF EXISTS(SELECT NAME FROM dbo.sysobjects WHERE name ='USP_GES_AP_COMPARECODEVALIDATION')
	DROP PROCEDURE [USP_GES_AP_COMPARECODEVALIDATION]
	GO
	CREATE PROCEDURE [USP_GES_AP_COMPARECODEVALIDATION](
		@userMail VARCHAR(300),
		@codeValidation VARCHAR(MAX)
	)
	as
		Select userMail from USP_GES_AP_USERS
		where userMail=@userMail
		and codeValidation=ltrim(rtrim(@codeValidation)) COLLATE SQL_Latin1_General_CP1_CS_AS;
  
	GO


--****************Fin de procedimientos para forgotPasswordControler**************************************


--***************Procedimientos para  usersPhotosControlers************************************************(No se usan)
	
	--Procedimiento encargado de actualizar, crear y eliminar urlImage (Imagen) del usuario
	IF EXISTS(SELECT NAME FROM dbo.sysobjects WHERE name ='USP_GES_AP_PUTUSERSIMAGES')
	DROP PROCEDURE [USP_GES_AP_PUTUSERSIMAGES]
	GO
	CREATE PROCEDURE [USP_GES_AP_PUTUSERSIMAGES](
		@userMail VARCHAR(300),
		@urlImage VARCHAR(MAX)
	)
	as
		UPDATE USP_GES_AP_USERS set urlImage=@urlImage
		where userMail=@userMail;
  
	GO


--*************** Fin de Procedimientos para  usersPhotosControlers*****************************************



SELECT* from USP_GES_AP_USERS


----Correo Gmail
--contraseña= ucr2022sap
--correo=protosystemsap@gmail.com
--contraseña aplicacion=ypxjgxkxrjpbnuyp

--sbgktgyz




	
