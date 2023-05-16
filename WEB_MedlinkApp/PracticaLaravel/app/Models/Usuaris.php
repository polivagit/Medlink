<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;

class Usuaris extends Model
{
    public $table = "usuaris";
    public $timestamps = false; 
    public $fillable = array ('nom',"type","password","mail","verificat");
}
