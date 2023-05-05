package com.example.medlinkapp.utils;

import com.google.gson.annotations.SerializedName;

public class LoginRequest {
    @SerializedName("user")
    private String username;

    @SerializedName("pass")
    private String password;

    public LoginRequest(String username, String password) {
        this.username = username;
        this.password = password;
    }
}
