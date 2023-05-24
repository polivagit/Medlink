package com.example.medlinkapp.model;

public class MedicineCategory {

    //region FIELDS --------------------------------------------------------------------------------
    private int meca_id;
    private String meca_name;
    //endregion

    //region CONSTRUCTORS --------------------------------------------------------------------------
    public MedicineCategory(int meca_id, String meca_name) {
        this.meca_id = meca_id;
        this.meca_name = meca_name;
    }
    //endregion

    //region GETTERS & SETTERS ---------------------------------------------------------------------
    public int getMeca_id() {
        return meca_id;
    }

    public void setMeca_id(int meca_id) {
        this.meca_id = meca_id;
    }

    public String getMeca_name() {
        return meca_name;
    }

    public void setMeca_name(String meca_name) {
        this.meca_name = meca_name;
    }
    //endregion
}
