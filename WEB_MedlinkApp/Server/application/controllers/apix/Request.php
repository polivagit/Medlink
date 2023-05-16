<?php

require APPPATH.'libraries/REST_Controller.php';

class Request extends REST_Controller{

  public $persons = array();
  
  public function __construct(){  
      parent::__construct();
      $this->load->model('Login');
      $this->load->model('Treatment');
  }

  public function login_post(){
    $user=$this->post("user");
    $pass=$this->post("pass");

    $patient=$this->Login->getPatient($user,$pass);

    if($patient==null){
      $this->response(
        array(
            "status" => "NOT FOUND",
            "message" => "Login",
            "data" => null
        ), 
        REST_Controller::HTTP_BAD_REQUEST
    );

    }else{
      $this->response(
        array(
            "status" => "FOUND",
            "message" => "Login",
            "data" => $patient
        ), 
        REST_Controller::HTTP_OK
    );
    }
  }

  public function treatment_post(){
    $id=$this->post("id");

    $treatments=$this->Treatment->getTreatment($id);
    $this->response(
        array(
          "status" => "FOUND",
          "message" => "Treatments",
          "data" => $treatments
      ), 
      REST_Controller::HTTP_OK

    );
  }

}

?>