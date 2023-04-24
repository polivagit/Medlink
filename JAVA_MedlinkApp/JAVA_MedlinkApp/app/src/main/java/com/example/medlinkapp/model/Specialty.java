package com.example.medlinkapp.model;

public class Specialty {

    //region FIELDS --------------------------------------------------------------------------------
    private int spec_id;
    private String spec_name;
    //endregion

    //region CONSTRUCTORS --------------------------------------------------------------------------
    public Specialty(){
    }

    public Specialty(int spec_id, String spec_name) {
        this.spec_id = spec_id;
        this.spec_name = spec_name;
    }
    //endregion

    //region GETTERS & SETTERS ---------------------------------------------------------------------
    public int getSpec_id() {
        return spec_id;
    }

    public void setSpec_id(int spec_id) {
        this.spec_id = spec_id;
    }

    public String getSpec_name() {
        return spec_name;
    }

    public void setSpec_name(String spec_name) {
        this.spec_name = spec_name;
    }
    //endregion
}
