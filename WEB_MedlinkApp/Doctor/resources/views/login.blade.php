<!DOCTYPE html>
<html>
    <head>
        <meta charset="utf-8">
        <meta name="viewport" content="width=device-width, initial-scale=1">
        <title>Doctor Login</title>
		<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/5.1.3/css/bootstrap.min.css">
    	<script src="https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/5.1.3/js/bootstrap.bundle.min.js"></script>
		<link rel="stylesheet" href="../resources/css/login.css">
    </head>
    <body>

	<div class="card">
        <div class="card-header">
			<h3 class="mb-3">Log In</h3>
			<img id="logo"> </img>
        </div>
        <div class="card-body">
            <form  method="post" action="{{route('login/isValid')}}" >
			@csrf
                <div class="form-floating">
                    <input type="text" class="form-control" name="user" id="user" placeholder=" " required>
                    <label for="username">Username</label>
                </div>
                <div class="form-floating">
                    <input type="password" class="form-control" name="password" id="password" placeholder=" " required>
                    <label for="password">Password</label>
                </div>
                <div class="d-grid gap-2">
                    <button type="submit" class="btn btn-primary">Log In</button>
                </div>
                <div class="mt-3 text-center">
                <a class="me-3" href="{{route('forget')}}">Forget your password</a>  <a class="ms-3" href="{{route('change')}}">Change your password</a>
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
                <div class="alert alert-danger mt-2">
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
