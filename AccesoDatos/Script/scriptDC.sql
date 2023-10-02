--Crear la base de datos
IF NOT EXISTS (SELECT name FROM master.sys.databases WHERE name = N'ProyectoDB')
Begin
	Create Database ProyectoDB	
End
Else
Begin
	print('Base [ProyectoDB] ya está creada.')
End
GO
--Seleccionar la base de datos
Use ProyectoDB
GO

--Crear Tabla de Catalogo de Empleados
IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'Empleados')
Begin
	Create Table Empleados (NumeroEmpleado int primary key not null, Nombre nvarchar(50), ApellidoPaterno nvarchar(50), ApellidoMaterno nvarchar(50), CodigoRol int)
End
Else
Begin
	print('Tabla [Empleados] ya está creada.')
End

--Crear Tabla de Catalogo de Roles
IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'Roles')
Begin
	Create Table Roles (CodigoRol int primary key not null, Descripcion nvarchar(50), BonoPorHora money)

	--Insertar datos de carga inicial
	Insert into Roles Values (1,'Chofer', 10.00)
	Insert into Roles Values (2,'Cargador', 5.00)
	Insert into Roles Values (3,'Auxiliar', 0.00)
End
Else
Begin
	print('Tabla [Roles] ya está creada.')
End

--Crear Tabla de Catalogo de Impuestos
IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'Impuestos')
Begin
	Create Table Impuestos (CodigoImpuesto int primary key not null, Descripcion nvarchar(50), Porcentaje money)
	
	--Insertar datos de carga inicial
	Insert into Impuestos Values (1,'ISR', 9.00)
	Insert into Impuestos Values (2,'ISR Adicional', 3.00)

End
Else
Begin
	print('Tabla [Impuestos] ya está creada.')
End

--Crear Tabla de Configuracion de Sueldo por Empleado
IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'ConfiguracionSueldosEmpleados')
Begin
	Create Table ConfiguracionSueldosEmpleados (NumeroEmpleado int primary key not null, SueldoBasePorHora money, PagoPorEntrega money,
	                                            PorcentajeVales money, LimiteSueldoMensual money)
End
Else
Begin
	print('Tabla [ConfiguracionSueldosEmpleados] ya está creada.')
End

--Crear Tabla de Configuracion de Impuestos por Empleado
IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'ConfiguracionImpuestosEmpleados')
Begin
	Create Table ConfiguracionImpuestosEmpleados (NumeroEmpleado int not null, CodigoImpuesto int not null,
	                                              Primary Key (NumeroEmpleado, CodigoImpuesto))
End
Else
Begin
	print('Tabla [ConfiguracionImpuestosEmpleados] ya está creada.')
End


IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'MovimientosSueldosMensual')
Begin
	Create Table MovimientosSueldosMensual (NumeroEmpleado int not null, CodigoRol int not null, Mes int not null, HorasTrabajadas int, 
	                                        CantidadEntregas int, SueldoBase money, ImportePagoPorEntregas money, ImportePagoPorBono money, 
											ImporteVales money, ISR money, ISRAdicional money, Primary Key (NumeroEmpleado, CodigoRol, Mes))
End
Else
Begin
	print('Tabla [MovimientosSueldosMensual] ya está creada.')
End
GO

--Procedimiento para Guardar Empleados
Create Procedure SP_GuardarEmpleado
(@NumeroEmpleado int, @Nombre nvarchar(50), @ApellidoPaterno nvarchar(50),  @ApellidoMaterno nvarchar(50), @CodigoRol int)
As
Begin

	Insert into Empleados
	(NumeroEmpleado, Nombre, ApellidoPaterno, ApellidoMaterno, CodigoRol)
	Values
	(@NumeroEmpleado, @Nombre, @ApellidoPaterno, @ApellidoMaterno, @CodigoRol)

End
GO

--Procedimiento para Actualizar Empleados
Create Procedure SP_ActualizarEmpleado
(@NumeroEmpleado int, @Nombre nvarchar(50), @ApellidoPaterno nvarchar(50),  @ApellidoMaterno nvarchar(50), @CodigoRol int)
As
Begin
	Update Empleados
		Set Nombre = @Nombre, 
			ApellidoPaterno = @ApellidoPaterno,
			ApellidoMaterno = @ApellidoMaterno,
			CodigoRol = @CodigoRol
	Where NumeroEmpleado = @NumeroEmpleado
	
End
GO

--Procedimiento para Obtener los datos del Empleado por NumeroEmpleado
Create Procedure SP_ObtenerEmpleado
(@NumeroEmpleado int)
As
Begin
	Select NumeroEmpleado, Nombre, ApellidoPaterno, ApellidoMaterno, CodigoRol
	From Empleados 
	Where NumeroEmpleado = @NumeroEmpleado
End
GO


--Guarda el Movimiento de Sueldo por Empleado y Mes
Create Procedure SP_GuardarMovimientosSueldos
(@NumeroEmpleado int, @CodigoRol int, @Mes int, @HorasTrabajadas int, @CantidadEntregas int, 
	@SueldoBase money, @ImportePagoPorEntregas money, @ImportePagoPorBono money, @ImporteVales money, @ISR money, @ISRAdicional money)
As
Begin
	Insert Into MovimientosSueldosMensual
	(NumeroEmpleado, CodigoRol, Mes, HorasTrabajadas, CantidadEntregas, SueldoBase, ImportePagoPorEntregas, 
		ImportePagoPorBono, ImporteVales, ISR, ISRAdicional)
	Values
	(@NumeroEmpleado, @CodigoRol, @Mes, @HorasTrabajadas, @CantidadEntregas, @SueldoBase, @ImportePagoPorEntregas, 
		@ImportePagoPorBono, @ImporteVales, @ISR, @ISRAdicional)

End
GO

--Actualiza el Movimiento de Sueldo por Empleado y Mes
Create Procedure SP_ActualizarMovimientosSueldos
(@NumeroEmpleado int, @CodigoRol int, @Mes int, @HorasTrabajadas int, @CantidadEntregas int, 
	@SueldoBase money, @ImportePagoPorEntregas money, @ImportePagoPorBono money, @ImporteVales money,
	@ISR money, @ISRAdicional money)
As
Begin

	Update MovimientosSueldosMensual
		Set HorasTrabajadas = @HorasTrabajadas,
			CantidadEntregas = @CantidadEntregas,
			SueldoBase = @SueldoBase,
			ImportePagoPorEntregas = @ImportePagoPorEntregas,
			ImportePagoPorBono = @ImportePagoPorBono,
			ImporteVales = @ImporteVales,
			ISR = @ISR,
			ISRAdicional = @ISRAdicional
	Where NumeroEmpleado = @NumeroEmpleado And CodigoRol = @CodigoRol And Mes = @Mes

End
GO

--Obtener el Movimiento de Sueldo por Numero de Empleado y Mes
Create Procedure SP_ObtenerMovimientosSueldos
(@NumeroEmpleado int, @CodigoRol int, @Mes int)
As
Begin
	Select NumeroEmpleado, CodigoRol, Mes, HorasTrabajadas, CantidadEntregas, SueldoBase, 
			ImportePagoPorEntregas, ImportePagoPorBono, ImporteVales, ISR, ISRAdicional
	From MovimientosSueldosMensual
	Where NumeroEmpleado = @NumeroEmpleado And CodigoRol = @CodigoRol And Mes = @Mes
End
GO


--Configuracion de Sueldos por Empleado
Create Procedure SP_ObtenerConfiguracionSueldosEmpleado
(@NumeroEmpleado int)
As
Begin
	Select NumeroEmpleado, SueldoBasePorHora, PagoPorEntrega, PorcentajeVales, LimiteSueldoMensual 
	From ConfiguracionSueldosEmpleados
	Where NumeroEmpleado = @NumeroEmpleado 
End
GO

--Actualiza la Configuracion de Sueldos por Empleado
Create Procedure SP_ActualizarConfiguracionSueldosEmpleado
(@NumeroEmpleado int, @SueldoBasePorHora money, @PagoPorEntrega money, @PorcentajeVales money, @LimiteSueldoMensual money)
As
Begin

	Update ConfiguracionSueldosEmpleados
		Set SueldoBasePorHora = @SueldoBasePorHora,
		    PagoPorEntrega = @PagoPorEntrega,
			PorcentajeVales = @PorcentajeVales,
			LimiteSueldoMensual = @LimiteSueldoMensual
	Where NumeroEmpleado = @NumeroEmpleado

End
GO

--Guarda la Configuracion de Sueldos por Empleado
Create Procedure SP_GuardarConfiguracionSueldosEmpleado
(@NumeroEmpleado int, @SueldoBasePorHora money, @PagoPorEntrega money, @PorcentajeVales money, @LimiteSueldoMensual money)
As
Begin

	Insert into ConfiguracionSueldosEmpleados
	(NumeroEmpleado,SueldoBasePorHora, PagoPorEntrega, PorcentajeVales, LimiteSueldoMensual)
	Values
	(@NumeroEmpleado, @SueldoBasePorHora, @PagoPorEntrega, @PorcentajeVales, @LimiteSueldoMensual)

End
GO

--Obtiene los impuestos configurados al empleado
Create Procedure SP_ObtenerConfiguracionImpuestosEmpleado
(@NumeroEmpleado int)
As
Begin
	Select CONF.NumeroEmpleado, CONF.CodigoImpuesto, IMP.Porcentaje
	From ConfiguracionImpuestosEmpleados CONF
	Inner Join Impuestos IMP
	        On CONF.CodigoImpuesto = IMP.CodigoImpuesto
	Where NumeroEmpleado = @NumeroEmpleado
End
GO

Create Procedure SP_GuardarConfiguracionImpuestosEmpleado
(@NumeroEmpleado int, @CodigoImpuesto int)
As
Begin
	Insert into ConfiguracionImpuestosEmpleados
	Values (@NumeroEmpleado, @CodigoImpuesto)
End
GO

Create Procedure SP_BorrarConfiguracionImpuestosEmpleado
(@NumeroEmpleado int)
As
Begin
	Delete From ConfiguracionImpuestosEmpleados
	Where NumeroEmpleado = @NumeroEmpleado
End
GO

Create Procedure SP_ObtenerRol
(@CodigoRol int)
As
Begin
	Select CodigoRol, Descripcion, BonoPorHora
	From Roles
	Where CodigoRol = @CodigoRol
End
GO