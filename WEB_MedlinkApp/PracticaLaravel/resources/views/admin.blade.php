<!DOCTYPE html>
<html>
    <head>
        <meta charset="utf-8">
        <meta name="viewport" content="width=device-width, initial-scale=1">
        <title>Practica Larabel</title>
    </head>
    <body >
		<div style="display:flex; align-items:center; justify-content:center;">
			<h1 style="text-align:center;">PAGINA ADMINISTRADOR</h1>
			<a style="margin-left:50px" href="../logout">Log Out</a>
		</div>
        <h2 style="text-align:center;">PUJA LES JORNADES</h2>
		<form method="post" name="penjar" enctype="multipart/form-data" action="{{route('admin/penjar')}}">
			@csrf
			<div style="display:flex;">
				<p>Selecciona el arxiu XML amb la jornada que vulgis </p>
				<input style="margin-top:16px; margin-left:10px" accept=".xml" type="file" name="xmlfile" size="20" />
				<input type="submit" name="penjar" value="Penjar" size="20" style="height:20px; margin-top:16px; margin-left:5px" />		
			</div>  
			<p style="color:red;">{{ session()->get("error") }}</p>
			<p style="color:green;">{{ session()->get("correctinsert") }}</p>
		</form>
		<form method="post" name="jornada" action="{{route('admin/partits')}}">
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
				<input type="submit" name="tancar" value="Tancar Jornada" size="20" style="height:20px; margin-top:16px; margin-left:5px" />					
				<p style="color:green; margin-left:5px">{{ session()->get("correctTancada") }}</p>
			</div> 
			@if(Session::has("partits"))
				@include('layouts.partits')
			@endif
 
		</form>

    </body>
</html>
