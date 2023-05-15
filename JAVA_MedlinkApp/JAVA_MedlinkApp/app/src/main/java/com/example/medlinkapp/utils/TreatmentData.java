package com.example.medlinkapp.utils;

import com.google.gson.annotations.SerializedName;

public class TreatmentData {

    @SerializedName("trea_id")
    private String trea_id;

    @SerializedName("trea_name")
    private String trea_name;

    @SerializedName("trea_description")
    private String trea_description;

    @SerializedName("trea_date_start")
    private String trea_date_start;

    @SerializedName("trea_date_end")
    private String trea_date_end;

    @SerializedName("trea_observations")
    private String trea_observations;

    @SerializedName("trea_is_active")
    private String trea_is_active;

    @SerializedName("trea_doctor_id")
    private String trea_doctor_id;

    @SerializedName("trea_patient_id")
    private String trea_patient_id;

    public String getTreaId() {
        return trea_id;
    }

    public String getTrea_name() {
        return trea_name;
    }

    public String getTrea_description() {
        return trea_description;
    }

    public String getTrea_date_start() {
        return trea_date_start;
    }

    public String getTrea_date_end() {
        return trea_date_end;
    }

    public String getTrea_observations() {
        return trea_observations;
    }

    public String getTrea_is_active() {
        return trea_is_active;
    }

    public String getTrea_doctor_id() {
        return trea_doctor_id;
    }

    public String getTrea_patient_id() {
        return trea_patient_id;
    }





}
