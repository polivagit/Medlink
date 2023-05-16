<?php

namespace App\Http\Controllers;

use Illuminate\Http\Request;
use \App\Models\Usuaris;
use PHPMailer\PHPMailer\PHPMailer;  
use PHPMailer\PHPMailer\Exception;


class LoginController extends Controller
{
    /**
     * Display a listing of the resource.
     *
     * @return \Illuminate\Http\Response
     */
    public function index()
    {
        return view('login'); 
    }

    public function verificar(){
        
    }

    public function logout(){
        session()->flush();
        return redirect()->route('login');

    }

    public function registrarse(){
        return view ("registre");
    }

    public function verificarRegistre(Request $request){
        $request->validate([
            'nom' => 'required',
            'password' => 'required|confirmed',
            'mail' => 'required|unique:usuaris,mail|email'
            ],
            [
            'nom.required' => 'El nom es obligatori',
            'password.required' => 'La contrasenya es obligatoria',
            'mail.required' => 'El mail es obligatori',
            'mail.email' => 'El mail ha de tenir un format correcte',
            'mail.unique' => 'El mail ha de ser unic',
            'password.confirmed' => 'Les contrasenyes han de ser iguals'
        ]);
        $correcte=$this->composeEmail($request->input('mail'));
        if($correcte){
            Usuaris::create(
                array(
                    "nom" => $request->input('nom'),
                    "type" => "user",
                    "password" => $request->input('password'),
                    "mail" => $request->input('mail'),
                    "verificat" => 0
                )
            );  
        }
        return redirect()->route('login')->with("error","S'ha enviat un correu al teu mail, entra al link per verificar el teu usuari");

    }

    public function composeEmail($adress) 
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
            $mail->setFrom('pauprovaemail@gmail.com', 'Pau');
            $mail->addAddress($adress);
            $mail->isHTML(true);                
            $mail->Subject = "VERIFICAR EL TEU COMPTE";
            $mail->Body = "Hola, nomes et falta un pas per poder verificar el teu compte, has de entrar dins del seguent link-> http://localhost/m07_uf2/laravel/PracticaLaravel/public/login/registrarUser/".$adress;

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

    public function registrarUser($mail){
        Usuaris::where('mail',$mail)->update(['verificat'=>1]);
        return redirect()->route('login')->with("error","S'ha verificat correctament el correu");
    }
    /**
     * Show the form for creating a new resource.
     *
     * @return \Illuminate\Http\Response
     */
    public function create()
    {
        //
    }

    /**
     * Store a newly created resource in storage.
     *
     * @param  \Illuminate\Http\Request  $request
     * @return \Illuminate\Http\Response
     */
    public function store(Request $request)
    {
        //
    }

    /**
     * Display the specified resource.
     *
     * @param  int  $id
     * @return \Illuminate\Http\Response
     */
    public function show($id)
    {
        //
    }

    /**
     * Show the form for editing the specified resource.
     *
     * @param  int  $id
     * @return \Illuminate\Http\Response
     */
    public function edit($id)
    {
        //
    }

    /**
     * Update the specified resource in storage.
     *
     * @param  \Illuminate\Http\Request  $request
     * @param  int  $id
     * @return \Illuminate\Http\Response
     */
    public function update(Request $request, $id)
    {
        //
    }

    /**
     * Remove the specified resource from storage.
     *
     * @param  int  $id
     * @return \Illuminate\Http\Response
     */
    public function destroy($id)
    {
        //
    }
}
