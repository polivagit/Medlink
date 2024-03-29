<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Treatment</title>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/5.1.3/css/bootstrap.min.css">
    <script src="https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/5.1.3/js/bootstrap.bundle.min.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.1.1/jquery.min.js"></script>
    <link rel="stylesheet" href="../../resources/css/treatment.css">
    <script src="../../resources/js/treatment.js"></script>
</head>
<body>
    <div class="container-fluid">
        <div class="row ms-3 me-43 mt-3 col-md-12" >
            <div class="col-md-6">
                <form action="{{route('treatment/filter')}}" method="post">
                @csrf
                    <div class="d-flex align-items-center">
                        <button type="submit" name="btns" value="doctor" class="btn btn-info">Doctor Page</button>
                        <h2 class="text-center flex-grow-1">Treatment List</h2>
                    </div>
                    <div class="col-md-12 d-flex" id="divFilter">
                        <label class="ms-3 label mt-1"><b>Name:</b></label>
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
            <div class="table-responsive" id="treatmentsTable1">
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
                        @if(Session::has("treatmentsPatient"))
                            @foreach (session()->get("treatmentsPatient") as $treat)
                                <tr>
                                    <input type="hidden" value="{{ $treat->trea_id }}" id="treatId">
                                    <td>{{ $treat->trea_name }}</td>
                                    <td>{{ $treat->trea_date_start }}</td>
                                    <td>{{ $treat->trea_date_end }}</td>
                                    <td>{{ $treat->trea_is_active == '0' ? 'Active' : 'No Active' }}</td>
                                </tr>
                            @endforeach
                        @endif

                        @if(count(Session::get("treatmentsPatient")) == 0)
                            <tr>
                                <td colspan="4">No treatments found for this patient with you.</td>
                            </tr>
                        @endif
                    </tbody>

                </table>
                
            </div>

            <h2 class="text-center mt-2">Selected Treatments Medicines</h2>
            <form action="{{route('medicine/index')}}" method="post">
            @csrf
                <div class="table-responsive" id="medicinesTable">
                    <table class="table" >
                        <thead>
                            <tr>
                                <th>Name</th>
                                <th>Category</th>
                                <th>Qtat for day</th>
                                <th>Unit</th>
                            </tr>
                        </thead>
                        <tbody>
                        </tbody>
                    </table>
                </div>
                <div class="col-md-12 mt-3 " id="btnDetail">
                    <button type="submit" id="medicineDetails" class="btn btn-primary">See Medicine Details</button>
                </div>
            </form>
     
        </div>
        
            <div class="col-md-6 mt-3">
                <h2 class="text-center">New/Selected Treatment Form</h2>
                <form action="{{route('treatment/manageButtons')}}" method="post">
                @csrf
                    <input type="hidden" name="treatSelect" id="treatSelect" >
                    <div class="row mt-3">                            
                        <div class="col-md-12 d-flex">
                            <div class="form-group d-flex col-md-6">
                                <label class="labelForm">Name: <span class="text-danger">*</span></label>
                                <input type="text" id="name" name="name" value="{{ old('name') }}" class="form-control">
                             </div>
                             <div class="form-group d-flex col-md-6">
                                <label class="labelForm">Is Active: <span class="text-danger">*</span></label>
                                <select name="active" id="active"  class="form-select">
                                    <option value="-1"></option>
                                    <option value="0" {{ old('active') == '0' ? 'selected' : 'true' }} >Active</option>
                                    <option value="1" {{ old('active') == '1' ? 'selected' : 'true' }}>No Active</option>
                                </select>
                             </div>
                        </div>
                    </div>
                    <div class="row mt-3">                            
                        <div class="col-md-12 d-flex">
                            <div class="form-group d-flex col-md-6">
                                <label class="labelForm">Start Date: <span class="text-danger">*</span></label>
                                <input type="date" id="start" name="start" value="{{ old('start') }}" class="form-control">
                             </div>
                             <div class="form-group d-flex col-md-6">
                                <label class="labelForm">End Date: <span class="text-danger">*</span></label>
                                <input type="date" id="end" name="end" value="{{ old('end') }}" class="form-control">
                             </div>

                        </div>
                    </div>

                    <div class="row mt-3">                            
                        <div class="col-md-12 d-flex ">
                            <div class="form-group d-flex col-md-12">
                                <label class="labelForm1">Description: <span class="text-danger">*</span></label>
                                <textarea class="form-control" name="desc" id="desc" rows="2">{{ old('desc') }}</textarea>
                             </div>
                        </div>
                    </div>

                    <div class="row mt-3">                            
                        <div class="col-md-12 d-flex ">
                            <div class="form-group d-flex col-md-12">
                                <label class="labelForm1">Observation: </label>
                                <textarea class="form-control" name="obs" id="obs"  rows="2">{{ old('obs') }}</textarea>
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