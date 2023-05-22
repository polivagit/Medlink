package com.example.medlinkapp.utils.medicines;

import com.google.gson.annotations.SerializedName;

public class MedicineTreatmentData {

    @SerializedName("trme_date_start")
    private String trme_date_start;
    @SerializedName("trme_date_end")
    private String trme_date_end;
    @SerializedName("trme_quantity_per_day")
    private String trme_quantity_per_day;
    @SerializedName("medi_name")
    private String medi_name;
    @SerializedName("unme_abbreviation")
    private String unme_abbreviation;

    public MedicineTreatmentData(String trme_date_start, String trme_date_end, String trme_quantity_per_day, String medi_name, String unme_abbreviation) {
        this.trme_date_start = trme_date_start;
        this.trme_date_end = trme_date_end;
        this.trme_quantity_per_day = trme_quantity_per_day;
        this.medi_name = medi_name;
        this.unme_abbreviation = unme_abbreviation;
    }

    public String getTrme_date_start() {
        return trme_date_start;
    }

    public String getTrme_date_end() {
        return trme_date_end;
    }

    public String getTrme_quantity_per_day() {
        return trme_quantity_per_day;
    }

    public String getMedi_name() {
        return medi_name;
    }

    public String getUnme_abbreviation() {
        return unme_abbreviation;
    }
}
