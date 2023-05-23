<?php

namespace App\Http\Controllers;

use App\Http\Controllers\Controller;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\DB;
use Illuminate\Support\Facades\Session;
use App\Rules\CustomRules;



class DoctorController extends Controller
{
    public function index(Request $request)
    {
        $doctor=session()->get('doctor');
        if($doctor!=null){
            $patients=$request->session()->get("patients");
            $caregivers=$request->session()->get("caregivers");
            
            if($patients==null){
                $patients = DB::table('person as p')
                ->join('patient as pp', 'p.pers_id', '=', 'pp.pati_person_id')
                ->select('*')
                ->get();
                $request->session()->put("patients",$patients);
            }
            if($caregivers==null){
                $caregivers = DB::table('person')
                    ->whereNotIn('pers_id', function ($query) {
                        $query->select('pati_person_id')->from('patient');
                    })
                    ->whereNotIn('pers_id', function ($query) {
                        $query->select('doct_person_id')->from('doctor');
                    })
                    ->get();
                $request->session()->put("caregivers",$caregivers);
                
            }
            return view('doctor'); 

        }else{
            return redirect()->route('login')->with("error","WRITE CREDENTIALS!!");
        }
    }

    public function filter(Request $request){
        $btn = $request->input("btns");  
        if($btn=="filter"){
            $doctor=session()->get('doctor');
            $fullName="%".strtoupper($request->input('filter'))."%";
    
            $patients = DB::table('person as p')
            ->join('patient as pp', 'p.pers_id', '=', 'pp.pati_person_id')
            ->where(DB::raw("UPPER(CONCAT(p.pers_first_name, ' ', p.pers_last_name_1, ' ', p.pers_last_name_2))"), 'like', $fullName)
            ->select('*')
            ->get();
    
            $request->session()->put("patients",$patients);
            return redirect()->route('doctor');  
        }else{
            Session::flush();
            return redirect()->route('login');
        }
     
    }

    public function putInfo(Request $request){
        $patientId = $request->input('patient');
        $info=DB::table('person as p')
        ->join('patient as pp', 'p.pers_id', '=', 'pp.pati_person_id')
        ->where('p.pers_id', '=', $patientId)
        ->select('*')
        ->get();

        $treatmentss = DB::table('treatment')
        ->where('trea_doctor_id', session()->get('doctor')->pers_id)
        ->where('trea_patient_id', $patientId)
        ->get();
        $request->session()->put("treatmentsPatient",$treatmentss);

        $request->session()->put("patientId",$patientId);
        return response()->json(['info' => $info]);
    }

    public function putTreatments(Request $request ){
        $patientId = $request->input('patient');
        $treatments = DB::table('treatment as t')
        ->join('person as p', 't.trea_patient_id', '=', 'p.pers_id')
        ->select('*')
        ->where('t.trea_patient_id', '=', $patientId)
        ->get();

        $request->session()->put("treatments",$treatments);    
        $request->session()->put("patientId",$patientId);

        $treatmentss = DB::table('treatment')
            ->where('trea_doctor_id', session()->get('doctor')->pers_id)
            ->where('trea_patient_id', $patientId)
            ->get();
        $request->session()->put("treatmentsPatient",$treatmentss);
    
        return response()->json(['treatments' => $treatments]);
    }

    public function manageButtons(Request $request ){
        $btns = $request->input("buttons");  
        if ($btns == 'add') {  
            $ok=$this->validForm($request);   
            if($ok){
                try{
                    DB::table('person')->insert([
                        'pers_nif' => $request->input('nif'),
                        'pers_first_name' => $request->input('first_name'),
                        'pers_last_name_1' => $request->input('last_name'),
                        'pers_last_name_2' => $request->input('last_name1'),
                        'pers_birthdate' => $request->input('birthdate'),
                        'pers_phone_number' => $request->input('phone'),
                        'pers_email' => $request->input('email'),
                        'pers_gender' => $request->input('gender'),
                        'pers_addrs_street' => $request->input('street'),
                        'pers_addrs_zip_code' => $request->input('postal_code'),
                        'pers_addrs_city' => $request->input('city'),
                        'pers_addrs_province' => $request->input('province'),
                        'pers_addrs_country' => $request->input('country'),
                        'pers_login_username' => $request->input('username'),
                        'pers_login_password' => substr($request->input('nif'), -5)
                    ]);

                    $new = DB::table('person')->orderBy('pers_id', 'desc')->first();
                    $caregiver=$request->input('caregiver');
                    if($caregiver==-1){
                        $caregiver=null;
                    }
                    DB::table('patient')->insert([
                        'pati_person_id' => $new->pers_id,
                        'pati_height' => $request->input('heigth'),
                        'pati_weight' => $request->input('weigth'),
                        'pati_remarks' => $request->input('remarks'),
                        'pati_caregiver_id' => $caregiver
                    ]); 
                    $patients = DB::table('person as p')
                    ->join('patient as pp', 'p.pers_id', '=', 'pp.pati_person_id')
                    ->select('*')
                    ->get();
                    $request->session()->put("patients",$patients);
                    return redirect()->route('doctor')->with("success","Insert OK");


                }catch(\Illuminate\Database\QueryException $ex){
                    return redirect()->route('doctor')->with("error","Insert Error, may be this mail, username, nif, phone... already exist")->withInput();
                }
                               
            }
            return redirect()->route('doctor');

        } elseif ($btns == 'yes') {
            return redirect()->route('doctor');

        }elseif ($btns == 'yes1') {
            try{
                DB::table('person')
                    ->where('pers_id', $request->input('patientSelect'))
                    ->update(['pers_first_name' => $request->input('first_name'),
                    'pers_last_name_1' => $request->input('last_name'),
                    'pers_last_name_2' => $request->input('last_name1'),
                    'pers_phone_number' => $request->input('phone'),
                    'pers_email' => $request->input('email'),
                    'pers_gender' => $request->input('gender'),
                    'pers_addrs_street' => $request->input('street'),
                    'pers_addrs_zip_code' => $request->input('postal_code'),
                    'pers_addrs_city' => $request->input('city'),
                    'pers_addrs_province' => $request->input('province'),
                    'pers_addrs_country' => $request->input('country'),
                ]);

                $caregiver=$request->input('caregiver');
                if($caregiver==-1){
                    $caregiver=null;
                }
                DB::table('patient')
                    ->where('pati_person_id', $request->input('patientSelect'))
                    ->update([
                        'pati_height' => $request->input('heigth'),
                        'pati_weight' => $request->input('weigth'),
                        'pati_remarks' => $request->input('remarks'),
                        'pati_caregiver_id' => $caregiver
                    ]);

                $patients = DB::table('person as p')
                    ->join('patient as pp', 'p.pers_id', '=', 'pp.pati_person_id')
                    ->select('*')
                    ->get();
                $request->session()->put("patients",$patients);
                return redirect()->route('doctor')->with("success","Update OK");                 

            }catch(\Illuminate\Database\QueryException){
                return redirect()->route('doctor')->with("error","Update Error, may be this mail, username, nif, phone... already exist")->withInput();
            }
           
        }            
        else if($btns == "yes2"){
            DB::table('patient')
                ->where('pati_person_id', $request->input('patientSelect'))
                ->delete();
            DB::table('person')
                ->where('pers_id', $request->input('patientSelect'))
                ->delete();

            $patients = DB::table('person as p')
            ->join('patient as pp', 'p.pers_id', '=', 'pp.pati_person_id')
            ->select('*')
            ->get();
            $request->session()->put("patients",$patients);
            return redirect()->route('doctor')->with("success","Remove OK");


        }
    }

    public function validForm(Request $request){
        $request->validate([
            'first_name' => 'required|min:2',
            'last_name' => 'required|min:2',
            'nif' => 'required|regex:/^[0-9]{8}[A-J]{1}$/',
            'phone' => 'required|regex:/^[0-9]{9}$/',
            'email' => 'required|email',
            'birthdate' => 'required|before_or_equal:today',
            'gender' => 'regex:/^[0-3]{1}$/',
            'street' => 'required|min:10',
            'postal_code' => 'required|regex:/^[0-9]{5}$/',
            'city' => 'required|min:3',
            'province' => 'required|min:3',
            'country' => 'required|min:3',
            'username' => 'required|min:2',
            'weigth' => ['required', new CustomRules(2, 400)],
            'heigth' => ['required', new CustomRules(30, 260)],
            'first_name.required' => 'First name is required',
            'first_name.min' => 'First name must have 2 caracters or more',
            'last_name.required' => 'Last name is required',
            'last_name.min' => 'Last name must have 2 caracters or more',
            'nif.required' => 'Nif is required',
            'nif.regex' => 'Error format in nif',
            'phone.required' => 'Phone is required',
            'phone.regex' => 'Error format in phone',
            'email.required' => 'Email is required',
            'email.email' => 'Error format in email',
            'birthdate.required' => 'Birthdate is required',
            'birthdate.before_or_equal' => 'Birthdate must be past',
            'gender.regex' => 'Gender is required',
            'street.required' => 'Street is required',
            'street.min' => 'Street must have 10 caracters or more',
            'postal_code.required' => 'Postal code is required',
            'postal_code.regex' => 'Error format in postal code ',
            'city.required' => 'City is required',
            'city.min' => 'City must have 3 caracters or more',
            'province.required' => 'Province is required',
            'province.min' => 'Province must have 3 caracters or more',
            'country.required' => 'Country is required',
            'country.min' => 'Country must have 3 caracters or more',
            'username.required' => 'Username is required',
            'username.min' => 'Username must have 2 caracters or more',
            'weigth.required' => 'Weigth is required',
            'heigth.required' => 'Heigth is required',
        ]);

        return true;
    }

    public function details(Request $request){
        $doctor=session()->get('doctor');
        if($doctor!=null){
            $fullDoctor=DB::table('person as p')
                        ->join('doctor as d', 'p.pers_id', '=', 'd.doct_person_id')
                        ->join('specialty as s', 'd.doct_specialty_id', '=', 's.spec_id')
                        ->where('p.pers_id', '=', 5)
                        ->select('*')
                        ->first();
            $request->session()->put("fullDoctor",$fullDoctor);
            return view('doctorDetail'); 
        }else{
            return redirect()->route('login')->with("error","WRITE CREDENTIALS!!");
        }
    }
    
    public function manageButtonsDetail(Request $request){
        $btns = $request->input("buttons");  
        if($btns=="yes"){
            return redirect()->route('doctor/details');

        }else if($btns=="yes1"){
            $ok=$this->validFormDetails($request);
            if($ok){
                try{
                    $doctor=session()->get('fullDoctor');
                    DB::table('person')
                    ->where('pers_id', $doctor->pers_id)
                    ->update([
                        'pers_first_name' => $request->input('first_name'),
                        'pers_last_name_1' => $request->input('last_name'),
                        'pers_last_name_2' => $request->input('last_name1'),
                        'pers_phone_number' => $request->input('phone'),
                        'pers_email' => $request->input('email'),
                        'pers_gender' => $request->input('gender'),
                        'pers_addrs_street' => $request->input('street'),
                        'pers_addrs_zip_code' => $request->input('postal_code'),
                        'pers_addrs_city' => $request->input('city'),
                        'pers_addrs_province' => $request->input('province'),
                        'pers_addrs_country' => $request->input('country'),
                    ]);
                }catch(\Illuminate\Database\QueryException $ex){
                    return redirect()->route('doctor/details')->with("error","Update Error, may be this mail, username, nif, phone... already exist")->withInput();
                }
                return redirect()->route('doctor/details')->with("success","Update OK");
            }
            return redirect()->route('doctor/details');            
        }else if($btns=="go"){
            return redirect()->route('doctor');    
        }
    }

    public function validFormDetails(Request $request){
        $request->validate([
            'first_name' => 'required|min:2',
            'last_name' => 'required|min:2',
            'phone' => 'required|regex:/^[0-9]{9}$/',
            'email' => 'required|email',
            'gender' => 'regex:/^[0-3]{1}$/',
            'street' => 'required|min:10',
            'postal_code' => 'required|regex:/^[0-9]{5}$/',
            'city' => 'required|min:3',
            'province' => 'required|min:3',
            'country' => 'required|min:3',
            ],
            [
            'first_name.required' => 'First name is required',
            'first_name.min' => 'First name must have 2 caracters or more',
            'last_name.required' => 'Last name is required',
            'last_name.min' => 'Last name must have 2 caracters or more',
            'phone.required' => 'Phone is required',
            'phone.regex' => 'Error format in phone',
            'email.required' => 'Email is required',
            'email.email' => 'Error format in email',
            'gender.regex' => 'Gender is required',
            'street.required' => 'Street is required',
            'street.min' => 'Street must have 10 caracters or more',
            'postal_code.required' => 'Postal code is required',
            'postal_code.regex' => 'Error format in postal code ',
            'city.required' => 'City is required',
            'city.min' => 'City must have 3 caracters or more',
            'province.required' => 'Province is required',
            'province.min' => 'Province must have 3 caracters or more',
            'country.required' => 'Country is required',
            'country.min' => 'Country must have 3 caracters or more',
        ]);

        return true;
    }



    
}