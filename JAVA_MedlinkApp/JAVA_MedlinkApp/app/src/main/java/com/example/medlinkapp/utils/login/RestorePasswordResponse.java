package com.example.medlinkapp.utils.login;

import com.google.gson.annotations.SerializedName;

public class RestorePasswordResponse {

    @SerializedName("message")
    private String message;
    @SerializedName("status")
    private String status;

    public String getMessage() {
        return message;
    }

    public String getStatus() {
        return status;
    }


}