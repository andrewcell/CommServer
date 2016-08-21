
CREATE TABLE `accounts` (
	`id` DATETIME NOT NULL,
	`username` VARCHAR(50) NULL DEFAULT NULL,
	`password` VARCHAR(50) NULL DEFAULT NULL,
	`nickname` VARCHAR(50) NULL DEFAULT NULL,
	`lastip` VARCHAR(50) NULL DEFAULT NULL,
	`lastlogin` DATETIME NULL DEFAULT NULL,
	`email` VARCHAR(50) NULL DEFAULT NULL,
	`recoveryenabled` INT(11) NULL DEFAULT NULL,
	`recoverytoken` VARCHAR(50) NULL DEFAULT NULL,
	PRIMARY KEY (`id`)
)
COLLATE='utf8_general_ci'
ENGINE=InnoDB
;
CREATE TABLE `articles` (
	`id` INT(11) NOT NULL,
	`authorid` INT(11) NOT NULL,
	`title` VARCHAR(50) NOT NULL,
	`descr` VARCHAR(50) NOT NULL,
	`up` VARCHAR(50) NOT NULL,
	`down` VARCHAR(50) NOT NULL,
	`notice` INT(11) NULL DEFAULT NULL,
	`hot` INT(11) NULL DEFAULT NULL,
	`badarticle` INT(11) NULL DEFAULT NULL,
	`badarticledesc` VARCHAR(50) NULL DEFAULT NULL,
	PRIMARY KEY (`id`)
)
COLLATE='utf8_general_ci'
ENGINE=InnoDB
;

CREATE TABLE `comments` (
	`id` INT(11) NOT NULL,
	`articleid` INT(11) NOT NULL,
	`authorid` INT(11) NOT NULL,
	`descr` VARCHAR(45) NOT NULL,
	`up` INT(11) NOT NULL,
	`down` INT(11) NOT NULL,
	`date` DATE NOT NULL,
	`permission` INT(11) NOT NULL,
	PRIMARY KEY (`id`)
)
COLLATE='utf8_general_ci'
ENGINE=InnoDB
;
