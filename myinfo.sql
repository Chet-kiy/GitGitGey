-- phpMyAdmin SQL Dump
-- version 4.9.5
-- https://www.phpmyadmin.net/
--
-- Хост: localhost:3306
-- Время создания: Май 25 2020 г., 16:03
-- Версия сервера: 5.7.24
-- Версия PHP: 7.4.1

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- База данных: `myinfo`
--

-- --------------------------------------------------------

--
-- Структура таблицы `infousers`
--

CREATE TABLE `infousers` (
  `idAccount` int(11) UNSIGNED NOT NULL,
  `name` varchar(100) NOT NULL,
  `password` varchar(100) NOT NULL,
  `Description` varchar(100) NOT NULL,
  `idUser` int(11) UNSIGNED NOT NULL
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

--
-- Дамп данных таблицы `infousers`
--

INSERT INTO `infousers` (`idAccount`, `name`, `password`, `Description`, `idUser`) VALUES
(8, 'VK', 'iyadura', 'hehehe', 11),
(7, 'Steam', 'myaccount', 'blablabla', 11),
(6, 'Origin', 'Sobaka2002', 'Account Origin', 10),
(5, 'Steam', '123', 'Play Game ', 9),
(9, 'Classroom', '123456', 'my love family', 9),
(10, 'Origin', 'qwerty', 'game and fun', 9);

-- --------------------------------------------------------

--
-- Структура таблицы `users`
--

CREATE TABLE `users` (
  `id` int(11) UNSIGNED NOT NULL,
  `login` varchar(100) NOT NULL,
  `pass` varchar(255) NOT NULL,
  `name` varchar(100) NOT NULL,
  `surname` varchar(100) NOT NULL
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

--
-- Дамп данных таблицы `users`
--

INSERT INTO `users` (`id`, `login`, `pass`, `name`, `surname`) VALUES
(11, 'Alex', '0b50457862262de3ad31befe963f5f48d113e0c1e5bf9567bbc18700e8b4aa55', 'LExa', 'Lepexa'),
(10, 'Admin', 'a2738733997d50b6bfa133dbe35a0e97e9168f70fc73fdf646974ead5d6fb44c', 'aaa', 'bbbb'),
(9, 'Test', '29c2e2b20cfbf45e181a97016dc86dc194076e6a170a5d3535c3cf2b4e89e3a4', 'Natali', 'Entiti'),
(12, 'Nikita_dodik', '31165d47dcaf4ed416e026ee9082023d858b3d56be27fd3993882b1f6fecdab1', 'Nikita', 'Ermolaev');

--
-- Индексы сохранённых таблиц
--

--
-- Индексы таблицы `infousers`
--
ALTER TABLE `infousers`
  ADD UNIQUE KEY `idAccount` (`idAccount`);

--
-- Индексы таблицы `users`
--
ALTER TABLE `users`
  ADD UNIQUE KEY `id` (`id`);

--
-- AUTO_INCREMENT для сохранённых таблиц
--

--
-- AUTO_INCREMENT для таблицы `infousers`
--
ALTER TABLE `infousers`
  MODIFY `idAccount` int(11) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- AUTO_INCREMENT для таблицы `users`
--
ALTER TABLE `users`
  MODIFY `id` int(11) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=13;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
