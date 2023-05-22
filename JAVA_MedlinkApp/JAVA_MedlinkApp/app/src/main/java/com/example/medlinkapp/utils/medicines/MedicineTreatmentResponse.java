package com.example.medlinkapp.utils.medicines;

import com.example.medlinkapp.utils.treatment.TreatmentData;
import com.google.gson.annotations.SerializedName;

import java.util.List;

public class MedicineTreatmentResponse {
    @SerializedName("status")
    private String status;

    @SerializedName("message")
    private String message;

    @SerializedName("data")
    private List<MedicineTreatmentData> data;

    public String getStatus() {
        return status;
    }

    public String getMessage() {
        return message;
    }


    public List<MedicineTreatmentData> getData() {
        return data;
    }
}
