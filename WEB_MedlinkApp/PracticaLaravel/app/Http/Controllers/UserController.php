<?php

namespace App\Http\Controllers;

use Illuminate\Http\Request;
use \App\Models\Jornada;
use \App\Models\Partit;
use \App\Models\Resultat;

class UserController extends Controller
{
    /**
     * Display a listing of the resource.
     *
     * @return \Illuminate\Http\Response
     */
    public function index()
    {
        $user=session()->get('usuari');
        if($user!=null){
            $jornades=Jornada::All();
            session()->put("jornades",$jornades);
            return view('user'); 
        }else{
            echo "Estas intentant entrar sense session";
        }    
    }

    public function partits(Request $request){
        session()->forget('prediccions');
        $jornadaId = $request->input('jornadaTriada');
        if($jornadaId!=null){

            $prediccions=Resultat::where([
                "jornada" => $jornadaId,
                "user" => session()->get('usuari')->id
            ])->get();

            if(sizeof($prediccions)!=0){
                session()->put("prediccions",$prediccions);
            }

            $jornada=Jornada::where("num","=",$jornadaId)->first();
            $request->session()->put("jornadaTriada",$jornada);

            $counts[]=array();
            for($i=0;$i<15;$i++){
                $countLocal=Resultat::where([
                    "jornada" => $jornadaId,
                    "partit" => $i,
                    "resultat" => 1
                ])->count();
                $countVisitant=Resultat::where([
                    "jornada" => $jornadaId,
                    "partit" => $i,
                    "resultat" => 2
                ])->count();
                $countEmpat=Resultat::where([
                    "jornada" => $jornadaId,
                    "partit" => $i,
                    "resultat" => 0
                ])->count();
                
                $counts[$i]=array(
                    "countLocal" =>$countLocal,
                    "countVisitant" =>$countVisitant,
                    "countEmpat" =>$countEmpat
                );                
            }
            session()->put("counts",$counts);

            if(isset($_POST["mostrar"])){
                $request->session()->put("partits",$jornada->partits);
                return redirect()->route('user/index');    
            }
            if(isset($_POST["guardar"])){
                $preds=array();
                for($i=0;$i<15;$i++){
                    $select=$request->input('radio'.$i+1);
                    if($select==null){
                        return redirect()->route('user/index')->with("errorGuardat","ERROR-> No has predit tots els partits");                    
                    }
                    $preds[]=array(
                        "partit" => $i,
                        "jornada" => $jornadaId,
                        "user" => session()->get('usuari')->id,
                        "resultat" => $select
                    );
                }
                Resultat::upsert($preds,["partit","jornada","user"],["resultat"]);
                $prediccions=Resultat::where([
                    "jornada" => $jornadaId,
                    "user" => session()->get('usuari')->id
                ])->get();
    
                if(sizeof($prediccions)!=0){
                    session()->put("prediccions",$prediccions);
                }

                $counts[]=array();
                for($i=0;$i<15;$i++){
                    $countLocal=Resultat::where([
                        "jornada" => $jornadaId,
                        "partit" => $i,
                        "resultat" => 1
                    ])->count();
                    $countVisitant=Resultat::where([
                        "jornada" => $jornadaId,
                        "partit" => $i,
                        "resultat" => 2
                    ])->count();
                    $countEmpat=Resultat::where([
                        "jornada" => $jornadaId,
                        "partit" => $i,
                        "resultat" => 0
                    ])->count();

                    $counts[$i]=array(
                        "countLocal" =>$countLocal,
                        "countVisitant" =>$countVisitant,
                        "countEmpat" =>$countEmpat
                    );
                }
                session()->put("counts",$counts);
                return redirect()->route('user/index')->with("correctGuardat","La predicció s'ha guardat correctament");
            }
        }
        return redirect()->route('user/index')->with("errorGuardat","ERROR-> No has seleccionat cap jornada");                          
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
