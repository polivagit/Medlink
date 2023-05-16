<!DOCTYPE html>
<html>
    <head>
        <meta charset="utf-8">
        <meta name="viewport" content="width=device-width, initial-scale=1">
        <title>Practica Larabel</title>
    </head>
    <body >
		<div style="display:flex; align-items:center; justify-content:center;">
			<h1 style="text-align:center;">PAGINA USER</h1>
			<a style="margin-left:50px" href="../logout">Log Out</a>
		</div>
		<form method="post" name="jornada" action="{{route('user/partits')}}">
			@csrf
			<div style="display:flex;">
				<p>Selecciona una jornada</p>
				<select name="jornadaTriada" style="height:20px; margin-top:16px; margin-left:5px;" redonly>
					@if(Session::has("jornades"))
        				@foreach (session()->get("jornades") as $jornada)
            				<option value="{{ $jornada->num }}">{{ $jornada->nom }}</option>
        				@endforeach
            		@endif
				</select>
				<input type="submit" name="mostrar" value="Veure Partits" size="20" style="height:20px; margin-top:16px; margin-left:5px" />	
				<input type="submit" name="guardar" value="Guardar prediccions" size="20" style="height:20px; margin-top:16px; margin-left:5px" />					
				<p style="color:green; margin-left:5px">{{ session()->get("correctGuardat") }}</p>
				<p style="color:red; margin-left:5px">{{ session()->get("errorGuardat") }}</p>
			</div> 
			@if(Session::has("partits"))
				@include('layouts.partits')
			@endif
		</form>
    </body>
</html>
