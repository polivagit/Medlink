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
        Schema::create('partit', function (Blueprint $table) {
            $table->bigIncrements("id");
            $table->integer('jornada');
            $table->string('local',50);
            $table->string('visitant',50);
            $table->date('data');
            
            $table->foreign('jornada')->references('num')->on('jornada');


        });
    }

    /**
     * Reverse the migrations.
     *
     * @return void
     */
    public function down()
    {
        Schema::dropIfExists('partit');
    }
};
