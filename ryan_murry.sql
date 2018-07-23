-- phpMyAdmin SQL Dump
-- version 4.7.7
-- https://www.phpmyadmin.net/
--
-- Host: localhost:8889
-- Generation Time: Jul 23, 2018 at 09:02 AM
-- Server version: 5.6.38
-- PHP Version: 7.2.1

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `ryan_murry`
--
CREATE DATABASE IF NOT EXISTS `ryan_murry` DEFAULT CHARACTER SET utf8 COLLATE utf8_general_ci;
USE `ryan_murry`;

-- --------------------------------------------------------

--
-- Table structure for table `clients`
--

CREATE TABLE `clients` (
  `id` int(11) NOT NULL,
  `first_name` varchar(255) NOT NULL,
  `last_name` varchar(255) NOT NULL,
  `email` varchar(255) NOT NULL,
  `phone_number` varchar(255) NOT NULL,
  `street_address` varchar(255) NOT NULL,
  `city` varchar(255) NOT NULL,
  `state` varchar(255) NOT NULL,
  `zip` varchar(255) NOT NULL,
  `age` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `clients_needs`
--

CREATE TABLE `clients_needs` (
  `id` int(11) NOT NULL,
  `client_id` int(11) NOT NULL,
  `specialty_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `specialties`
--

CREATE TABLE `specialties` (
  `id` int(11) NOT NULL,
  `subject` varchar(255) NOT NULL,
  `discipline` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `specialties`
--

INSERT INTO `specialties` (`id`, `subject`, `discipline`) VALUES
(1, 'Geometry', 'Math'),
(2, 'Trigonometry', 'Math'),
(3, 'Pre-Algebra', 'Math'),
(4, 'Algebra 1', 'Math'),
(5, 'Algebra 2', 'Math'),
(6, 'Pre-Calculus', 'Math'),
(7, 'Calculus', 'Math'),
(8, 'Statistics', 'Math'),
(9, 'Discrete Math', 'Math'),
(10, 'Anatomy & Physiology', 'Science'),
(11, 'Earth Science', 'Science'),
(12, 'Biology', 'Science'),
(13, 'Chemistry', 'Science'),
(14, 'Physics', 'Science'),
(15, 'Essay Writing', 'Humanities'),
(16, 'Literature', 'Humanities'),
(17, 'Philosophy', 'Humanities'),
(18, 'U.S. History', 'Humanities'),
(19, 'World History', 'Humanities'),
(20, 'Elementary School Social Studies', 'Humanities'),
(21, 'Middle School Social Studies', 'Humanities'),
(22, 'High School Social Studies', 'Humanities'),
(23, 'AP Calculus AB', 'AP'),
(24, 'AP Calculus BC', 'AP'),
(25, 'AP Statistics', 'AP'),
(26, 'AP Computer Science A', 'AP'),
(27, 'AP Computer Science Principles', 'AP'),
(28, 'AP Biology', 'AP'),
(29, 'AP Chemistry', 'AP'),
(30, 'AP Environmental Science', 'AP'),
(31, 'AP Physics B', 'AP'),
(32, 'AP Physics C', 'AP'),
(33, 'AP Psychology', 'AP'),
(34, 'AP English Language', 'AP'),
(35, 'AP English Literature', 'AP'),
(36, 'AP U.S. History', 'AP'),
(37, 'AP World History', 'AP'),
(38, 'AP European History', 'AP'),
(39, 'AP Government and Politics', 'AP'),
(40, 'AP Macroeconomics', 'AP'),
(41, 'AP Microeconomics', 'AP'),
(42, 'Chinese', 'Foreign Language'),
(43, 'French', 'Foreign Language'),
(44, 'Japanese', 'Foreign Language'),
(45, 'German', 'Foreign Language'),
(46, 'Russian', 'Foreign Language'),
(47, 'Spanish', 'Foreign Language');

-- --------------------------------------------------------

--
-- Table structure for table `tutors`
--

CREATE TABLE `tutors` (
  `id` int(11) NOT NULL,
  `first_name` varchar(255) NOT NULL,
  `last_name` varchar(255) NOT NULL,
  `email` varchar(255) NOT NULL,
  `phone_number` varchar(255) NOT NULL,
  `experience` int(11) NOT NULL,
  `credential` tinyint(1) NOT NULL,
  `availability` varchar(255) NOT NULL,
  `rate` double NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `tutors_availability`
--

CREATE TABLE `tutors_availability` (
  `id` int(11) NOT NULL,
  `tutor_id` int(11) NOT NULL,
  `availability_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `tutors_clients`
--

CREATE TABLE `tutors_clients` (
  `id` int(11) NOT NULL,
  `tutor_id` int(11) NOT NULL,
  `client_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `tutors_specialties`
--

CREATE TABLE `tutors_specialties` (
  `id` int(11) NOT NULL,
  `tutor_id` int(11) NOT NULL,
  `specialty_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Indexes for dumped tables
--

--
-- Indexes for table `clients`
--
ALTER TABLE `clients`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `clients_needs`
--
ALTER TABLE `clients_needs`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `specialties`
--
ALTER TABLE `specialties`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `tutors`
--
ALTER TABLE `tutors`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `tutors_availability`
--
ALTER TABLE `tutors_availability`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `tutors_clients`
--
ALTER TABLE `tutors_clients`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `tutors_specialties`
--
ALTER TABLE `tutors_specialties`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `clients`
--
ALTER TABLE `clients`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=15;

--
-- AUTO_INCREMENT for table `clients_needs`
--
ALTER TABLE `clients_needs`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=39;

--
-- AUTO_INCREMENT for table `specialties`
--
ALTER TABLE `specialties`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=50;

--
-- AUTO_INCREMENT for table `tutors`
--
ALTER TABLE `tutors`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=21;

--
-- AUTO_INCREMENT for table `tutors_availability`
--
ALTER TABLE `tutors_availability`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `tutors_clients`
--
ALTER TABLE `tutors_clients`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=26;

--
-- AUTO_INCREMENT for table `tutors_specialties`
--
ALTER TABLE `tutors_specialties`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=110;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
