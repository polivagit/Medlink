<!DOCTYPE html>
<html>
    <head>
        <meta charset="utf-8">
        <meta name="viewport" content="width=device-width, initial-scale=1">
        <title>Practica Larabel</title>
    </head>
    <body >
        <h1 style="text-align:center;">REGISTRE QUINIELA 11</h1>
        <form method="post" name="form" action="{{route('login/verificarReg')}}">
			@csrf
		    <div style="display:flex;" >
		    	<p style="margin-left:5px;"><b>Nom:</b></p>
		    	<input type="text" name="nom" style="margin-top:10px; height:20px; margin-left:84px; width:250px;" />
		    </div>
			<div style="display:flex;" >
		    	<p style="margin-left:5px;"><b>Mail:</b></p>
		    	<input type="text" name="mail" style="margin-top:10px; height:20px; margin-left:85px; width:250px;" />
		    </div>
		    <div style="display:flex;">
		    	<p style="margin-left:5px;"><b>Password:</b></p>
		    	<input type="password" name="password"  style="margin-top:10px; height:20px; margin-left:50px; width:250px;"/>
		    </div>
			<div style="display:flex;">
		    	<p style="margin-left:5px;"><b>Rep password:</b></p>
		    	<input type="password" name="password_confirmation"  style="margin-top:10px; height:20px; margin-left:20px; width:250px;"/>
		    </div>
		    <br>
		    <br>
		    <input type="submit" name="reg" value="Registrarse" style="margin-left:90px; width:100px" />
		    <br>
		    <br>
		    <a style="margin-left:5px;" href="logout">Log IN</a>
			@if ($errors->any())
        	<div>
                <ul>
                    @foreach ($errors->all() as $error)
                        <li style="color:red;">{{ $error }}</li>
                    @endforeach
                </ul>
            </div>
        	@endif
	    </form>

    </body>
</html>
