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
        ->leftJoin('medicine_category AS mm', 'm.medi_category_id', '=', 'mm.meca_id')
        ->select('*')
        ->where('t.trme_treatment_id', $treatId)
        ->get();
        $request->session()->put("medicines",$medicines);    
        $request->session()->put("treatId",$treatId);

        return response()->json(['medicines' => $medicines]);
    }
    
    public function manageButtons(Request $request ){
        $btns = $request->input("buttons");  
        $doctor=session()->get('doctor');
        $patientId=session()->get('patientId');
        if ($btns == 'add') {  
            $ok=$this->validForm($request);   
            if($ok){
                try{
                    DB::table('treatment')->insert([
                        'trea_name' => $request->input('name'),
                        'trea_description' => $request->input('desc'),
                        'trea_date_start' => $request->input('start'),
                        'trea_date_end' => $request->input('end'),
                        'trea_observations' => $request->input('obs'),
                        'trea_is_active' => $request->input('active'),
                        'trea_doctor_id' => $doctor->pers_id,
                        'trea_patient_id' => $patientId
                    ]);
                    $this->actInfo($doctor,$patientId,$request);

                }catch(\Illuminate\Database\QueryException $ex){
                    return redirect()->route('treatment/index')->with("error","Insert Error, may be this mail, username, nif, phone... already exist")->withInput();
                }
                return redirect()->route('treatment/index')->with("success","Insert OK");                               
            }
            return redirect()->route('treatment/index');

        } elseif ($btns == 'yes') {
            return redirect()->route('treatment/index');

        }elseif ($btns == 'yes1') {
            try{
                DB::table('treatment')
                    ->where('trea_id', $request->input('treatSelect'))
                    ->update([
                        'trea_name' => $request->input('name'),
                        'trea_description' => $request->input('desc'),
                        'trea_date_start' => $request->input('start'),
                        'trea_date_end' => $request->input('end'),
                        'trea_observations' => $request->input('obs'),
                        'trea_is_active' => $request->input('active'),
                ]);
                $this->actInfo($doctor,$patientId,$request);
                return redirect()->route('treatment/index')->with("success","Update OK");            

            }catch(\Illuminate\Database\QueryException){
                return redirect()->route('treatment/index')->with("error","Update Error, may be this mail, username, nif, phone... already exist")->withInput();
            }
        }            
        else if($btns == "yes2"){
            DB::table('treatment_medicine')
            ->where('trme_treatment_id', $request->input('treatSelect'))
            ->delete();

            DB::table('treatment')
                ->where('trea_id', $request->input('treatSelect'))
                ->delete();
            $this->actInfo($doctor,$patientId,$request);

            return redirect()->route('treatment/index')->with("success","Remove OK");
        }
    }

    public function validForm(Request $request){
        $request->validate([
            'name' => 'required|min:2',
            'active' => 'regex:/^[0-1]{1}$/',
            'end' => 'required|after:today',
            'desc' => 'required|min:10',],[
            'name.required' => 'Name is required',
            'name.min' => 'Name must have 2 caracters or more',
            'active.regex' => 'Is Active is required',
            'end.required' => 'Date End is required',
            'end.after' => 'Date End must be future',
            'desc.required' => 'Description is required',
            'desc.min' => 'Description must have 10 caracters or more',
        ]);
        return true;
    }

    public function actInfo($doctor,$patientId,$request){
        $treatments = DB::table('treatment')
        ->where('trea_doctor_id', $doctor->pers_id)
        ->where('trea_patient_id', $patientId)
        ->get();
        $request->session()->put("treatmentsPatient",$treatments);
    }
}
