package com.example.medlinkapp.utils.medicines_with_static_json;

import com.google.gson.annotations.SerializedName;

public class MedicineTreatmentDataStatic {

    @SerializedName("medi_intake_time")
    private String medi_intake_time;


    public String getMedi_intake_time() {
        return medi_intake_time;
    }
}
