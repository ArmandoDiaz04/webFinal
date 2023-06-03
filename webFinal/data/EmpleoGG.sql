
use empleos
CREATE TABLE Usuarios (
    IdUsuario INT PRIMARY KEY,
    Nombre VARCHAR(100),
    Apellido VARCHAR(100),
    Email VARCHAR(100),
    InformacionLaboral VARCHAR(MAX),
    Telefono VARCHAR(20),
    Direccion VARCHAR(200),
    Ciudad VARCHAR(100),
    Estado VARCHAR(100),
    Pais VARCHAR(100),
    CONSTRAINT UQ_Usuarios_Email UNIQUE (Email)
);

CREATE TABLE Empresas (
    IdEmpresa INT PRIMARY KEY,
    Nombre VARCHAR(100),
    NumeroEmpleados INT,
    Rubro VARCHAR(100),
    Telefono VARCHAR(20),
    Direccion VARCHAR(200),
    Ciudad VARCHAR(100),
    Estado VARCHAR(100),
    Pais VARCHAR(100),
    CONSTRAINT UQ_Empresas_Nombre UNIQUE (Nombre)
);
CREATE TABLE Publicaciones (
    IdPublicacion INT PRIMARY KEY,
    IdEmpresa INT,
    Titulo VARCHAR(100),
    Descripcion VARCHAR(MAX),
    FechaPublicacion DATETIME,
    CONSTRAINT FK_Publicaciones_Empresas FOREIGN KEY (IdEmpresa) REFERENCES Empresas(IdEmpresa)
);
CREATE TABLE ValoracionesEmpresas (
    IdValoracion INT PRIMARY KEY,
    IdEmpresa INT,
    IdUsuario INT,
    Comentario VARCHAR(MAX),
    Calificacion INT,
    FechaValoracion DATETIME,
    CONSTRAINT FK_ValoracionesEmpresas_Empresas FOREIGN KEY (IdEmpresa) REFERENCES Empresas(IdEmpresa),
    CONSTRAINT FK_ValoracionesEmpresas_Usuarios FOREIGN KEY (IdUsuario) REFERENCES Usuarios(IdUsuario)
);

INSERT INTO Usuarios (IdUsuario, Nombre, Apellido, Email, InformacionLaboral, Telefono, Direccion, Ciudad, Estado, Pais)
VALUES (1, 'Juan', 'P�rez', 'juan@example.com', 'Experiencia en desarrollo web', '1234567890', 'Calle Principal 123', 'Ciudad de M�xico', 'Ciudad de M�xico', 'M�xico');



INSERT INTO Empresas (IdEmpresa, Nombre, NumeroEmpleados, Rubro, Telefono, Direccion, Ciudad, Estado, Pais)
VALUES (1, 'Empresa XYZ', 100, 'Tecnolog�a', '9876543210', 'Avenida Principal 456', 'Guadalajara', 'Jalisco', 'M�xico');


INSERT INTO Publicaciones (IdPublicacion, IdEmpresa, Titulo, Descripcion, FechaPublicacion)
VALUES (1, 1, 'Desarrollador Web', 'Se busca desarrollador web con experiencia en HTML, CSS y JavaScript.', GETDATE());


INSERT INTO ValoracionesEmpresas (IdValoracion, IdEmpresa, IdUsuario, Comentario, Calificacion, FechaValoracion)
VALUES (1, 1, 1, 'Empresa excelente, ambiente de trabajo agradable.', 5, GETDATE());


---------------------------------------
INSERT INTO Empresas (IdEmpresa, Nombre, NumeroEmpleados, Rubro, Telefono, Direccion, Ciudad, Estado, Pais)
VALUES (2, 'NuevaEmpresa', 50, 'Deporte', '78981919', 'Santa Ana', 'santa an', 'santa ana', 'el salvador');
GO
INSERT INTO Publicaciones (IdPublicacion, IdEmpresa, Titulo, Descripcion, FechaPublicacion)
VALUES (2, 2, 'Ingeniero de Software', 'Se solicita ingeniero de software con conocimientos en Java y Python.', GETDATE());
GO

INSERT INTO Publicaciones (IdPublicacion, IdEmpresa, Titulo, Descripcion, FechaPublicacion)
VALUES (3, 1, 'Dise�ador Gr�fico', 'Empresa busca dise�ador gr�fico con experiencia en Adobe Photoshop e Illustrator.', GETDATE());
GO

INSERT INTO Publicaciones (IdPublicacion, IdEmpresa, Titulo, Descripcion, FechaPublicacion)
VALUES (4, 2, 'Asistente Administrativo', 'Se necesita asistente administrativo con habilidades en gesti�n de documentos y atenci�n al cliente.', GETDATE());
GO

INSERT INTO Publicaciones (IdPublicacion, IdEmpresa, Titulo, Descripcion, FechaPublicacion)
VALUES (5, 1, 'Analista de Datos', 'Empresa de an�lisis de datos busca analista con conocimientos en SQL y Excel.', GETDATE());
GO

INSERT INTO Publicaciones (IdPublicacion, IdEmpresa, Titulo, Descripcion, FechaPublicacion)
VALUES (6, 1, 'Desarrollador Mobile', 'Se solicita desarrollador mobile con experiencia en desarrollo de aplicaciones Android e iOS.', GETDATE());
GO

INSERT INTO Publicaciones (IdPublicacion, IdEmpresa, Titulo, Descripcion, FechaPublicacion)
VALUES (7, 2, 'Contador P�blico', 'Empresa contable busca contador con experiencia en auditor�a financiera y declaraci�n de impuestos.', GETDATE());
GO

INSERT INTO Publicaciones (IdPublicacion, IdEmpresa, Titulo, Descripcion, FechaPublicacion)
VALUES (8, 1, 'T�cnico de Soporte', 'Se necesita t�cnico de soporte con conocimientos en redes y resoluci�n de problemas de hardware.', GETDATE());
GO

INSERT INTO Publicaciones (IdPublicacion, IdEmpresa, Titulo, Descripcion, FechaPublicacion)
VALUES (9, 2, 'Instructor Fitness', 'Gimnasio busca instructor fitness con experiencia en entrenamiento funcional y clases grupales.', GETDATE());
GO

SELECT  * FROM Empresas

DROP table Empresas
DROP table Publicaciones