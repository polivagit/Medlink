<div>
    @if(Session::has("partits"))
        <h2 style="text-align:center;">PARTITS</h2>
        <table style="width:100%;text-align: center;">
            <tr>
              <th>Num</th>
              <th>Local</th>
              <th>Visitant</th>
              <th>Data</th>
              @if(session()->get("usuari")->type == "user" && session()->get("jornadaTriada")->tancada==0)
                <th>Victoria Local</th>
                <th>Victoria Visitant</th>
                <th>Empat</th>
              @endif
              @if(Session::has("counts"))
                <th>Qt Local</th>
                <th>Qt Visitant</th>
                <th>Qt Empat</th>
              @endif
            </tr>
            <?php $i=1; ?>
            @foreach (session()->get("partits") as $partit)
                <tr>
                    <td><?=$i?></td>
                    <td>{{ $partit->local }}</td>
                    <td>{{ $partit->visitant }}</td>
                    <td>{{ $partit->data }}</td>                    
                    @if(Session::has("prediccions") && session()->get("jornadaTriada")->tancada==0 )
                        <?php $pred=session()->get("prediccions")[$i-1];?>
                        @switch($pred->resultat)
                            @case(1)
                                <td><input type="radio"  name="{{'radio' . $i}}" value="1" checked></td>
                                <td><input type="radio"  name="{{'radio' . $i}}" value="2"></td>
                                <td><input type="radio"  name="{{'radio' . $i}}" value="0"></td>
                            @break
                            @case(0)
                                <td><input type="radio"  name="{{'radio' . $i}}" value="1"></td>
                                <td><input type="radio"  name="{{'radio' . $i}}" value="2"></td>
                                <td><input type="radio"  name="{{'radio' . $i}}" value="0" checked></td>
                            @break
                            @case(2)
                                <td><input type="radio"  name="{{'radio' . $i}}" value="1"></td>
                                <td><input type="radio"  name="{{'radio' . $i}}" value="2" checked></td>
                                <td><input type="radio"  name="{{'radio' . $i}}" value="0"></td>
                            @break; 
                        @endswitch
                    @endif
                    @if(!Session::has("prediccions") && session()->get("usuari")->type == "user")
                        <td><input type="radio"  name="{{'radio' . $i}}" value="1"></td>
                        <td><input type="radio"  name="{{'radio' . $i}}" value="2"></td>
                        <td><input type="radio"  name="{{'radio' . $i}}" value="0"></td>
                    @endif
                    @if(Session::has("counts"))
                        <?php $countLocal=session()->get("counts")[$i-1]["countLocal"];
                              $countVisitant=session()->get("counts")[$i-1]["countVisitant"];
                              $countEmpat=session()->get("counts")[$i-1]["countEmpat"];
                        ?>
                        <td>{{ $countLocal }}</td>
                        <td>{{ $countVisitant }}</td>
                        <td>{{ $countEmpat }}</td>
                    @endif
                </tr>
                <?php $i++; ?>
            @endforeach
        </table>                
    @endif
</div>

