package com.example.medlinkapp.utils;

import okhttp3.ResponseBody;
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


}
