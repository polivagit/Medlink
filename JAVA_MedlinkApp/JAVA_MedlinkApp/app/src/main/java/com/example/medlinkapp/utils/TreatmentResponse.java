package com.example.medlinkapp.utils;

import com.example.medlinkapp.adapters.TreatmentAdapter;
import com.google.gson.annotations.SerializedName;

import java.util.List;

public class TreatmentResponse {


    @SerializedName("name")
    private String name;

    @SerializedName("data")
    private List<TreatmentData> data;

    public String getName() {
        return name;
    }


    public List<TreatmentData> getData() {
        return data;
    }
}
