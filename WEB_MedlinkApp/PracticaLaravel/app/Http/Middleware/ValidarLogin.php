<?php

namespace App\Http\Middleware;

use Closure;
use Illuminate\Http\Request;
use \App\Models\Usuaris;

class ValidarLogin
{
    /**
     * Handle an incoming request.
     *
     * @param  \Illuminate\Http\Request  $request
     * @param  \Closure(\Illuminate\Http\Request): (\Illuminate\Http\Response|\Illuminate\Http\RedirectResponse)  $next
     * @return \Illuminate\Http\Response|\Illuminate\Http\RedirectResponse
     */
    public function handle(Request $request, Closure $next)
    {
        $user = $request->input('mail');
        $pass = $request->input('password');        
        $error="";
        var_dump($user);
        
        if($user=="" || $pass==""){
            $error="El usuari i la contrasenya son obligatoris";
            return redirect()->route('login')->with("error",$error);
        }
        else{
            try{
                $usuari=Usuaris::where("mail","=",$user)->firstOrFail();
            } catch (\Illuminate\Database\Eloquent\ModelNotFoundException $e) {
                return redirect()->route('login')->with("error","El mail no existeix");
            }
            if($pass==$usuari->password && $usuari->verificat==1){
                $login=$usuari->type;
                $request->session()->put("usuari",$usuari);
                if($login=="admin"){
                    return redirect()->route('admin/index')->with("error","ADMIN");;

                }else{
                    return redirect()->route('user/index')->with("error","USER");;

                }
            }else{
                return redirect()->route('login')->with("error","El password es incorrecte o el mail no esta activat");
            }
        }
    }
}
