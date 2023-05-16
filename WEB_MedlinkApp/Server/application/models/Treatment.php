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
}
?>