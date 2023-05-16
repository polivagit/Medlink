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
        Schema::create('usuaris', function (Blueprint $table) {
            $table->bigIncrements("id");
            $table->string('nom',50);
            $table->string("type",50);
            $table->string("password",50);
            $table->string('mail',50)->unique();
            $table->integer('verificat');
        });    
    }

    /**
     * Reverse the migrations.
     *
     * @return void
     */
    public function down()
    {
        Schema::dropIfExists('usuaris');
    }
};
