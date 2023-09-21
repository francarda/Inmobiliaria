-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 21-09-2023 a las 13:18:49
-- Versión del servidor: 10.4.25-MariaDB
-- Versión de PHP: 8.1.10

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `inmobiliaria`
--

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `contratos`
--

CREATE TABLE `contratos` (
  `idContrato` int(11) NOT NULL,
  `idInquilino` int(11) NOT NULL,
  `idInmueble` int(11) NOT NULL,
  `desde` date NOT NULL,
  `hasta` date NOT NULL,
  `monto` decimal(10,0) NOT NULL,
  `activo` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Volcado de datos para la tabla `contratos`
--

INSERT INTO `contratos` (`idContrato`, `idInquilino`, `idInmueble`, `desde`, `hasta`, `monto`, `activo`) VALUES
(1, 2, 8, '2023-08-02', '2023-08-25', '20000', 0),
(5, 1, 5, '2023-05-10', '2023-05-15', '155500', 0),
(6, 3, 13, '2023-09-01', '2023-05-15', '1000', 1),
(7, 2, 13, '2023-09-02', '2023-09-14', '1321321', 1),
(8, 3, 8, '2023-09-19', '2023-09-30', '1235464', 0),
(9, 1, 8, '2023-09-01', '2023-09-28', '232321', 0),
(10, 2, 13, '2023-09-08', '2023-09-21', '232321', 1),
(11, 2, 9, '2023-09-05', '2023-09-29', '1235464', 1),
(12, 2, 13, '2023-10-10', '2023-10-28', '1500', 1),
(13, 2, 5, '2023-09-13', '2023-09-20', '155500', 1);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `inmuebles`
--

CREATE TABLE `inmuebles` (
  `idInmueble` int(11) NOT NULL,
  `direccion` varchar(40) NOT NULL,
  `uso` varchar(20) NOT NULL,
  `tipo` varchar(20) NOT NULL,
  `cantAmbientes` int(4) NOT NULL,
  `latitud` varchar(30) NOT NULL,
  `precio` decimal(10,0) NOT NULL,
  `visible` tinyint(1) NOT NULL,
  `longitud` varchar(10) NOT NULL,
  `idPropietario` int(11) NOT NULL,
  `estado` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Volcado de datos para la tabla `inmuebles`
--

INSERT INTO `inmuebles` (`idInmueble`, `direccion`, `uso`, `tipo`, `cantAmbientes`, `latitud`, `precio`, `visible`, `longitud`, `idPropietario`, `estado`) VALUES
(1, 'Av norte 45', 'comercial', 'local', 3, '12345', '12345', 1, '123', 5, 1),
(2, 'avenida1', 'familiar', 'casa', 12, '100000000', '12368', 1, '1235', 5, 1),
(4, 'avenida1', 'familiar', 'casa', 12, '100000000', '12368', 1, '1235', 5, 1),
(5, 'avenida1', 'familiar', 'casa', 4, '1234', '12354', 1, '123', 2, 1),
(6, 'avenida1', 'familiar', 'casa', 12, '100000000', '12368', 1, '1235', 5, 1),
(7, 'avenida1', 'familiar', 'casa', 12, '100000000', '12368', 1, '1235', 5, 1),
(8, 'av mitre 1555', 'comercial', 'deposito', 8, '266565', '5555', 0, '555', 2, 1),
(9, 'av mitre 1555', 'comercial', 'deposito', 8, '266565', '5555', 0, '555', 2, 1),
(10, 'avenida1', 'familiar', 'casa', 12, '100000000', '12368', 1, '1235', 5, 1),
(11, 'avenida1', 'familiar', 'casa', 12, '100000000', '12368', 1, '1235', 5, 1),
(12, 'av mitre 1555', 'comercial', 'deposito', 8, '266565', '5555', 0, '555', 2, 1),
(13, 'av mitre 1555', 'comercial', 'deposito', 8, '266565', '5555', 0, '555', 2, 1),
(14, 'av mitre 1555', 'comercial', 'deposito', 8, '266565', '5555', 0, '555', 2, 1),
(15, 'avenida1', 'familiar', 'casa', 12, '100000000', '12368', 1, '1235', 5, 0),
(16, 'av mitre 1555', 'comercial', 'deposito supermercad', 8, '266565', '5555', 0, '555', 2, 1),
(17, 'av mitre 1555', 'comercial', 'deposito supermercad', 8, '266565', '5555', 0, '555', 2, 1),
(18, 'av mitre 1555', 'comercial', 'deposito supermercad', 8, '266565', '5555', 0, '555', 2, 1),
(23, 'las golondrinas', 'personal', 'casa', 4, '8000000', '12368', 0, '3215465', 2, 1),
(25, 'Las aguilas 12', 'familiar', 'casa', 4, '100000000', '321321', 0, '1235', 5, 0),
(26, 'Las aguilas 12', 'comercial', 'casa', 4, '100000000', '321321', 0, '1235', 4, 1),
(27, 'Las aguilas 12', 'familiar', 'deposito', 4, '200000', '321321', 1, '1235', 11, 1);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `inquilinos`
--

CREATE TABLE `inquilinos` (
  `idInquilino` int(11) NOT NULL,
  `dni` varchar(12) NOT NULL,
  `nombre` varchar(20) NOT NULL,
  `apellido` varchar(20) NOT NULL,
  `telefono` varchar(20) NOT NULL,
  `mail` varchar(40) NOT NULL,
  `direccion` varchar(40) NOT NULL,
  `ciudad` varchar(40) NOT NULL,
  `estado` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Volcado de datos para la tabla `inquilinos`
--

INSERT INTO `inquilinos` (`idInquilino`, `dni`, `nombre`, `apellido`, `telefono`, `mail`, `direccion`, `ciudad`, `estado`) VALUES
(1, '321321', 'primero', 'primer', '21345', 'sdfsdf@laskdjf', 'avenida1', 'merlo', 1),
(2, '2222222', 'segundo', 'segundito', '2121212121', 'segundo@hotmail.com', 'valle del sol 4568', 'Merlo', 1),
(3, '33333333333', 'tercero', 'Tercero', '313333333', 'tercero@hotmail.com', 'av norte 333', 'Los Molles', 1),
(6, '4444444', 'cuarto', 'Cuartos', '456845', 'cuatro@cuarta.com', 'Long valley', 'Carpinteria', 1),
(7, '555555555', 'Alguno', 'otro', '2134564', 'kjflaskjdf@laksdjfl', 'avenida1', 'merlo', 1);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `pagos`
--

CREATE TABLE `pagos` (
  `idPago` int(11) NOT NULL,
  `IdContrato` int(11) NOT NULL,
  `numeroDePago` int(11) NOT NULL,
  `fechaDePago` date NOT NULL,
  `estado` tinyint(1) NOT NULL,
  `monto` decimal(10,0) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Volcado de datos para la tabla `pagos`
--

INSERT INTO `pagos` (`idPago`, `IdContrato`, `numeroDePago`, `fechaDePago`, `estado`, `monto`) VALUES
(1, 10, 3, '2023-09-01', 1, '20005'),
(3, 10, 1, '2023-01-01', 1, '1500'),
(4, 7, 1, '2023-09-05', 1, '155500'),
(5, 10, 1, '2023-09-05', 0, '155500');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `propietarios`
--

CREATE TABLE `propietarios` (
  `idPropietario` int(11) NOT NULL,
  `dni` varchar(12) NOT NULL,
  `apellido` varchar(20) NOT NULL,
  `nombre` varchar(20) NOT NULL,
  `telefono` varchar(20) NOT NULL,
  `direccion` varchar(30) NOT NULL,
  `ciudad` varchar(30) NOT NULL,
  `mail` varchar(50) NOT NULL,
  `estado` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Volcado de datos para la tabla `propietarios`
--

INSERT INTO `propietarios` (`idPropietario`, `dni`, `apellido`, `nombre`, `telefono`, `direccion`, `ciudad`, `mail`, `estado`) VALUES
(1, '33429037', 'Cardarelli', 'francisco', '2664167311', 'las golondrinas', 'PIEDRA BLANCA', 'franciscocardarelli@hotmail.com', 1),
(2, '232658459', 'Scar', 'Lucia', '2664167312', '9 de julio 1500', 'Merlo', 'lucia@gmail.com', 1),
(3, '56088565', 'Franccesca', 'Emma', '2664167355', 'Av norte 1556', 'Merlo', 'emma@franccesca.com', 1),
(4, '11989456', 'Cardarelli', 'Jorge', '2657547060', '9 de julio 1564', 'Villa Mercedes', 'jorgecardarelli@hotmail.com', 1),
(5, '16568456', 'Said', 'Liliana', '2664758486', 'Las aguilas 12', 'Carpinteria', 'liliana@gmail.com', 1),
(11, '1234', 'luza', 'mariano', '2224', 'avenida1', 'merlo', 'mariano@hotmail.com', 1);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `usuarios`
--

CREATE TABLE `usuarios` (
  `id` int(11) NOT NULL,
  `nombre` varchar(40) NOT NULL,
  `apellido` varchar(40) NOT NULL,
  `avatar` varchar(40) DEFAULT NULL,
  `email` varchar(60) NOT NULL,
  `clave` varchar(60) NOT NULL,
  `rol` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Volcado de datos para la tabla `usuarios`
--

INSERT INTO `usuarios` (`id`, `nombre`, `apellido`, `avatar`, `email`, `clave`, `rol`) VALUES
(1, 'Carlitos', 'carlos', '', 'carlos@carlos', '123', 2),
(6, 'Juan', 'Juan', '/Uploads\\avatar_6.jpg', 'juan@juan', 'qYM7JOKErgp/dPvpAnIoj37QqFlc3Mh8h9tlz4kuRY0=', 3),
(13, 'lucia', 'sca', '/Uploads\\avatar_13.jpg', 'luci@luci', 'TlSu1CQvtaIAAswpBfoG3HqSpcuDDAgXuyn3sXc40s4=', 2),
(14, 'empla', 'Carmelio', '/Uploads\\avatar_14.png', 'emp@emp', 'qYM7JOKErgp/dPvpAnIoj37QqFlc3Mh8h9tlz4kuRY0=', 3),
(15, 'pepes', 'pepe', '/Uploads\\avatar_15.jpg', 'pepe@pepe', 'qYM7JOKErgp/dPvpAnIoj37QqFlc3Mh8h9tlz4kuRY0=', 3);

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `contratos`
--
ALTER TABLE `contratos`
  ADD PRIMARY KEY (`idContrato`),
  ADD KEY `inquilino` (`idInquilino`),
  ADD KEY `inmueble` (`idInmueble`);

--
-- Indices de la tabla `inmuebles`
--
ALTER TABLE `inmuebles`
  ADD PRIMARY KEY (`idInmueble`),
  ADD KEY `idPropietario` (`idPropietario`);

--
-- Indices de la tabla `inquilinos`
--
ALTER TABLE `inquilinos`
  ADD PRIMARY KEY (`idInquilino`);

--
-- Indices de la tabla `pagos`
--
ALTER TABLE `pagos`
  ADD PRIMARY KEY (`idPago`),
  ADD KEY `IdContrato` (`IdContrato`);

--
-- Indices de la tabla `propietarios`
--
ALTER TABLE `propietarios`
  ADD PRIMARY KEY (`idPropietario`);

--
-- Indices de la tabla `usuarios`
--
ALTER TABLE `usuarios`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT de las tablas volcadas
--

--
-- AUTO_INCREMENT de la tabla `contratos`
--
ALTER TABLE `contratos`
  MODIFY `idContrato` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=14;

--
-- AUTO_INCREMENT de la tabla `inmuebles`
--
ALTER TABLE `inmuebles`
  MODIFY `idInmueble` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=28;

--
-- AUTO_INCREMENT de la tabla `inquilinos`
--
ALTER TABLE `inquilinos`
  MODIFY `idInquilino` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- AUTO_INCREMENT de la tabla `pagos`
--
ALTER TABLE `pagos`
  MODIFY `idPago` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT de la tabla `propietarios`
--
ALTER TABLE `propietarios`
  MODIFY `idPropietario` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=12;

--
-- AUTO_INCREMENT de la tabla `usuarios`
--
ALTER TABLE `usuarios`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=16;

--
-- Restricciones para tablas volcadas
--

--
-- Filtros para la tabla `contratos`
--
ALTER TABLE `contratos`
  ADD CONSTRAINT `contratos_ibfk_1` FOREIGN KEY (`idInquilino`) REFERENCES `inquilinos` (`idInquilino`) ON DELETE CASCADE,
  ADD CONSTRAINT `contratos_ibfk_2` FOREIGN KEY (`idInmueble`) REFERENCES `inmuebles` (`idInmueble`) ON DELETE CASCADE;

--
-- Filtros para la tabla `inmuebles`
--
ALTER TABLE `inmuebles`
  ADD CONSTRAINT `inmuebles_ibfk_1` FOREIGN KEY (`idPropietario`) REFERENCES `propietarios` (`idPropietario`) ON DELETE CASCADE;

--
-- Filtros para la tabla `pagos`
--
ALTER TABLE `pagos`
  ADD CONSTRAINT `pagos_ibfk_1` FOREIGN KEY (`IdContrato`) REFERENCES `contratos` (`idContrato`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
