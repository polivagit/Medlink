package com.example.medlinkapp.utils;

import com.example.medlinkapp.adapters.TreatmentAdapter;
import com.google.gson.annotations.SerializedName;

import java.util.List;

public class TreatmentResponse {


    private String name;

    private List<TreatmentData> data;

    public String getName() {
        return name;
    }


    public List<TreatmentData> getData() {
        return data;
    }
}
