package com.example.medlinkapp.utils.login;

import com.example.medlinkapp.utils.login.LoginData;

import java.util.List;

public class LoginResponse {

        private String name;
        private String  status;
        private List<LoginData> data;

        public String getName() {
            return name;
        }

        public String getStatus() {
            return status;
        }

        public List<LoginData> getData() {
            return data;
        }
    }


