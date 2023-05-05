package com.example.medlinkapp.utils;

import com.example.medlinkapp.model.Person;
import com.google.gson.annotations.SerializedName;

public class LoginResponse {
    @SerializedName("token")
    private String token;

    @SerializedName("user")
    private Person user;

    public String getToken() {
        return token;
    }

    public Person getUser() {
        return user;
    }
}
