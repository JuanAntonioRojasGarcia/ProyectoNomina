
--Crear la base de datos
IF NOT EXISTS (SELECT name FROM master.sys.databases WHERE name = N'ProyectoDB')
Begin
	Create Database ProyectoDB	
End
Else
Begin
	print('Base [ProyectoDB] ya est� creada.')
End

--Seleccionar la base de datos
Use ProyectoDB

--Crear Tabla de Catalogo de Empleados
IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'Empleados')
Begin
	Create Table Empleados (NumeroEmpleado int primary key not null, Nombre nvarchar(50), ApellidoPaterno nvarchar(50), ApellidoMaterno nvarchar(50), CodigoRol int)
End
Else
Begin
	print('Tabla [Empleados] ya est� creada.')
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
	print('Tabla [Roles] ya est� creada.')
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
	print('Tabla [Impuestos] ya est� creada.')
End

--Crear Tabla de Configuracion de Sueldo por Empleado
IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'ConfiguracionSueldosEmpleados')
Begin
	Create Table ConfiguracionSueldosEmpleados (NumeroEmpleado int primary key not null, SueldoBasePorHora money, PagoPorEntrega money,
	                                            PorcentajeVales money, LimiteSueldoMensual money)
End
Else
Begin
	print('Tabla [ConfiguracionSueldosEmpleados] ya est� creada.')
End

--Crear Tabla de Configuracion de Impuestos por Empleado
IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'ConfiguracionImpuestosEmpleados')
Begin
	Create Table ConfiguracionImpuestosEmpleados (NumeroEmpleado int not null, CodigoImpuesto int not null,
	                                              Primary Key (NumeroEmpleado, CodigoImpuesto))
End
Else
Begin
	print('Tabla [ConfiguracionImpuestosEmpleados] ya est� creada.')
End


IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'MovimientosSueldosMensual')
Begin
	Create Table MovimientosSueldosMensual (NumeroEmpleado int not null, CodigoRol int not null, Mes int not null, HorasTrabajadas int, 
	                                        CantidadEntregas int, SalarioBase money, ImportePagoPorEntregas money, ImportePagoPorBono money, 
											ImporteVales money, ISR money, ISRAdicional money, Primary Key (NumeroEmpleado, CodigoRol, Mes))
End
Else
Begin
	print('Tabla [MovimientosSueldosMensual] ya est� creada.')
End


--Procedimiento para Guardar o Actualizar Empleados
Create Procedure SP_GuardarEmpleado
(@NumeroEmpleado int, @Nombre nvarchar(50), @ApellidoPaterno nvarchar(50),  @ApellidoMaterno nvarchar(50), @CodigoRol int)
As
Begin

	Insert into Empleados
	(NumeroEmpleado, Nombre, ApellidoPaterno, ApellidoMaterno, CodigoRol)
	Values
	(@NumeroEmpleado, @Nombre, @ApellidoPaterno, @ApellidoMaterno, @CodigoRol)

End

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

--Procedimiento para Obtener los datos del Empleado por NumeroEmpleado
Create Procedure SP_ObtenerEmpleado
(@NumeroEmpleado int)
As
Begin
	Select NumeroEmpleado, Nombre, ApellidoPaterno, ApellidoMaterno, CodigoRol
	From Empleados 
	Where NumeroEmpleado = @NumeroEmpleado
End


--Guarda el Movimiento de Sueldo por Empleado y Mes
Create Procedure SP_GuardarMovimientosSueldos
(@NumeroEmpleado int, @CodigoRol int, @Mes int, @HorasTrabajadas int, @CantidadEntregas int, 
 @SalarioBase money, @ImportePagoPorEntregas money, @ImportePagoPorBono money, @ImporteVales money, @ISR money, @ISRAdicional money)
As
Begin
	Insert Into MovimientosSueldosMensual
	(NumeroEmpleado, CodigoRol, Mes, HorasTrabajadas, CantidadEntregas, SalarioBase, ImportePagoPorEntregas, 
	 ImportePagoPorBono, ImporteVales, ISR, ISRAdicional)
	Values
	(@NumeroEmpleado, @CodigoRol, @Mes, @HorasTrabajadas, @CantidadEntregas, @SalarioBase, @ImportePagoPorEntregas, 
	 @ImportePagoPorBono, @ImporteVales, @ISR, @ISRAdicional)

End

--Actualiza el Movimiento de Sueldo por Empleado y Mes
Create Procedure SP_ActualizarMovimientosSueldos
(@NumeroEmpleado int, @CodigoRol int, @Mes int, @HorasTrabajadas int, @CantidadEntregas int, 
 @SalarioBase money, @ImportePagoPorEntregas money, @ImportePagoPorBono money, @ImporteVales money,
 @ISR money, @ISRAdicional money)
As
Begin

	Update MovimientosSueldosMensual
		Set HorasTrabajadas = @HorasTrabajadas,
		    CantidadEntregas = @CantidadEntregas,
			SalarioBase = @SalarioBase,
			ImportePagoPorEntregas = @ImportePagoPorEntregas,
			ImportePagoPorBono = @ImportePagoPorBono,
			ImporteVales = @ImporteVales,
			ISR = @ISR,
			ISRAdicional = @ISRAdicional
	Where NumeroEmpleado = @NumeroEmpleado And CodigoRol = @CodigoRol And Mes = @Mes

End

--Obtener el Movimiento de Sueldo por Numero de Empleado y Mes
Create Procedure SP_ObtenerMovimientosSueldosMensual
(@NumeroEmpleado int, @CodigoRol int, @Mes int)
As
Begin
	Select NumeroEmpleado, CodigoRol, Mes, HorasTrabajadas, CantidadEntregas, SalarioBase, 
	       ImportePagoPorEntregas, ImportePagoPorBono, ImporteVales, ISR, ISRAdicional
	From MovimientosSueldosMensual
	Where NumeroEmpleado = @NumeroEmpleado And CodigoRol = @CodigoRol And Mes = @Mes
End

--Configuracion de Sueldos por Empleado
Create Procedure SP_ObtenerConfiguracionSueldosEmpleado
(@NumeroEmpleado int)
As
Begin
	Select NumeroEmpleado, SueldoBasePorHora, PagoPorEntrega, PorcentajeVales, LimiteSueldoMensual 
	From ConfiguracionSueldosEmpleados
	Where NumeroEmpleado = @NumeroEmpleado 
End

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

--Obtiene los impuestos configurados al empleado
Create Procedure SP_ObtenerConfiguracionImpuestosEmpleado
(@NumeroEmpleado int)
As
Begin
	Select NumeroEmpleado, CodigoImpuesto
	From ConfiguracionImpuestosEmpleados
	Where NumeroEmpleado = @NumeroEmpleado
End

Create Procedure SP_GuardarConfiguracionImpuestosEmpleado
(@NumeroEmpleado int, @CodigoImpuesto int)
As
Begin
	Insert into ConfiguracionImpuestosEmpleados
	Values (@NumeroEmpleado, @CodigoImpuesto)
End

Create Procedure SP_BorrarConfiguracionImpuestosEmpleado
(@NumeroEmpleado int)
As
Begin
	Delete From ConfiguracionImpuestosEmpleados
	Where NumeroEmpleado = @NumeroEmpleado
End


Create Procedure SP_GuardarMovimientoSueldoMensual
(@NumeroEmpleado int, @CodigoRol int, @Mes int, @HorasTrabajadas int, @CantidadEntregas int, @SalarioBase money,
 @ImportePagoPorEntregas money, @ImportePagoPorBono money, @ImporteVales money, @ISR money, @ISRAdicional money)
As
Begin
	Insert into MovimientosSueldosMensual
	(NumeroEmpleado, CodigoRol, Mes, HorasTrabajadas, CantidadEntregas, SalarioBase,
     ImportePagoPorEntregas, ImportePagoPorBono, ImporteVales, ISR, ISRAdicional)
	Values
	(@NumeroEmpleado, @CodigoRol, @Mes, @HorasTrabajadas, @CantidadEntregas, @SalarioBase,
    @ImportePagoPorEntregas, @ImportePagoPorBono, @ImporteVales, @ISR, @ISRAdicional)
End

Create Procedure SP_ActualizarMovimientoSueldoMensual
(@NumeroEmpleado int, @CodigoRol int, @Mes int, @HorasTrabajadas int, @CantidadEntregas int, @SalarioBase money,
 @ImportePagoPorEntregas money, @ImportePagoPorBono money, @ImporteVales money, @ISR money, @ISRAdicional money)
As
Begin
	Update MovimientosSueldosMensual
	   Set HorasTrabajadas = @HorasTrabajadas,
	       CantidadEntregas = @CantidadEntregas,
		   SalarioBase = @SalarioBase,
		   ImportePagoPorEntregas = @ImportePagoPorEntregas,
		   ImportePagoPorBono = @ImportePagoPorBono ,
		   ImporteVales = @ImporteVales,
		   ISR = @ISR,
		   ISRAdicional = @ISRAdicional
	Where NumeroEmpleado = @NumeroEmpleado And CodigoRol = @CodigoRol And Mes = @Mes
End

Create Procedure SP_ObtenerMovimientoSueldoMensual
(@NumeroEmpleado int, @CodigoRol int, @Mes int)
As
Begin
	Select NumeroEmpleado, CodigoRol, Mes, HorasTrabajadas, CantidadEntregas, SalarioBase,
           ImportePagoPorEntregas, ImportePagoPorBono, ImporteVales, ISR, ISRAdicional 
	From MovimientosSueldosMensual
	Where NumeroEmpleado = @NumeroEmpleado And CodigoRol = @CodigoRol And Mes = @Mes
End


Create Procedure SP_ObtenerRol
(@CodigoRol int)
As
Begin
	Select CodigoRol, Descripcion, BonoPorHora
	From Roles
	Where CodigoRol = @CodigoRol
End