USE Consultorio_Seguros
 
 GO

  CREATE PROCEDURE [Dbo].[SP_CLIENTES_LISTAR]
  AS
	BEGIN
		SELECT 
			Id
			,Cedula
			,Nombre
			,Telefono
			,Edad
		FROM Dbo.Clientes WITH (NOLOCK)
	END

GO

CREATE PROCEDURE [Dbo].[SP_CLIENTES_ID]
(
	@Id INT
)
AS
	BEGIN
		SELECT 
			Id
			,Cedula
			,Nombre
			,Telefono
			,Edad
			
		FROM
			Dbo.Clientes WITH (NOLOCK)
		WHERE 
			Id=@Id
	END

GO
CREATE PROCEDURE [Dbo].[SP_CLIENTES_CREAR]
(
	@Cedula nvarchar(10)
	,@Nombre nvarchar(40)
	,@Telefono nvarchar(10)
	,@Edad int
)
AS
	BEGIN
	DECLARE @ROWCOUNT INT = 0
		BEGIN TRY
		SET @ROWCOUNT = (SELECT COUNT(1) FROM dbo.Clientes WITH (NOLOCK) WHERE Cedula = @Cedula)
		IF (@ROWCOUNT = 0)
		BEGIN
			BEGIN TRAN
				INSERT INTO Clientes 
					(
						Cedula
						,Nombre
						,Telefono
						,Edad
					)
				VALUES
					(
						@Cedula
						,@Nombre
						,@Telefono
						,@Edad
					)
			COMMIT TRAN
			END
		END TRY

		BEGIN CATCH
			ROLLBACK TRAN
		END CATCH
	END

GO
CREATE PROCEDURE [Dbo].[SP_CLIENTES_EDITAR]
(
	@Id int
	,@Cedula nvarchar(10)
	,@Nombre nvarchar(40)
	,@Telefono nvarchar(10)
	,@Edad int
)
AS
	BEGIN
		DECLARE @count INT = 0
		BEGIN TRY
			SET @count = (SELECT COUNT(1) FROM dbo.Clientes WITH (NOLOCK) WHERE Id=@Id)

			IF (@count > 0)
				BEGIN
					BEGIN TRAN
						UPDATE dbo.Clientes 
							SET
								Cedula = @Cedula
								,Nombre = @Nombre
								,Telefono = @Telefono
								,Edad = @Edad
							WHERE 
								Id = @Id
					COMMIT TRAN
				END
		END TRY

		BEGIN CATCH
			ROLLBACK TRAN
		END CATCH
	END

GO

CREATE PROCEDURE [Dbo].[SP_CLIENTES_ELIMINAR]
(
	@Id int
)
AS
	BEGIN
		DECLARE @count INT = 0
		BEGIN TRY
			SET @count = (SELECT COUNT(1) FROM dbo.Clientes WITH (NOLOCK) WHERE Id=@Id)

			IF (@count > 0)
				BEGIN
					BEGIN TRAN
						DELETE FROM dbo.Clientes
							WHERE 
								Id = @Id
					COMMIT TRAN
				END
		END TRY

		BEGIN CATCH
			ROLLBACK TRAN
		END CATCH
	END
GO

-- P R O C E D U R E    S E G U R O
  CREATE PROCEDURE [Dbo].[SP_SEGUROS_LISTAR]
  AS
	BEGIN
		SELECT 
			Id
			,Codigo
			,Nombre
			,SemiAsegurada
			,Prima
		FROM Dbo.Seguros WITH (NOLOCK)
	END
GO

CREATE PROCEDURE [Dbo].[SP_SEGUROS_ID]
(
	@Id INT
)
AS
	BEGIN
		SELECT 
			Id
			,Codigo
			,Nombre
			,SemiAsegurada
			,Prima
			
		FROM
			Dbo.Seguros WITH (NOLOCK)
		WHERE 
			Id=@Id
	END

	
GO
CREATE PROCEDURE [Dbo].[SP_SEGUROS_CREAR]
(
	@Codigo nvarchar(50)
	,@Nombre nvarchar(150)
	,@SemiA decimal(18,2)
	,@Prima decimal(18,2)
)
AS
	BEGIN
		DECLARE @ROWCOUNT INT = 0
		BEGIN TRY
			SET @ROWCOUNT = (SELECT COUNT(1) FROM dbo.Seguros WITH (NOLOCK) WHERE Codigo = @Codigo)
			IF (@ROWCOUNT = 0)
			BEGIN
			BEGIN TRAN
				INSERT INTO Seguros 
					(
						Codigo
						,Nombre
						,SemiAsegurada
						,Prima
					)

				VALUES
					(
						@Codigo
						,@Nombre
						,@SemiA
						,@Prima
					)
			COMMIT TRAN
			END
		END TRY

		BEGIN CATCH
			ROLLBACK TRAN
		END CATCH
	END
GO
CREATE PROCEDURE [Dbo].[SP_SEGUROS_EDITAR]
(
	@Id int
	,@Codigo nvarchar(50)
	,@Nombre nvarchar(150)
	,@SemiA decimal(18,2)
	,@Prima decimal(18,2)
)
AS
	BEGIN
		DECLARE @count INT = 0
		BEGIN TRY
			SET @count = (SELECT COUNT(1) FROM dbo.Seguros WITH (NOLOCK) WHERE Id=@Id)

			IF (@count > 0)
				BEGIN
					BEGIN TRAN
						UPDATE dbo.Seguros 
							SET
								Codigo = @Codigo
								,Nombre = @Nombre
								,SemiAsegurada = @SemiA
								,Prima = @Prima
							WHERE 
								Id = @Id
					COMMIT TRAN
				END
		END TRY

		BEGIN CATCH
			ROLLBACK TRAN
		END CATCH
	END
go
CREATE PROCEDURE [Dbo].[SP_SEGUROS_ELIMINAR]
(
	@Id int
)
AS
	BEGIN
		DECLARE @count INT = 0
		BEGIN TRY
			SET @count = (SELECT COUNT(1) FROM dbo.Seguros WITH (NOLOCK) WHERE Id=@Id)

			IF (@count > 0)
				BEGIN
					BEGIN TRAN
						DELETE FROM dbo.Seguros
							WHERE 
								Id = @Id
					COMMIT TRAN
				END
		END TRY

		BEGIN CATCH
			ROLLBACK TRAN
		END CATCH
	END
GO

-- P R O C E D U R E S   A S E G U R A D O S
CREATE PROCEDURE [Dbo].[SP_ASEGURADOS_LISTAR]
  AS
	BEGIN
		SELECT 
			t1.Id
			,t2.Cedula		AS CedulaCliente
			,t2.Nombre		AS NombreCliente
			,t3.Codigo		AS CodigoSeguro
			,t3.Nombre		AS NombreSeguro
			,t3.SemiAsegurada AS Asegurada
			,t3.Prima		AS Prima
		FROM Dbo.Asegurados t1
			INNER JOIN Dbo.Clientes t2
				ON t1.ClienteId = t2.Id
			INNER JOIN Dbo.Seguros t3
				ON t1.SeguroId = t3.Id
	END
GO

INSERT INTO Clientes (Cedula, Nombre, Telefono, Edad) VALUES ('0924826480', 'Fernando Reyes', '0981071134', 35)
INSERT INTO Clientes (Cedula, Nombre, Telefono, Edad) VALUES ('0926726423', 'Samantha Peraza', '0966071275', 36)
INSERT INTO Clientes (Cedula, Nombre, Telefono, Edad) VALUES ('0923325466', 'Arianna Ibarra', '0988071654', 28)
INSERT INTO Clientes (Cedula, Nombre, Telefono, Edad) VALUES ('0925526118', 'Sophia Walket', '0955779988', 20)

INSERT INTO Seguros (Codigo, Nombre, SemiAsegurada, Prima) VALUES ('SEG-XX001', 'SEGUROS ESTEBAN S.A.', 1300.50, 50)
INSERT INTO Seguros (Codigo, Nombre, SemiAsegurada, Prima) VALUES ('SEG-XX002', 'SEGUROS REYES COORPORATION', 800.25, 90)
INSERT INTO Seguros (Codigo, Nombre, SemiAsegurada, Prima) VALUES ('SEG-XX003', 'ASISTENCIA LAYENNE', 1780.00, 100)
INSERT INTO Seguros (Codigo, Nombre, SemiAsegurada, Prima) VALUES ('SEG-XX004', 'COORP. ADA LOVELACE', 3200.00, 222)


