<?php
class Login extends CI_Model{

    public function getPatient($user,$pass){
        $array=array("pers_login_username"=>$user,"pers_login_password"=>$pass);
        $this->db->select('*');
        $this->db->from('person p');
        $this->db->join('patient pp', 'p.pers_id = pp.pati_person_id');
        $this->db->where($array);
        $query = $this->db->get();
        $data=$query->result_array();
        return $data;
    }
}
?>