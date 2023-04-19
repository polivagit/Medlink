-- -----------------------------------------------------
-- Data for table `medlink`.`patient`
-- -----------------------------------------------------
START TRANSACTION;
USE `medlink`;

INSERT INTO `medlink`.`patient` (`pati_person_id`, `pati_height`, `pati_weight`, `pati_remarks`, `pati_caregiver_id`) VALUES (1,178,77.3,'',null);
INSERT INTO `medlink`.`patient` (`pati_person_id`, `pati_height`, `pati_weight`, `pati_remarks`, `pati_caregiver_id`) VALUES (2,168,67.7,'',null);
INSERT INTO `medlink`.`patient` (`pati_person_id`, `pati_height`, `pati_weight`, `pati_remarks`, `pati_caregiver_id`) VALUES (3,190,83.2,'',4);
INSERT INTO `medlink`.`patient` (`pati_person_id`, `pati_height`, `pati_weight`, `pati_remarks`, `pati_caregiver_id`) VALUES (4,171,69.1,'',null);

COMMIT;