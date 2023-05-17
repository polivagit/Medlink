<?php

namespace App\Http\Controllers;

use Illuminate\Http\Request;
use Illuminate\Support\Facades\DB;
use Illuminate\Support\Facades\Session;
use PHPMailer\PHPMailer\PHPMailer;  
use PHPMailer\PHPMailer\Exception;


class LoginController extends Controller
{
    public function index(){
        return view("login");
    }

    public function isValid(Request $request){
        $user=$request->input('user');
        $pass=$request->input('password');

        $doctor = DB::table('person as p')
        ->select('*')
        ->join('doctor as d', 'p.pers_id', '=', 'd.doct_specialty_id')
        ->join('specialty as s', 's.spec_id', '=', 'd.doct_person_id')
        ->where('p.pers_login_username', $user)
        ->where('p.pers_login_password', $pass)
        ->first();

        if($doctor==null){
            return redirect()->route('login')->with("error","Invalid Credentials");

        }else{
            $request->session()->put("doctor",$doctor);
            return redirect()->route('doctor');
        }
    }

    public function forget(Request $request){
        return view("forget");
    }

    public function change(Request $request){
        return view("change");
    }

    public function recover(Request $request){
        $mail=$request->input('mail');
        $doctor = DB::table('person as p')
        ->select('*')
        ->join('doctor as d', 'p.pers_id', '=', 'd.doct_specialty_id')
        ->join('specialty as s', 's.spec_id', '=', 'd.doct_person_id')
        ->where('p.pers_email', $mail)
        ->first();

        if($doctor==null){
            return redirect()->route('forget')->with("error","Invalid Mail");

        }else{
            $newPass= bin2hex(random_bytes(10));
            $body="Hello, your new password is: " . $newPass ." ,remember your username is: " . $doctor->pers_login_username;
            $ok=$this->composeEmail($mail,$body);
            if($ok){
                DB::table('person')
                ->where('pers_email', $mail)
                ->update(['pers_login_password' => $newPass
                ]);
            }
            return redirect()->route('login')->with("success","Check your mail to restore password");
        }
    }

    public function changePassword(Request $request){
        $user=$request->input('user');
        $old=$request->input('old');
        $new=$request->input('new');
        $confirm=$request->input('confirm');

        $doctor = DB::table('person as p')
        ->select('*')
        ->join('doctor as d', 'p.pers_id', '=', 'd.doct_specialty_id')
        ->join('specialty as s', 's.spec_id', '=', 'd.doct_person_id')
        ->where('p.pers_login_username', $user)
        ->where('p.pers_login_password', $old)
        ->first();

        if($doctor==null){
            return redirect()->route('change')->with("error","Invalid Credentials");

        }else{
            if($new!=$confirm){
                return redirect()->route('change')->with("error","Passwords Doesen't Match ");
            }else{
                DB::table('person')
                ->where('pers_login_username', $user)
                ->update(['pers_login_password' => $new
                ]);
                return redirect()->route('login')->with("success","Password Change Success");

            }
        }

    }

    public function composeEmail($adress,$body) 
    {
        require base_path("vendor/autoload.php");
        $mail = new PHPMailer(true);    
        try {
            $mail->SMTPDebug = 1;
            $mail->isSMTP();
            $mail->Host = 'smtp.gmail.com';             
            $mail->SMTPAuth = true;
            $mail->Username = 'pauprovaemail@gmail.com';   
            $mail->Password = 'mjqkcovlgbyhvlyb';       
            $mail->SMTPSecure = 'ssl';                  
            $mail->Port = 465;                          
            $mail->setFrom('pauprovaemail@gmail.com', 'MedLink');
            $mail->addAddress($adress);
            $mail->isHTML(true);                
            $mail->Subject = "NEW PASSWORD";
            $mail->Body = $body;

            if(!$mail->send() ) {  
                return false;
            }
            else {
                return true;
            }
        } catch (Exception $e) {
            return false;
        }
    }
}
