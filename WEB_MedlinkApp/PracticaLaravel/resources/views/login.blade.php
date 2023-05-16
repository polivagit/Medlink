<!DOCTYPE html>
<html>
    <head>
        <meta charset="utf-8">
        <meta name="viewport" content="width=device-width, initial-scale=1">
        <title>Practica Larabel</title>
    </head>
    <body >
        <h1 style="text-align:center;">LOGIN QUINIELA 11</h1>
        <form method="post" name="form" action="{{route('login/verificar')}}">
			@csrf
		    <div style="display:flex;" >
		    	<p style="margin-left:5px;"><b>Mail:</b></p>
		    	<input type="text" name="mail" style="margin-top:10px; height:20px; margin-left:52px; width:250px;" />
		    </div>
		    <br>
		    <div style="display:flex;">
		    	<p style="margin-left:5px;"><b>Password:</b></p>
		    	<input type="password" name="password"  style="margin-top:10px; height:20px; margin-left:20px; width:250px;"/>
		    </div>
			<p style="color:red;">{{ session()->get("error") }}</p>
		    <br>
		    <br>
		    <input type="submit" name="login" value="Log IN" style="margin-left:90px; width:100px" />
		    <br>
		    <br>
		    <br>
		    <a style="margin-left:5px;" href="registre">Registrarse</a>
            <br>
            <br>
		    <a style="margin-left:5px;"  href="public">Entrar com a perfil públic</a>
	    </form>

    </body>
</html>
