-- -----------------------------------------------------
-- Data for table `medlink`.`person`
-- -----------------------------------------------------

START TRANSACTION;
USE `medlink`;

INSERT INTO `medlink`.`person` (`pers_nif`,`pers_first_name`,`pers_last_name_1`,`pers_last_name_2`,`pers_birthdate`,`pers_phone_number`,`pers_email`,`pers_gender`,`pers_addrs_street`,`pers_addrs_zip_code`,`pers_addrs_city`,`pers_addrs_province`,`pers_addrs_country`,`pers_login_username`,`pers_login_password`)
VALUES ( '47834720A','Pablo','García','Rodríguez','1989-04-22','635346505','pgarcia1@gmail.com',0,'Carrer de Sant Magí 3','08700','Igualada','Barcelona','España','pgarcia1','4720A');
INSERT INTO `medlink`.`person` (`pers_nif`,`pers_first_name`,`pers_last_name_1`,`pers_last_name_2`,`pers_birthdate`,`pers_phone_number`,`pers_email`,`pers_gender`,`pers_addrs_street`,`pers_addrs_zip_code`,`pers_addrs_city`,`pers_addrs_province`,`pers_addrs_country`,`pers_login_username`,`pers_login_password`)
VALUES ( '48929311Z','Paula','Sánchez','Pérez','1973-01-11','911161347','psanchez1@gmail.com',1,'Carrer Aurora 17','08700','Igualada','Barcelona','España','psanchez1','9311Z');
INSERT INTO `medlink`.`person` (`pers_nif`,`pers_first_name`,`pers_last_name_1`,`pers_last_name_2`,`pers_birthdate`,`pers_phone_number`,`pers_email`,`pers_gender`,`pers_addrs_street`,`pers_addrs_zip_code`,`pers_addrs_city`,`pers_addrs_province`,`pers_addrs_country`,`pers_login_username`,`pers_login_password`)
VALUES ( '46372813J','Lucas','Fernández','Ruiz','1954-06-02','931639934','lfernandez1@gmail.com',0,'Carrer de Santa Anna 22','08700','Igualada','Barcelona','España','lfernandez1','2813J');
INSERT INTO `medlink`.`person` (`pers_nif`,`pers_first_name`,`pers_last_name_1`,`pers_last_name_2`,`pers_birthdate`,`pers_phone_number`,`pers_email`,`pers_gender`,`pers_addrs_street`,`pers_addrs_zip_code`,`pers_addrs_city`,`pers_addrs_province`,`pers_addrs_country`,`pers_login_username`,`pers_login_password`)
VALUES ( '48339921N','Lucía','Moreno','Díaz','1993-07-23','960628778','lmoreno1@gmail.com',3,'Carrer de la Trinitat 7','08700','Igualada','Barcelona','España','lmoreno1','9921N');
INSERT INTO `medlink`.`person` (`pers_nif`,`pers_first_name`,`pers_last_name_1`,`pers_last_name_2`,`pers_birthdate`,`pers_phone_number`,`pers_email`,`pers_gender`,`pers_addrs_street`,`pers_addrs_zip_code`,`pers_addrs_city`,`pers_addrs_province`,`pers_addrs_country`,`pers_login_username`,`pers_login_password`)
VALUES ( '45884032H','Martín','Hernández','Muñoz','1969-11-05','653460845','mhernandez1@gmail.com',3,'Carrer de Vic 2','08700','Igualada','Barcelona','España','mhernandez1','4032H');
INSERT INTO `medlink`.`person` (`pers_nif`,`pers_first_name`,`pers_last_name_1`,`pers_last_name_2`,`pers_birthdate`,`pers_phone_number`,`pers_email`,`pers_gender`,`pers_addrs_street`,`pers_addrs_zip_code`,`pers_addrs_city`,`pers_addrs_province`,`pers_addrs_country`,`pers_login_username`,`pers_login_password`)
VALUES ( '47849493T','Martina','Álvarez','Jiménez','1991-08-14','685332940','malvarez1@gmail.com',1,'Carrer de Grècia 31','08700','Igualada','Barcelona','España','malvarez1','9493T');

COMMIT;