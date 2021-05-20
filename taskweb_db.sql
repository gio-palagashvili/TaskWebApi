-- phpMyAdmin SQL Dump
-- version 5.1.0
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: May 21, 2021 at 01:07 AM
-- Server version: 10.4.18-MariaDB
-- PHP Version: 7.3.27

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `taskweb_db`
--

-- --------------------------------------------------------

--
-- Table structure for table `persons_tbl`
--

CREATE TABLE `persons_tbl` (
  `Index` int(11) NOT NULL,
  `Fname` varchar(255) NOT NULL,
  `Lname` varchar(255) NOT NULL,
  `Gender` int(11) NOT NULL,
  `PrivateNumber` int(11) NOT NULL,
  `Date` text NOT NULL,
  `City` varchar(255) NOT NULL,
  `PhoneNumber` varchar(255) NOT NULL,
  `Image` text NOT NULL,
  `PersonId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `persons_tbl`
--

INSERT INTO `persons_tbl` (`Index`, `Fname`, `Lname`, `Gender`, `PrivateNumber`, `Date`, `City`, `PhoneNumber`, `Image`, `PersonId`) VALUES
(2, 'sas', 'sas', 1, 2147483647, '2003/01/16', 'sas', '595595959', '..\\..\\..\\..\\images\\176885674.png', 176885674),
(9, 'string', 'string', 1, 2147483647, '2003/01/16', 'string', '595595742', '..\\..\\..\\..\\images\\176885674.png', 606924769),
(10, 'string', 'string', 1, 2147483647, '2003/01/16', 'string', '595595742', 'string', 854268765),
(11, 'string', 'string', 0, 0, '2000/12/12', '2000/12/12', 'string', 'string', 586752500),
(12, 'string', 'string', 0, 0, '2000/12/12', '2000/12/12', 'string', 'string', 426510263),
(13, 'string', 'string', 0, 0, '2000/12/12', '2000/12/12', 'string', 'string', 223920812),
(14, 'string', 'string', 0, 0, '2000/12/12', 'string', 'string', 'string', 871051025),
(15, 'string', 'string', 0, 0, '2000/12/12', 'string', 'string', 'string', 827262803),
(16, 'string', 'string', 0, 0, 'string', 'string', 'string', 'string', 615382014);

-- --------------------------------------------------------

--
-- Table structure for table `relations_tbl`
--

CREATE TABLE `relations_tbl` (
  `Id` int(11) NOT NULL,
  `PersonId` text NOT NULL,
  `RelationId` text NOT NULL,
  `RelationType` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `relations_tbl`
--

INSERT INTO `relations_tbl` (`Id`, `PersonId`, `RelationId`, `RelationType`) VALUES
(5, '606924769', '176885674', 'other'),
(6, '606924769', '426510263', 'colleague');

-- --------------------------------------------------------

--
-- Table structure for table `requestlog_tbl`
--

CREATE TABLE `requestlog_tbl` (
  `Id` int(11) NOT NULL,
  `Method` varchar(255) NOT NULL,
  `Path` varchar(255) NOT NULL,
  `StatusCode` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `requestlog_tbl`
--

INSERT INTO `requestlog_tbl` (`Id`, `Method`, `Path`, `StatusCode`) VALUES
(1, 'GET', '/swagger/index.html', '200'),
(2, 'GET', '/swagger/v1/swagger.json', '200'),
(3, 'POST', '/Person/176885674', '405');

-- --------------------------------------------------------

--
-- Table structure for table `test`
--

CREATE TABLE `test` (
  `id` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `test`
--

INSERT INTO `test` (`id`) VALUES
('[value-1]'),
('[value-1]'),
('4'),
('3'),
('2'),
('2'),
('3'),
('1'),
('111');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `persons_tbl`
--
ALTER TABLE `persons_tbl`
  ADD PRIMARY KEY (`Index`);

--
-- Indexes for table `relations_tbl`
--
ALTER TABLE `relations_tbl`
  ADD PRIMARY KEY (`Id`);

--
-- Indexes for table `requestlog_tbl`
--
ALTER TABLE `requestlog_tbl`
  ADD PRIMARY KEY (`Id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `persons_tbl`
--
ALTER TABLE `persons_tbl`
  MODIFY `Index` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=17;

--
-- AUTO_INCREMENT for table `relations_tbl`
--
ALTER TABLE `relations_tbl`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- AUTO_INCREMENT for table `requestlog_tbl`
--
ALTER TABLE `requestlog_tbl`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
