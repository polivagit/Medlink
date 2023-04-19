-- -----------------------------------------------------
-- Data for table `medlink`.`treatment`
-- -----------------------------------------------------
START TRANSACTION;
USE `medlink`;

INSERT INTO `medlink`.`treatment` (`trea_name`, `trea_description`, `trea_date_start`, `trea_date_end`, `trea_observations`, `trea_is_active`, `trea_doctor_id`, `trea_patient_id`) VALUES ('Treatment 1', 'Description for Treatment 1', '2022-05-01','2023-10-12', null, 0, 5, 1);
INSERT INTO `medlink`.`treatment` (`trea_name`, `trea_description`, `trea_date_start`, `trea_date_end`, `trea_observations`, `trea_is_active`, `trea_doctor_id`, `trea_patient_id`) VALUES ('Treatment 2', 'Description for Treatment 2', '2021-07-08','2023-10-12', null, 0, 5, 1);

INSERT INTO `medlink`.`treatment` (`trea_name`, `trea_description`, `trea_date_start`, `trea_date_end`, `trea_observations`, `trea_is_active`, `trea_doctor_id`, `trea_patient_id`) VALUES ('Treatment 1', 'Description for Treatment 1', '2022-01-20','2023-09-01', null, 0, 5, 2);
INSERT INTO `medlink`.`treatment` (`trea_name`, `trea_description`, `trea_date_start`, `trea_date_end`, `trea_observations`, `trea_is_active`, `trea_doctor_id`, `trea_patient_id`) VALUES ('Treatment 2', 'Description for Treatment 2', '2022-07-08','2023-08-14', null, 0, 5, 2);
INSERT INTO `medlink`.`treatment` (`trea_name`, `trea_description`, `trea_date_start`, `trea_date_end`, `trea_observations`, `trea_is_active`, `trea_doctor_id`, `trea_patient_id`) VALUES ('Treatment 3', 'Description for Treatment 3', '2023-01-03','2023-10-03', null, 0, 5, 2);

INSERT INTO `medlink`.`treatment` (`trea_name`, `trea_description`, `trea_date_start`, `trea_date_end`, `trea_observations`, `trea_is_active`, `trea_doctor_id`, `trea_patient_id`) VALUES ('Treatment 1', 'Description for Treatment 1', '2022-09-01','2023-11-20', null, 0, 6, 3);

COMMIT;