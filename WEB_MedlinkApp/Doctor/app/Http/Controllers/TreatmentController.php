<?php

namespace App\Http\Controllers;

use Illuminate\Http\Request;
use Illuminate\Support\Facades\DB;
use Illuminate\Support\Facades\Session;


class TreatmentController extends Controller
{
    public function index(Request $request)
    {
        $doctor=session()->get('doctor');
        $patientId=session()->get('patientId');
        if($doctor!=null){
            $treatments=$request->session()->get("treatmentsPatient");            
            if($treatments==null){
                $treatments = DB::table('treatment')
                ->where('trea_doctor_id', $doctor->pers_id)
                ->where('trea_patient_id', $patientId)
                ->get();
                $request->session()->put("treatmentsPatient",$treatments);
            }
            return view('treatments'); 
        }else{
            return redirect()->route('login')->with("error","WRITE CREDENTIALS!!");
        }
    }

    public function filter(Request $request){
        $btn = $request->input("btns");  
        if($btn=="filter"){
            $doctor=session()->get('doctor');
            $patientId=session()->get('patientId');

            $name="%".strtoupper($request->input('filter'))."%";
            $treatments = DB::table('treatment')
            ->where('trea_doctor_id', $doctor->pers_id)
            ->where('trea_patient_id', $patientId)
            ->where(DB::raw("UPPER(trea_name)"), 'like', $name)
            ->get();
            $request->session()->put("treatmentsPatient",$treatments);
            return redirect()->route('treatment/index');  
        }else{
            return redirect()->route('doctor'); 
        }  
    }

    public function putInfo(Request $request){
        $treatId = $request->input('treat');

        $treatment = DB::table('treatment')
        ->where('trea_id', $treatId)
        ->get();
 
        $request->session()->put("treatId",$treatId);
        return response()->json(['info' => $treatment]);
    }

    public function putMedicines(Request $request ){
        $treatId = $request->input('treat');

        $medicines = DB::table('treatment_medicine AS t')
        ->leftJoin('medicine AS m', 't.trme_medicine_id', '=', 'm.medi_id')
        ->leftJoin('units_of_measure AS u', 't.trme_unit_of_measure_id', '=', 'u.unme_id')
        ->leftJoin('medicine_category AS mm', 'm.medi_id', '=', 'mm.meca_id')
        ->select('*')
        ->where('t.trme_treatment_id', $treatId)
        ->get();
        $request->session()->put("medicines",$medicines);    
        $request->session()->put("treatId",$treatId);

        return response()->json(['medicines' => $medicines]);
    }
}
