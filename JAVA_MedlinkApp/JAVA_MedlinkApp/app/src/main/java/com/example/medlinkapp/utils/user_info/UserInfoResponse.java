package com.example.medlinkapp.utils.user_info;

import com.example.medlinkapp.utils.login.LoginData;

import java.util.List;

public class UserInfoResponse {

        private String name;
        private String  status;
        private List<UserInfoData> data;

        public String getName() {
            return name;
        }

        public String getStatus() {
            return status;
        }

        public List<UserInfoData> getData() {
            return data;
        }
    }


