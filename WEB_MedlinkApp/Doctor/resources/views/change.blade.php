<!DOCTYPE html>
<html>
    <head>
        <meta charset="utf-8">
        <meta name="viewport" content="width=device-width, initial-scale=1">
        <title>Doctor Change Password</title>
		<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/5.1.3/css/bootstrap.min.css">
    	<script src="https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/5.1.3/js/bootstrap.bundle.min.js"></script>
		<link rel="stylesheet" href="../resources/css/login.css">
    </head>
    <body>

	<div class="card">
        <div class="card-header">
			<h3 class="mb-3">Change Password</h3>
			<img id="logo"> </img>
        </div>
        <div class="card-body">
            <form  method="post" action="{{route('login/changePassword')}}" >
			@csrf
                <div class="form-floating">
                    <input type="text" class="form-control" name="user" id="user" placeholder=" " required>
                    <label for="username">Username</label>
                </div>
                <div class="form-floating">
                    <input type="password" class="form-control" name="old" id="password" placeholder=" " required>
                    <label for="password">Old Password</label>
                </div>
                <div class="form-floating">
                    <input type="password" class="form-control" name="new"  placeholder=" " required>
                    <label for="password">New Password</label>
                </div>
                <div class="form-floating">
                    <input type="password" class="form-control" name="confirm"  placeholder=" " required>
                    <label for="password">Confirm New Password</label>
                </div>
                <div class="d-grid gap-2 mb-3">
                    <button type="submit" class="btn btn-primary">Change Password</button>
                </div>
                <a class="text-center mt-2 w-100 d-block" href="{{route('login')}}">Go Log In</a> 

                <div class="mt-3 text-center">
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
