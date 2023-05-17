package com.example.medlinkapp.utils.api;

import com.example.medlinkapp.utils.login.LoginResponse;
import com.example.medlinkapp.utils.login.RestorePasswordResponse;
import com.example.medlinkapp.utils.treatment.TreatmentResponse;

import retrofit2.Call;
import retrofit2.http.Field;
import retrofit2.http.FormUrlEncoded;
import retrofit2.http.Header;
import retrofit2.http.Headers;
import retrofit2.http.POST;

public interface ApiService {
    @Headers("Content-Type: application/x-www-form-urlencoded")
    @POST("login")
    @FormUrlEncoded
    Call<LoginResponse> login(
            @Header("Authorization") String authHeader,
            @Field("user") String username,
            @Field("pass") String password
    );

    @Headers("Content-Type: application/x-www-form-urlencoded")
    @POST("treatment")
    @FormUrlEncoded
    Call<TreatmentResponse> treatments(
            @Header("Authorization") String authHeader,
            @Field("id")  String patientId
    );

    @Headers("Content-Type: application/x-www-form-urlencoded")
    @POST("treatment")
    @FormUrlEncoded
    Call<RestorePasswordResponse> restorePassword(
            @Header("Authorization") String authHeader,
            @Field("id")  String patientId
    );


}
