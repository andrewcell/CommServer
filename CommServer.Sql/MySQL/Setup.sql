CREATE TABLE `comm`.`comments` ( `articleid` INT NOT NULL , `authorid` INT NOT NULL , `descr` VARCHAR(45) NOT NULL , `up` INT NOT NULL , `down` INT NOT NULL , `date` DATE NOT NULL , `permission` INT NOT NULL ) ENGINE = InnoDB;
CREATE TABLE `accounts` (
	`id` VARCHAR(50) NULL,
	`password` VARCHAR(50) NULL,
	`nickname` VARCHAR(50) NULL,
	`lastip` VARCHAR(50) NULL,
	`lastlogin` DATE NULL,
	`email` DATE NULL,
	`recoveryenabled` INT NULL,
	`recoverytoken` VARCHAR(50) NULL
)
COLLATE='utf8_general_ci'
ENGINE=InnoDB
;
CREATE TABLE `articles` (
	`authorid` INT NOT NULL,
	`title` VARCHAR(50) NOT NULL,
	`descr` VARCHAR(50) NOT NULL,
	`up` VARCHAR(50) NOT NULL,
	`down` VARCHAR(50) NOT NULL,
	`notice` INT NULL,
	`hot` INT NULL,
	`badarticle` INT NULL,
	`badarticledesc` VARCHAR(50) NULL
)
COLLATE='utf8_general_ci'
ENGINE=InnoDB
;

