<?php

use Illuminate\Support\Facades\Route;
use \App\Http\Controllers\LoginController;
use \App\Http\Controllers\AdminController;
use \App\Http\Controllers\UserController;
use \App\Http\Controllers\PublicController;

Route::get('login', [LoginController::class, "index"]) -> name ('login');
Route::post('login/verificar', [LoginController::class, "verificar"]) -> name ('login/verificar')->middleware("validarLogin");
Route::get('registre', [LoginController::class, "registrarse"]) -> name ('registre');
Route::post('login/verificarReg', [LoginController::class, "verificarRegistre"]) -> name ('login/verificarReg');
Route::get('login/registrarUser/{mail}', [LoginController::class, "registrarUser"]) -> name ('login/registrarUser/{mail}');
Route::get('logout', [LoginController::class, "logout"]) -> name ('logout');

Route::get('admin/index', [AdminController::class, "index"]) -> name ('admin/index');
Route::post('admin/penjar', [AdminController::class, "penjar"]) -> name ('admin/penjar');
Route::post('admin/partits', [AdminController::class, "partits"]) -> name ('admin/partits');

Route::get('user/index', [UserController::class, "index"]) -> name ('user/index');
Route::post('user/partits', [UserController::class, "partits"]) -> name ('user/partits');

Route::get('public', [PublicController::class, "index"]) -> name ('public');
Route::post('public/partits', [PublicController::class, "partits"]) -> name ('public/partits');







