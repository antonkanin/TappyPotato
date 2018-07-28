CREATE TABLE `score_board` (
  `number` int(11) NOT NULL,
  `player_id` varchar(100) NOT NULL,
  `player_name` varchar(100) NOT NULL,
  `score` int(11) NOT NULL,
  `death_position` int(11) NOT NULL,
  `date_created` datetime NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

ALTER TABLE `score_board`
  ADD PRIMARY KEY (`number`);

ALTER TABLE `score_board`
  MODIFY `number` int(11) NOT NULL AUTO_INCREMENT;
