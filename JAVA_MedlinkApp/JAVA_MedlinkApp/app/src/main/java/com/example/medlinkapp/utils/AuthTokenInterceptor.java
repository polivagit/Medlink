package com.example.medlinkapp.utils;

import org.jetbrains.annotations.NotNull;

import java.io.IOException;

import okhttp3.Interceptor;
import okhttp3.Request;
import okhttp3.Response;

public class AuthTokenInterceptor implements Interceptor {
    private String authToken;
    public AuthTokenInterceptor(String token) {
        this.authToken = token;
    }

    @NotNull
    @Override
    public Response intercept(Chain chain) throws IOException {
        Request original = chain.request();
        Request.Builder builder = original.newBuilder()
                .header("Authorization", authToken)
                .method(original.method(), original.body());
        Request request = builder.build();
        return chain.proceed(request);
    }
}
