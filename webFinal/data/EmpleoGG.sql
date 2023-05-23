create database empleos
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
VALUES (1, 'Juan', 'Pérez', 'juan@example.com', 'Experiencia en desarrollo web', '1234567890', 'Calle Principal 123', 'Ciudad de México', 'Ciudad de México', 'México');



INSERT INTO Empresas (IdEmpresa, Nombre, NumeroEmpleados, Rubro, Telefono, Direccion, Ciudad, Estado, Pais)
VALUES (1, 'Empresa XYZ', 100, 'Tecnología', '9876543210', 'Avenida Principal 456', 'Guadalajara', 'Jalisco', 'México');


INSERT INTO Publicaciones (IdPublicacion, IdEmpresa, Titulo, Descripcion, FechaPublicacion)
VALUES (1, 1, 'Desarrollador Web', 'Se busca desarrollador web con experiencia en HTML, CSS y JavaScript.', GETDATE());


INSERT INTO ValoracionesEmpresas (IdValoracion, IdEmpresa, IdUsuario, Comentario, Calificacion, FechaValoracion)
VALUES (1, 1, 1, 'Empresa excelente, ambiente de trabajo agradable.', 5, GETDATE());
