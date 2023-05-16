<?php

namespace Database\Seeders;

use Illuminate\Database\Console\Seeds\WithoutModelEvents;
use Illuminate\Database\Seeder;
use Illuminate\Support\Facades\DB;


class UsuariSeeder extends Seeder
{
    /**
     * Run the database seeds.
     *
     * @return void
     */
    public function run()
    {
        DB::table('usuaris')->insert(
            [
                [
                    'nom' => 'Administrador',
                    "type" => "admin",
                    "password" => "admin",
                    "mail" => "admin",
                    "verificat" => 1
                ],
                [
                    'nom' => 'Public',
                    "type" => "public",
                    "password" => "public",
                    "mail" => "public",
                    "verificat" => 1
                ],
            ]);     
    }
}
