<?php

use Illuminate\Support\Facades\Route;
use \App\Http\Controllers\LoginController;
use \App\Http\Controllers\DoctorController;
use \App\Http\Controllers\TreatmentController;


/*
|--------------------------------------------------------------------------
| Web Routes
|--------------------------------------------------------------------------
|
| Here is where you can register web routes for your application. These
| routes are loaded by the RouteServiceProvider within a group which
| contains the "web" middleware group. Now create something great!
|
*/

Route::get('login', [LoginController::class, "index"]) -> name ('login');
Route::post('login/isValid', [LoginController::class, "isValid"]) -> name ('login/isValid');
Route::get('forget', [LoginController::class, "forget"]) -> name ('forget');
Route::get('change', [LoginController::class, "change"]) -> name ('change');
Route::post('login/recover', [LoginController::class, "recover"]) -> name ('login/recover');
Route::post('login/changePassword', [LoginController::class, "changePassword"]) -> name ('login/changePassword');

Route::match(['get', 'post'], 'doctor', [DoctorController::class, "index"]) -> name ('doctor');
Route::post('doctor/filter', [DoctorController::class, "filter"]) -> name ('doctor/filter');
Route::post('doctor/details', [DoctorController::class, "details"]) -> name ('doctor/details');
Route::post('doctor/manageButtons', [DoctorController::class, "manageButtons"]) -> name ('doctor/manageButtons');
Route::get('doctor/putInfo', [DoctorController::class, "putInfo"]) -> name ('doctor/putInfo');
Route::get('doctor/putTreatments', [DoctorController::class, "putTreatments"]) -> name ('doctor/putTreatments');
Route::match(['get', 'post'],'doctor/details', [DoctorController::class, "details"]) -> name ('doctor/details');
Route::post('doctor/manageButtonsDetail', [DoctorController::class, "manageButtonsDetail"]) -> name ('doctor/manageButtonsDetail');

Route::match(['get', 'post'], 'treatment/index', [TreatmentController::class, "index"]) -> name ('treatment/index');
Route::post('treatment/filter', [TreatmentController::class, "filter"]) -> name ('treatment/filter');
Route::get('treatment/putInfo', [TreatmentController::class, "putInfo"]) -> name ('treatment/putInfo');
Route::get('treatment/putMedicines', [TreatmentController::class, "putMedicines"]) -> name ('treatment/putMedicines');




