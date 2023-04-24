package com.example.medlinkapp.model;

import java.util.Date;

public abstract class Person {

    //region FIELDS --------------------------------------------------------------------------------
    private int pers_id;
    private String pers_nif;
    private String pers_first_name;
    private String pers_last_name_1;
    private String pers_last_name_2;
    private Date pers_birthdate;
    private String pers_phone_number;
    private String pers_email;
    private GenderType pers_gender;
    private String pers_addrs_street;
    private String pers_addrs_zip_code;
    private String pers_addrs_city;
    private String pers_addrs_province;
    private String pers_addrs_country;
    private String pers_login_username;
    private String pers_login_password;
    //endregion

    //region CONSTRUCTOR ---------------------------------------------------------------------------
    public Person() {
    }

    public Person(int pers_id, String pers_nif, String pers_first_name, String pers_last_name_1,
                  String pers_last_name_2, Date pers_birthdate, String pers_phone_number,
                  String pers_email, GenderType pers_gender, String pers_addrs_street,
                  String pers_addrs_zip_code, String pers_addrs_city, String pers_addrs_province,
                  String pers_addrs_country, String pers_login_username, String pers_login_password) {
        this.pers_id = pers_id;
        this.pers_nif = pers_nif;
        this.pers_first_name = pers_first_name;
        this.pers_last_name_1 = pers_last_name_1;
        this.pers_last_name_2 = pers_last_name_2;
        this.pers_birthdate = pers_birthdate;
        this.pers_phone_number = pers_phone_number;
        this.pers_email = pers_email;
        this.pers_gender = pers_gender;
        this.pers_addrs_street = pers_addrs_street;
        this.pers_addrs_zip_code = pers_addrs_zip_code;
        this.pers_addrs_city = pers_addrs_city;
        this.pers_addrs_province = pers_addrs_province;
        this.pers_addrs_country = pers_addrs_country;
        this.pers_login_username = pers_login_username;
        this.pers_login_password = pers_login_password;
    }

    public Person(int pers_id, String pers_nif, String pers_first_name, String pers_last_name_1,
                  Date pers_birthdate, String pers_phone_number, String pers_email,
                  GenderType pers_gender, String pers_addrs_street, String pers_addrs_zip_code,
                  String pers_addrs_city, String pers_addrs_province, String pers_addrs_country,
                  String pers_login_username, String pers_login_password) {
        this.pers_id = pers_id;
        this.pers_nif = pers_nif;
        this.pers_first_name = pers_first_name;
        this.pers_last_name_1 = pers_last_name_1;
        this.pers_birthdate = pers_birthdate;
        this.pers_phone_number = pers_phone_number;
        this.pers_email = pers_email;
        this.pers_gender = pers_gender;
        this.pers_addrs_street = pers_addrs_street;
        this.pers_addrs_zip_code = pers_addrs_zip_code;
        this.pers_addrs_city = pers_addrs_city;
        this.pers_addrs_province = pers_addrs_province;
        this.pers_addrs_country = pers_addrs_country;
        this.pers_login_username = pers_login_username;
        this.pers_login_password = pers_login_password;
    }
    //endregion

    //region GETTERS & SETTERS ---------------------------------------------------------------------
    public int getPers_id() {
        return pers_id;
    }

    public void setPers_id(int pers_id) {
        this.pers_id = pers_id;
    }

    public String getPers_nif() {
        return pers_nif;
    }

    public void setPers_nif(String pers_nif) {
        this.pers_nif = pers_nif;
    }

    public String getPers_first_name() {
        return pers_first_name;
    }

    public void setPers_first_name(String pers_first_name) {
        this.pers_first_name = pers_first_name;
    }

    public String getPers_last_name_1() {
        return pers_last_name_1;
    }

    public void setPers_last_name_1(String pers_last_name_1) {
        this.pers_last_name_1 = pers_last_name_1;
    }

    public String getPers_last_name_2() {
        return pers_last_name_2;
    }

    public void setPers_last_name_2(String pers_last_name_2) {
        this.pers_last_name_2 = pers_last_name_2;
    }

    public Date getPers_birthdate() {
        return pers_birthdate;
    }

    public void setPers_birthdate(Date pers_birthdate) {
        this.pers_birthdate = pers_birthdate;
    }

    public String getPers_phone_number() {
        return pers_phone_number;
    }

    public void setPers_phone_number(String pers_phone_number) {
        this.pers_phone_number = pers_phone_number;
    }

    public String getPers_email() {
        return pers_email;
    }

    public void setPers_email(String pers_email) {
        this.pers_email = pers_email;
    }

    public GenderType getPers_gender() {
        return pers_gender;
    }

    public void setPers_gender(GenderType pers_gender) {
        this.pers_gender = pers_gender;
    }

    public String getPers_addrs_street() {
        return pers_addrs_street;
    }

    public void setPers_addrs_street(String pers_addrs_street) {
        this.pers_addrs_street = pers_addrs_street;
    }

    public String getPers_addrs_zip_code() {
        return pers_addrs_zip_code;
    }

    public void setPers_addrs_zip_code(String pers_addrs_zip_code) {
        this.pers_addrs_zip_code = pers_addrs_zip_code;
    }

    public String getPers_addrs_city() {
        return pers_addrs_city;
    }

    public void setPers_addrs_city(String pers_addrs_city) {
        this.pers_addrs_city = pers_addrs_city;
    }

    public String getPers_addrs_province() {
        return pers_addrs_province;
    }

    public void setPers_addrs_province(String pers_addrs_province) {
        this.pers_addrs_province = pers_addrs_province;
    }

    public String getPers_addrs_country() {
        return pers_addrs_country;
    }

    public void setPers_addrs_country(String pers_addrs_country) {
        this.pers_addrs_country = pers_addrs_country;
    }

    public String getPers_login_username() {
        return pers_login_username;
    }

    public void setPers_login_username(String pers_login_username) {
        this.pers_login_username = pers_login_username;
    }

    public String getPers_login_password() {
        return pers_login_password;
    }

    public void setPers_login_password(String pers_login_password) {
        this.pers_login_password = pers_login_password;
    }
    //endregion
}
