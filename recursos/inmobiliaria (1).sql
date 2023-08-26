-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 25-08-2023 a las 13:18:49
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
  `idPropietario` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

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
  `ciudad` varchar(40) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Volcado de datos para la tabla `inquilinos`
--

INSERT INTO `inquilinos` (`idInquilino`, `dni`, `nombre`, `apellido`, `telefono`, `mail`, `direccion`, `ciudad`) VALUES
(1, '321321', 'primero', 'primer', '2134564', 'sdfsdf@laskdjf', 'avenida1', 'merlo'),
(2, '2222222', 'segundo', 'segundito', '2121212121', 'segundo@hotmail.com', 'valle del sol 4568', 'Merlo'),
(3, '33333333333', 'tercero', 'Tercero', '313333333', 'tercero@hotmail.com', 'av norte 333', 'Los Molles'),
(6, '4444444', 'cuarto', 'Cuartos', '456845', 'cuatro@cuarta.com', 'Long valley', 'Carpinteria');

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
  `mail` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Volcado de datos para la tabla `propietarios`
--

INSERT INTO `propietarios` (`idPropietario`, `dni`, `apellido`, `nombre`, `telefono`, `direccion`, `ciudad`, `mail`) VALUES
(1, '33429037', 'Cardarelli', 'francisco', '2664167311', 'las golondrinas', 'PIEDRA BLANCA', 'franciscocardarelli@hotmail.com'),
(2, '232658459', 'Scar', 'Lucia', '2664167312', '9 de julio 1500', 'Merlo', 'lucia@gmail.com'),
(3, '56088565', 'Franccesca', 'Emma', '2664167355', 'Av norte 1556', 'Merlo', 'emma@franccesca.com'),
(4, '11989456', 'Cardarelli', 'Jorge', '2657547060', '9 de julio 1564', 'Villa Mercedes', 'jorgecardarelli@hotmail.com'),
(5, '16568456', 'Said', 'Liliana', '2664758486', 'Las aguilas 12', 'Carpinteria', 'liliana@gmail.com');

--
-- Índices para tablas volcadas
--

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
-- Indices de la tabla `propietarios`
--
ALTER TABLE `propietarios`
  ADD PRIMARY KEY (`idPropietario`);

--
-- AUTO_INCREMENT de las tablas volcadas
--

--
-- AUTO_INCREMENT de la tabla `inmuebles`
--
ALTER TABLE `inmuebles`
  MODIFY `idInmueble` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `inquilinos`
--
ALTER TABLE `inquilinos`
  MODIFY `idInquilino` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- AUTO_INCREMENT de la tabla `propietarios`
--
ALTER TABLE `propietarios`
  MODIFY `idPropietario` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- Restricciones para tablas volcadas
--

--
-- Filtros para la tabla `inmuebles`
--
ALTER TABLE `inmuebles`
  ADD CONSTRAINT `inmuebles_ibfk_1` FOREIGN KEY (`idPropietario`) REFERENCES `propietarios` (`idPropietario`) ON DELETE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
