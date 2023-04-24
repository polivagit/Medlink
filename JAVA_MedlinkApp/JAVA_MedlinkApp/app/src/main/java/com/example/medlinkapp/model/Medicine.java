package com.example.medlinkapp.model;

public class Medicine {

    //region FIELDS --------------------------------------------------------------------------------
    private int medi_id;
    private String medi_name;
    private int medi_category_id;
    //endregion

    //region CONSTRUCTORS --------------------------------------------------------------------------
    public Medicine() {
    }

    public Medicine(int medi_id, String medi_name, int medi_category_id) {
        this.medi_id = medi_id;
        this.medi_name = medi_name;
        this.medi_category_id = medi_category_id;
    }
    //endregion

    //region GETTERS & SETTERS ---------------------------------------------------------------------
    public int getMedi_id() {
        return medi_id;
    }

    public void setMedi_id(int medi_id) {
        this.medi_id = medi_id;
    }

    public String getMedi_name() {
        return medi_name;
    }

    public void setMedi_name(String medi_name) {
        this.medi_name = medi_name;
    }

    public int getMedi_category_id() {
        return medi_category_id;
    }

    public void setMedi_category_id(int medi_category_id) {
        this.medi_category_id = medi_category_id;
    }
    //endregion
}
