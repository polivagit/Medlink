-- -----------------------------------------------------
-- Data for table `medlink`.`units_of_measure`
-- -----------------------------------------------------
START TRANSACTION;
USE `medlink`;

INSERT INTO `medlink`.`units_of_measure` (`unme_name`, `unme_abbreviation`) VALUES ('kilogram', 'Kg');
INSERT INTO `medlink`.`units_of_measure` (`unme_name`, `unme_abbreviation`) VALUES ('gram', 'g');
INSERT INTO `medlink`.`units_of_measure` (`unme_name`, `unme_abbreviation`) VALUES ('milligram', 'mg');
INSERT INTO `medlink`.`units_of_measure` (`unme_name`, `unme_abbreviation`) VALUES ('microgram', 'mcg');
INSERT INTO `medlink`.`units_of_measure` (`unme_name`, `unme_abbreviation`) VALUES ('litre', 'L');
INSERT INTO `medlink`.`units_of_measure` (`unme_name`, `unme_abbreviation`) VALUES ('millilitre', 'ml');
INSERT INTO `medlink`.`units_of_measure` (`unme_name`, `unme_abbreviation`) VALUES ('cubic centimetre', 'cc');
INSERT INTO `medlink`.`units_of_measure` (`unme_name`, `unme_abbreviation`) VALUES ('mole', 'mol');
INSERT INTO `medlink`.`units_of_measure` (`unme_name`, `unme_abbreviation`) VALUES ('millimole', 'mmol');

COMMIT;