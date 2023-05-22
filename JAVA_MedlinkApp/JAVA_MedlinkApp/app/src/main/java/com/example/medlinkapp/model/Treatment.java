package com.example.medlinkapp.model;

import android.icu.text.DateFormat;
import android.os.Build;

import androidx.annotation.RequiresApi;

import java.text.SimpleDateFormat;
import java.time.LocalDate;
import java.util.ArrayList;
import java.util.Date;
import java.util.GregorianCalendar;
import java.util.List;

public class Treatment {

    //region FIELDS --------------------------------------------------------------------------------
    private int trea_id;
    private String trea_name;
    private String trea_description;
    private GregorianCalendar trea_date_start;
    private GregorianCalendar trea_date_end;
    private String trea_observations;
    private boolean trea_is_active;
    private int trea_doctor_id;
    private int trea_patient_id;

    //endregion

    //region CONSTRUCTORS --------------------------------------------------------------------------
    public Treatment() {
    }

    public Treatment(int trea_id, String trea_name, String trea_description, GregorianCalendar trea_date_start,
                     GregorianCalendar trea_date_end, String trea_observations, boolean trea_is_active,
                     int trea_doctor_id, int trea_patient_id) {
        this.trea_id = trea_id;
        this.trea_name = trea_name;
        this.trea_description = trea_description;
        this.trea_date_start = trea_date_start;
        this.trea_date_end = trea_date_end;
        this.trea_observations = trea_observations;
        this.trea_is_active = trea_is_active;
        this.trea_doctor_id = trea_doctor_id;
        this.trea_patient_id = trea_patient_id;
    }

    public Treatment(int trea_id, String trea_name, String trea_description, GregorianCalendar trea_date_start,
                     GregorianCalendar trea_date_end, boolean trea_is_active, int trea_doctor_id,
                     int trea_patient_id) {
        this.trea_id = trea_id;
        this.trea_name = trea_name;
        this.trea_description = trea_description;
        this.trea_date_start = trea_date_start;
        this.trea_date_end = trea_date_end;
        this.trea_is_active = trea_is_active;
        this.trea_doctor_id = trea_doctor_id;
        this.trea_patient_id = trea_patient_id;
    }
    //endregion

    //region GETTERS & SETTERS ---------------------------------------------------------------------
    public int getTrea_id() {
        return trea_id;
    }

    public void setTrea_id(int trea_id) {
        this.trea_id = trea_id;
    }

    public String getTrea_name() {
        return trea_name;
    }

    public void setTrea_name(String trea_name) {
        this.trea_name = trea_name;
    }

    public String getTrea_description() {
        return trea_description;
    }

    public void setTrea_description(String trea_description) {
        this.trea_description = trea_description;
    }

    public GregorianCalendar getTrea_date_start() {
        return trea_date_start;
    }

    public void setTrea_date_start(GregorianCalendar trea_date_start) {
        this.trea_date_start = trea_date_start;
    }

    public GregorianCalendar getTrea_date_end() {
        return trea_date_end;
    }

    public void setTrea_date_end(GregorianCalendar trea_date_end) {
        this.trea_date_end = trea_date_end;
    }

    public String getTrea_observations() {
        return trea_observations;
    }

    public void setTrea_observations(String trea_observations) {
        this.trea_observations = trea_observations;
    }

    public boolean isTrea_is_active() {
        return trea_is_active;
    }

    public void setTrea_is_active(boolean trea_is_active) {
        this.trea_is_active = trea_is_active;
    }

    public int getTrea_doctor_id() {
        return trea_doctor_id;
    }

    public void setTrea_doctor_id(int trea_doctor_id) {
        this.trea_doctor_id = trea_doctor_id;
    }

    public int getTrea_patient_id() {
        return trea_patient_id;
    }

    public void setTrea_patient_id(int trea_patient_id) {
        this.trea_patient_id = trea_patient_id;
    }


    //endregion
}
