-- -----------------------------------------------------
-- Data for table `medlink`.`person`
-- -----------------------------------------------------
START TRANSACTION;
USE `medlink`;

INSERT INTO `medlink`.`person` (`pers_nif`,`pers_first_name`,`pers_last_name_1`,`pers_last_name_2`,`pers_birthdate`,`pers_phone_number`,`pers_email`,`pers_gender`,`pers_address`,`pers_login_username`,`pers_login_password`) VALUES ( '111222435','aaa','bbb','ccc','1900-01-01','999999955','emai43',0,'blablbla','aaatsm','aaa');

COMMIT;