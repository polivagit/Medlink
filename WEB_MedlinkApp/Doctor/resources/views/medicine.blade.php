<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Medicine</title>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/5.1.3/css/bootstrap.min.css">
    <script src="https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/5.1.3/js/bootstrap.bundle.min.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.1.1/jquery.min.js"></script>
    <link rel="stylesheet" href="../../resources/css/medicine.css"> 
    <script src="../../resources/js/medicine.js"></script>
</head>
<body>
    <div class="container-fluid">
        <div class="row col-md-12">
            <div class="col-md-4">
                <h2 class="text-center mt-3 mb-2">Add Medicine</h2> 
                <form  method="post" action="{{route('medicine/filter')}}">                    
                @csrf
                    <div class="d-flex align-items-center mt-3">
                        <button type="submit" name="btns" value="treatment" class="btn btn-info">Treatment Page</button>
                        <h2 class="text-center flex-grow-1">Filter</h2>
                    </div>
                    <div class="col-md-12 d-flex mt-4" >
                        <label class="ms-3 label mt-1"><b>Name:</b></label>
                        <input type="text" id="filter" name="filter" class="form-control ms-3">
                    </div>   
                    <div class="col-md-12 d-flex mt-4">
                        <label class="ms-3 label mt-1"><b>Category:</b></label>
                        <select name="medicines" id="medicines" class="form-select ms-3">
                            <option value="-1"></option>
                            @if(Session::has("category"))
                                    @foreach (session()->get("category") as $cat)
                                        <option value="{{ $cat->meca_id }}">{{ $cat->meca_name }} </option>
                                    @endforeach
                            @endif                      
                        </select>
                    </div>
                    <div class="col-md-12 d-flex mt-4 align-items-center justify-content-center">
                        <button type="submit" name="btns" value="filter" class="btn btn-primary ms-5 w-25">Filter</button>
                    </div>
                </form>
                <div id="divMedicines" class="mt-4 ms-2">
                    <table class="table " id="tableMedicines">
                        <thead>
                            <tr>
                                <th>Name</th>
                                <th>Category</th>
                            </tr>
                        </thead>
                        <tbody>
                            @if(Session::has("medicinesList"))
                                @foreach (session()->get("medicinesList") as $medi)
                                    <tr>
                                        <input type="hidden" value="{{ $medi->meca_id }}" id="mediID">
                                        <td>{{ $medi->medi_name }}</td>
                                        <td>{{ $medi->meca_name }}</td>
                                    </tr>
                                @endforeach
                            @endif
                        </tbody>
                    </table>
                </div>
            </div>

            <div class="col-md-8">
                <h2 class="text-center mt-3 mb-2">Current Medicines for Treatment</h2> 
                <form class="col-md-12 text-center" method="post" id="formMedicine" action="{{route('medicine/manageButtons')}}">
                <input type="hidden" id="mediId" name="mediId">                
                @csrf
                <div class="row">
                    <div class="col-md-8">
                        <div class="form-group row mt-4">
                            <label class="col-md-3 col-form-label text-end">Selected Medicine: <span class="text-danger">*</span></label>
                            <div class="col-md-9">
                                <input type="text" id="selectedMedicine" name="selectedMedicine" value="{{ old('selectedMedicine') }}" class="form-control" readonly>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="row mt-3">
                    <div class="col-md-8">
                        <div class="form-group row">
                            <label  class="col-md-3 col-form-label text-end">Start Date: <span class="text-danger">*</span></label>
                            <div class="col-md-4">
                                <input type="date" id="start" name="start" value="{{ old('start') }}" class="form-control">
                            </div>
                            <label class="col-md-2 col-form-label text-end">Total Qtat: <span class="text-danger">*</span></label>
                            <div class="col-md-3">
                                <input type="text" id="total" name="total" value="{{ old('total') }}" class="form-control">
                            </div>
                        </div>
                    </div>
                </div>
    
                <div class="row mt-3">
                    <div class="col-md-8">
                        <div class="form-group row">
                            <label class="col-md-3 col-form-label text-end">End Date: <span class="text-danger">*</span></label>
                            <div class="col-md-4">
                                <input type="date" id="end" name="end" value="{{ old('end') }}" class="form-control">
                            </div>
                            <label class="col-md-2 col-form-label text-end">Unit: <span class="text-danger">*</span></label>
                            <div class="col-md-3">
                                <select name="unit" id="unit" class="form-select">
                                    <option value="-1"></option>
                                    @if(Session::has("units"))
                                        @foreach (session()->get("units") as $unit)
                                            <option value="{{ $unit->unme_id }}" {{ old('unit') == $unit->unme_id ? 'selected' : '' }}>{{ $unit->unme_name }}</option>
                                        @endforeach
                                    @endif
                                </select>
                            </div>
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
                            <button type="submit" name="buttons" value="yes3" id="yes3" class="btn btn-success ms-3">Yes</button>
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
            <h2 class="text-center mt-3 mb-2">Medicines</h2> 

            <div class="col-md-8" id="divTableMedis">
            <div class="table-responsive" id="medicinesTable">
                <table class="table">
                    <thead>
                        <tr>
                            <th>Name</th>
                            <th>Category</th>
                        </tr>
                    </thead>
                    <tbody>
                        @if(Session::has("medicines"))
                            @foreach (session()->get("medicines") as $medi)
                                <tr>
                                    <input type="hidden" value="{{ $medi->medi_id }}" id="mediId2">
                                    <td>{{ $medi->medi_name }}</td>
                                    <td>{{ $medi->meca_name }}</td>
                                </tr>
                            @endforeach
                        @endif
                    </tbody>

                </table>
                
            </div>  
        </div>
        
        </div>
        
        </div>
    </body>
</html>