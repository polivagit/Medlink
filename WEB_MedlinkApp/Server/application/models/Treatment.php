<?php
class Treatment extends CI_Model{

    public function getTreatment($id){
        $array=array("trea_patient_id"=>$id);
        $this->db->select('*');
        $this->db->from('treatment');
        $this->db->where($array);
        $query = $this->db->get();
        $data=$query->result_array();
        return $data;
    }

    public function getMedicines($id){
        $array=array("trme_treatment_id"=>$id);

        $this->db->select('*');
        $this->db->from('treatment_medicine AS t');
        $this->db->join('medicine AS m', 't.trme_medicine_id = m.medi_id', 'left');
        $this->db->join('units_of_measure AS u', 't.trme_unit_of_measure_id = u.unme_id', 'left');
        $this->db->join('medicine_category AS mm', 'm.medi_category_id = mm.meca_id', 'left');
        $this->db->where($array);

        $query = $this->db->get();
        $data=$query->result_array();
        return $data;
    }
}
?>