<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;
class Resultat extends Model
{
    use HasFactory;
    public $table = "resultat";
    public $timestamps = false; 
    public $fillable = array ("resultat");
}
