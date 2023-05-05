package com.example.medlinkapp.utils;

import android.util.Base64;

import java.io.IOException;

import okhttp3.Interceptor;
import okhttp3.OkHttpClient;
import okhttp3.Request;
import okhttp3.ResponseBody;
import retrofit2.Call;
import retrofit2.Response;
import retrofit2.http.Body;
import retrofit2.http.Field;
import retrofit2.http.FormUrlEncoded;
import retrofit2.http.GET;
import retrofit2.http.Header;
import retrofit2.http.POST;

public interface ApiService {

    @FormUrlEncoded
    @POST("login")
    Call<LoginResponse> login(
            @Field("username") String username,
            @Field("password") String password
    );

    @POST("login")
    Call<LoginResponse> authenticate();

    /*OkHttpClient authenticate(String username, String password) {
        // Create a new instance of OkHttpClient
        OkHttpClient.Builder builder = new OkHttpClient.Builder();

        // Set the username and password as the basic authentication header
        String credentials = username + ":" + password;
        final String basicAuth = "Basic " + Base64.encodeToString(credentials.getBytes(), Base64.NO_WRAP);
        builder.addInterceptor(new Interceptor() {
            @Override
            public okhttp3.Response intercept(Chain chain) throws IOException {
                Request original = chain.request();
                Request.Builder requestBuilder = original.newBuilder()
                        .header("Authorization", basicAuth)
                        .method(original.method(), original.body());
                Request request = requestBuilder.build();
                return chain.proceed(request);
            }
        });
        return builder.build();
    }*/
/*
    @POST("login")
    Call<LoginRequest> loginUser(@Body LoginRequest loginRequest);*/
}
