<?php

use Illuminate\Database\Migrations\Migration;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Support\Facades\Schema;

return new class extends Migration
{
    /**
     * Run the migrations.
     *
     * @return void
     */
    public function up()
    {
        Schema::create('resultat', function (Blueprint $table) {
            $table->bigIncrements('id');
            $table->unsignedBigInteger('partit');
            $table->integer('jornada');
            $table->unsignedBigInteger('user');
            $table->integer('resultat');
            $table->foreign('jornada')->references('num')->on('jornada');
            $table->foreign('user')->references('id')->on('usuaris');    
            
            $table->unique(["partit","jornada","user"]);
        });
    }

    /**
     * Reverse the migrations.
     *
     * @return void
     */
    public function down()
    {
        Schema::dropIfExists('resultat');
    }
};
