package com.example.medlinkapp.model;

public class UnitsOfMeasure {

    //region FIELDS --------------------------------------------------------------------------------
    private int unme_id;
    private String unme_name;
    private String unme_abbreviation;
    //endregion

    //region CONSTRUCTORS --------------------------------------------------------------------------
    public UnitsOfMeasure() {
    }

    public UnitsOfMeasure(int unme_id, String unme_name, String unme_abbreviation) {
        this.unme_id = unme_id;
        this.unme_name = unme_name;
        this.unme_abbreviation = unme_abbreviation;
    }
    //endregion

    //region GETTERS & SETTERS ---------------------------------------------------------------------
    public int getUnme_id() {
        return unme_id;
    }

    public void setUnme_id(int unme_id) {
        this.unme_id = unme_id;
    }

    public String getUnme_name() {
        return unme_name;
    }

    public void setUnme_name(String unme_name) {
        this.unme_name = unme_name;
    }

    public String getUnme_abbreviation() {
        return unme_abbreviation;
    }

    public void setUnme_abbreviation(String unme_abbreviation) {
        this.unme_abbreviation = unme_abbreviation;
    }
    //endregion
}
