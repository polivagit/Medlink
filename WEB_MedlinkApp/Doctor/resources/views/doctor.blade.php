<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Medicines</title>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/5.1.3/css/bootstrap.min.css">
    <script src="https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/5.1.3/js/bootstrap.bundle.min.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.1.1/jquery.min.js"></script>
    <link rel="stylesheet" href="../resources/css/doctor.css">
    <script src="../resources/js/doctor.js"></script>
</head>
<body>
    <div class="container-fluid">
        <div class="row ms-3 me-43 mt-3 col-md-12" >
            <div class="col-md-6">
                <form action="{{route('doctor/filter')}}" method="post">
                @csrf
                    <div class="d-flex align-items-center">
                        <button type="submit" name="btns" value="logout" class="btn btn-danger">Log Out</button>
                        <h2 class="text-center flex-grow-1">Patients List</h2>
                    </div>
                    <div class="col-md-12 d-flex" id="divFilter">
                        <label class="ms-3 label"><b>Full Name:</b></label>
                        <input type="text" id="filter" name="filter" class="form-control ms-3">
                        <button type="submit" name="btns" value="filter" class="btn btn-primary ms-3">Filter</button>
                    </div>                
                </form>
            </div>
            <div class="col-md-6  " id="info">
                <form action="{{route('doctor/details')}}" method="get">
                @csrf
                    <div class="col-md-12 text-center mb-1" >
                        <h3>Doctor Info</h3>
                    </div>
                    <div class="col-md-12 text-center mb-1 d-flex" >
                        <div class="col-md-4 text-center" >
                            <label class="fw-bold">Full Name:</label>
                            <label>{{ session()->get("doctor")->pers_first_name }} {{ session()->get("doctor")->pers_last_name_1 }} {{ session()->get("doctor")->pers_last_name_2 }}</label>
                        </div>
                        <div class="col-md-4">
                            <label class="fw-bold">Speciality:</label>
                            <label>{{ session()->get("doctor")->spec_name }}</label>
                        </div>
                        <div class="col-md-4">
                            <button class="btn btn-primary">See Doctor Details</button>
                        </div>
                    </div>
                </form>
            </div>
        </div>

        <div class="row mt-2 ">
        <div class="col-md-6">
            <div class="table-responsive" id="patientsTable">
                <table class="table">
                    <thead>
                        <tr>
                            <th>NIF</th>
                            <th>Full Name</th>
                            <th>Phone Number</th>
                            <th>Email</th>
                            <th>Birthdate</th>
                        </tr>
                    </thead>
                    <tbody>
                        @if(Session::has("patients"))
        			    	@foreach (session()->get("patients") as $patient)
                                <tr>
                                    <input type="hidden" value="{{ $patient->pers_id }}" id="patientId">
                                    <td>{{ $patient->pers_nif }}</td>
                                    <td>{{ $patient->pers_first_name }} {{ $patient->pers_last_name_1 }} {{ $patient->pers_last_name_2 }}</td>
                                    <td>{{ $patient->pers_phone_number }}</td>
                                    <td>{{ $patient->pers_email }}</td>
                                    <td>{{ $patient->pers_birthdate }}</td>
                                </tr>
        			    	@endforeach
            		    @endif
                    </tbody>
                </table>
                
            </div>

            <h2 class="text-center mt-2">Selected Patient Treatments</h2>
            <form action="{{route('treatment/index')}}" method="post">
            @csrf
                <div class="table-responsive" id="treatmentsTable">
                    <table class="table">
                        <thead>
                            <tr>
                                <th>Name</th>
                                <th>Start Date</th>
                                <th>End Date</th>
                                <th>Is Active</th>
                            </tr>
                        </thead>
                        <tbody>
                        </tbody>
                    </table>
                </div>
                <div class="col-md-12 mt-3 " id="btnDetail">
                    <button type="submit" id="treatmentDetails" class="btn btn-primary">See Treatment Details</button>
                </div>
            </form>
     
        </div>
        
            <div class="col-md-6 mt-3">
                <h2 class="text-center">New/Selected Patient Form</h2>
                <form action="{{route('doctor/manageButtons')}}" method="post">
                @csrf
                    <input type="hidden" name="patientSelect" id="patientSelect" >
                    <div class="row mt-3">                            
                        <div class="col-md-12 d-flex">
                            <div class="form-group d-flex col-md-4">
                                <label class="labelForm">First Name: <span class="text-danger">*</span></label>
                                <input type="text" id="first_name" name="first_name" value="{{ old('first_name') }}" class="form-control">
                             </div>
                             <div class="form-group d-flex col-md-4">
                                <label class="labelForm">Last Name: <span class="text-danger">*</span></label>
                                <input type="text" id="last_name" name="last_name" value="{{ old('last_name') }}" class="form-control">
                             </div>
                             <div class="form-group d-flex col-md-4">
                                <label class="labelForm">Last Name 2: </label>
                                <input type="text" id="last_name1" name="last_name1" value="{{ old('last_name1') }}" class="form-control">
                             </div>
                        </div>
                    </div>
                    <div class="row mt-3">                            
                        <div class="col-md-12 d-flex">
                            <div class="form-group d-flex col-md-4">
                                <label class="labelForm">Nif: <span class="text-danger">*</span></label>
                                <input type="text" id="nif" name="nif" value="{{ old('nif') }}" class="form-control">
                             </div>
                             <div class="form-group d-flex col-md-4">
                                <label class="labelForm">Phone: <span class="text-danger">*</span></label>
                                <input type="text" id="phone" name="phone" value="{{ old('phone') }}" class="form-control">
                             </div>
                             <div class="form-group d-flex col-md-4">
                                <label class="labelForm">Email: <span class="text-danger">*</span></label>
                                <input type="text" id="email" name="email" value="{{ old('email') }}" class="form-control">
                             </div>
                        </div>
                    </div>
                    <div class="row mt-3" >                            
                        <div class="col-md-12 d-flex">
                            <div class="form-group d-flex col-md-4">
                                <label class="labelForm">Birthdate: <span class="text-danger">*</span></label>
                                <input type="date" id="birthdate" name="birthdate" value="{{ old('birthdate') }}" class="form-control">
                             </div>
                             <div class="form-group d-flex col-md-4">
                                <label class="labelForm">Gender: <span class="text-danger">*</span></label>
                                <select name="gender" id="gender"  class="form-select">
                                    <option value="-1"></option>
                                    <option value="0" {{ old('gender') == '0' ? 'selected' : 'true' }} >Male</option>
                                    <option value="1" {{ old('gender') == '1' ? 'selected' : 'true' }}>Female</option>
                                    <option value="2" {{ old('gender') == '2' ? 'selected' : 'true' }}>No-Binary</option>
                                    <option value="3" {{ old('gender') == '3' ? 'selected' : 'true' }}>No-Specific</option>
                                </select>
                             </div>
                             <div class="form-group d-flex col-md-4">
                                <label class="labelForm">Caregiver:</label>
                                <select name="caregiver"  id="caregiver"  class="form-select">
                                <option value="-1"></option>
                                @if(Session::has("caregivers"))
        			    	        @foreach (session()->get("caregivers") as $caregiver)
                                        <option value="{{ $caregiver->pers_id }}" {{ old('caregiver') == $caregiver->pers_id ? 'selected' : 'true' }}>{{ $caregiver->pers_first_name }} {{ $caregiver->pers_last_name_1 }}</option>
        			    	        @endforeach
            		            @endif
                                </select>
                            </div>
                        </div>
                    </div>
                    <div class="row mt-3">                            
                        <div class="col-md-12 d-flex">
                            <div class="form-group d-flex col-md-4">
                                <label class="labelForm">Street: <span class="text-danger">*</span></label>
                                <input type="text" id="street" name="street" value="{{ old('street') }}" class="form-control">
                             </div>
                             <div class="form-group d-flex col-md-4">
                                <label class="labelForm">Postal Code: <span class="text-danger">*</span></label>
                                <input type="text" id="postal_code" name="postal_code" value="{{ old('postal_code') }}" class="form-control">
                             </div>
                             <div class="form-group d-flex col-md-4">
                                <label class="labelForm">City: <span class="text-danger">*</span></label>
                                <input type="text" id="city" name="city" value="{{ old('city') }}" class="form-control">
                             </div>
                        </div>
                    </div>
                    <div class="row mt-3" >                            
                        <div class="col-md-12 d-flex">
                            <div class="form-group d-flex col-md-4">
                                <label class="labelForm">Province: <span class="text-danger">*</span></label>
                                <input type="text" id="province" name="province" value="{{ old('province') }}" class="form-control">
                             </div>
                             <div class="form-group d-flex col-md-4">
                                <label class="labelForm">Country: <span class="text-danger">*</span></label>
                                <input type="text" id="country" name="country" value="{{ old('country') }}" class="form-control">
                             </div>
                             <div class="form-group d-flex col-md-4">
                                <label class="labelForm">Username: <span class="text-danger">*</span></label>
                                <input type="text" id="username" name="username" value="{{ old('username') }}" class="form-control">
                             </div>
                        </div>                        
                    </div>
                    <div class="row mt-3">                            
                        <div class="col-md-12 d-flex">
                            <div class="form-group d-flex col-md-4">
                                <label class="labelForm">Weigth: (Kg) <span class="text-danger">*</span></label>
                                <input type="text" id="weigth" name="weigth" value="{{ old('weigth') }}" class="form-control">
                             </div>
                             <div class="form-group d-flex col-md-4">
                                <label class="labelForm">Heigth: (Cm) <span class="text-danger">*</span></label>
                                <input type="text" id="heigth" name="heigth" value="{{ old('heigth') }}" class="form-control">
                             </div>
                             <div class="form-group d-flex col-md-4"> </div>
                        </div>
                    </div>
                    <div class="row mt-3">                            
                        <div class="col-md-12 d-flex ">
                            <div class="form-group d-flex col-md-12">
                                <label class="labelForm1">Remarks: </label>
                                <textarea class="form-control" name="remarks" id="remarks" class="remarks" rows="2">{{ old('remarks') }}</textarea>
                             </div>
                        </div>
                    </div>
                    
                    <div class="row mt-5">                            
                        <div class="col-md-12 d-flex" id="btns"> 
                            <button type="submit" name="buttons" value="add" id="add" class="btn btn-success">Add</button>
                            <button type="submit" name="buttons" value="new" id="cancel" class="btn btn-info">New</button>
                            <button type="submit" name="buttons" value="update" id="update" class="btn">Update</button>
                            <button type="submit" name="buttons" value="remove" id="remove" class="btn btn-danger">Remove</button>
                        </div>
                        <div class="col-md-12 d-flex mt-3" id="avis"> 
                            <label id="lblAvis">Are you sure you want to continue?</label>
                            <button type="submit" name="buttons" value="yes" id="yes" class="btn btn-success ms-3">Yes</button>
                            <button type="submit" name="buttons" value="yes1" id="yes1" class="btn btn-success ms-3">Yes</button>
                            <button type="submit" name="buttons" value="yes2" id="yes2" class="btn btn-success ms-3">Yes</button>
                            <button type="submit" name="buttons" value="no" id="no" class="btn btn-danger ms-3">No</button>
                        </div>
                    </div>
                    <div id="errorModal">
                        <h2>Errors</h2>
                        <ul id="errorList"></ul>
                        <button class="btn btn-danger" id="closeModal">Close</button>
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