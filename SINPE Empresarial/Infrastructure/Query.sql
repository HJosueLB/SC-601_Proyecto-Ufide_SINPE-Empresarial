INSERT INTO TipoDeIdentificacion (Nombre)
VALUES ('Física'), ('Jurídica');

-- Tipos de Comercio
INSERT INTO TipoDeComercio (Nombre)
VALUES ('Restaurante'), ('Ferretería'), ('Supermercado');


INSERT INTO Comercio (
    Identificacion,
    TipoDeIdentificacionId,
    Nombre,
    TipoDeComercioId,
    Telefono,
    CorreoElectronico,
    Direccion,
    FechaDeRegistro,
    FechaDeModificacion,
    Estado
)
VALUES 
('123456789', 1, 'Soda El Buen Sabor', 1, '22223333', 'soda@sabor.com', 'Calle 5, San José', GETDATE(), GETDATE(), 1),
('3101234567', 2, 'Ferretería Birri', 2, '24335566', 'contacto@tornillofeliz.com', 'Diagonal a la plaza', GETDATE(), GETDATE(), 1),
('3109988776', 2, 'Super Cali', 3, '88887777', 'ventas@supertico.cr', 'Frente a la gasolinera', GETDATE(), GETDATE(), 0);

