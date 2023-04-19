-- -----------------------------------------------------
-- Data for table `medlink`.`doctor`
-- -----------------------------------------------------
START TRANSACTION;
USE `medlink`;

INSERT INTO `medlink`.`doctor` (`doct_person_id`, `doct_collegiate_uid`, `doct_specialty_id`) VALUES (5,'01-100-AAAAA',3);
INSERT INTO `medlink`.`doctor` (`doct_person_id`, `doct_collegiate_uid`, `doct_specialty_id`) VALUES (6,'02-200-BBBBB',8);

COMMIT;