-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1:3306
-- Generation Time: Jan 06, 2024 at 09:19 AM
-- Server version: 10.4.27-MariaDB
-- PHP Version: 8.2.0

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `dataofmed`
--

-- --------------------------------------------------------

--
-- Table structure for table `invt`
--

CREATE TABLE `invt` (
  `Id` int(11) NOT NULL,
  `Userid` int(11) NOT NULL,
  `Typemed` varchar(50) NOT NULL,
  `medicinetype` varchar(123) NOT NULL,
  `Mendname` varchar(50) NOT NULL,
  `Price` int(11) NOT NULL,
  `Medis` varchar(150) NOT NULL,
  `statusmed` varchar(23) NOT NULL,
  `stock` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `invt`
--

INSERT INTO `invt` (`Id`, `Userid`, `Typemed`, `medicinetype`, `Mendname`, `Price`, `Medis`, `statusmed`, `stock`) VALUES
(19, 16, 'wewe', '', 'asdsad', 232, 'asdsadasd', '', 0),
(20, 17, 'asdadasdadasd', '', 'awa', 2321, 'sahdfkhsdfh', '', 0),
(21, 18, 'awtsffff', '', 'awa', 231, 'sdqededq', '', 0),
(40, 8, 'Vitamin B Complex', 'Syrup', 'asd', 23, 'sdfsf', 'Active', 21),
(42, 8, 'Vitamin D3', 'Syrup', 'asd', 23, 'sdfsfs', 'InActive', 2),
(43, 27, 'Vitamin B Complex', 'Tablet', 'wqe', 238, 'mnnnnasdasd', 'Active', 323),
(44, 8, 'Vitamin B-6 ', 'Syrup', 'asd', 23, 'sdflsdjflsjk', 'InActive', 65),
(45, 27, 'Vitamin B Complex', 'Syrup', 'hgffgh', 12, 'ccccbv', 'InActive', 3),
(46, 27, 'Vitamin D3', 'Syrup', 'aksdh', 23, 'asdafhdksfisdfhs', 'Active', 2),
(47, 27, 'amoxicillin', 'Tablet', 'sakul', 123, 'kshdfjshdkjfshfk', 'InActive', 67),
(49, 29, 'amoxicillin', 'Tablet', 'wew', 34, 'sdfjlsdfjlsdfs', 'Active', 232);

-- --------------------------------------------------------

--
-- Table structure for table `typesmed`
--

CREATE TABLE `typesmed` (
  `id` int(11) NOT NULL,
  `Name` varchar(23) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `typesmed`
--

INSERT INTO `typesmed` (`id`, `Name`) VALUES
(1, 'paracetamol'),
(2, 'amoxicillin'),
(3, 'Vitamin B Complex'),
(4, 'Vitamin B-6 '),
(5, 'Vitamin D3');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `invt`
--
ALTER TABLE `invt`
  ADD PRIMARY KEY (`Id`);

--
-- Indexes for table `typesmed`
--
ALTER TABLE `typesmed`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `invt`
--
ALTER TABLE `invt`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=50;

--
-- AUTO_INCREMENT for table `typesmed`
--
ALTER TABLE `typesmed`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
