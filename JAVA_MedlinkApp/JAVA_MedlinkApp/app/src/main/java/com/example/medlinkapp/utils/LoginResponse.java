package com.example.medlinkapp.utils;

import com.example.medlinkapp.model.Person;
import com.google.gson.annotations.SerializedName;

import java.util.List;

public class LoginResponse {

        private String name;
        private int trobat;
        private List<LoginData> data;

        public String getName() {
            return name;
        }

        public int getTrobat() {
            return trobat;
        }

        public List<LoginData> getData() {
            return data;
        }
    }


