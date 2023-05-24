package com.example.medlinkapp.utils.treatment;

import com.example.medlinkapp.utils.treatment.TreatmentData;
import com.google.gson.annotations.SerializedName;

import java.util.List;

public class TreatmentResponse {


    @SerializedName("status")
    private String status;

    @SerializedName("message")
    private String message;

    @SerializedName("data")
    private List<TreatmentData> data;

    public String getStatus() {
        return status;
    }

    public String getMessage() {
        return message;
    }


    public List<TreatmentData> getData() {
        return data;
    }
}
