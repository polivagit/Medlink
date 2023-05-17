<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Doctor Details</title>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/5.1.3/css/bootstrap.min.css">
    <script src="https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/5.1.3/js/bootstrap.bundle.min.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.1.1/jquery.min.js"></script>
    <link rel="stylesheet" href="../../resources/css/doctorDetails.css">
    <script src="../../resources/js/doctorDetails.js"></script>
</head>
<body>
    <div class="container-fluid ">
        <div class="row mt-2 ">  
        <h2 class="text-center">DOCTOR DETAILS</h2>
      
            <div id="div" class="col-md-12 mt-3">
                <form class="col-md-8" action="{{route('doctor/manageButtonsDetail')}}" method="post">
                @csrf
                    <input type="hidden" name="patientSelect" id="patientSelect" >
                    <div class="row mt-3">                            
                        <div class="col-md-12 d-flex">
                            <div class="form-group d-flex col-md-6">
                                <label class="labelForm">First Name: <span class="text-danger">*</span></label>
                                <input type="text" id="first_name" name="first_name" value="{{ session()->get('fullDoctor')->pers_first_name }}" class="form-control">
                             </div>
                             <div class="form-group d-flex col-md-6">
                                <label class="labelForm">Nif: <span class="text-danger">*</span></label>
                                <input type="text" id="nif" name="nif" value="{{ session()->get('fullDoctor')->pers_nif }}" class="form-control">
                             </div>
                        </div>
                    </div>
                    <div class="row mt-3">                            
                        <div class="col-md-12 d-flex">
                             <div class="form-group d-flex col-md-6">
                                <label class="labelForm">Last Name: <span class="text-danger">*</span></label>
                                <input type="text" id="last_name" name="last_name" value="{{ session()->get('fullDoctor')->pers_last_name_1 }}" class="form-control">
                             </div>
                             <div class="form-group d-flex col-md-6">
                                <label class="labelForm">Phone: <span class="text-danger">*</span></label>
                                <input type="text" id="phone" name="phone" value="{{ session()->get('fullDoctor')->pers_phone_number }}" class="form-control">
                             </div>

                        </div>
                    </div>
                    <div class="row mt-3">                            
                        <div class="col-md-12 d-flex">
                            <div class="form-group d-flex col-md-6">
                                <label class="labelForm">Last Name 2: </label>
                                <input type="text" id="last_name1" name="last_name1" value="{{ session()->get('fullDoctor')->pers_last_name_2 }}" class="form-control">
                             </div>
                             <div class="form-group d-flex col-md-6">
                                <label class="labelForm">Email: <span class="text-danger">*</span></label>
                                <input type="text" id="email" name="email" value="{{ session()->get('fullDoctor')->pers_email }}" class="form-control">
                             </div>
                        </div>
                    </div>
                    <div class="row mt-3" >                            
                        <div class="col-md-12 d-flex">
                            <div class="form-group d-flex col-md-6">
                                <label class="labelForm">Street: <span class="text-danger">*</span></label>
                                <input type="text" id="street" name="street" value="{{ session()->get('fullDoctor')->pers_addrs_street }}" class="form-control">
                             </div>
                            <div class="form-group d-flex col-md-6">
                                <label class="labelForm">Birthdate: <span class="text-danger">*</span></label>
                                <input type="date" id="birthdate" name="birthdate" value="{{ session()->get('fullDoctor')->pers_birthdate }}" class="form-control">
                             </div>

                        </div>
                    </div>
                    <div class="row mt-3">                            
                        <div class="col-md-12 d-flex">

                             <div class="form-group d-flex col-md-6">
                                <label class="labelForm">Postal Code: <span class="text-danger">*</span></label>
                                <input type="text" id="postal_code" name="postal_code" value="{{ session()->get('fullDoctor')->pers_addrs_zip_code }}" class="form-control">
                             </div>
                             <div class="form-group d-flex col-md-6">
                                <label class="labelForm">Gender: <span class="text-danger">*</span></label>
                                <select name="gender" id="gender"  class="form-select" >
                                    <option value="-1"></option>
                                    <option value="0" {{ session()->get('fullDoctor')->pers_gender  == '0' ? 'selected' : 'true' }} >Male</option>
                                    <option value="1" {{ session()->get('fullDoctor')->pers_gender  == '1' ? 'selected' : 'true' }}>Female</option>
                                    <option value="2" {{ session()->get('fullDoctor')->pers_gender  == '2' ? 'selected' : 'true' }}>No-Binary</option>
                                    <option value="3" {{ session()->get('fullDoctor')->pers_gender  == '3' ? 'selected' : 'true' }}>No-Specific</option>
                                </select>
                             </div>
                        </div>
                    </div>
                    <div class="row mt-3">                            
                        <div class="col-md-12 d-flex">
                             <div class="form-group d-flex col-md-6">
                                <label class="labelForm">City: <span class="text-danger">*</span></label>
                                <input type="text" id="city" name="city" value="{{ session()->get('fullDoctor')->pers_addrs_city }}" class="form-control">
                             </div>
                             <div class="form-group d-flex col-md-6">
                                <label class="labelForm">Collegiate UID: <span class="text-danger">*</span></label>
                                <input type="text" id="collegiate_uid" name="collegiate_uid" value="{{ session()->get('fullDoctor')->doct_collegiate_uid }}" class="form-control">
                             </div>
                        </div>
                    </div>
                    <div class="row mt-3" >                            
                        <div class="col-md-12 d-flex">
                            <div class="form-group d-flex col-md-6">
                                <label class="labelForm">Province: <span class="text-danger">*</span></label>
                                <input type="text" id="province" name="province" value="{{ session()->get('fullDoctor')->pers_addrs_province }}" class="form-control">
                             </div>
                             <div class="form-group d-flex col-md-6">
                                <label class="labelForm">Speciality: <span class="text-danger">*</span></label>
                                <input type="text" id="speciality" name="speciality" value="{{ session()->get('fullDoctor')->spec_name }}" class="form-control">
                             </div>

                        </div>                        
                    </div>
                    <div class="row mt-3">                            
                        <div class="col-md-12 d-flex">
                        <div class="form-group d-flex col-md-6">
                                <label class="labelForm">Country: <span class="text-danger">*</span></label>
                                <input type="text" id="country" name="country" value="{{ session()->get('fullDoctor')->pers_addrs_country }}" class="form-control">
                             </div>
                             <div class="form-group d-flex col-md-6">
                                <label class="labelForm">Username: <span class="text-danger">*</span></label>
                                <input type="text" id="username" name="username" value="{{ session()->get('fullDoctor')->pers_login_username }}" class="form-control">
                             </div>
                        </div>
                    </div>

                    
                    <div class="row mt-5">                            
                        <div class="col-md-12 d-flex" id="btns"> 
                            <button type="submit" name="buttons" value="new" id="cancel" class="btn btn-danger">Cancel</button>
                            <button type="submit" name="buttons" value="update" id="update" class="btn">Update</button>
                            <button type="submit" name="buttons" value="go" id="go" class="btn btn-info" >Go Doctor Page</button>
                        </div>
                        <div class="col-md-12 d-flex mt-3" id="avis"> 
                            <label id="lblAvis">Are you sure you want to continue?</label>
                            <button type="submit" name="buttons" value="yes" id="yes" class="btn btn-success ms-3">Yes</button>
                            <button type="submit" name="buttons" value="yes1" id="yes1" class="btn btn-success ms-3">Yes</button>
                            <button type="submit" name="buttons" value="no" id="no" class="btn btn-danger ms-3">No</button>
                        </div>
                    </div>

                    
                    @if ($errors->any())
                    <div id="errForm" class="alert alert-danger mt-2">
                        <ul>
                            @foreach ($errors->all() as $error)
                                <li>{{ $error }}</li>
                            @endforeach
                        </ul>
                    </div>
                    @endif
                    @if (session()->get("error") != null )
                    <div id="errInsert" class="alert alert-danger mt-2">
                        <ul>
                            <li>{{ session()->get("error") }}</li>
                        </ul>
                    </div>
                    @endif
                    @if (session()->get("success") != null )
                    <div id="OkInsert" class="alert alert-success  mt-2">
                        <ul>
                            <li>{{ session()->get("success") }}</li>
                        </ul>
                    </div>
                    @endif
                </form>
            </div>
        </div>
    </body>
</html>