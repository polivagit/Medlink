<?php

namespace App\Http\Controllers;

use Illuminate\Http\Request;
use \App\Models\Usuaris;
use \App\Models\Jornada;
use \App\Models\Resultat;


class PublicController extends Controller
{
    /**
     * Display a listing of the resource.
     *
     * @return \Illuminate\Http\Response
     */
    public function index()
    {
        try{
            $usuari=Usuaris::where("type","=","public")->firstOrFail();
            session()->put('usuari',$usuari);
            $jornades=Jornada::All();
            session()->put("jornades",$jornades);
            return view('public');

        } catch (\Illuminate\Database\Eloquent\ModelNotFoundException $e) {
            var_dump("ERROR BASE DE DADES");
            exit;
        }
        
    }
    public function partits(Request $request){
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
            $request->session()->put("partits",$jornada->partits);

            if(isset($_POST["pdf"])){
                $partits=session()->get("partits");
                $counts=session()->get("counts");
                $pdf = app('dompdf.wrapper');
                $content='<h3 align="center">Resultats jornada ' . session()->get("jornadaTriada")->num . '</h3>
                            <table width="100%" style="text-align:center;">
                            <tr>
                                <th width="30%">Equip Local</th>
                                <th width="30%">Equip Visitant</th>
                                <th width="10%">Qt Local</th>
                                <th width="10%">Qt Visitant</th>
                                <th width="10%">Qt Empat</th>
                            </tr>'; 
                $i=0;
                foreach ($partits as $partit) {
                    $countLocal=$counts[$i]["countLocal"];
                    $countVisitant=$counts[$i]["countVisitant"];
                    $countEmpat=$counts[$i]["countEmpat"];

                    $content .= '
                    <tr>
                        <td>' . $partit->local . '</td>
                        <td>' . $partit->visitant . '</td>
                        <td>' . $countLocal . '</td>
                        <td>' . $countVisitant . '</td>
                        <td>' . $countEmpat . '</td>

                    </tr>';
                    $i++;
                }

                $pdf->loadHTML($content);
                return $pdf->stream();  
            }
            return redirect()->route('public');    
        }
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
