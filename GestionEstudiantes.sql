CREATE DATABASE GestionEstudiantes
drop database GestionEstudiantes
USE GestionEstudiantes

-- Tabla de estudiantes
CREATE TABLE Estudiantes (
    EstudianteId INT PRIMARY KEY IDENTITY,
    Nombre NVARCHAR(100) NOT NULL,
    Apellido NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL UNIQUE,
    FechaNacimiento DATE NOT NULL,
    FechaRegistro DATETIME DEFAULT GETDATE() -- Marca de tiempo para la auditor�a
);

-- Tabla de profesores
CREATE TABLE Profesores (
    ProfesorId INT PRIMARY KEY IDENTITY,
    Nombre NVARCHAR(100) NOT NULL,
    Apellido NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL UNIQUE,
    FechaRegistro DATETIME DEFAULT GETDATE() -- Marca de tiempo para la auditor�a
);

-- Tabla de clases
CREATE TABLE Clases (
    ClaseId INT PRIMARY KEY IDENTITY,
    Nombre NVARCHAR(100) NOT NULL,
    Descripcion NVARCHAR(255) NOT NULL,
    ProfesorId INT NOT NULL,
    FechaRegistro DATETIME DEFAULT GETDATE(), -- Marca de tiempo para la auditor�a
    FOREIGN KEY (ProfesorId) REFERENCES Profesores(ProfesorId)
);

-- Tabla de aulas
CREATE TABLE Aulas (
    AulaId INT PRIMARY KEY IDENTITY,
    Nombre NVARCHAR(100) NOT NULL,
    Capacidad INT NOT NULL,
    FechaRegistro DATETIME DEFAULT GETDATE() -- Marca de tiempo para la auditor�a
);

-- Tabla de d�as
CREATE TABLE Dias (
    DiaId INT PRIMARY KEY IDENTITY,
    Nombre NVARCHAR(50) NOT NULL UNIQUE -- Lunes, Martes, etc.
);

-- Tabla de horarios
CREATE TABLE Horarios (
    HorarioId INT PRIMARY KEY IDENTITY,
    ClaseId INT NOT NULL,
    AulaId INT NOT NULL,
    DiaId INT NOT NULL,
    HoraInicio TIME NOT NULL,
    HoraFin TIME NOT NULL,
    FechaRegistro DATETIME DEFAULT GETDATE(), -- Marca de tiempo para la auditor�a
    FOREIGN KEY (ClaseId) REFERENCES Clases(ClaseId),
    FOREIGN KEY (AulaId) REFERENCES Aulas(AulaId),
    FOREIGN KEY (DiaId) REFERENCES Dias(DiaId),
    CONSTRAINT CK_Horarios_Hora CHECK (HoraInicio < HoraFin) -- Constraint para asegurar integridad de los horarios
);

-- Tabla intermedia HorarioDias
CREATE TABLE HorarioDias (
    HorarioDiaId INT PRIMARY KEY IDENTITY,
    HorarioId INT NOT NULL,
    DiaId INT NOT NULL,
    FOREIGN KEY (HorarioId) REFERENCES Horarios(HorarioId),
    FOREIGN KEY (DiaId) REFERENCES Dias(DiaId)
);

-- Tabla intermedia HorarioAsignatura
CREATE TABLE HorarioAsignatura (
    HorarioAsignaturaId INT PRIMARY KEY IDENTITY,
    HorarioId INT NOT NULL,
    DiaId INT NOT NULL,
    ProfesorId INT NOT NULL,
    FOREIGN KEY (HorarioId) REFERENCES Horarios(HorarioId),
    FOREIGN KEY (DiaId) REFERENCES Dias(DiaId),
    FOREIGN KEY (ProfesorId) REFERENCES Profesores(ProfesorId)
);

-- Tabla de inscripciones
CREATE TABLE Inscripciones (
    InscripcionId INT PRIMARY KEY IDENTITY,
    EstudianteId INT NOT NULL,
    ClaseId INT NOT NULL,
    FechaRegistro DATETIME DEFAULT GETDATE(), -- Marca de tiempo para la auditor�a
    FOREIGN KEY (EstudianteId) REFERENCES Estudiantes(EstudianteId),
    FOREIGN KEY (ClaseId) REFERENCES Clases(ClaseId),
    CONSTRAINT UK_Inscripciones UNIQUE (EstudianteId, ClaseId) -- Constraint para asegurar que un estudiante no se inscriba m�s de una vez en la misma clase
);

-- Tabla de usuarios
CREATE TABLE Usuarios (
    UsuarioId INT PRIMARY KEY IDENTITY,
    Nombre NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL UNIQUE,
    Contrase�a NVARCHAR(100) NOT NULL,
    Rol NVARCHAR(50) NOT NULL, -- Administrador, Profesor, Estudiante
    FechaRegistro DATETIME DEFAULT GETDATE() -- Marca de tiempo para la auditor�a
);

INSERT INTO Dias (Nombre) VALUES 
('Lunes'),
('Martes'),
('Mi�rcoles'),
('Jueves'),
('Viernes'),
('S�bado'),
('Domingo');

INSERT INTO Estudiantes (Nombre, Apellido, Email, FechaNacimiento) VALUES 
('Juan', 'P�rez', 'juan.perez@example.com', '2001-01-15'),
('Mar�a', 'Gonz�lez', 'maria.gonzalez@example.com', '2002-03-22'),
('Carlos', 'Ram�rez', 'carlos.ramirez@example.com', '2003-07-10');


INSERT INTO Profesores (Nombre, Apellido, Email) VALUES 
('Ana', 'Mart�nez', 'ana.martinez@example.com'),
('Luis', 'Hern�ndez', 'luis.hernandez@example.com'),
('Sof�a', 'Jim�nez', 'sofia.jimenez@example.com');


INSERT INTO Clases (Nombre, Descripcion, ProfesorId) VALUES 
('Matem�ticas', 'Clase de matem�ticas b�sica', 1), -- Profesor Ana Mart�nez
('Historia', 'Clase de historia universal', 2), -- Profesor Luis Hern�ndez
('Biolog�a', 'Clase de biolog�a general', 3); -- Profesor Sof�a Jim�nez


INSERT INTO Aulas (Nombre, Capacidad) VALUES 
('Aula 101', 30),
('Aula 102', 25),
('Aula 103', 20);

INSERT INTO Horarios (ClaseId, AulaId, DiaId, HoraInicio, HoraFin) VALUES 
(1, 1, 1, '08:00', '10:00'), -- Matem�ticas en Aula 101 el Lunes
(2, 2, 2, '10:00', '12:00'), -- Historia en Aula 102 el Martes
(3, 3, 3, '12:00', '14:00'); -- Biolog�a en Aula 103 el Mi�rcoles

INSERT INTO Inscripciones (EstudianteId, ClaseId) VALUES 
(1, 1), -- Juan P�rez inscrito en Matem�ticas
(2, 2), -- Mar�a Gonz�lez inscrita en Historia
(3, 3), -- Carlos Ram�rez inscrito en Biolog�a
(1, 2), -- Juan P�rez inscrito en Historia
(2, 3); -- Mar�a Gonz�lez inscrita en Biolog�a

INSERT INTO Usuarios (Nombre, Email, Contrase�a, Rol) VALUES 
('admin', 'admin@example.com', 'admin123', 'Administrador'),
('Ana Mart�nez', 'ana.martinez@example.com', 'profesor123', 'Profesor'),
('Luis Hern�ndez', 'luis.hernandez@example.com', 'profesor123', 'Profesor'),
('Sof�a Jim�nez', 'sofia.jimenez@example.com', 'profesor123', 'Profesor'),
('Juan P�rez', 'juan.perez@example.com', 'estudiante123', 'Estudiante'),
('Mar�a Gonz�lez', 'maria.gonzalez@example.com', 'estudiante123', 'Estudiante'),
('Carlos Ram�rez', 'carlos.ramirez@example.com', 'estudiante123', 'Estudiante');

INSERT INTO HorarioDias (HorarioId, DiaId) VALUES 
(1, 1), -- Horario 1 (Matem�ticas) el Lunes
(2, 2), -- Horario 2 (Historia) el Martes
(3, 3); -- Horario 3 (Biolog�a) el Mi�rcoles

INSERT INTO HorarioAsignatura (HorarioId, DiaId, ProfesorId) VALUES 
(1, 1, 1), -- Horario 1 (Matem�ticas) el Lunes por el Profesor Ana Mart�nez
(2, 2, 2), -- Horario 2 (Historia) el Martes por el Profesor Luis Hern�ndez
(3, 3, 3); -- Horario 3 (Biolog�a) el Mi�rcoles por el Profesor Sof�a Jim�nez
