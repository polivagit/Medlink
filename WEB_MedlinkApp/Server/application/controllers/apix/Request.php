<?php

require APPPATH.'libraries/REST_Controller.php';
use PHPMailer\PHPMailer\PHPMailer;
use PHPMailer\PHPMailer\Exception;

require (APPPATH . "third_party\PHPMailer-master\src\Exception.php");
require (APPPATH . "third_party\PHPMailer-master\src\PHPMailer.php");
require (APPPATH . "third_party\PHPMailer-master\src\SMTP.php");

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

  public function changePassword_post(){
    $mail=$this->post("mail");
    $patient=$this->Login->getPatientMail($mail);
    $found="Not Found";    
    if($patient!=null){
      $found="Found";  
      $newPass= bin2hex(random_bytes(10));
      $body="Hello, your new password is: " . $newPass ." ,remember your username is: " . $patient[0]['pers_login_username'];
      $ok=$this->seendMaail($body,$mail);
      if($ok){
        $patient=$this->Login->updatePassword($mail,$newPass);
      }
    }

    $this->response(
        array(
          "status" => $found,
          "message" => "Change Password"
        ), 
      REST_Controller::HTTP_OK
    );
  }
  function seendMaail($body,$email){    
    $mail = new PHPMailer();
    $mail->IsSMTP();
    $mail->CharSet="UTF-8";
    $mail->SMTPSecure = 'ssl';
    $mail->Host = "smtp.gmail.com";
    $mail->Port = '465';
    $mail->Username = 'pauprovaemail@gmail.com';
    $mail->Password = 'mjqkcovlgbyhvlyb';
    $mail->SMTPAuth = true;
    $mail->SMTPDebug = -1;
    $mail->SetFrom = 'pauprovaemail@gmail.com';
    $mail->FromName = 'MedLink';
    $mail->AddAddress($email); 
    $mail->IsHTML(true);
    $mail->Subject = "NEW PASSWORD";
    // $mail->AltBody = $data["body"];
    $mail->Body = $body ; 
    if(!$mail->Send()){
        return false;
    }else{
        return true;
    }
  }

}

?>