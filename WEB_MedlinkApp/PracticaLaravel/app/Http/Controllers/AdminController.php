<?php

namespace App\Http\Controllers;

use Illuminate\Http\Request;
use \App\Models\Jornada;
use \App\Models\Partit;
use Illuminate\Support\Facades\DB;
use \App\Models\Resultat;



class AdminController extends Controller
{
    /**
     * Display a listing of the resource.
     *
     * @return \Illuminate\Http\Response
     */
    public function index(Request $request)
    {
        $user=session()->get('usuari');
        if($user!=null){
            $jornades=Jornada::All();
            $request->session()->put("jornades",$jornades);
            return view('admin'); 
        }else{
            echo "Estas intentant entrar sense session";
        }
    }

    public function penjar(Request $request){
        $xml=$request->file('xmlfile');
        if($xml==null){
            return redirect()->route('admin/index')->with("error","No has selecionat cap fitxer");
        }
        
        $xmlObject = simplexml_load_string(file_get_contents($xml));
        if($xmlObject->children()->count()!=15){
            return redirect()->route('admin/index')->with("error","El arxiu xml no te 15 partits");
        }

        $jornada=array(
            "nom" => $xmlObject->attributes()["nom"],
            "num" => $xmlObject->attributes()["num"],
            "tancada" => $xmlObject->attributes()["tancada"]
        );
        
        $partits=[];
        for($i=0;$i<$xmlObject->children()->count();$i++){
            $partits[$i]=array(
                "jornada" => $xmlObject->children()[$i]->jornada,
                "local" => $xmlObject->children()[$i]->local,
                "visitant" => $xmlObject->children()[$i]->visitant,
                "data" => $xmlObject->children()[$i]->data,
            );
        }
        try{
            Jornada::insert($jornada);
            Partit::insert($partits);

        }catch(\Illuminate\Database\QueryException $e){
            $error = $e->errorInfo[1];
            if($error == 1062){
                return redirect()->route('admin/index')->with("error","No es pot afegir una jornada que ja s'ha afegit");
            }else{
                return redirect()->route('admin/index')->with("error","Error al fer el insert");
            }
        }
        return redirect()->route('admin/index')->with("correctinsert","S'ha inserit correctament la jornada i els partits");
    }

    public function partits(Request $request){
        $jornadaId = $request->input('jornadaTriada');
        if($jornadaId!=null){
            $jornada=Jornada::where("num","=",$jornadaId)->first();

            if(isset($_POST["mostrar"])){
                $request->session()->put("partits",$jornada->partits);
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
                return redirect()->route('admin/index');    
            }
            if(isset($_POST["tancar"])){
                DB::table('jornada')->where('num', $jornadaId)->update(["tancada" => 1]);  
                return redirect()->route('admin/index')->with("correctTancada","La jornada s'ha tancat correctament");
            }    
        }
       
    }

    public function tancarJornada(){

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
