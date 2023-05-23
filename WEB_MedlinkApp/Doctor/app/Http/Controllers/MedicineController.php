<?php

namespace App\Http\Controllers;

use Illuminate\Http\Request;
use Illuminate\Support\Facades\DB;
use Carbon\Carbon;



class MedicineController extends Controller
{
    public function index(Request $request)
    {
        $treatId=session()->get('treatId');
        if($treatId!=null){
            $category=session()->get('category');
            if($category==null){
                $cats = DB::table('medicine_category')->get();
                $request->session()->put("category",$cats);
            }
            $units=session()->get('units');
            if($units==null){
                $units = DB::table('units_of_measure')->get();
                $request->session()->put("units",$units);
            }
            $medis = DB::table('medicine')
            ->join('medicine_category', 'medicine.medi_category_id', '=', 'medicine_category.meca_id')
            ->select('*')
            ->get();                
            $request->session()->put("medicinesList",$medis);
            return view('medicine'); 
        }
        return redirect()->route('login')->with("error","WRITE CREDENTIALS!!");
    }

    public function filter(Request $request){
        $btn = $request->input("btns");  
        if($btn=="filter"){
            $name="%".strtoupper($request->input('filter'))."%";
            $cat=$request->input('medicines');
            $medicines = DB::table('medicine')
            ->join('medicine_category', 'medicine.medi_category_id', '=', 'medicine_category.meca_id')
            ->select('*');
            if (!empty($name)) {
                $medicines->where(DB::raw("UPPER(medi_name)"), 'like', $name);
            }
            if ($cat!=-1) {
                $medicines->where('medicine_category.meca_id', '=', $cat);
            }
            $results = $medicines->get();
            $request->session()->put("medicinesList",$results);
            return redirect()->route('medicine/index');  

        }else if($btn=="treatment"){
            return redirect()->route('treatment/index'); 
        }
    }

    public function manageButtons(Request $request ){
        $btns = $request->input("buttons"); 
        $treatId=session()->get('treatId');
        $date1 = Carbon::parse($request->input('start'));
        $date2 = Carbon::parse($request->input('end'));
        $interval = $date1->diff($date2);
        $days = $interval->days; 
        if ($btns == 'yes3') {  
            try{
                DB::table('treatment_medicine')->insert([
                    'trme_treatment_id' => $treatId,
                    'trme_medicine_id' => $request->input('mediId'),
                    'trme_date_start' => $request->input('start'),
                    'trme_date_end' => $request->input('end'),
                    'trme_quantity_per_day' => $request->input('total') / $days,
                    'trme_total_quantity' => $request->input('total'),
                    'trme_unit_of_measure_id' => $request->input('unit'),
                ]);

                $this->actMedicines($treatId,$request);
            }catch(\Illuminate\Database\QueryException $ex){
                return redirect()->route('medicine/index')->with("error","Insert Error")->withInput();
            }
            return redirect()->route('medicine/index')->with("success","Insert OK");                               
            

        } elseif ($btns == 'yes') {
            return redirect()->route('medicine/index');

        }elseif ($btns == 'yes1') {
            try{
                DB::table('treatment_medicine')
                    ->where('trme_treatment_id', $treatId)
                    ->where('trme_medicine_id', $request->input('mediId'))
                    ->update([
                        'trme_date_start' => $request->input('start'),
                        'trme_date_end' => $request->input('end'),
                        'trme_quantity_per_day' => $request->input('total') / $days,
                        'trme_total_quantity' => $request->input('total'),
                        'trme_unit_of_measure_id' => $request->input('unit'),
                ]);
                $this->actMedicines($treatId,$request);
            }catch(\Illuminate\Database\QueryException){
                return redirect()->route('medicine/index')->with("error","Update Error, may be this mail, username, nif, phone... already exist")->withInput();
            }
            return redirect()->route('medicine/index')->with("success","Update OK");                               

        }            
        else if($btns == "yes2"){
            DB::table('treatment_medicine')
            ->where('trme_treatment_id', $treatId)
            ->where('trme_medicine_id', $request->input('mediId'))
            ->delete();
            $this->actMedicines($treatId,$request);
            return redirect()->route('medicine/index')->with("success","Remove OK");
        }
    }

    public function getTreatment(Request $request){
        $id=session()->get('treatId');
        $treat = DB::table('treatment')->where('trea_id', $id)->get();
        return response()->json(['info' => $treat]);
    }

    public function getMedicineTreat(Request $request ){
        $mediId = $request->input('medi');
        $id=session()->get('treatId');
        $medi = DB::table('treatment_medicine')->where('trme_medicine_id', $mediId)->where('trme_treatment_id', $id)->get();
        return response()->json(['info' => $medi]);
    }

    public function actMedicines($treatId,Request $request){
        $medicines = DB::table('treatment_medicine AS t')
        ->leftJoin('medicine AS m', 't.trme_medicine_id', '=', 'm.medi_id')
        ->leftJoin('units_of_measure AS u', 't.trme_unit_of_measure_id', '=', 'u.unme_id')
        ->leftJoin('medicine_category AS mm', 'm.medi_category_id', '=', 'mm.meca_id')
        ->select('*')
        ->where('t.trme_treatment_id', $treatId)
        ->get();
        $request->session()->put("medicines",$medicines);
    }

    
}
