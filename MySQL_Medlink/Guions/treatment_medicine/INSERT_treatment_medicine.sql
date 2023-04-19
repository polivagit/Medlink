-- -----------------------------------------------------
-- Data for table `medlink`.`treatment_medicine`
-- -----------------------------------------------------
START TRANSACTION;
USE `medlink`;

INSERT INTO `medlink`.`treatment` (`trme_treatment_id`, `trme_medicine_id`, `trme_quantity_per_day`, `trme_total_quantity`, `trme_unit_of_measure_id`) VALUES (1, 4, 10, 5290, 3);

INSERT INTO `medlink`.`treatment` (`trme_treatment_id`, `trme_medicine_id`, `trme_quantity_per_day`, `trme_total_quantity`, `trme_unit_of_measure_id`) VALUES (2, 12, 30, 24780, 5);


INSERT INTO `medlink`.`treatment` (`trme_treatment_id`, `trme_medicine_id`, `trme_quantity_per_day`, `trme_total_quantity`, `trme_unit_of_measure_id`) VALUES (3, 49, 24, 14136, 5);
INSERT INTO `medlink`.`treatment` (`trme_treatment_id`, `trme_medicine_id`, `trme_quantity_per_day`, `trme_total_quantity`, `trme_unit_of_measure_id`) VALUES (4, 33, 50, 16400, 3);
INSERT INTO `medlink`.`treatment` (`trme_treatment_id`, `trme_medicine_id`, `trme_quantity_per_day`, `trme_total_quantity`, `trme_unit_of_measure_id`) VALUES (5, 11, 32, 8768, 5);


INSERT INTO `medlink`.`treatment` (`trme_treatment_id`, `trme_medicine_id`, `trme_quantity_per_day`, `trme_total_quantity`, `trme_unit_of_measure_id`) VALUES (6, 80, 80, 35680, 3);

COMMIT;