<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;

class Partit extends Model
{
    use HasFactory;
    public $table = "partit";
    public $timestamps = false; 
    public $fillable = array ();


    public function jornada()
    {
        return $this->belongsTo(Jornada::class, 'num');
    }
}
