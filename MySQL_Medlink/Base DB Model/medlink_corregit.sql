-- -----------------------------------------------------
-- Schema medlink
-- -----------------------------------------------------
DROP SCHEMA IF EXISTS `medlink` ;
CREATE SCHEMA `medlink` DEFAULT CHARACTER SET utf8 ;
USE `medlink` ;

-- -----------------------------------------------------
-- Table `medlink`.`person`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `medlink`.`person` ;

CREATE TABLE `medlink`.`person` (
  `pers_id` INT AUTO_INCREMENT,
  `pers_nif` VARCHAR(9) NOT NULL CHECK (REGEXP_INSTR(pers_nif, '^[0-9]{8}[A-Z]{1}$')),
  `pers_first_name` VARCHAR(50) NOT NULL,
  `pers_last_name_1` VARCHAR(50) NOT NULL,
  `pers_last_name_2` VARCHAR(50),
  `pers_birthdate` DATE NOT NULL, -- VALUE RANGE DEFINED BY TRIGGER
  `pers_phone_number` VARCHAR(9) NOT NULL,
  `pers_email` VARCHAR(50) NOT NULL,
  `pers_gender` INT NOT NULL CHECK (`pers_gender` IN (0,1,2,3)),
  `pers_address` VARCHAR(100) NOT NULL,
  `pers_login_username` VARCHAR(40) NOT NULL,
  `pers_login_password` VARCHAR(40) NOT NULL,
  PRIMARY KEY (`pers_id`),
  UNIQUE INDEX `pers_nif_UNIQUE` (`pers_nif` ASC),
  UNIQUE INDEX `pers_phone_number_UNIQUE` (`pers_phone_number` ASC),
  UNIQUE INDEX `pers_email_UNIQUE` (`pers_email` ASC),
  UNIQUE INDEX `pers_login_username_UNIQUE` (`pers_login_username` ASC)
  );

-- -----------------------------------------------------
-- Table `medlink`.`patient`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `medlink`.`patient` ;

CREATE TABLE `medlink`.`patient` (
  `pati_person_id` INT,
  `pati_height` INT NOT NULL CHECK (`pati_height` >= 30 AND `pati_height` <= 260),
  `pati_weight` FLOAT NOT NULL CHECK (`pati_weight` >= 2 AND `pati_weight` <= 400),
  `pati_remarks` MEDIUMTEXT,
  `pati_caregiver_id` INT,
  PRIMARY KEY (`pati_person_id`),
  INDEX `fk_patient_person_idx` (`pati_person_id` ASC),
  CONSTRAINT `fk_patient_person`
    FOREIGN KEY (`pati_person_id`)
    REFERENCES `medlink`.`person` (`pers_id`),
  CONSTRAINT `fk_caregiver_person`
    FOREIGN KEY (`pati_caregiver_id`)
    REFERENCES `medlink`.`person` (`pers_id`)
	);


-- -----------------------------------------------------
-- Table `medlink`.`specialty`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `medlink`.`specialty` ;

CREATE TABLE `medlink`.`specialty` (
  `spec_id` INT AUTO_INCREMENT,
  `spec_name` VARCHAR(60) NOT NULL,
  PRIMARY KEY (`spec_id`),
  UNIQUE INDEX `spec_name_UNIQUE` (`spec_name` ASC)
);


-- -----------------------------------------------------
-- Table `medlink`.`doctor`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `medlink`.`doctor` ;

CREATE TABLE `medlink`.`doctor` (
  `doct_person_id` INT,
  `doct_collegiate_uid` VARCHAR(12) NOT NULL,
  `doct_specialty_id` INT NOT NULL,
  PRIMARY KEY (`doct_person_id`),
  UNIQUE INDEX `doct_collegiate_uid_UNIQUE` (`doct_collegiate_uid` ASC),
  INDEX `fk_doctor_person_idx` (`doct_person_id` ASC),
  INDEX `fk_doctor_specialty_idx` (`doct_specialty_id` ASC),
  CONSTRAINT `fk_doctor_person`
    FOREIGN KEY (`doct_person_id`)
    REFERENCES `medlink`.`person` (`pers_id`),
  CONSTRAINT `fk_doctor_specialty`
    FOREIGN KEY (`doct_specialty_id`)
    REFERENCES `medlink`.`specialty` (`spec_id`)
    );


-- -----------------------------------------------------
-- Table `medlink`.`treatment`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `medlink`.`treatment` ;

CREATE TABLE `medlink`.`treatment` (
  `trea_id` INT AUTO_INCREMENT,
  `trea_name` VARCHAR(60) NOT NULL,
  `trea_description` VARCHAR(60) NOT NULL,
  `trea_date_start` DATE NOT NULL, -- VALUE RANGE DEFINED BY TRIGGER
  `trea_date_end` DATE NOT NULL, -- VALUE RANGE DEFINED BY TRIGGER
  `trea_observations` MEDIUMTEXT,
  `trea_is_active` TINYINT NOT NULL CHECK (`trea_is_active` IN (0,1)), -- 0 = true or 1 = false
  `trea_doctor_id` INT NOT NULL,
  `trea_patient_id` INT NOT NULL,
  PRIMARY KEY (`trea_id`),
  INDEX `fk_treatment_doctor_idx` (`trea_doctor_id` ASC),
  INDEX `fk_treatment_patient_idx` (`trea_patient_id` ASC),
  CONSTRAINT `fk_treatment_doctor`
    FOREIGN KEY (`trea_doctor_id`)
    REFERENCES `medlink`.`doctor` (`doct_person_id`),
  CONSTRAINT `fk_treatment_patient`
    FOREIGN KEY (`trea_patient_id`)
    REFERENCES `medlink`.`patient` (`pati_person_id`)
    );


-- -----------------------------------------------------
-- Table `medlink`.`medicine_category`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `medlink`.`medicine_category` ;

CREATE TABLE `medlink`.`medicine_category` (
  `meca_id` INT AUTO_INCREMENT,
  `meca_name` VARCHAR(60) NOT NULL,
  PRIMARY KEY (`meca_id`)    
);


-- -----------------------------------------------------
-- Table `medlink`.`medicine`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `medlink`.`medicine` ;

CREATE TABLE `medlink`.`medicine` (
  `medi_id` INT AUTO_INCREMENT,
  `medi_name` VARCHAR(50) NOT NULL,
  `medi_category_id` INT NOT NULL,
  PRIMARY KEY (`medi_id`),
  INDEX `fk_medicine_medicine_category_idx` (`medi_category_id` ASC),
  CONSTRAINT `fk_medicine_medicine_category`
    FOREIGN KEY (`medi_category_id`)
    REFERENCES `medlink`.`medicine_category` (`meca_id`)
    );

-- -----------------------------------------------------
-- Table `medlink`.`units_of_measure`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `medlink`.`units_of_measure` ;

CREATE TABLE `medlink`.`units_of_measure` (
  `unme_id` INT AUTO_INCREMENT,
  `unme_name` VARCHAR(20) NOT NULL,
  `unme_abbreviation` VARCHAR(20) NOT NULL,
  PRIMARY KEY (`unme_id`),
  UNIQUE INDEX `fk_units_of_measure_name_UNIQUE` (`unme_name` ASC),
  UNIQUE INDEX `fk_units_of_measure_abbreviation_UNIQUE` (`unme_abbreviation` ASC)
 );
 

-- -----------------------------------------------------
-- Table `medlink`.`treatment_medicine`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `medlink`.`treatment_medicine` ;

CREATE TABLE `medlink`.`treatment_medicine` (
  `trme_treatment_id` INT,
  `trme_medicine_id` INT,
  `trme_quantity_per_day` FLOAT NOT NULL,
  `trme_total_quantity` FLOAT NOT NULL,
  `trme_unit_of_measure_id` INT NOT NULL,
  PRIMARY KEY (`trme_treatment_id`, `trme_medicine_id`),
  INDEX `fk_treatment_medicine_medicine_idx` (`trme_medicine_id` ASC),
  INDEX `fk_treatment_medicine_treatment_idx` (`trme_treatment_id` ASC),
  UNIQUE INDEX `trme_medicine_id_UNIQUE` (`trme_medicine_id` ASC),
  CONSTRAINT `fk_treatment_medicine_medicine`
    FOREIGN KEY (`trme_medicine_id`)
    REFERENCES `medlink`.`medicine` (`medi_id`),
  CONSTRAINT `fk_treatment_medicine_treatment`
    FOREIGN KEY (`trme_treatment_id`)
    REFERENCES `medlink`.`treatment` (`trea_id`),
  CONSTRAINT `fk_treatment_treatment_unit_of_measure`
    FOREIGN KEY (`trme_unit_of_measure_id`)
    REFERENCES `medlink`.`units_of_measure` (`unme_id`)
    
);
-- -----------------------------------------------------
-- TRIGGERS
-- -----------------------------------------------------

-- [`pers_birthdate` DATE NOT NULL] CHECK (`pers_birthdate` BETWEEN '1900-01-01' AND CURRENT_DATE):
-- BEFORE INSERT 
DELIMITER //
CREATE TRIGGER trg_check_before_insert_person_pers_birthdate BEFORE INSERT ON person
FOR EACH ROW
BEGIN
    IF NEW.pers_birthdate < '1900-01-01' OR NEW.pers_birthdate > CURDATE() THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Invalid person birthdate. Birthdate must be between 1900-01-01 and CURRENT DATE!';
    END IF;
END;
//


-- BEFORE UPDATE
DELIMITER //
CREATE TRIGGER trg_check_before_update_person_pers_birthdate BEFORE UPDATE ON person
FOR EACH ROW
BEGIN
    IF NEW.pers_birthdate < '1900-01-01' OR NEW.pers_birthdate > CURDATE() THEN
        SIGNAL SQLSTATE '45001' SET MESSAGE_TEXT = 'Invalid person birthdate. Birthdate must be between 1900-01-01 and CURRENT DATE!';
    END IF;
END;
//

-- [`trea_date_start` DATE NOT NULL] CHECK (`trea_date_start` BETWEEN '2000-01-01' AND CURRENT_DATE):
-- BEFORE INSERT 
DELIMITER //
CREATE TRIGGER trg_check_before_insert_treatment_trea_date_start BEFORE INSERT ON treatment
FOR EACH ROW
BEGIN
    IF NEW.trea_date_start < '2000-01-01' OR NEW.trea_date_start > CURDATE() THEN
        SIGNAL SQLSTATE '45002' SET MESSAGE_TEXT = 'Invalid treatment date_start. Date_start of treatment must be between ''2000-01-01'' and ''CURRENT DATE''!';
    END IF;
END;
//

-- BEFORE UPDATE
DELIMITER //
CREATE TRIGGER trg_check_before_update_treatment_trea_date_start BEFORE UPDATE ON treatment
FOR EACH ROW
BEGIN
    IF NEW.trea_date_start < '2000-01-01' OR NEW.trea_date_start > CURDATE() THEN
        SIGNAL SQLSTATE '45003' SET MESSAGE_TEXT = 'Invalid treatment date_start. Date_start of treatment must be between ''2000-01-01'' and ''CURRENT DATE''!';
    END IF;
END;
//

-- [`trea_date_end` DATE NOT NULL] CHECK (`trea_date_end` BETWEEN `trea_date_start` AND '3000-01-01'):
-- BEFORE INSERT
DELIMITER //
CREATE TRIGGER trg_check_treatment_before_insert_date_end 
BEFORE INSERT ON treatment
FOR EACH ROW
BEGIN
    IF NEW.trea_date_end < NEW.trea_date_start OR NEW.trea_date_end > '3000-01-01' THEN
        SIGNAL SQLSTATE '45004' SET MESSAGE_TEXT = 'Invalid treatment date_end. Date_end of treatment must be between date_start and ''3000-01-01!';
    END IF;
END;
//

-- BEFORE UPDATE
DELIMITER //
CREATE TRIGGER trg_check_treatment_before_update_date_end BEFORE UPDATE ON treatment
FOR EACH ROW
BEGIN
    IF NEW.trea_date_end < NEW.trea_date_start OR NEW.trea_date_end > '3000-01-01' THEN
        SIGNAL SQLSTATE '45005' SET MESSAGE_TEXT = 'Invalid treatment date_end. Date_end of treatment must be between date_start and 3000-01-01!';
    END IF;
END; 
//
