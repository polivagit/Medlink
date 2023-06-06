-- ----------------------------------------------------------------------------------------------------------
-- ALL INSERTS 
-- ----------------------------------------------------------------------------------------------------------

-- -----------------------------------------------------
-- Data for table `medlink2`.`specialty`
-- -----------------------------------------------------
START TRANSACTION;
USE `medlink2`;
INSERT INTO `medlink2`.`specialty` (`spec_name`) VALUES ('Allergy and immunology');
INSERT INTO `medlink2`.`specialty` (`spec_name`) VALUES ('Anesthesiology');
INSERT INTO `medlink2`.`specialty` (`spec_name`) VALUES ('Dermatology');
INSERT INTO `medlink2`.`specialty` (`spec_name`) VALUES ('Diagnostic radiology');
INSERT INTO `medlink2`.`specialty` (`spec_name`) VALUES ('Emergency medicine');
INSERT INTO `medlink2`.`specialty` (`spec_name`) VALUES ('Family medicine');
INSERT INTO `medlink2`.`specialty` (`spec_name`) VALUES ('Internal genetics');
INSERT INTO `medlink2`.`specialty` (`spec_name`) VALUES ('Neurology');
INSERT INTO `medlink2`.`specialty` (`spec_name`) VALUES ('Nuclear medicine');
INSERT INTO `medlink2`.`specialty` (`spec_name`) VALUES ('Obstetrics and gynecology');
INSERT INTO `medlink2`.`specialty` (`spec_name`) VALUES ('Ophthalmology');
INSERT INTO `medlink2`.`specialty` (`spec_name`) VALUES ('Pathology');
INSERT INTO `medlink2`.`specialty` (`spec_name`) VALUES ('Pediatrics');
INSERT INTO `medlink2`.`specialty` (`spec_name`) VALUES ('Pyshical medicine and rehabilitation');
INSERT INTO `medlink2`.`specialty` (`spec_name`) VALUES ('Preventive medicine');
INSERT INTO `medlink2`.`specialty` (`spec_name`) VALUES ('Psychiatry');
INSERT INTO `medlink2`.`specialty` (`spec_name`) VALUES ('Radiation oncology');
INSERT INTO `medlink2`.`specialty` (`spec_name`) VALUES ('Surgery');
INSERT INTO `medlink2`.`specialty` (`spec_name`) VALUES ('Urology');

COMMIT;

-- -----------------------------------------------------
-- Data for table `medlink2`.`medicine_category`
-- -----------------------------------------------------
START TRANSACTION;
USE `medlink2`;
INSERT INTO `medlink2`.`medicine_category` (`meca_name`) VALUES ('Rheumatologic');
INSERT INTO `medlink2`.`medicine_category` (`meca_name`) VALUES ('Cardiovascular');
INSERT INTO `medlink2`.`medicine_category` (`meca_name`) VALUES ('Dermatologic');
INSERT INTO `medlink2`.`medicine_category` (`meca_name`) VALUES ('Endocrinology');
INSERT INTO `medlink2`.`medicine_category` (`meca_name`) VALUES ('Gastrointestinal');
INSERT INTO `medlink2`.`medicine_category` (`meca_name`) VALUES ('Infectious');
INSERT INTO `medlink2`.`medicine_category` (`meca_name`) VALUES ('Neurologic');
INSERT INTO `medlink2`.`medicine_category` (`meca_name`) VALUES ('Ophtalmotolarying');
INSERT INTO `medlink2`.`medicine_category` (`meca_name`) VALUES ('Psychiatric');
INSERT INTO `medlink2`.`medicine_category` (`meca_name`) VALUES ('Renal');
INSERT INTO `medlink2`.`medicine_category` (`meca_name`) VALUES ('Urologic');

COMMIT;

-- -----------------------------------------------------
-- Data for table `medlink2`.`medicine`
-- -----------------------------------------------------
START TRANSACTION;
USE `medlink2`;
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Risedronate', 1);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Alendronate',  1);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Ibandronate',  1);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Raloxifene',  1);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Infliximab',  1);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Adalimumab',  1);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Etanercept',  1);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Methtrexate',  1);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Hydroxychloroquine',  1);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Allopurinol',  1);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Colchicine',  1);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Amiodarone',  2);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Digoxin',  2);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Aspirin',  2);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Aspirin/Dipyridamole',  2);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Clopidogrel',  2);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Prasugrel',  2);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Ticagrelor',  2);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Nitroglycerin',  2);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Isosorbide mononitrate',  2);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Ranolazine',  2);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Hydrochlorthiazide',  2);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Metolazone',  2);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Furosemide',  2);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Torsemide',  2);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Bumetanide',  2);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Spironolactone',  2);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Nifedipine XL',  2);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Amlodipine',  2);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Amlodipine/benazepril',  2);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Diltiazem',  2);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Fenofibrate',  2);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Genfibrozil',  2);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Omega-3 Acid Ethyl Esters',  2);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Niacin',  2);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Ezetimibe',  2);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Colesevelam',  2);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Atenolol',  2);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Metoprolol tartrate',  2);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Metoprolol succinate',  2);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Nebivolol',  2);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Propranolol',  2);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Carvedilol',  2);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Lisinopril',  2);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Lisinopril/hydrochlorthiazide',  2);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Enalapril',  2);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Pamipril',  2);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Losartan',  2);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Olmesartan',  2);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Valsartan',  2);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Dabigatran',  2);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Rivaroxaban',  2);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Apixaban',  2);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Warfarin',  2);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Enoxaparin',  2);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Pravastatin',  2);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Simvastatin',  2);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Simvastatin/ezetimibe',  2);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Atorvastatin',  2);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Rosuvastatin',  2);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Adapalene',  3);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Isotretinoin',  3);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Rosiglitazone',  4);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Pioglitazone',  4);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Canagliflozin',  4);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Norgestimate/ethinyl estradiol',  4);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Drospirenone/ethinyl estradiol',  4);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Norethindrone',  4);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Norelgestromin/ethinyl estradiol',  4);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Etonogestrel/ethinyl estradiol',  4);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Insulin aspart',  4);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Insulin lispro',  4);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Insulin NPH',  4);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Insulin glargine',  4);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Insulin detemir',  4);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Exenatide',  4);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Prednisone',  4);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Methylprednisolone',  4);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Conjugated Estrogens',  4);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Testosterone',  4);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Levothyroxine',  4);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Anastrozole',  4);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Tamoxifen',  4);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Metformin',  4);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Sitagliptin',  4);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Glyburide',  4);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Glimepiride',  4);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Glipizide',  4);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Glyburide/metformin',  4);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Sitagliptin/metformin',  4);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Senna',  5);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Bisacodyl',  5);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Docusate',  5);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Senna/docusate',  5);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('PEG 3350',  5);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('PEG 3350 with electrolytes',  5);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Omeprazole',  5);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Esomeprazole',  5);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Pantoprazole',  5);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Lansoprazole',  5);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Dexlansoprazole',  5);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Famotidine',  5);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Ranitidine',  5);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Ondansetron',  5);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Prochlorperazine',  5);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Promethazine',  5);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Metoclopramide',  5);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Dicyclomine',  5);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Loperamide',  5);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Diphenoxylate/atropine',  5);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Mesalamine',  5);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Amoxicillin',  6);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Amoxicillin/clavulanate',  6);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Cephalexin',  6);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Cefuroxime',  6);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Cefdinir',  6);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Nitrofurantoin',  6);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Metronidazole',  6);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Sulfamethoxazole/trimethoprim',  6);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Clarithromycin',  6);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Azithromycin',  6);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Oseltamivir',  6);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Acyclovir',  6);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Valacyclovir',  6);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Ciprofloxacin',  6);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Levofloxacin',  6);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Moxifloxacin',  6);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Emtricitabine/tenofovir',  6);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Emtricitabine/tenofovir/efavirenz',  6);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Emtricitabine/tenofovir/rilpivirine',  6);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Emtricitabine/tenofovir/elvitegravir/cobicistat',  6);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Fluconazole',  6);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Itraconazole',  6);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Clindamycin',  6);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Doxycycline',  6);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Linezolid',  6);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Donepezil',  7);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Memantine',  7);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Meloxicam',  7);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Naproxen',  7);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Ibuprofen',  7);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Diclofenac',  7);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Celecoxib',  7);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Phenytoin',  7);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Valproic acid',  7);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Lamotrigine',  7);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Carbamazepine',  7);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Oxcarbazepine',  7);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Levetiracetam',  7);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Nicotine Patch',  7);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Varenicline',  7);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Sumatriptan',  7);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Eletriptan',  7);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Rizatriptan',  7);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Butalbital/acetaminophen/caffeine',  7);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Butalbital/aspirin/caffeine',  7);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Carisoprodol',  7);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Cyclobenzaprine',  7);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Morphine',  7);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Hydromorphone',  7);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Oxycodone',  7);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Oxycodone/APAP',  7);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Fentanyl',  7);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Hydrocodone/APAP',  7);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Buprenorphine/naloxone',  7);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Codeine/APAP',  7);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Tramadol',  7);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Gabapentin',  7);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Pregabalin',  7);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Lidocaine patch',  7);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Mometasone',  8);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Fluticasone',  8);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Diphenhydramine',  8);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Loratadine',  8);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Desloratadine',  8);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Fexofenadine',  8);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Olopatadine',  8);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Azelastine',  8);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Epinephrine',  8);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Cyclosporine (ophthalmic)',  8);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Timolol',  8);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Brimonidine',  8);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Brimonidine/timolol',  8);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Dorzolamide',  8);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Bimatoprost',  8);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Latanoprost',  8);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Travoprost',  8);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Hydrocodone/chlorpheniramine',  8);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Benzonatate',  8);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Guaifenesin',  8);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Dextromethorphan',  8);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Pseudoephedrine',  8);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Clonazepam',  9);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Lorazepam',  9);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Diazepam',  9);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Alprazolam',  9);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Amitriptyline',  9);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Nortriptyline',  9);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Trazodone',  9);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Mirtazapine',  9);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Bupropion',  9);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Fluoxetine',  9);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Sertraline',  9);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Paroxetine',  9);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Citalopram',  9);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Escitalopram',  9);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Zolpidem',  9);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Eszopiclone',  9);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Methylphenidate',  9);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Amphetamine/dextroamphetamine',  9);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Lisdexamfetamine',  9);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Atomoxetine',  9);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Modafinil',  9);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Venlafaxine',  9);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Desvenlafaxine',  9);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Duloxetine',  9);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Olanzapine',  9);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Quetiapine',  9);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Risperidone',  9);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Ziprasidone',  9);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Aripiprazole',  9);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Epoetin alfa',  10);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Darbepoetin',  10);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Cinacalcet',  10);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Sevelamer carbonate',  10);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Potassium chloride',  10);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Sildenafil',  11);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Vardenafil',  11);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Tadalafil',  11);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Tolterodine',  11);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Solifenacin',  11);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Darifenacin',  11);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Dutasteride',  11);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Finasteride',  11);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Terazosin',  11);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Doxazosin',  11);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Alfuzosin',  11);
INSERT INTO `medlink2`.`medicine` (`medi_name`, `medi_category_id`) VALUES ('Tamsulosin',  11);

COMMIT;

-- -----------------------------------------------------
-- Data for table `medlink2`.`units_of_measure`
-- -----------------------------------------------------
START TRANSACTION;
USE `medlink2`;

INSERT INTO `medlink2`.`units_of_measure` (`unme_name`, `unme_abbreviation`) VALUES ('Kilogram', 'Kg');
INSERT INTO `medlink2`.`units_of_measure` (`unme_name`, `unme_abbreviation`) VALUES ('Gram', 'g');
INSERT INTO `medlink2`.`units_of_measure` (`unme_name`, `unme_abbreviation`) VALUES ('Milligram', 'mg');
INSERT INTO `medlink2`.`units_of_measure` (`unme_name`, `unme_abbreviation`) VALUES ('Microgram', 'mcg');
INSERT INTO `medlink2`.`units_of_measure` (`unme_name`, `unme_abbreviation`) VALUES ('Litre', 'L');
INSERT INTO `medlink2`.`units_of_measure` (`unme_name`, `unme_abbreviation`) VALUES ('Millilitre', 'ml');
INSERT INTO `medlink2`.`units_of_measure` (`unme_name`, `unme_abbreviation`) VALUES ('Cubic centimetre', 'cc');
INSERT INTO `medlink2`.`units_of_measure` (`unme_name`, `unme_abbreviation`) VALUES ('Mole', 'mol');
INSERT INTO `medlink2`.`units_of_measure` (`unme_name`, `unme_abbreviation`) VALUES ('Millimole', 'mmol');

COMMIT;

-- -----------------------------------------------------
-- Data for table `medlink2`.`person`
-- -----------------------------------------------------

START TRANSACTION;
USE `medlink2`;

INSERT INTO `medlink2`.`person` (`pers_nif`,`pers_first_name`,`pers_last_name_1`,`pers_last_name_2`,`pers_birthdate`,`pers_phone_number`,`pers_email`,`pers_gender`,`pers_addrs_street`,`pers_addrs_zip_code`,`pers_addrs_city`,`pers_addrs_province`,`pers_addrs_country`,`pers_login_username`,`pers_login_password`)
VALUES ( '47834720A','Pablo','García','Rodríguez','1989-04-22','635346505','pgarcia1@gmail.com',0,'Carrer de Sant Magí 3','08700','Igualada','Barcelona','España','pgarcia1','4720A');
INSERT INTO `medlink2`.`person` (`pers_nif`,`pers_first_name`,`pers_last_name_1`,`pers_last_name_2`,`pers_birthdate`,`pers_phone_number`,`pers_email`,`pers_gender`,`pers_addrs_street`,`pers_addrs_zip_code`,`pers_addrs_city`,`pers_addrs_province`,`pers_addrs_country`,`pers_login_username`,`pers_login_password`)
VALUES ( '48929311Z','Paula','Sánchez','Pérez','1973-01-11','911161347','psanchez1@gmail.com',1,'Carrer Aurora 17','08700','Igualada','Barcelona','España','psanchez1','9311Z');
INSERT INTO `medlink2`.`person` (`pers_nif`,`pers_first_name`,`pers_last_name_1`,`pers_last_name_2`,`pers_birthdate`,`pers_phone_number`,`pers_email`,`pers_gender`,`pers_addrs_street`,`pers_addrs_zip_code`,`pers_addrs_city`,`pers_addrs_province`,`pers_addrs_country`,`pers_login_username`,`pers_login_password`)
VALUES ( '46372813J','Lucas','Fernández','Ruiz','1954-06-02','931639934','lfernandez1@gmail.com',0,'Carrer de Santa Anna 22','08700','Igualada','Barcelona','España','lfernandez1','2813J');
INSERT INTO `medlink2`.`person` (`pers_nif`,`pers_first_name`,`pers_last_name_1`,`pers_last_name_2`,`pers_birthdate`,`pers_phone_number`,`pers_email`,`pers_gender`,`pers_addrs_street`,`pers_addrs_zip_code`,`pers_addrs_city`,`pers_addrs_province`,`pers_addrs_country`,`pers_login_username`,`pers_login_password`)
VALUES ( '48339921N','Lucía','Moreno','Díaz','1993-07-23','960628778','lmoreno1@gmail.com',3,'Carrer de la Trinitat 7','08700','Igualada','Barcelona','España','lmoreno1','9921N');

-- DOCTORS:
INSERT INTO `medlink2`.`person` (`pers_nif`,`pers_first_name`,`pers_last_name_1`,`pers_last_name_2`,`pers_birthdate`,`pers_phone_number`,`pers_email`,`pers_gender`,`pers_addrs_street`,`pers_addrs_zip_code`,`pers_addrs_city`,`pers_addrs_province`,`pers_addrs_country`,`pers_login_username`,`pers_login_password`)
VALUES ( '45884032H','Martín','Hernández','Muñoz','1969-11-05','653460845','mhernandez1@gmail.com',3,'Carrer de Vic 2','08700','Igualada','Barcelona','España','mhernandez1','4032H');
INSERT INTO `medlink2`.`person` (`pers_nif`,`pers_first_name`,`pers_last_name_1`,`pers_last_name_2`,`pers_birthdate`,`pers_phone_number`,`pers_email`,`pers_gender`,`pers_addrs_street`,`pers_addrs_zip_code`,`pers_addrs_city`,`pers_addrs_province`,`pers_addrs_country`,`pers_login_username`,`pers_login_password`)
VALUES ( '47849493T','Martina','Álvarez','Jiménez','1991-08-14','685332940','malvarez1@gmail.com',1,'Carrer de Grècia 31','08700','Igualada','Barcelona','España','malvarez1','9493T');
INSERT INTO `medlink2`.`person` (`pers_nif`,`pers_first_name`,`pers_last_name_1`,`pers_last_name_2`,`pers_birthdate`,`pers_phone_number`,`pers_email`,`pers_gender`,`pers_addrs_street`,`pers_addrs_zip_code`,`pers_addrs_city`,`pers_addrs_province`,`pers_addrs_country`,`pers_login_username`,`pers_login_password`)
VALUES ( '47443923Z','Toni','Olmo','Rovira','1988-06-16','685228394','lolmo1@gmail.com',0,'Carrer de les flors 2','08700','Igualada','Barcelona','España','lolmo1','3923Z');
INSERT INTO `medlink2`.`person` (`pers_nif`,`pers_first_name`,`pers_last_name_1`,`pers_last_name_2`,`pers_birthdate`,`pers_phone_number`,`pers_email`,`pers_gender`,`pers_addrs_street`,`pers_addrs_zip_code`,`pers_addrs_city`,`pers_addrs_province`,`pers_addrs_country`,`pers_login_username`,`pers_login_password`)
VALUES ( '47904030J','Laura','Jové','Roldan','1983-04-13','685428276','ljove1@gmail.com',1,'Carrer de les fonts 19','08700','Igualada','Barcelona','España','ljove1','4030J');
INSERT INTO `medlink2`.`person` (`pers_nif`,`pers_first_name`,`pers_last_name_1`,`pers_last_name_2`,`pers_birthdate`,`pers_phone_number`,`pers_email`,`pers_gender`,`pers_addrs_street`,`pers_addrs_zip_code`,`pers_addrs_city`,`pers_addrs_province`,`pers_addrs_country`,`pers_login_username`,`pers_login_password`)
VALUES ( '48911829H','Ricard','Pla','Solé','1980-03-11','694875286','rpla1@gmail.com',0,'Carrer de les portes 44','08700','Igualada','Barcelona','España','rpla1','1829H');
INSERT INTO `medlink2`.`person` (`pers_nif`,`pers_first_name`,`pers_last_name_1`,`pers_last_name_2`,`pers_birthdate`,`pers_phone_number`,`pers_email`,`pers_gender`,`pers_addrs_street`,`pers_addrs_zip_code`,`pers_addrs_city`,`pers_addrs_province`,`pers_addrs_country`,`pers_login_username`,`pers_login_password`)
VALUES ( '47944883T','Jaume','García','Font','1971-07-18','694879311','jgarcia1@gmail.com',0,'Carrer de les llums 11','08700','Igualada','Barcelona','España','jgarcia1','4883T');
INSERT INTO `medlink2`.`person` (`pers_nif`,`pers_first_name`,`pers_last_name_1`,`pers_last_name_2`,`pers_birthdate`,`pers_phone_number`,`pers_email`,`pers_gender`,`pers_addrs_street`,`pers_addrs_zip_code`,`pers_addrs_city`,`pers_addrs_province`,`pers_addrs_country`,`pers_login_username`,`pers_login_password`)
VALUES ( '45130849A','Anna','Ansaldo','Fandos','1974-10-01','694283714','aansaldo1@gmail.com',1,'Carrer de Barcelona 2','08700','Igualada','Barcelona','España','aansaldo1','0849A');
INSERT INTO `medlink2`.`person` (`pers_nif`,`pers_first_name`,`pers_last_name_1`,`pers_last_name_2`,`pers_birthdate`,`pers_phone_number`,`pers_email`,`pers_gender`,`pers_addrs_street`,`pers_addrs_zip_code`,`pers_addrs_city`,`pers_addrs_province`,`pers_addrs_country`,`pers_login_username`,`pers_login_password`)
VALUES ( '48584924N','Carme','Rodríguez','Rodríguez','1981-03-19','694211234','crodriguez1@gmail.com',1,'Carrer de Valencia 6','08700','Igualada','Barcelona','España','crodriguez1','4924N');
INSERT INTO `medlink2`.`person` (`pers_nif`,`pers_first_name`,`pers_last_name_1`,`pers_last_name_2`,`pers_birthdate`,`pers_phone_number`,`pers_email`,`pers_gender`,`pers_addrs_street`,`pers_addrs_zip_code`,`pers_addrs_city`,`pers_addrs_province`,`pers_addrs_country`,`pers_login_username`,`pers_login_password`)
VALUES ( '48331824H','Carla','Fombona','Delgado','1977-06-22','694987555','cfombona1@gmail.com',1,'Carrer de Vilafranca 9','08700','Igualada','Barcelona','España','cfombona1','1824H');
INSERT INTO `medlink2`.`person` (`pers_nif`,`pers_first_name`,`pers_last_name_1`,`pers_last_name_2`,`pers_birthdate`,`pers_phone_number`,`pers_email`,`pers_gender`,`pers_addrs_street`,`pers_addrs_zip_code`,`pers_addrs_city`,`pers_addrs_province`,`pers_addrs_country`,`pers_login_username`,`pers_login_password`)
VALUES ( '45832913T','Carlos','Berga','Farrés','1970-11-01','694695381','cberga1@gmail.com',0,'Carrer de Vallbona 12','08700','Igualada','Barcelona','España','cberga1','2913T');
INSERT INTO `medlink2`.`person` (`pers_nif`,`pers_first_name`,`pers_last_name_1`,`pers_last_name_2`,`pers_birthdate`,`pers_phone_number`,`pers_email`,`pers_gender`,`pers_addrs_street`,`pers_addrs_zip_code`,`pers_addrs_city`,`pers_addrs_province`,`pers_addrs_country`,`pers_login_username`,`pers_login_password`)
VALUES ( '45829134A','Jordi','Ubaldo','Fabregat','1971-12-05','688832922','jubaldo1@gmail.com',0,'Carrer de Carme 33','08700','Igualada','Barcelona','España','jubaldo1','9134A');
INSERT INTO `medlink2`.`person` (`pers_nif`,`pers_first_name`,`pers_last_name_1`,`pers_last_name_2`,`pers_birthdate`,`pers_phone_number`,`pers_email`,`pers_gender`,`pers_addrs_street`,`pers_addrs_zip_code`,`pers_addrs_city`,`pers_addrs_province`,`pers_addrs_country`,`pers_login_username`,`pers_login_password`)
VALUES ( '45859438Z','Georgina','Rius','Rius','1990-01-01','687893821','grius1@gmail.com',1,'Carrer de la Torre 8','08700','Igualada','Barcelona','España','grius1','9438Z');
INSERT INTO `medlink2`.`person` (`pers_nif`,`pers_first_name`,`pers_last_name_1`,`pers_last_name_2`,`pers_birthdate`,`pers_phone_number`,`pers_email`,`pers_gender`,`pers_addrs_street`,`pers_addrs_zip_code`,`pers_addrs_city`,`pers_addrs_province`,`pers_addrs_country`,`pers_login_username`,`pers_login_password`)
VALUES ( '45172394J','Francina','Gallardo','Margarit','1994-05-05','693342102','fgallardo1@gmail.com',1,'Carrer de les Cireres 14','08700','Igualada','Barcelona','España','fgallardo1','2394J');
INSERT INTO `medlink2`.`person` (`pers_nif`,`pers_first_name`,`pers_last_name_1`,`pers_last_name_2`,`pers_birthdate`,`pers_phone_number`,`pers_email`,`pers_gender`,`pers_addrs_street`,`pers_addrs_zip_code`,`pers_addrs_city`,`pers_addrs_province`,`pers_addrs_country`,`pers_login_username`,`pers_login_password`)
VALUES ( '45432012H','Àngel','Blaya','Riba','1981-06-07','693753824','ablaya1@gmail.com',0,'Carrer de les Pomes 25','08700','Igualada','Barcelona','España','ablaya1','2012H');
INSERT INTO `medlink2`.`person` (`pers_nif`,`pers_first_name`,`pers_last_name_1`,`pers_last_name_2`,`pers_birthdate`,`pers_phone_number`,`pers_email`,`pers_gender`,`pers_addrs_street`,`pers_addrs_zip_code`,`pers_addrs_city`,`pers_addrs_province`,`pers_addrs_country`,`pers_login_username`,`pers_login_password`)
VALUES ( '48493209T','Siro','Rodríguez','Fernández','1971-02-11','696477832','srodriguez1@gmail.com',0,'Carrer de Manresa 1','08700','Igualada','Barcelona','España','srodriguez1','3209T');
INSERT INTO `medlink2`.`person` (`pers_nif`,`pers_first_name`,`pers_last_name_1`,`pers_last_name_2`,`pers_birthdate`,`pers_phone_number`,`pers_email`,`pers_gender`,`pers_addrs_street`,`pers_addrs_zip_code`,`pers_addrs_city`,`pers_addrs_province`,`pers_addrs_country`,`pers_login_username`,`pers_login_password`)
VALUES ( '48777954A','Lídia','Sanz','Smith','1975-11-04','697448963','lsanz1@gmail.com',0,'Carrer de Carme 35','08700','Igualada','Barcelona','España','lsanz1','7954A');
INSERT INTO `medlink2`.`person` (`pers_nif`,`pers_first_name`,`pers_last_name_1`,`pers_last_name_2`,`pers_birthdate`,`pers_phone_number`,`pers_email`,`pers_gender`,`pers_addrs_street`,`pers_addrs_zip_code`,`pers_addrs_city`,`pers_addrs_province`,`pers_addrs_country`,`pers_login_username`,`pers_login_password`)
VALUES ( '45423434Z','Angela','Barrios','Madrid','1969-10-13','664783928','abarrios1@gmail.com',1,'Carrer de Florida 4','08700','Igualada','Barcelona','España','abarrios1','3434Z');
INSERT INTO `medlink2`.`person` (`pers_nif`,`pers_first_name`,`pers_last_name_1`,`pers_last_name_2`,`pers_birthdate`,`pers_phone_number`,`pers_email`,`pers_gender`,`pers_addrs_street`,`pers_addrs_zip_code`,`pers_addrs_city`,`pers_addrs_province`,`pers_addrs_country`,`pers_login_username`,`pers_login_password`)
VALUES ( '45084543A','Manel','Alonso','Iglesias','1980-02-11','688942340','aiglesias1@gmail.com',0,'Carrer de Madrid 7','08700','Igualada','Barcelona','España','aiglesias1','4543A');
INSERT INTO `medlink2`.`person` (`pers_nif`,`pers_first_name`,`pers_last_name_1`,`pers_last_name_2`,`pers_birthdate`,`pers_phone_number`,`pers_email`,`pers_gender`,`pers_addrs_street`,`pers_addrs_zip_code`,`pers_addrs_city`,`pers_addrs_province`,`pers_addrs_country`,`pers_login_username`,`pers_login_password`)
VALUES ( '45834985T','Didac','Roure','Mensa','1972-07-03','687483921','droure1@gmail.com',0,'Carrer Universitat 12','08700','Igualada','Barcelona','España','droure1','4985T');
INSERT INTO `medlink2`.`person` (`pers_nif`,`pers_first_name`,`pers_last_name_1`,`pers_last_name_2`,`pers_birthdate`,`pers_phone_number`,`pers_email`,`pers_gender`,`pers_addrs_street`,`pers_addrs_zip_code`,`pers_addrs_city`,`pers_addrs_province`,`pers_addrs_country`,`pers_login_username`,`pers_login_password`)
VALUES ( '48110905E','Pere','Lopez','Mensa','1992-06-03','654210099','plopez@gmail.com',0,'Carrer Vidre 122','08700','Igualada','Barcelona','España','plopez','0905E');

COMMIT;

-- -----------------------------------------------------
-- Data for table `medlink2`.`patient`
-- -----------------------------------------------------
START TRANSACTION;
USE `medlink2`;

INSERT INTO `medlink2`.`patient` (`pati_person_id`, `pati_height`, `pati_weight`, `pati_remarks`, `pati_caregiver_id`) VALUES (1,178,77.3,null,null);
INSERT INTO `medlink2`.`patient` (`pati_person_id`, `pati_height`, `pati_weight`, `pati_remarks`, `pati_caregiver_id`) VALUES (2,168,67.7,null,null);
INSERT INTO `medlink2`.`patient` (`pati_person_id`, `pati_height`, `pati_weight`, `pati_remarks`, `pati_caregiver_id`) VALUES (3,190,83.2,null,24);
INSERT INTO `medlink2`.`patient` (`pati_person_id`, `pati_height`, `pati_weight`, `pati_remarks`, `pati_caregiver_id`) VALUES (4,171,69.1,null,null);

COMMIT;

-- -----------------------------------------------------
-- Data for table `medlink2`.`doctor`
-- -----------------------------------------------------
START TRANSACTION;
USE `medlink2`;

INSERT INTO `medlink2`.`doctor` (`doct_person_id`, `doct_collegiate_uid`, `doct_specialty_id`) VALUES (5,'01-110-AHBAA',1);
INSERT INTO `medlink2`.`doctor` (`doct_person_id`, `doct_collegiate_uid`, `doct_specialty_id`) VALUES (6,'02-210-BHBAA',2);
INSERT INTO `medlink2`.`doctor` (`doct_person_id`, `doct_collegiate_uid`, `doct_specialty_id`) VALUES (7,'03-310-CHBAA',3);
INSERT INTO `medlink2`.`doctor` (`doct_person_id`, `doct_collegiate_uid`, `doct_specialty_id`) VALUES (8,'04-410-DHBAA',4);
INSERT INTO `medlink2`.`doctor` (`doct_person_id`, `doct_collegiate_uid`, `doct_specialty_id`) VALUES (9,'05-510-EHBAA',5);
INSERT INTO `medlink2`.`doctor` (`doct_person_id`, `doct_collegiate_uid`, `doct_specialty_id`) VALUES (10,'06-610-FHBAA',6);
INSERT INTO `medlink2`.`doctor` (`doct_person_id`, `doct_collegiate_uid`, `doct_specialty_id`) VALUES (11,'07-710-GHBAA',7);
INSERT INTO `medlink2`.`doctor` (`doct_person_id`, `doct_collegiate_uid`, `doct_specialty_id`) VALUES (12,'08-810-HHBAA',8);
INSERT INTO `medlink2`.`doctor` (`doct_person_id`, `doct_collegiate_uid`, `doct_specialty_id`) VALUES (13,'09-910-IHBAA',9);
INSERT INTO `medlink2`.`doctor` (`doct_person_id`, `doct_collegiate_uid`, `doct_specialty_id`) VALUES (14,'10-110-JHBAA',10);
INSERT INTO `medlink2`.`doctor` (`doct_person_id`, `doct_collegiate_uid`, `doct_specialty_id`) VALUES (15,'11-210-KHBAA',11);
INSERT INTO `medlink2`.`doctor` (`doct_person_id`, `doct_collegiate_uid`, `doct_specialty_id`) VALUES (16,'12-310-LHBAA',12);
INSERT INTO `medlink2`.`doctor` (`doct_person_id`, `doct_collegiate_uid`, `doct_specialty_id`) VALUES (17,'13-410-MHBAA',13);
INSERT INTO `medlink2`.`doctor` (`doct_person_id`, `doct_collegiate_uid`, `doct_specialty_id`) VALUES (18,'14-510-NHBAA',14);
INSERT INTO `medlink2`.`doctor` (`doct_person_id`, `doct_collegiate_uid`, `doct_specialty_id`) VALUES (19,'15-610-OHBAA',15);
INSERT INTO `medlink2`.`doctor` (`doct_person_id`, `doct_collegiate_uid`, `doct_specialty_id`) VALUES (20,'16-710-PHBAA',16);
INSERT INTO `medlink2`.`doctor` (`doct_person_id`, `doct_collegiate_uid`, `doct_specialty_id`) VALUES (21,'17-810-QHBAA',17);
INSERT INTO `medlink2`.`doctor` (`doct_person_id`, `doct_collegiate_uid`, `doct_specialty_id`) VALUES (22,'18-910-RHBAA',18);
INSERT INTO `medlink2`.`doctor` (`doct_person_id`, `doct_collegiate_uid`, `doct_specialty_id`) VALUES (23,'19-110-SHBAA',19);

COMMIT;

-- -----------------------------------------------------
-- Data for table `medlink2`.`treatment`
-- -----------------------------------------------------
START TRANSACTION;
USE `medlink2`;

INSERT INTO `medlink2`.`treatment` (`trea_name`, `trea_description`, `trea_date_start`, `trea_date_end`, `trea_observations`, `trea_is_active`, `trea_doctor_id`, `trea_patient_id`) VALUES ('Treatment 1', 'Description for Treatment 1', '2022-05-01','2023-10-12', null, 0, 5, 1);
INSERT INTO `medlink2`.`treatment` (`trea_name`, `trea_description`, `trea_date_start`, `trea_date_end`, `trea_observations`, `trea_is_active`, `trea_doctor_id`, `trea_patient_id`) VALUES ('Treatment 2', 'Description for Treatment 2', '2021-07-08','2023-10-12', null, 0, 5, 1);

INSERT INTO `medlink2`.`treatment` (`trea_name`, `trea_description`, `trea_date_start`, `trea_date_end`, `trea_observations`, `trea_is_active`, `trea_doctor_id`, `trea_patient_id`) VALUES ('Treatment 1', 'Description for Treatment 1', '2022-01-20','2023-09-01', null, 0, 5, 2);
INSERT INTO `medlink2`.`treatment` (`trea_name`, `trea_description`, `trea_date_start`, `trea_date_end`, `trea_observations`, `trea_is_active`, `trea_doctor_id`, `trea_patient_id`) VALUES ('Treatment 2', 'Description for Treatment 2', '2022-07-08','2023-08-14', null, 0, 5, 2);
INSERT INTO `medlink2`.`treatment` (`trea_name`, `trea_description`, `trea_date_start`, `trea_date_end`, `trea_observations`, `trea_is_active`, `trea_doctor_id`, `trea_patient_id`) VALUES ('Treatment 3', 'Description for Treatment 3', '2023-01-03','2023-10-03', null, 0, 5, 2);

INSERT INTO `medlink2`.`treatment` (`trea_name`, `trea_description`, `trea_date_start`, `trea_date_end`, `trea_observations`, `trea_is_active`, `trea_doctor_id`, `trea_patient_id`) VALUES ('Treatment 1', 'Description for Treatment 1', '2022-09-01','2023-11-20', null, 0, 6, 3);

COMMIT;

-- -----------------------------------------------------
-- Data for table `medlink2`.`treatment_medicine`
-- -----------------------------------------------------
START TRANSACTION;
USE `medlink2`;

INSERT INTO `medlink2`.`treatment_medicine` (`trme_treatment_id`, `trme_medicine_id`, `trme_date_start`, `trme_date_end`, `trme_quantity_per_day`, `trme_total_quantity`, `trme_frequency`, `trme_unit_of_measure_id`) VALUES (1, 4, '2022-05-01', '2023-10-12', 10, 5290, 'Take 1 pill after every meal.', 3);
INSERT INTO `medlink2`.`treatment_medicine` (`trme_treatment_id`, `trme_medicine_id`, `trme_date_start`, `trme_date_end`, `trme_quantity_per_day`, `trme_total_quantity`, `trme_frequency`, `trme_unit_of_measure_id`) VALUES (2, 12, '2021-07-08', '2023-10-12', 30, 24780,  'Take 2 pills every 8 hours.', 6);
                                                                                                                                                                                                                                                                                                                                        
INSERT INTO `medlink2`.`treatment_medicine` (`trme_treatment_id`, `trme_medicine_id`, `trme_date_start`, `trme_date_end`, `trme_quantity_per_day`, `trme_total_quantity`, `trme_frequency`, `trme_unit_of_measure_id`) VALUES (3, 49, '2022-01-20', '2023-09-01', 24, 14136, 'Take 1 pill every 24 hours.', 6);
INSERT INTO `medlink2`.`treatment_medicine` (`trme_treatment_id`, `trme_medicine_id`, `trme_date_start`, `trme_date_end`, `trme_quantity_per_day`, `trme_total_quantity`, `trme_frequency`, `trme_unit_of_measure_id`) VALUES (4, 33, '2022-07-08', '2023-08-14', 50, 16400, 'Take half pill before every meal.', 3);
INSERT INTO `medlink2`.`treatment_medicine` (`trme_treatment_id`, `trme_medicine_id`, `trme_date_start`, `trme_date_end`, `trme_quantity_per_day`, `trme_total_quantity`, `trme_frequency`, `trme_unit_of_measure_id`) VALUES (5, 11, '2023-01-03', '2023-10-03', 32, 8768, 'Inject every 12 hours.', 6);
                                                                                                                                                                                                                  
INSERT INTO `medlink2`.`treatment_medicine` (`trme_treatment_id`, `trme_medicine_id`, `trme_date_start`, `trme_date_end`, `trme_quantity_per_day`, `trme_total_quantity`, `trme_frequency`, `trme_unit_of_measure_id`) VALUES (6, 80, '2022-09-01', '2023-11-20', 80, 35680, 'Take 1 pill after breakfast.', 3);

COMMIT;
