<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;
class Jornada extends Model
{
    use HasFactory;
    public $table = "jornada";
    public $timestamps = false; 
    public $fillable = array ("tancada","nom");

    public function partits()
    {
        return $this->hasMany(Partit::class, 'jornada');
    }

    /*public function resultats()
    {
        return $this->hasMany(Resultat::class, 'jornada');
    }*/
}
