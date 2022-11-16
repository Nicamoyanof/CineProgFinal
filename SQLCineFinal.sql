create database CINE1_0

USE CINE1_0
SELECT * FROM EMPLEADOS 

create table Paises (
id_pais int identity,
nombre_pais varchar(50)
constraint pk_id_pais primary key (id_pais)
)

create table Usuarios(
id_usuario int identity,
nom_usuario varchar(250),
contraseña varchar(250)
constraint pk_usuario primary key (id_usuario)
)

create table Provincias(
id_provincia int identity,
nombre_provincia varchar(50),
id_pais int
constraint pk_id_provincia primary key (id_provincia)
constraint fk_id_pais foreign key (id_pais)
references Paises (id_pais)
)

create table Ciudades(
id_ciudad int identity,
nombre_ciudad varchar(50),
id_provincia int
constraint pk_id_ciudad primary key (id_ciudad)
constraint fk_id_provincia foreign key (id_provincia)
references Provincias (id_provincia)
)

create table Clientes (
id_cliente int identity,
nombre varchar(50),
id_ciudad int,

email varchar(50),
fecha_nac datetime,
socio bit
constraint pk_id_cliente primary key (id_cliente)
constraint fk_id_ciudad foreign key (id_ciudad)
references Ciudades (id_ciudad)
)

create table Tipos_cargos(
id_tipo_cargo int identity,
nombre_cargo varchar(50),
descripcion_cargo varchar(250)
constraint pk_id_tipo_cargo primary key (id_tipo_cargo)
)

create table Empleados(
id_empleado int identity,
nombre_empleado varchar(50),
id_ciudad int,
id_tipo_cargo int,

fecha_ingreso datetime,
telefono varchar(50),
cuil varchar(50),
fecha_nac datetime
constraint pk_id_empleado primary key(id_empleado)
constraint fk_id_ciudad_empleado foreign key (id_ciudad)
references ciudades (id_ciudad),
constraint fk_id_tipo_cargo foreign key (id_tipo_cargo)
references Tipos_cargos (id_tipo_cargo)
)

create table Generos_peliculas(
id_genero_pelicula int identity,
nombre_genero varchar(50),
descripcion_genero varchar(500)
constraint pk_id_genero_pelicula primary key (id_genero_pelicula)
)
create table Edades_permitidas(
id_edad_permitida int,
nombre_edad varchar(50),
minimo_edad int
constraint pk_id_edad_permitida primary key (id_edad_permitida)
)



create table Asientos(
id_asiento int identity,
numero_asiento int ,
disponible bit
constraint pk_id_asiento primary key (id_asiento)
)

create table Tipos_Salas(
id_tipo_sala int identity,
nombre_tipo_sala varchar(50),
descripcion_tipo_sala varchar(50),
precio_sala float
constraint pk_id_tipo_sala primary key(id_tipo_sala)
)

create table Salas(
id_sala int identity,
numero_sala int,
id_tipo_sala int
constraint pk_id_sala primary key (id_sala)
constraint fk_id_tipo_sala foreign key(id_tipo_sala)
references Tipos_Salas(id_tipo_sala)
)

create table Asientos_Por_Salas(
id_asiento_sala int identity,
id_sala int,
id_asiento int
constraint pk_id_asiento_sala primary key (id_asiento_sala)
constraint fk_id_sala_asiento foreign key (id_sala)
references Salas(id_sala),
constraint fk_id_asiento_sala foreign key(id_asiento)
references Asientos(id_asiento)
)


create table Tipos_Pagos(
id_tipo_pago int identity,
nombre_tipo_pago varchar(50),
constraint fk_id_tipo_pago primary key (id_tipo_pago)
)

create table Peliculas(
id_pelicula int identity,
nombre_pelicula varchar(50),
id_edad_permitida int,
id_genero_pelicula int,
descripcion_pelicula varchar(500),
nombre_imagen varchar(250)
constraint pk_id_pelicula primary key (id_pelicula)
constraint fk_id_gender_pelicula foreign key (id_genero_pelicula)
references Generos_peliculas (id_genero_pelicula),
constraint fk_id_edad_permitida foreign key (id_edad_permitida)
references Edades_permitidas (id_edad_permitida)
)

create table Funciones(
id_funcion int identity,
id_pelicula int,
id_sala int,
horario datetime
constraint pk_id_funcion primary key(id_funcion)
constraint fk_id_pelicula foreign key(id_pelicula)
references Peliculas(id_pelicula),
constraint fk_id_salas_funcion foreign key(id_sala)
references Salas(id_sala)
)

create table Descuentos(
id_descuento int identity, 
descripcion_descuento varchar(250),
valor_descuento float
constraint pk_id_descuento primary key(id_descuento)
)


create table Reservas(
id_reserva int identity,
id_funcion int,
id_cliente int,
fecha_reserva datetime,
hora_confirmacion datetime
constraint pk_id_reserva primary key(id_reserva)
constraint fk_id_funcion_reserva foreign key(id_funcion)
references Funciones (id_funcion),
constraint fk_id_cliente_reserva foreign key(id_cliente)
references Clientes (id_cliente)
)


create table Detalles_Reservas(
id_detalle_reserva int identity,
id_reserva int,
precio_venta float,
id_descuento int,
id_asiento_sala int 
constraint pk_id_detalle_reserva primary key(id_detalle_reserva)
constraint fk_id_reserva foreign key(id_reserva)
references Reservas(id_reserva),
constraint fk_asiento_sala_reserva foreign key(id_asiento_sala)
references Asientos_Por_Salas(id_asiento_sala),
constraint fk_id_descuento_reserva foreign key(id_descuento)
references Descuentos(id_descuento)
)


create table Tickets(
id_ticket int identity,
id_reserva int,
id_funcion int,
id_empleado int,
id_cliente int,
fecha_ticket datetime
constraint pk_id_ticket primary key(id_ticket)
constraint fk_id_reservaT foreign key(id_reserva)
references Reservas(id_reserva),
constraint fk_id_funcion foreign key(id_funcion)
references Funciones (id_funcion),
constraint fk_id_empleado_funcion foreign key(id_empleado)
references Empleados (id_empleado),
constraint fk_id_cliente foreign key(id_cliente)
references Clientes (id_cliente)
)


create table Detalles_Tickets(
id_detalle_ticket int IDENTITY,
id_ticket int,
id_tipo_pago int,
precio_venta float,
id_descuento int,
id_asiento_sala int 
constraint pk_id_detalle_ticket primary key (id_detalle_ticket)
constraint fk_id_ticket foreign key(id_ticket)
references Tickets(id_ticket),
constraint fk_id_tipo_pago_ticket foreign key(id_tipo_pago)
references Tipos_Pagos(id_tipo_pago),
constraint fk_asiento_sala_ticket foreign key(id_asiento_sala)
references Asientos_Por_Salas(id_asiento_sala),
constraint fk_id_descuento_ticket foreign key(id_descuento)
references Descuentos(id_descuento)
)



INSERT INTO Asientos (numero_asiento)
			VALUES 
			(1),
			(2),	
			(3),
			(4),
			(5),
			(6),
			(7),
			(8),
			(9),
			(10),
			(11),
			(12),
			(13),
			(14),
			(15),
			(16),
			(17),
			(18),
			(19),
			(20)


INSERT INTO Tipos_Salas (nombre_tipo_sala, descripcion_tipo_sala, precio_sala)
		VALUES ('2D', 'Dos dimensiones', 940),
		('3D', 'Tres dimensiones', 1060)
		


INSERT INTO Salas (numero_sala, id_tipo_sala)
		VALUES (1,1),
		(2,1),
		(3,1),
		(4,2),
		(5,2)
		
INSERT INTO Asientos_Por_Salas (id_sala, id_asiento)
		VALUES (1,1),
		(1,2),
		(1,3),
		(1,4),
		(1,5),
		(1,6),
		(1,7),
		(1,8),
		(1,9),
		(1,10),
		(1,11),
		(1,12),
		(1,13),
		(1,14),
		(1,15),
		(1,16),
		(1,17),
		(1,18),
		(1,19),
		(1,20),
		(2,1),
		(2,2),
		(2,3),
		(2,4),
		(2,5),
		(2,6),
		(2,7),
		(2,8),
		(2,9),
		(2,10),
		(2,11),
		(2,12),
		(2,13),
		(2,14),
		(2,15),
		(2,16),
		(2,17),
		(2,18),
		(2,19),
		(2,20),
		(3,1),
		(3,2),
		(3,3),
		(3,4),
		(3,5),
		(3,6),
		(3,7),
		(3,8),
		(3,9),
		(3,10),
		(3,11),
		(3,12),
		(3,13),
		(3,14),
		(3,15),
		(3,16),
		(3,17),
		(3,18),
		(3,19),
		(3,20),
		(4,1),
		(4,2),
		(4,3),
		(4,4),
		(4,5),
		(4,6),
		(4,7),
		(4,8),
		(4,9),
		(4,10),
		(4,11),
		(4,12),
		(4,13),
		(4,14),
		(4,15),
		(4,16),
		(4,17),
		(4,18),
		(4,19),
		(4,20),
		(5,1),
		(5,2),
		(5,3),
		(5,4),
		(5,5),
		(5,6),
		(5,7),
		(5,8),
		(5,9),
		(5,10),
		(5,11),
		(5,12),
		(5,13),
		(5,14),
		(5,15),
		(5,16),
		(5,17),
		(5,18),
		(5,19),
		(5,20)




--Insertar países, provincias, ciudades, tipos de cargos, empleados, clientes
set dateformat dmy

--1) Insert países
insert into paises(nombre_pais) values ('Argentina')
insert into paises(nombre_pais) values ('Brasil')
insert into paises(nombre_pais) values ('Chile')
insert into paises(nombre_pais) values ('Paraguay')
insert into paises(nombre_pais) values ('Bolivia')
insert into paises(nombre_pais) values ('Peru')
insert into paises(nombre_pais) values ('Venezuela')
insert into paises(nombre_pais) values ('Ecuador')
insert into paises(nombre_pais) values ('Colombia')
insert into paises(nombre_pais) values ('Uruguay')
insert into paises(nombre_pais) values ('El salvador')
insert into paises(nombre_pais) values ('Mexico')
insert into paises(nombre_pais) values ('Estados Unidos')
insert into paises(nombre_pais) values ('Costa Rica')

--2) Insert provincias
--Argentina
insert into Provincias(id_pais, nombre_provincia) values(1, 'Buenos Aires')
insert into Provincias(id_pais, nombre_provincia) values(1, 'Ciudad Autónoma de Buenos Aires')
insert into Provincias(id_pais, nombre_provincia) values(1, 'Catamarca')
insert into Provincias(id_pais, nombre_provincia) values(1, 'Chaco')
insert into Provincias(id_pais, nombre_provincia) values(1, 'Chubut')
insert into Provincias(id_pais, nombre_provincia) values(1, 'Córdoba')
insert into Provincias(id_pais, nombre_provincia) values(1, 'Corrientes')
insert into Provincias(id_pais, nombre_provincia) values(1, 'Entre Ríos')
insert into Provincias(id_pais, nombre_provincia) values(1, 'Formosa')
insert into Provincias(id_pais, nombre_provincia) values(1, 'Jujuy')
insert into Provincias(id_pais, nombre_provincia) values(1, 'La Pampa')
insert into Provincias(id_pais, nombre_provincia) values(1, 'La Rioja')
insert into Provincias(id_pais, nombre_provincia) values(1, 'Mendoza')
insert into Provincias(id_pais, nombre_provincia) values(1, 'Misiones')
insert into Provincias(id_pais, nombre_provincia) values(1, 'Neuquén')
insert into Provincias(id_pais, nombre_provincia) values(1, 'Río Negro')
insert into Provincias(id_pais, nombre_provincia) values(1, 'Salta')
insert into Provincias(id_pais, nombre_provincia) values(1, 'San Juan')
insert into Provincias(id_pais, nombre_provincia) values(1, 'San Luis')
insert into Provincias(id_pais, nombre_provincia) values(1, 'Santa Cruz')
insert into Provincias(id_pais, nombre_provincia) values(1, 'Santa Fe')
insert into Provincias(id_pais, nombre_provincia) values(1, 'Santiago del Estero')
insert into Provincias(id_pais, nombre_provincia) values(1, 'Tierra del Fuego')
insert into Provincias(id_pais, nombre_provincia) values(1, 'Tucumán')
--Brasil
insert into Provincias(id_pais, nombre_provincia) values(2, 'Río de Janeiro')
insert into Provincias(id_pais, nombre_provincia) values(2, 'Río Grande del Norte')
insert into Provincias(id_pais, nombre_provincia) values(2, 'Río Grande del Sur')
insert into Provincias(id_pais, nombre_provincia) values(2, 'São Paulo')
insert into Provincias(id_pais, nombre_provincia) values(2, 'Ceará')
--Chile
insert into Provincias(id_pais, nombre_provincia) values(3, 'Santiago')
insert into Provincias(id_pais, nombre_provincia) values(3, 'La Serena')
insert into Provincias(id_pais, nombre_provincia) values(3, 'Valdivia')
--Paraguay
insert into Provincias(id_pais, nombre_provincia) values(4, 'Alto Paraguay')
insert into Provincias(id_pais, nombre_provincia) values(4, ' Alto Paraná')
insert into Provincias(id_pais, nombre_provincia) values(4, 'Concepción')
--Bolivia
insert into Provincias(id_pais, nombre_provincia) values(5, 'La Paz')
insert into Provincias(id_pais, nombre_provincia) values(5, 'Oruro')
insert into Provincias(id_pais, nombre_provincia) values(5, 'Potosí')
--Peru
insert into Provincias(id_pais, nombre_provincia) values(6, 'Lima')
insert into Provincias(id_pais, nombre_provincia) values(6, 'Amazonas')
insert into Provincias(id_pais, nombre_provincia) values(6, 'Tacna')
--Venezuela
insert into Provincias(id_pais, nombre_provincia) values(7, 'Caracas')
insert into Provincias(id_pais, nombre_provincia) values(7, 'Aragua')
insert into Provincias(id_pais, nombre_provincia) values(7, 'Barquisimeto')
--Ecuador
insert into Provincias(id_pais, nombre_provincia) values(8, 'Bolivar')
insert into Provincias(id_pais, nombre_provincia) values(8, 'Cañar')
insert into Provincias(id_pais, nombre_provincia) values(8, 'Chimborazo')
--Colombia
insert into Provincias(id_pais, nombre_provincia) values(9, 'Antioquia')
insert into Provincias(id_pais, nombre_provincia) values(9, 'Atlántico')
insert into Provincias(id_pais, nombre_provincia) values(9, 'Bolívar')
--Uruguay
insert into Provincias(id_pais, nombre_provincia) values(10, 'Cerro Largo')
insert into Provincias(id_pais, nombre_provincia) values(10, 'Colonia')
insert into Provincias(id_pais, nombre_provincia) values(10, 'Durazno')
--El salvador
insert into Provincias(id_pais, nombre_provincia) values(11, 'La Libertad')
insert into Provincias(id_pais, nombre_provincia) values(11, 'Cuscatlán')
insert into Provincias(id_pais, nombre_provincia) values(11, 'La Unión')
--Mexico
insert into Provincias(id_pais, nombre_provincia) values(12, 'Las Californias')
insert into Provincias(id_pais, nombre_provincia) values(12, 'México')
insert into Provincias(id_pais, nombre_provincia) values(12, 'Nuevo México')
--Estados Unidos
insert into Provincias(id_pais, nombre_provincia) values(13, 'California')
insert into Provincias(id_pais, nombre_provincia) values(13, 'Florida')
insert into Provincias(id_pais, nombre_provincia) values(13, 'Washington')
--Costa Rica
insert into Provincias(id_pais, nombre_provincia) values(14, 'San José')
insert into Provincias(id_pais, nombre_provincia) values(14, 'Alajuela')
insert into Provincias(id_pais, nombre_provincia) values(14, 'Heredia')

--3) Insert ciudades
--Argentina
--Buenos Aires
insert into ciudades(nombre_ciudad, id_provincia) values ('Capital', 1)
insert into ciudades(nombre_ciudad, id_provincia) values ('Carlos Casares', 1)
insert into ciudades(nombre_ciudad, id_provincia) values ('Caseros', 1)
--Ciudad Autonoma De Buenos Aires
insert into ciudades(nombre_ciudad, id_provincia) values ('Almagro', 2)
insert into ciudades(nombre_ciudad, id_provincia) values ('Balvanera', 2)
insert into ciudades(nombre_ciudad, id_provincia) values ('Belgrano', 2)
--Catamarca
insert into ciudades(nombre_ciudad, id_provincia) values ('San Fernando del Valle de Catamarca', 3)
insert into ciudades(nombre_ciudad, id_provincia) values ('Santa María', 3)
insert into ciudades(nombre_ciudad, id_provincia) values ('Antofagasta de la Sierra', 3)
--Chaco
insert into ciudades(nombre_ciudad, id_provincia) values ('Resistencia', 4)
insert into ciudades(nombre_ciudad, id_provincia) values ('General José de San Martín', 4)
insert into ciudades(nombre_ciudad, id_provincia) values ('Juan José Castelli', 4)
--Chubut
insert into ciudades(nombre_ciudad, id_provincia) values ('Rawson', 5)
insert into ciudades(nombre_ciudad, id_provincia) values ('Puerto Madryn', 5)
insert into ciudades(nombre_ciudad, id_provincia) values ('Trelew', 5)
--Cordoba 
insert into ciudades(nombre_ciudad, id_provincia) values ('Capital', 6)
insert into ciudades(nombre_ciudad, id_provincia) values ('Carlos Paz', 6)
insert into ciudades(nombre_ciudad, id_provincia) values ('Alta Gracia', 6)
--Corrientes 
insert into ciudades(nombre_ciudad, id_provincia) values ('Bella Vista', 7)
insert into ciudades(nombre_ciudad, id_provincia) values ('Berón de Astrada', 7)
insert into ciudades(nombre_ciudad, id_provincia) values ('Bonpland', 7)
--Entre Rios
insert into ciudades(nombre_ciudad, id_provincia) values ('Parana', 8)
insert into ciudades(nombre_ciudad, id_provincia) values ('Colón', 8)
insert into ciudades(nombre_ciudad, id_provincia) values ('Gualeguaychu', 8)
--Formosa
insert into ciudades(nombre_ciudad, id_provincia) values ('El colorado', 9)
insert into ciudades(nombre_ciudad, id_provincia) values ('El espinillo', 9)
insert into ciudades(nombre_ciudad, id_provincia) values ('Formosa', 9)
--Jujuy
insert into ciudades(nombre_ciudad, id_provincia) values ('San Salvador de Jujuy', 10)
insert into ciudades(nombre_ciudad, id_provincia) values ('Humahuaca', 10)
insert into ciudades(nombre_ciudad, id_provincia) values ('Tilcara', 10)
--La pampa
insert into ciudades(nombre_ciudad, id_provincia) values ('Ciudad de Santa Rosa', 11)
insert into ciudades(nombre_ciudad, id_provincia) values ('Toay', 11)
insert into ciudades(nombre_ciudad, id_provincia) values ('General Pico', 11)
--La rioja
insert into ciudades(nombre_ciudad, id_provincia) values ('La Rioja', 12)
insert into ciudades(nombre_ciudad, id_provincia) values ('Chilecito', 12)
insert into ciudades(nombre_ciudad, id_provincia) values ('Famatina', 12)
--Mendoza
insert into ciudades(nombre_ciudad, id_provincia) values ('General Alvear', 13)
insert into ciudades(nombre_ciudad, id_provincia) values ('Godoy Cruz', 13)
insert into ciudades(nombre_ciudad, id_provincia) values ('Junín', 13)
--Misiones
insert into ciudades(nombre_ciudad, id_provincia) values ('Posadas', 14)
insert into ciudades(nombre_ciudad, id_provincia) values ('Puerto Iguazú', 14)
insert into ciudades(nombre_ciudad, id_provincia) values ('Oberá', 14)
--Neuquen
insert into ciudades(nombre_ciudad, id_provincia) values ('Centenario', 15)
insert into ciudades(nombre_ciudad, id_provincia) values ('Plottier', 15)
insert into ciudades(nombre_ciudad, id_provincia) values ('Neuquén', 15)
--Rio negro
insert into ciudades(nombre_ciudad, id_provincia) values ('San Carlos de Bariloche', 16)
insert into ciudades(nombre_ciudad, id_provincia) values ('Viedma', 16)
insert into ciudades(nombre_ciudad, id_provincia) values ('Cipolletti', 16)
--Salta
insert into ciudades(nombre_ciudad, id_provincia) values ('La Viña', 17)
insert into ciudades(nombre_ciudad, id_provincia) values ('Salta', 17)
insert into ciudades(nombre_ciudad, id_provincia) values ('Chaco', 17)
--San Juan
insert into ciudades(nombre_ciudad, id_provincia) values ('Chimbas', 18)
insert into ciudades(nombre_ciudad, id_provincia) values ('Rawson', 18)
insert into ciudades(nombre_ciudad, id_provincia) values ('Rivadavia', 18)
--San Luis
insert into ciudades(nombre_ciudad, id_provincia) values ('San Luis', 19)
insert into ciudades(nombre_ciudad, id_provincia) values ('Villa Mercedes', 19)
insert into ciudades(nombre_ciudad, id_provincia) values ('Merlo', 19)
--Santa cruz
insert into ciudades(nombre_ciudad, id_provincia) values ('Rio Gallegos', 20)
insert into ciudades(nombre_ciudad, id_provincia) values ('El calafate', 20)
insert into ciudades(nombre_ciudad, id_provincia) values ('Perito Moreno', 20)
--Santa fe
insert into ciudades(nombre_ciudad, id_provincia) values ('Frontera', 21)
insert into ciudades(nombre_ciudad, id_provincia) values ('Funez', 21)
insert into ciudades(nombre_ciudad, id_provincia) values ('Galvez', 21)
--Santiago del estero
insert into ciudades(nombre_ciudad, id_provincia) values ('La Banda', 22)
insert into ciudades(nombre_ciudad, id_provincia) values ('Frías', 22)
insert into ciudades(nombre_ciudad, id_provincia) values ('Añatuya', 22)
--Tucuman
insert into ciudades(nombre_ciudad, id_provincia) values ('San Miguel de Tucumán', 24)
insert into ciudades(nombre_ciudad, id_provincia) values ('Yerba Buena', 24)
insert into ciudades(nombre_ciudad, id_provincia) values ('Famaillá', 24)
--Tierra del fuego
insert into ciudades(nombre_ciudad, id_provincia) values ('Ushuaia', 26)
insert into ciudades(nombre_ciudad, id_provincia) values ('Rio Grande', 26)
insert into ciudades(nombre_ciudad, id_provincia) values ('Porvenir', 26)
--Otras provincias
insert into ciudades(nombre_ciudad, id_provincia) values ('Rio de janeiro', 27)
insert into ciudades(nombre_ciudad, id_provincia) values ('Río Grande del Norte', 28)
insert into ciudades(nombre_ciudad, id_provincia) values ('Río Grande del Sur', 29)
insert into ciudades(nombre_ciudad, id_provincia) values ('São Paulo', 30)
insert into ciudades(nombre_ciudad, id_provincia) values ('Ceará', 31)
insert into ciudades(nombre_ciudad, id_provincia) values ('Santiago', 32)
insert into ciudades(nombre_ciudad, id_provincia) values ('La Serena', 33)
insert into ciudades(nombre_ciudad, id_provincia) values ('Valdivia', 34)



--4) Insert tipos de cargos
insert into Tipos_cargos(nombre_cargo, descripcion_cargo) values('Cajero','Recepcionar, entregar y custodiar dinero')
insert into Tipos_cargos(nombre_cargo, descripcion_cargo) values('Limpieza','Barrer, aspirar, lavar y pulir pisos')
insert into Tipos_cargos(nombre_cargo, descripcion_cargo) values('Community Manager','construir, ampliar y administrar comunidades online')
insert into Tipos_cargos(nombre_cargo, descripcion_cargo) values('Encargado de sala',' planifica y organiza el servicio')
insert into Tipos_cargos(nombre_cargo, descripcion_cargo) values('Proyectista','revelado de películas')
insert into Tipos_cargos(nombre_cargo, descripcion_cargo) values('Administrador','tareas administrativas y de oficina')
insert into Tipos_cargos(nombre_cargo, descripcion_cargo) values('Seguridad','Vigilar y proteger bienes muebles e inmuebles, personas')
insert into Tipos_cargos(nombre_cargo, descripcion_cargo) values('Sonidista','responsable de la calidad técnica y artística del sonido')
insert into Tipos_cargos(nombre_cargo, descripcion_cargo) values('Iluminador','orquestar todos los recursos luminosos')
insert into Tipos_cargos(nombre_cargo, descripcion_cargo) values('Técnico en Visuales','Conocer, interpretar, crear y producir tecnicas')
insert into Tipos_cargos(nombre_cargo, descripcion_cargo) values('Atención al cliente','soporte al consumidor')
insert into Tipos_cargos(nombre_cargo, descripcion_cargo) values('Buffet','Venta de snacks y bebidas')
insert into Tipos_cargos(nombre_cargo, descripcion_cargo) values('Encargado','se responsabiliza de que los trabajadores estén coordinados')
insert into Tipos_cargos(nombre_cargo, descripcion_cargo) values('Call Center', 'gestionar las llamadas telefónicas')
insert into Tipos_cargos(nombre_cargo, descripcion_cargo) values('Publicista', 'Gestionar y crear pautas publicitarias')
insert into Tipos_cargos(nombre_cargo, descripcion_cargo) values('Gerente', 'Autoridad maxima')

--5)Insert Empleados
insert into Empleados(nombre_empleado,id_ciudad,id_tipo_cargo,fecha_ingreso,telefono,cuil,fecha_nac)
values('Sebastian Sanchez',4,3,'03/05/2005', 3514448994, 27118444568,'17/08/1964')
insert into Empleados(nombre_empleado,id_ciudad,id_tipo_cargo,fecha_ingreso,telefono,cuil,fecha_nac)
values('Miguel Perez',6,7,'07/06/2017', 3514556954, 2144566879,'22/12/1988')
insert into Empleados(nombre_empleado,id_ciudad,id_tipo_cargo,fecha_ingreso,telefono,cuil,fecha_nac)
values('Matias Rodriguez',18,3,'25/06/2009', 488645298, 27114487759,'14/02/1972')
insert into Empleados(nombre_empleado,id_ciudad,id_tipo_cargo,fecha_ingreso,telefono,cuil,fecha_nac)
values('Miguel Sanchez',25,3,'28/03/2014', 156447889, 28441555698,'15/01/1987')
insert into Empleados(nombre_empleado,id_ciudad,id_tipo_cargo,fecha_ingreso,telefono,cuil,fecha_nac)
values('Santiago Rolan',4,1,'19/03/2015', 157448998, 9911544478,'17/08/1982')
insert into Empleados(nombre_empleado,id_ciudad,id_tipo_cargo,fecha_ingreso,telefono,cuil,fecha_nac)
values('Rocio Menendez',6,1,'20/04/2011', 3514789556, 2711455638,'24/08/1996')
insert into Empleados(nombre_empleado,id_ciudad,id_tipo_cargo,fecha_ingreso,telefono,cuil,fecha_nac)
values('Silvana Rojas',6,1,'25/07/2005', 348556889, 2277895686,'27/11/1986')
insert into Empleados(nombre_empleado,id_ciudad,id_tipo_cargo,fecha_ingreso,telefono,cuil,fecha_nac)
values('Belen Migueles',35,2,'28/11/2004', 5668789, 27268874561,'14/12/1999')
insert into Empleados(nombre_empleado,id_ciudad,id_tipo_cargo,fecha_ingreso,telefono,cuil,fecha_nac)
values('Claudia Lopez',7,2,'05/05/2005', 3516284759, 27114852,'13/05/2007')
insert into Empleados(nombre_empleado,id_ciudad,id_tipo_cargo,fecha_ingreso,telefono,cuil,fecha_nac)
values('Javier Barbero',14,2,'06/07/1986', 351244788, 2045561188,'25/05/1972')
insert into Empleados(nombre_empleado,id_ciudad,id_tipo_cargo,fecha_ingreso,telefono,cuil,fecha_nac)
values('Adrian Lopez',2,4,'06/06/1992', 351622488, 272555441,'17/03/1976')
insert into Empleados(nombre_empleado,id_ciudad,id_tipo_cargo,fecha_ingreso,telefono,cuil,fecha_nac)
values('Irene Sanchez',5,4,'15/08/1978', 3514789333, 274448982,'25/06/1988')
insert into Empleados(nombre_empleado,id_ciudad,id_tipo_cargo,fecha_ingreso,telefono,cuil,fecha_nac)
values('Franco Suarez',56,4,'20/07/1986', 4885462, 2145556689,'01/03/1996')
insert into Empleados(nombre_empleado,id_ciudad,id_tipo_cargo,fecha_ingreso,telefono,cuil,fecha_nac)
values('Facundo Cavalli',1,5,'06/12/2004', 3512441897, 274587569,'17/12/1985')
insert into Empleados(nombre_empleado,id_ciudad,id_tipo_cargo,fecha_ingreso,telefono,cuil,fecha_nac)
values('Kevin Romero',7,5,'15/05/2020', 351628345, 27224448982,'25/07/2000')
insert into Empleados(nombre_empleado,id_ciudad,id_tipo_cargo,fecha_ingreso,telefono,cuil,fecha_nac)
values('Nicolas Lopez',12,5,'27/03/2012', 45688972, 22275546638,'01/04/1996')
insert into Empleados(nombre_empleado,id_ciudad,id_tipo_cargo,fecha_ingreso,telefono,cuil,fecha_nac)
values('Sandra Gomez',75,6,'25/04/2018', 3564448995, 2711166938,'15/06/1975')
insert into Empleados(nombre_empleado,id_ciudad,id_tipo_cargo,fecha_ingreso,telefono,cuil,fecha_nac)
values('Miguel Lascano',32,6,'27/04/2007', 358779825, 22456897418,'17/08/1972')
insert into Empleados(nombre_empleado,id_ciudad,id_tipo_cargo,fecha_ingreso,telefono,cuil,fecha_nac)
values('Micaela Dominguez',9,6,'13/06/1998', 45568972, 255554187459,'22/08/1978')
insert into Empleados(nombre_empleado,id_ciudad,id_tipo_cargo,fecha_ingreso,telefono,cuil,fecha_nac)
values('Jorge Zorzal',17,7,'27/03/2005', 3516668953, 274555688898,'22/05/1965')
insert into Empleados(nombre_empleado,id_ciudad,id_tipo_cargo,fecha_ingreso,telefono,cuil,fecha_nac)
values('Lucia Menendez',24,7,'05/06/2011', 3514789633, 24411145598,'14/08/1990')
insert into Empleados(nombre_empleado,id_ciudad,id_tipo_cargo,fecha_ingreso,telefono,cuil,fecha_nac)
values('Lucas Juarez',55,8,'26/04/2017', 35147896332, 2744456892,'27/04/1991')
insert into Empleados(nombre_empleado,id_ciudad,id_tipo_cargo,fecha_ingreso,telefono,cuil,fecha_nac)
values('Marta Dominguez',101,8,'25/07/2020', 3514448953, 274788968898,'13/05/1980')
insert into Empleados(nombre_empleado,id_ciudad,id_tipo_cargo,fecha_ingreso,telefono,cuil,fecha_nac)
values('Matias Suarez',1,8,'25/06/2019', 3514748877, 2455331598,'17/09/2000')
insert into Empleados(nombre_empleado,id_ciudad,id_tipo_cargo,fecha_ingreso,telefono,cuil,fecha_nac)
values('Alejandro Morales',30,9,'15/02/2011', 35466296332, 2568956892,'13/04/1975')
insert into Empleados(nombre_empleado,id_ciudad,id_tipo_cargo,fecha_ingreso,cuil,fecha_nac)
values('Carlos Carrera García',25,9,'28/02/2020', 25444788896,'13/05/1973')
insert into Empleados(nombre_empleado,id_ciudad,id_tipo_cargo,fecha_ingreso,cuil,fecha_nac)
values('Fermina Chajón Soto',47,9,'13/06/2022', 26889744412,'25/05/1998')
insert into Empleados(nombre_empleado,id_ciudad,id_tipo_cargo,fecha_ingreso,telefono,cuil,fecha_nac)
values('Hugo Leonel Alarcón',5,10,'17/02/2011', 35466277758, 2568956892,'27/04/1998')
insert into Empleados(nombre_empleado,id_ciudad,id_tipo_cargo,fecha_ingreso,telefono,fecha_nac)
values('Marco Tulio Soto Juárez',6,10,'24/02/2020',3541164489,'27/01/1976')
insert into Empleados(nombre_empleado,id_ciudad,id_tipo_cargo,fecha_ingreso,telefono,fecha_nac)
values('María Vásquez',104,10,'05/06/2022',35147779882,'27/11/1973')
insert into Empleados(nombre_empleado,id_ciudad,id_tipo_cargo,fecha_ingreso,telefono,cuil,fecha_nac)
values('Octaviano Morales',25,11,'24/07/2008', 35758996332, 256354892,'13/05/1988')
insert into Empleados(nombre_empleado,id_ciudad,id_tipo_cargo,fecha_ingreso,telefono,cuil,fecha_nac)
values('Osman López',98,11,'17/07/2011', 3525696332, 3453956892,'27/01/1975')
insert into Empleados(nombre_empleado,id_ciudad,id_tipo_cargo,fecha_ingreso,telefono,cuil,fecha_nac)
values('David Sanchez',26,11,'22/02/2018', 35544696332, 24538956892,'08/08/1999')



--6) Insert clientes
insert into clientes(nombre,id_ciudad,email,fecha_nac,socio)values('Matias Barbero',17,'matiasbarbero@gmail.com','17/04/2001',1)
insert into clientes(nombre,id_ciudad,email,fecha_nac,socio)values('Emiliano Lopez',57,'emilopez@yahoo.com','25/01/1987',0)
insert into clientes(nombre,id_ciudad,email,fecha_nac,socio)values('Abarca Ingrid',1,'iabarcae@yahoo.es','13/01/1986',1)
insert into clientes(nombre,id_ciudad,email,fecha_nac,socio)values('Julia Illanes',7,'maeillanes@hotmail.com','26/04/1977',1)
insert into clientes(nombre,id_ciudad,email,fecha_nac,socio)values('Miguel Abarca',23,'osabarca@hotmail.com','27/01/1987',1)
insert into clientes(nombre,id_ciudad,email,fecha_nac,socio)values('Emiliano Mendia',45,'cabrigo@garmendia.cl','05/06/1995',0)
insert into clientes(nombre,id_ciudad,email,fecha_nac,socio)values('Sebastian Lopez',5,'Sb.nashxo.sk8@hotmail.com','04/03/1986',0)
insert into clientes(nombre,id_ciudad,email,fecha_nac,socio)values('Francisco Fuller',80,'fran.afull@live.cl','07/12/1986',1)
insert into clientes(nombre,id_ciudad,email,fecha_nac,socio)values('Carlos Aguilera',26,'carlosaguileram@hotmail.com','23/05/1968',1)
insert into clientes(nombre,id_ciudad,email,fecha_nac,socio)values('Daniela Aguilera',33,'daniela_aguilera_m500@hotmail.com','14/02/1978',0)
insert into clientes(nombre,id_ciudad,email,socio)values('Juan Fernando Uribe',23,'juribe@idiomas.udea.edu.co',0)
insert into clientes(nombre,id_ciudad,email,fecha_nac,socio)values('Herlaynne Segura',2,'hersy@epm.net.co','13/06/1979',0)
insert into clientes(nombre,id_ciudad,email,fecha_nac,socio)values('José Ferdusi',4,'jferdusi@terra.com','25/04/1999',1)
insert into clientes(nombre,id_ciudad,fecha_nac,socio)values('Maria Victoria',6,'17/05/1999',1)
insert into clientes(nombre,id_ciudad,email,fecha_nac,socio)values('Miguel Lopez',18,'mlopez@hotmail.com','01/01/1963',1)
insert into clientes(nombre,id_ciudad,email,socio)values('Matias Zorzal',75,'zorzalmat@garmendia.cl',1)
insert into clientes(nombre,id_ciudad,fecha_nac,socio)values('Sebastian Aguirre',33,'04/03/1986',1)
insert into clientes(nombre,id_ciudad,email,fecha_nac,socio)values('Javier Dominguez',99,'jdom@live.cl','25/04/1994',1)
insert into clientes(nombre,id_ciudad,fecha_nac,socio)values('Adrian Barbero',25,'23/05/1968',0)
insert into clientes(nombre,id_ciudad,email,socio)values('Daniela Lopez',2,'ldaniela@hotmail.com',0)
insert into clientes(nombre,id_ciudad,email,socio)values('Jose Lopez',4,'jlopez@gmail.com',1)
insert into clientes(nombre,id_ciudad,email,socio)values('Facundo Lopez',9,'faculopez@gmail.com',0)
insert into clientes(nombre,id_ciudad,email,fecha_nac,socio)values('Javier Del carro',56,'delcarro@live.cl','17/04/2000',1)
insert into clientes(nombre,id_ciudad,email,fecha_nac,socio)values('Juliana Paris',17,'jpariso@live.cl','13/06/2000',1)
insert into clientes(nombre,id_ciudad,email,fecha_nac,socio)values('Miguel Juarez',17,'mjuarez@live.cl','27/01/2003',0)
insert into clientes(nombre,id_ciudad,email,fecha_nac,socio)values('Jose Barbero',25,'jose44@live.cl','27/07/2000',0)
insert into clientes(nombre,id_ciudad,fecha_nac,socio)values('Adrian Castro',64,'27/05/1975',1)
insert into clientes(nombre,id_ciudad,fecha_nac,socio)values('Lucia Aguirre',18,'13/03/1955',0)
insert into clientes(nombre,id_ciudad,socio)values('Valeria Azarre',16,1)
insert into clientes(nombre,id_ciudad,email,socio)values('Martin Hierro',88,'mhierro@gmail.com',0)





INSERT INTO Edades_permitidas (id_edad_permitida ,nombre_edad	,minimo_edad) VALUES (1,'Todo publico', 0);
INSERT INTO Edades_permitidas (id_edad_permitida ,nombre_edad	,minimo_edad) VALUES (2,'SAM 13', 13);
INSERT INTO Edades_permitidas (id_edad_permitida ,nombre_edad	,minimo_edad) VALUES (3,'SAM 15',15);
INSERT INTO Edades_permitidas (id_edad_permitida ,nombre_edad	,minimo_edad) VALUES (4,'SAM 18', 18);

INSERT INTO Generos_peliculas (nombre_genero,descripcion_genero) VALUES (' Acción','En este género prevalecen altas dosis de adrenalina con una buena carga de movimiento, fugas, acrobacias, peleas, guerras, persecuciones y una lucha contra el mal.');
INSERT INTO Generos_peliculas (nombre_genero,descripcion_genero) VALUES ('Aventuras','Similares a las de acción, predominan las nuevas experiencias y situaciones.');
INSERT INTO Generos_peliculas (nombre_genero,descripcion_genero) VALUES ('Ciencia Ficción', 'Basados en fenómenos imaginarios, en la ciencia ficción son usuales los extraterrestres, sociedades inventadas, otros planetas…');
INSERT INTO Generos_peliculas (nombre_genero,descripcion_genero) VALUES ('Comedia', 'Diseñadas específicamente para provocar la risa o la alegría entre los espectadores.');
INSERT INTO Generos_peliculas (nombre_genero,descripcion_genero) VALUES (' No- Ficción / documental', 'Este género analiza un hecho o situación real sin ficcionarlo.');
INSERT INTO Generos_peliculas (nombre_genero,descripcion_genero) VALUES ('Drama', 'Los dramas se centran en desarrollar el problema o problemas entre los diferentes protagonistas. Este es quizás uno de los géneros cinematográficos más amplios de la lista. No predominan las aventuras o la acción, aunque pueden aparecer puntualmente Generalmente se basan en desarrollar la interacción y caracteres humanos.');
INSERT INTO Generos_peliculas (nombre_genero,descripcion_genero) VALUES ('Fantasía', 'En ellas se incluyen personajes irreales o totalmente inventados, inexistentes en nuestra realidad. También podemos conocer este género de cine como “fantástico”. No se basa en ideas que puedan llegar a materializarse.');
INSERT INTO Generos_peliculas (nombre_genero,descripcion_genero) VALUES ('Musical' ,'Las películas que cortan su desarrollo natural con fragmentos musicales son protagonistas de este género.');
INSERT INTO Generos_peliculas (nombre_genero,descripcion_genero) VALUES ('Suspense.' ,'Conocido también como intriga, estas películas se desarrollan rápidamente, y todos sus elementos giran entorno un mismo elemento intrigante.');
INSERT INTO Generos_peliculas (nombre_genero,descripcion_genero) VALUES ('Terror' ,'su principal objetivo es causar miedo, horror, incomodidad o preocupación.');
INSERT INTO Generos_peliculas (nombre_genero,descripcion_genero) VALUES ('Western', 'Famosas por centrarse en el territorio occidental de los Estados Unidos de América.');
INSERT INTO Generos_peliculas (nombre_genero,descripcion_genero) VALUES ('Deportivas', 'Deportes famosos en los cuales se reecrean escenas para que queden guardado en la memoria de los espectadores');
INSERT INTO Generos_peliculas (nombre_genero,descripcion_genero) VALUES ('Históricas', 'Se ambientan en épocas determinadas, con recreaciones de personas, hechos, lugares o argumentos.');
INSERT INTO Generos_peliculas (nombre_genero,descripcion_genero) VALUES ('Bélicas', 'También conocidas por centrarse en conflictos bélicos o guerras.');
INSERT INTO Generos_peliculas (nombre_genero,descripcion_genero) VALUES ('Crimen', 'Su foco se posa sobre la vida de los delincuentes o criminales.');
INSERT INTO Generos_peliculas (nombre_genero,descripcion_genero) VALUES ('Policíacas', 'Suelen tener lugar en una escena del crimen y se centran en resolverlo.');
INSERT INTO Generos_peliculas (nombre_genero,descripcion_genero) VALUES ('Futuristas', 'Tienen lugar en épocas futuras, y sus personajes pueden ser realistas o ficticios.');
INSERT INTO Generos_peliculas (nombre_genero,descripcion_genero) VALUES ('Religiosas', 'Su temática está enfocada a una religión.');

INSERT INTO Peliculas  (nombre_pelicula,id_edad_permitida ,id_genero_pelicula ,descripcion_pelicula,nombre_imagen)
VALUES ('American History X ',3 , 15,'Un exmilitante de los skinheads neonazis intenta disuadir a su hermano pequeño de tomar las mismas malas decisiones que él tomó.', 'AmericanHistoryX');

INSERT INTO Peliculas  (nombre_pelicula,id_edad_permitida ,id_genero_pelicula ,descripcion_pelicula,nombre_imagen)
VALUES ('Infiltrados',3, 16,'Un policía de incógnito y un topo en el departamento de policía intentan identificarse el uno al otro mientras se infiltran en una banda irlandesa en el sur de Boston.', 'Infiltrados');

INSERT INTO Peliculas (nombre_pelicula,id_edad_permitida ,id_genero_pelicula ,descripcion_pelicula,nombre_imagen)
VALUES ('Sospechosos habituales',3, 15,'El único superviviente de un terrible tiroteo en un barco relata los eventos que condujeron a esta masacre, empezando con cinco criminales que se encuentran aparentemente al azar','Sospechososhabituales');

INSERT INTO Peliculas (nombre_pelicula,id_edad_permitida ,id_genero_pelicula ,descripcion_pelicula,nombre_imagen)
VALUES ('El truco final ',2,9,'Tras un trágico accidente, dos magos en el Londres de 1890 se enfrentan para crear la ilusión definitiva, mientras sacrifican todo lo que tienen para superar al otro.', 'Eltrucofinal');

INSERT INTO Peliculas(nombre_pelicula,id_edad_permitida ,id_genero_pelicula ,descripcion_pelicula,nombre_imagen)
VALUES ('Casablanca',1,6,'Un cínico expatriado norteamericano, dueño de un café, se debate entre ayudar o no a su antigua amante y a su marido fugitivo a escapar de los nazis en el Marruecos francés.', 'Casablanca');

INSERT INTO Peliculas (nombre_pelicula,id_edad_permitida ,id_genero_pelicula ,descripcion_pelicula,nombre_imagen)
VALUES ('Whiplash', 2, 6,'Un joven y prometedor batería se inscribe en un competitivo conservatorio donde sus sueños de grandeza son guiados por un instructor que no se detiene ante nada para realizar el potencial de el', 'Whiplash');

INSERT INTO Peliculas (nombre_pelicula,id_edad_permitida ,id_genero_pelicula ,descripcion_pelicula,nombre_imagen)
VALUES ('Intocable',1, 6,'Un accidente de parapente deja a un aristócrata cuadripléjico, quien contrata a un joven de un barrio pobre para que sea su cuidador.', 'Intocable');

INSERT INTO Peliculas (nombre_pelicula,id_edad_permitida ,id_genero_pelicula ,descripcion_pelicula,nombre_imagen)
VALUES ('La tumba de las luciérnagas', 1, 13,'Un chico y su hermana pequeña intentan sobrevivir en Japón durante la Segunda Guerra Mundial.', 'LaTumbaDeLasLuciérnagas');

INSERT INTO Peliculas (nombre_pelicula,id_edad_permitida ,id_genero_pelicula ,descripcion_pelicula,nombre_imagen)
VALUES ('Hasta que llegó su hora',3 ,11 ,'Un misterioso extraño con una armónica se alía con un famoso forajido para proteger a una bella viuda de un despiadado asesino que trabaja para el ferrocarril.', 'HastaQueLlegoSuHora');


INSERT INTO Peliculas (nombre_pelicula,id_edad_permitida ,id_genero_pelicula ,descripcion_pelicula,nombre_imagen)
VALUES ('La ventana indiscreta',2 ,9 ,'Un fotógrafo en silla de ruedas espía a sus vecinos desde la ventana de su apartamento en Greenwich Village, y se convence de que uno de ellos ha cometido un asesinato', 'LaVentanaIndiscreta');

INSERT INTO Peliculas (nombre_pelicula,id_edad_permitida ,id_genero_pelicula ,descripcion_pelicula,nombre_imagen)
VALUES ('Alien', 2,10 ,'La tripulación de una nave espacial comercial se encuentra con una forma de vida mortal tras investigar una transmisión desconocida.', 'Alien');

INSERT INTO Peliculas (nombre_pelicula,id_edad_permitida ,id_genero_pelicula ,descripcion_pelicula,nombre_imagen)
VALUES ('Luces de la ciudad', 1, 4,'Con la ayuda de un hombre adinerado, borracho e imprevisible, un ingenuo vagabundo que se ha enamorado de una florista ciega intenta conseguir dinero para poder proporcionarle ayuda médica.', 'LucesDeLaCiudad');

INSERT INTO Peliculas (nombre_pelicula,id_edad_permitida ,id_genero_pelicula ,descripcion_pelicula,nombre_imagen)
VALUES ('Cinema Paradiso',1 , 6,'Un director de cine recuerda cómo en su infancia se enamoró del cine de su pueblo y entabló una profunda amistad con el proyeccionista.', 'CinemaParadiso');
--------
INSERT INTO Peliculas (nombre_pelicula,id_edad_permitida ,id_genero_pelicula ,descripcion_pelicula,nombre_imagen)
VALUES ('Apocalypse Now', 3, 6,'Un oficial del ejército estadounidense que sirve en Vietnam recibe el encargo de asesinar a un coronel renegado de las fuerzas especiales que se ve a sí mismo como un dios.', 'ApocalypseNow');

INSERT INTO Peliculas (nombre_pelicula,id_edad_permitida ,id_genero_pelicula ,descripcion_pelicula,nombre_imagen)
VALUES ('Memento',2 , 9,'Un hombre con pérdida de memoria a corto plazo intenta encontrar al asesino de su esposa.', 'Memento');

INSERT INTO Peliculas (nombre_pelicula,id_edad_permitida ,id_genero_pelicula ,descripcion_pelicula,nombre_imagen)
VALUES ('En busca del arca perdida', 1,13 ,'En 1936, el arqueólogo y aventurero Indiana Jones es contratado por el Gobierno de Estados Unidos para encontrar el Arca de la Alianza', 'EnBuscaDelArcaPerdida');

INSERT INTO Peliculas(nombre_pelicula,id_edad_permitida ,id_genero_pelicula ,descripcion_pelicula,nombre_imagen)
VALUES ('Django desencadenado',3,11,'Con la ayuda de un cazarrecompensas alemán, un esclavo liberado se propone rescatar a su esposa de un brutal propietario de una plantación en Mississippi.', 'DjangoDesencadenado');

INSERT INTO Peliculas (nombre_pelicula,id_edad_permitida ,id_genero_pelicula ,descripcion_pelicula,nombre_imagen)
VALUES ('La vida de los otros', 2, 6,'En el este de Berlín en 1984 un agente de la policía secreta vigilando a un escritor y su amante se encuentra cada vez más absorto en sus vidas.', 'LaVidaDeLosOtros');

INSERT INTO Peliculas (nombre_pelicula,id_edad_permitida ,id_genero_pelicula ,descripcion_pelicula,nombre_imagen)
VALUES ('WALL·E', 1,7 ,'En un futuro lejano, un pequeño robot recolector de residuos se embarca inadvertidamente en un viaje espacial que decidirá en última instancia el destino de la humanidad.', 'WALLE');

INSERT INTO Peliculas (nombre_pelicula,id_edad_permitida ,id_genero_pelicula ,descripcion_pelicula,nombre_imagen)
VALUES ('Senderos de gloria', 2, 6,'Tras negarse a atacar una posición enemiga, un general acusa a los soldados de cobardía y su oficial al mando debe defenderlos.', 'SenderosDeGloria');





insert into Funciones (id_pelicula, id_sala, horario) values (1,1,'01/10/2022 09:30:00'),
							(2,1,'01/10/2022 12:00:00'),
							(3,1,'01/10/2022 15:00:00'),
							(4,1,'01/10/2022 17:30:00'),
							(5,1,'01/10/2022 20:00:00'),
							(6,1,'01/10/2022 23:30:00'),
							(7,2,'01/10/2022 09:30:00'),
					
							--- 2/10
							(1,1,'02/10/2022 09:30:00'),
							(2,1,'02/10/2022 12:00:00'),
							(3,1,'02/10/2022 15:00:00'),
							(4,1,'02/10/2022 17:30:00'),
							(5,1,'02/10/2022 20:00:00'),
							(6,1,'02/10/2022 23:30:00'),
	
							--03/10
							(1,1,'03/10/2022 09:30:00'),
							(2,1,'03/10/2022 12:00:00'),
							(3,1,'03/10/2022 15:00:00'),
							(4,1,'03/10/2022 17:30:00'),
							(5,1,'03/10/2022 20:00:00'),
							(6,1,'03/10/2022 23:30:00'),
							(7,2,'03/10/2022 09:30:00'),
							(8,2,'03/10/2022 12:00:00'),
							(9,2,'03/10/2022 15:00:00'),
							(10,2,'03/10/2022 17:30:00'),
							(11,2,'03/10/2022 20:00:00'),
							(12,2,'03/10/2022 23:30:00'),
							(13,3,'03/10/2022 09:30:00'),
		
							--04/10
							(1,1,'04/10/2022 09:30:00'),
							(2,1,'04/10/2022 12:00:00'),
							(3,1,'04/10/2022 15:00:00'),
							(4,1,'04/10/2022 17:30:00'),
							(5,1,'04/10/2022 20:00:00'),
							(6,1,'04/10/2022 23:30:00'),
							(7,2,'04/10/2022 09:30:00'),
							(8,2,'04/10/2022 12:00:00'),
							(9,2,'04/10/2022 15:00:00'),
							(10,2,'04/10/2022 17:30:00'),

							--05/10
							(1,1,'05/10/2022 09:30:00'),
							(2,1,'05/10/2022 12:00:00'),
							(3,1,'05/10/2022 15:00:00'),
							(4,1,'05/10/2022 17:30:00'),
			
							--06/10
							(1,1,'06/10/2022 09:30:00'),
							(2,1,'06/10/2022 12:00:00'),
							(3,1,'06/10/2022 15:00:00'),
							(4,1,'06/10/2022 17:30:00'),
							(5,1,'06/10/2022 20:00:00'),
							(6,1,'06/10/2022 23:30:00'),
							(7,2,'06/10/2022 09:30:00'),
							(8,2,'06/10/2022 12:00:00'),
							(9,2,'06/10/2022 15:00:00'),
							(10,2,'06/10/2022 17:30:00'),
							(11,2,'06/10/2022 20:00:00'),
							(12,2,'06/10/2022 23:30:00'),
	
							--07/10
							(1,1,'07/10/2022 09:30:00'),
							(2,1,'07/10/2022 12:00:00'),
							(3,1,'07/10/2022 15:00:00'),
							(4,1,'07/10/2022 17:30:00'),
							(5,1,'07/10/2022 20:00:00'),
							(6,1,'07/10/2022 23:30:00'),
							(7,2,'07/10/2022 09:30:00'),
							(8,2,'07/10/2022 12:00:00'),

							--08/10
							(1,1,'08/10/2022 09:30:00'),
							(2,1,'08/10/2022 12:00:00'),
							(3,1,'08/10/2022 15:00:00'),
							(4,1,'08/10/2022 17:30:00'),
							(5,1,'08/10/2022 20:00:00'),
							(6,1,'08/10/2022 23:30:00'),

							--09/10
							(1,1,'09/10/2022 09:30:00'),
							(2,1,'09/10/2022 12:00:00'),
							(3,1,'09/10/2022 15:00:00'),
							(4,1,'09/10/2022 17:30:00'),
							(5,1,'09/10/2022 20:00:00'),
							(6,1,'09/10/2022 23:30:00'),
							(7,2,'09/10/2022 09:30:00'),
							(8,2,'09/10/2022 12:00:00'),
			
							--10/10
							(1,1,'10/10/2022 09:30:00'),
							(2,1,'10/10/2022 12:00:00'),
							(3,1,'10/10/2022 15:00:00'),
							(4,1,'10/10/2022 17:30:00'),
		
							--11/10
							(1,1,'11/10/2022 09:30:00'),
							(2,1,'11/10/2022 12:00:00'),
							(3,1,'11/10/2022 15:00:00'),
							(4,1,'11/10/2022 17:30:00'),
							(5,1,'11/10/2022 20:00:00'),
							(6,1,'11/10/2022 23:30:00'),
							(7,2,'11/10/2022 09:30:00'),
							(8,2,'11/10/2022 12:00:00'),
							(9,2,'11/10/2022 15:00:00'),
							(10,2,'11/10/2022 17:30:00')
		




insert into Reservas (id_funcion,id_cliente,fecha_reserva,hora_confirmacion)
values
							( 1,  1, '06/09/2022', '01/10/2022 09:00:00' ),
							( 1,  2, '26/09/2022', '01/10/2022 09:00:00' ),
							( 1,  3, '01/09/2022', '01/10/2022 09:00:00' ),
							( 1,  4, '30/09/2022', '01/10/2022 09:00:00' ),
							( 1,  5, '20/09/2022', '01/10/2022 09:00:00' ),
							--funcion 2 
							( 2,  4, '01/10/2022', '01/10/2022 11:30:00' ),
							( 2,  5, '28/09/2022', '01/10/2022 11:30:00' ),
							( 2,  6, '05/09/2022', '01/10/2022 11:30:00' ),
							( 2,  7, '18/09/2022', '01/10/2022 11:30:00' ),
							--funcion 3

							( 3,  8, '18/09/2022', '01/10/2022 14:30:00' ),
							( 3,  9, '25/09/2022', '01/10/2022 14:30:00' ),
							( 3,  10, '16/09/2022', '01/10/2022 14:30:00' ),
							( 3,  11, '03/09/2022', '01/10/2022 14:30:00' ),

							--4
							( 4,  12, '08/09/2022', '01/10/2022 17:00:00' ),
							( 4,  13, '09/09/2022', '01/10/2022 17:00:00' ),
							( 4,  14, '26/09/2022', '01/10/2022 17:00:00' ),
							( 2,  15, '16/09/2022', '01/10/2022 17:00:00' ),

							--5
							( 5,  16, '15/09/2022', '01/10/2022 19:00:00' ),
							( 5,  17, '16/09/2022', '01/10/2022 19:00:00' ),
							( 5, 15, '08/09/2022', '01/10/2022 19:00:00' ),
							( 5,  19, '03/09/2022', '01/10/2022 19:00:00' ),
							( 5,  20, '01/09/2022', '01/10/2022 19:00:00' ),

							--6
							( 6,  21, '17/09/2022', '01/10/2022 23:00:00' ),
							( 6,  22, '14/09/2022', '01/10/2022 23:00:00' ),
							( 6,  23, '12/09/2022', '01/10/2022 23:00:00' ),
							( 6,  24, '18/09/2022', '01/10/2022 23:00:00' ),

							--7
							( 7,  25, '30/09/2022', '01/10/2022 09:00:00' ),
							( 7,  26, '29/09/2022', '01/10/2022 09:00:00' ),
							( 7,  27, '30/09/2022', '01/10/2022 09:00:00' ),

							--8
							( 8,  28, '30/09/2022', '01/10/2022 12:00:00' ),
							( 8,  29, '28/09/2022', '01/10/2022 12:00:00' )








insert into  Tickets       (id_reserva,id_funcion,id_empleado,id_cliente,fecha_ticket)
			values		   (1,1,4,1,'01/10/2022'),
						   (2,1,19,2,'01/10/2022'),
						   (3,1,1,3,'01/10/2022'),
						   (4,1,7,4,'01/10/2022'),
						   (5,1,5,5,'01/10/2022'),
						   (6,2,6,4,'01/10/2022'),
						   (7,2,12,5,'01/10/2022'),
						   (8,2,24,6,'01/10/2022'),
						   (9,2,28,7,'01/10/2022'),
						   (10,3,12,8,'01/10/2022'),
						   (11,3,13,9,'01/10/2022'),
						   (12,3,21,10,'01/10/2022'),
						   (13,3,25,11,'01/10/2022'),
						   (14,4,16,12,'01/10/2022'),
						   (15,4,33,13,'01/10/2022')
						  


insert into Descuentos values ('Sin descuento', 0),
							  ('Descuento del 25% para socios del cine', 0.25),
							  ('Descuentos del 15% para clientes mayores de 50 años', 0.15)



INSERT INTO Detalles_Reservas Values(1,940,2,1),
								   (1,940,2,2),
								   (2,940,1,3),
								   (2,940,1,4),
								   (3,940,2,5),
								   (3,940,2,6),
								   (4,940,2,7),
								   (4,940,2,8),
								   (4,940,2,9),
								   (5,940,2,10),
								   (5,940,2,11),
								   (5,940,2,12),
								   (5,940,2,13),
								   --2
								   (6,940,2,7),
								   (6,940,2,8),
								   (6,940,2,9),
								   (7,940,2,10),
								   (7,940,2,11),
								   (7,940,2,12),
								   (7,940,2,13),
								   (8,940,1,1),
								   (8,940,1,2),
								   (8,940,1,3),
								   (8,940,1,4),
								   (8,940,1,2),
								   (8,940,1,3),
								   (8,940,1,4),
								   (9,940,1,5),
								   (17,940,2,14),

								   --3
								   (10,940,2,6),
								   (10,940,2,7),
								   (10,940,2,8),
								   (10,940,1,9),
								   (11,940,2,10),
								   (11,940,2,11),
								   (11,940,2,12),
								   (11,940,2,13),
								   (11,940,2,14),
								   (11,940,2,15),
								   (11,940,3,16),
								   (12,940,1,1),
								   (12,940,1,2),
								   (12,940,1,3),
								   (13,940,1,4),
								   (13,940,1,5),
								   (13,940,1,17),
								   (13,940,1,18),
								   (13,940,1,19),

								   --4
								   (14,940,1,1),
								   (14,940,1,2),
								   (14,940,1,3),
								   (14,940,1,4),
								   (15,940,2,5),
								   (15,940,2,6),
								   (15,940,2,7),
								   (15,940,2,8)
						

					




INSERT INTO Tipos_Pagos VALUES ('Tarjeta de Credito'),
							   ('Tarjeta de Debito'),
							   ('Efectivo'),
							   ('Transferencia')

INSERT INTO Detalles_Tickets (id_ticket,id_tipo_pago,precio_venta,id_descuento,id_asiento_sala)
					Values		   (1,3,940,2,1),
								   (1,3,940,2,2),
								   (2,2,940,1,3),
								   (2,4,940,1,4),
								   (3,2,940,2,5),
								   (3,4,940,2,6),
								   (4,1,940,2,7),
								   (4,2,940,2,8),
								   (4,1,940,2,9),
								   (5,4,940,2,10),
								   (5,2,940,2,11),
								   (5,3,940,2,12),
								   (5,1,940,2,13),
								   --2
								   (6,1,940,2,7),
								   (6,1,940,2,8),
								   (6,4,940,2,9),
								   (7,2,940,2,10),
								   (7,3,940,2,11),
								   (7,3,940,2,12),
								   (7,4,940,2,13),
								   (8,2,940,1,1),
								   (8,4,940,1,2),
								   (8,4,940,1,3),
								   (8,4,940,1,4),
								   (8,4,940,1,2),
								   (8,3,940,1,3),
								   (8,2,940,1,4),
								   (9,1,940,1,5),
								   (9,2,940,2,14),

								   --3
								   (10,1,940,2,6),
								   (10,2,940,2,7),
								   (10,1,940,2,8),
								   (10,1,940,1,9),
								   (11,2,940,2,10),
								   (11,3,940,2,11),
								   (11,3,940,2,12),
								   (11,2,940,2,13),
								   (11,4,940,2,14),
								   (11,1,940,2,15),
								   (11,3,940,3,16),
								   (12,4,940,1,1),
								   (12,1,940,1,2),
								   (12,4,940,1,3),
								   (13,4,940,1,4),
								   (13,4,940,1,5),
								   (13,2,940,1,17),
								   (13,2,940,1,18),
								   (13,1,940,1,19),

								   --4
								   (14,3,940,1,1),
								   (14,2,940,1,2),
								   (14,1,940,1,3),
								   (14,4,940,1,4),
								   (15,3,940,2,5),
								   (15,3,940,2,6),
								   (15,1,940,2,7),
								   (15,2,940,2,8)
								   


								   
create procedure sp_clientes_extranjeros
 @MinimoPelicuas int
AS
begin
select MONTH(t.fecha_ticket) 'Mes',c.nombre 'Cliente',
pa.nombre_pais, COUNT(t.id_ticket) 'Cantidad de peliculas'
from Clientes c
join Tickets t on t.id_cliente=c.id_cliente
JOIN Reservas r ON r.id_cliente = c.id_cliente
JOIN Ciudades ciu on ciu.id_ciudad = c.id_ciudad
JOIN Provincias pro on pro.id_provincia = ciu.id_provincia
JOIN Paises pa ON pa.id_pais = pro.id_pais
where pa.nombre_pais != 'Argentina'
AND c.id_cliente in (select id_cliente
						from Tickets
						where MONTH(fecha_ticket) = 10)
group by c.nombre ,t.fecha_ticket, pa.nombre_pais
HAVING COUNT(t.id_ticket) > @MinimoPelicuas
order by 2 desc
end


CREATE PROC sp_reservas_terror
@Genero varchar(50),
@edad int
as
begin
SELECT r.fecha_reserva 'Fecha de reserva' ,c.nombre Cliente,
(YEAR(GETDATE())-YEAR(c.fecha_nac)) 'Edad', p.nombre_pelicula Pelicula, g.nombre_genero
	from Clientes c
	JOIN Reservas r ON r.id_cliente = c.id_cliente
	JOIN Funciones f on f.id_funcion = r.id_funcion
	JOIN Peliculas p ON p.id_pelicula = f.id_pelicula
	JOIN Generos_peliculas g on g.id_genero_pelicula = p.id_genero_pelicula
WHERE c.fecha_nac in(SELECT c.fecha_nac
					  FROM Clientes c
					  WHERE (YEAR(GETDATE())-YEAR(c.fecha_nac)) > @edad)
	AND g.nombre_genero = @Genero
end



	create proc Gasto_promedio_clientes
as 
begin
set dateformat dmy
select c.nombre 'Cliente',t.fecha_ticket
'Fecha',AVG(dt.precio_venta)'Gasto Promedio',c.socio '¿Es socio?'
from Clientes c
join Tickets t on t.id_cliente=c.id_cliente
join Detalles_Tickets dt on dt.id_ticket=t.id_ticket
where c.id_cliente in (select id_cliente
from Tickets
where fecha_ticket='01/10/2022')
group by c.nombre, t.fecha_ticket ,c.socio
order by 3 desc
end




create proc Mas_Gastaron_Clientes_mas_menos_35_anios
as
begin
select c.nombre 'Cliente',c.fecha_nac 'Fecha
Nacimiento',count(t.id_ticket)'Tickets', Sum(dt.precio_venta)'Gasto Total',c.socio
'¿Es socio?','Mayor a 35' Tipo
from Clientes c
join Tickets t on t.id_cliente=c.id_cliente
join Detalles_Tickets dt on dt.id_ticket=t.id_ticket
where year(c.fecha_nac) <= 1987
and c.fecha_nac is not null
group by c.nombre,c.fecha_nac,c.socio
having count(t.id_ticket)>=4
union
select
c.nombre,c.fecha_nac,count(t.id_ticket),Sum(dt.precio_venta),c.socio,'Menor a 35'
from Clientes c
join Tickets t on t.id_cliente=c.id_cliente
join Detalles_Tickets dt on dt.id_ticket=t.id_ticket
where year(c.fecha_nac) > 1987
and c.fecha_nac is not null
group by c.nombre,c.fecha_nac,c.socio
having count(t.id_ticket)>=4
order by 4 desc
end




create proc pa_socios_por_provincia
as
begin
select nombre_provincia, count(*) 'Cantidad de socios' 
from clientes c join ciudades ci on c.id_ciudad = ci.id_ciudad join provincias pr on 
pr.id_provincia = ci.id_provincia join paises p on p.id_pais = pr.id_pais
where p.id_pais = 1 and socio = 1
group by nombre_provincia
order by 2 desc
end




create procedure pa_vacaciones_empleados
as
begin
select nombre_empleado 'Empleado', telefono 'Contacto' ,nombre_cargo 'Cargo', cuil,
case
when (datediff(year,fecha_ingreso,getdate()) > 10) then 'Le corresponden 21 días'
when (datediff(year,fecha_ingreso,getdate()) <= 10 and (datediff(year,fecha_ingreso,getdate()) > 5)) then 'Le corresponden 15 días'
when (datediff(year,fecha_ingreso,getdate()) <= 5) then 'Le corresponden 7 días'
end[Vacaciones]
from Empleados e join Tipos_cargos tc on e.id_tipo_cargo = tc.id_tipo_cargo
join Tickets t on e.id_empleado = t.id_empleado join Detalles_Tickets dt on 
t.id_ticket = dt.id_ticket 
group by nombre_empleado, telefono, nombre_cargo, cuil, fecha_ingreso
order by 1
end


create procedure SP_TOTAL_RECAUDADO_POR_PELICULA
as
begin
	select nombre_pelicula 'Pelicula' , gp.nombre_genero 'Genero',(
	select	sum(dt.precio_venta)
	From Tickets t
	join Funciones f on f.id_funcion = t.id_funcion
	join Detalles_Tickets dt on dt.id_ticket = t.id_ticket
	where f.id_pelicula = p.id_pelicula) 'Dinero recaudado'
	from Peliculas p
	join Generos_peliculas gp on gp.id_genero_pelicula = p.id_genero_pelicula
	order by 3 desc
	end

create procedure SP_CLIENTE_FUNCION
as
begin
select c.nombre 'Nombre cliente' , p.nombre_pelicula ' pelicula',
COUNT(dt.id_detalle_ticket) 'Cantidad de entradas compradas', FORMAT(f.horario,
'HH:mm') 'Horario funcion'
from Tickets t
join Detalles_Tickets dt on dt.id_ticket = t.id_ticket
join Funciones f on f.id_funcion = t.id_funcion
join Peliculas p on p.id_pelicula = f.id_pelicula
join Clientes c on c.id_cliente = t.id_cliente
where t.id_cliente = (
select top 1 tt.id_cliente
from Tickets tt
join Detalles_Tickets dtf on dtf.id_ticket = tt.id_ticket
join Clientes cc on cc.id_cliente = t.id_cliente
where cc.socio = 1
and dtf.id_ticket = tt.id_ticket
and t.id_funcion= tt.id_funcion
group by tt.id_cliente
order by count (dtf.id_ticket) desc
)
group by c.nombre , p.nombre_pelicula , f.horario
order by 3 desc, 2 desc, 4 asc
end



create procedure [dbo].[pa_consultar_empleados]
as
begin
select id_empleado, nombre_empleado,  fecha_ingreso, telefono, cuil, fecha_nac, c.id_ciudad, nombre_ciudad, e.id_tipo_cargo, nombre_cargo
from empleados e join ciudades c on e.id_ciudad = c.id_ciudad join Tipos_cargos tp on tp.id_tipo_cargo = e.id_tipo_cargo
end

create proc SP_INSERTAR_DETALLE
@ticket_nro int,
@tipo_pago int,
@precio_venta float,
@descuento int,
@id_asiento_sala int
as
begin
        insert into Detalles_Tickets values(@ticket_nro, @tipo_pago, @precio_venta, @descuento, @id_asiento_sala)
		end

create proc SP_INSERTAR_MAESTRO
@reserva int,
@funcion int,
@personal int,
@cliente int,
@fecha_ticket datetime,
@nro_ticket int output
as
begin
        insert into Tickets values(@reserva, @funcion, @personal, @cliente, @fecha_ticket)
        select @nro_ticket = SCOPE_IDENTITY() from Tickets
		end