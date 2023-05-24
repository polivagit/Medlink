package com.example.medlinkapp.model;

import java.util.ArrayList;
import java.util.Date;
import java.util.List;

public class TreatmentMedicine {

    //region FIELDS --------------------------------------------------------------------------------
    private int trme_treatment_id;
    private int trme_medicine_id;
    private Date trme_date_start;
    private Date trme_date_end;
    private float trme_quantity_per_day;
    private float trme_total_quantity;
    private int trme_units_of_measure_id;

    private static ArrayList<TreatmentMedicine> mTreatmentMedicine;
    //endregion

    //region CONSTRUCTORS --------------------------------------------------------------------------
    public TreatmentMedicine() {
    }

    public TreatmentMedicine(int trme_treatment_id, int trme_medicine_id, Date trme_date_start,
                             Date trme_date_end, float trme_quantity_per_day,
                             float trme_total_quantity, int trme_units_of_measure_id) {
        this.trme_treatment_id = trme_treatment_id;
        this.trme_medicine_id = trme_medicine_id;
        this.trme_date_start = trme_date_start;
        this.trme_date_end = trme_date_end;
        this.trme_quantity_per_day = trme_quantity_per_day;
        this.trme_total_quantity = trme_total_quantity;
        this.trme_units_of_measure_id = trme_units_of_measure_id;
    }
    //endregion

    //region METHODS -------------------------------------------------------------------------------
    public static List<TreatmentMedicine> getTreatmentMedicines(){
        if (mTreatmentMedicine == null){
            mTreatmentMedicine = new ArrayList<TreatmentMedicine>();
            mTreatmentMedicine.add(new TreatmentMedicine(1,200,new Date(2023,04,27),new Date(2023,05,12),3,48,2));
            mTreatmentMedicine.add(new TreatmentMedicine(1,23,new Date(2023,04,27),new Date(2023,05,4),1,7,1));
            mTreatmentMedicine.add(new TreatmentMedicine(2,100,new Date(2023,04,27),new Date(2023,05,4),2,14,3));
        }
        return mTreatmentMedicine;
    }
    //endregion

    //region GETTERS & SETTERS ---------------------------------------------------------------------
    public int getTrme_treatment_id() {
        return trme_treatment_id;
    }

    public void setTrme_treatment_id(int trme_treatment_id) {
        this.trme_treatment_id = trme_treatment_id;
    }

    public int getTrme_medicine_id() {
        return trme_medicine_id;
    }

    public void setTrme_medicine_id(int trme_medicine_id) {
        this.trme_medicine_id = trme_medicine_id;
    }

    public Date getTrme_date_start() {
        return trme_date_start;
    }

    public void setTrme_date_start(Date trme_date_start) {
        this.trme_date_start = trme_date_start;
    }

    public Date getTrme_date_end() {
        return trme_date_end;
    }

    public void setTrme_date_end(Date trme_date_end) {
        this.trme_date_end = trme_date_end;
    }

    public float getTrme_quantity_per_day() {
        return trme_quantity_per_day;
    }

    public void setTrme_quantity_per_day(float trme_quantity_per_day) {
        this.trme_quantity_per_day = trme_quantity_per_day;
    }

    public float getTrme_total_quantity() {
        return trme_total_quantity;
    }

    public void setTrme_total_quantity(float trme_total_quantity) {
        this.trme_total_quantity = trme_total_quantity;
    }

    public int getTrme_units_of_measure_id() {
        return trme_units_of_measure_id;
    }

    public void setTrme_units_of_measure_id(int trme_units_of_measure_id) {
        this.trme_units_of_measure_id = trme_units_of_measure_id;
    }
    //endregion
}
