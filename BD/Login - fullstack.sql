CREATE DATABASE Login;
GO

USE Login;
GO

-- TABLA DE ROLES:
CREATE TABLE roles (
    id INT IDENTITY(1,1) PRIMARY KEY, 
    nombre_rol VARCHAR(30) NOT NULL UNIQUE
);

INSERT INTO roles (nombre_rol)
VALUES ('Colaborador');

-- 3. TABLA DE USUARIOS
CREATE TABLE usuarios (
    id INT IDENTITY(1,1) PRIMARY KEY,
    usuario VARCHAR(50) NOT NULL UNIQUE,          
    correo VARCHAR(100) NOT NULL UNIQUE,         
    password_hash VARCHAR(255) NOT NULL,          
    rol_id INT NOT NULL,                          
    token_recuperacion VARCHAR(100) NULL,      
    expiracion_token DATETIME NULL,              
    fecha_creacion DATETIME DEFAULT GETDATE(),    
    activo BIT DEFAULT 1,                         

    FOREIGN KEY (rol_id)
        REFERENCES roles(id)
);
GO

-- PROCEDIMIENTOS ALMACENADOS 

-- Registrar un usuario nuevo
-- Simplemente inserta los datos recibidos asignándole el rol '1' por defecto (Colaborador)
CREATE PROCEDURE sp_registrarUsuario
    @p_usuario VARCHAR(50),
    @p_correo VARCHAR(100),
    @p_hash VARCHAR(255)
AS
BEGIN
    INSERT INTO usuarios
    (usuario, correo, password_hash, rol_id)
    VALUES
    (@p_usuario, @p_correo, @p_hash, 1);
END
GO

-- Obtener datos de inicio de sesión
-- Busca al usuario por su correo cruzando datos con los roles (JOIN) para traer el nombre del rol.
-- Solo trae cuentas que estén activas (activo = 1).
CREATE PROCEDURE sp_getUsuarioLogin
    @p_correo VARCHAR(100)
AS
BEGIN
    SELECT 
        u.id,
        u.usuario,
        u.correo,
        u.password_hash,
        r.nombre_rol AS rol
    FROM usuarios u
    INNER JOIN roles r
        ON u.rol_id = r.id
    WHERE u.correo = @p_correo
    AND u.activo = 1;
END
GO

-- Registra el nuevo Token enviado al correo del usuario y su hora de caducidad.
CREATE PROCEDURE sp_generarTokenRecuperacion
    @p_correo VARCHAR(100),
    @p_token VARCHAR(100),
    @p_expiracion DATETIME
AS
BEGIN
    UPDATE usuarios
    SET token_recuperacion = @p_token,
        expiracion_token = @p_expiracion
    WHERE correo = @p_correo;
END
GO

-- Cuando el usuario envía su código a la página Web, el servidor C# ejecuta esto 
-- para descubrir la respuesta verdadera y evitar trampas/hackeos.
CREATE PROCEDURE sp_obtenerTokenRecuperacion
    @p_correo VARCHAR(100)
AS
BEGIN
    SELECT token_recuperacion 
    FROM usuarios 
    WHERE correo = @p_correo;
END
GO

-- Actualizar clave y limpiar los rastros
-- Reemplaza la contraseña antigua o perdida por el nuevo "Hash"
CREATE PROCEDURE sp_actualizarPassword
    @p_correo VARCHAR(100),
    @p_nuevo_hash VARCHAR(255)
AS
BEGIN
    UPDATE usuarios 
    SET password_hash = @p_nuevo_hash,
        token_recuperacion = NULL,
        expiracion_token = NULL    
    WHERE correo = @p_correo;
END
GO
