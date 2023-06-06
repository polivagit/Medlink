package com.example.medlinkapp.utils.medicines_with_static_json;

import com.example.medlinkapp.utils.medicines.MedicineTreatmentData;
import com.google.gson.annotations.SerializedName;

import java.util.List;

public class MedicineTreatmentResponseStatic {
    @SerializedName("status")
    private String status;

    @SerializedName("message")
    private String message;

    @SerializedName("data")
    private List<MedicineTreatmentDataStatic> data;

    public String getStatus() {
        return status;
    }

    public String getMessage() {
        return message;
    }


    public List<MedicineTreatmentDataStatic> getData() {
        return data;
    }
}
