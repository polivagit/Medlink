package com.example.medlinkapp.ui.userInfo;

import android.content.Intent;
import android.os.Bundle;

import androidx.fragment.app.Fragment;

import android.util.Base64;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Toast;

import com.example.medlinkapp.LoginActivity;
import com.example.medlinkapp.MainActivity;
import com.example.medlinkapp.R;
import com.example.medlinkapp.databinding.FragmentUserInfoBinding;
import com.example.medlinkapp.utils.api.ApiService;
import com.example.medlinkapp.utils.login.LoginData;
import com.example.medlinkapp.utils.login.LoginResponse;
import com.example.medlinkapp.utils.user_info.UserInfoData;
import com.example.medlinkapp.utils.user_info.UserInfoResponse;

import java.util.List;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;
import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;

public class UserInfoFragment extends Fragment {

    private FragmentUserInfoBinding binding;
    String user,pass,nif,firstName,lastName1,lastName2,birthdate,email,phoneNumber,adressStreet,adressTown,adressCity,zipCode,country;
    private static final String WEBSERVICE_URL = "http://169.254.30.133/Medlink/WEB_MedlinkApp/Server/index.php/apix/Request/";

    String authHeader = "Basic " + Base64.encodeToString("pau:admin".getBytes(), Base64.NO_WRAP);

    public UserInfoFragment() {
        // Required empty public constructor
    }


    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        Bundle b2 = getArguments();
        user = b2.getString("userName",null);
        pass = b2.getString("pass",null);
        Log.d("user","user name " + user);
        Log.d("user","user pass " + pass);
        binding = FragmentUserInfoBinding.inflate(inflater, container, false);
        View v = binding.getRoot();
        getPersonInfo(user,pass);

        return v;
    }

    public void getPersonInfo(String username, String password) {

        Retrofit retrofit = new Retrofit.Builder()
                .baseUrl(WEBSERVICE_URL)
                .addConverterFactory(GsonConverterFactory.create())
                .build();

        ApiService apiService = retrofit.create(ApiService.class);

        Call<UserInfoResponse> call = apiService.userInfo(authHeader,username,password);
        call.enqueue(new Callback<UserInfoResponse>() {
            @Override
            public void onResponse(Call<UserInfoResponse> call, Response<UserInfoResponse> response) {
                Log.d("user","response" + response);
                if (response.isSuccessful()) {
                    UserInfoResponse userInfoResponse = response.body();
                    List<UserInfoData> userInfoDataList = userInfoResponse.getData();
                    if (!userInfoDataList.isEmpty()) {
                        for (UserInfoData usid: userInfoDataList) {
                            nif = usid.getPers_nif();
                            firstName = usid.getPers_first_name();
                            lastName1 = usid.getPers_last_name_1();
                            lastName2 = usid.getPers_last_name_2();
                            birthdate = usid.getPers_birthdate();
                            email = usid.getPers_email();
                            phoneNumber = usid.getPers_phone_number();
                            adressStreet = usid.getPers_addrs_street();
                            adressTown = usid.getPers_addrs_city();
                            zipCode = usid.getPers_addrs_zip_code();
                            adressCity = usid.getPers_addrs_province();
                            country = usid.getPers_addrs_country();
                        }

                        binding.txvNifPers.setText(nif);
                        binding.txvUserNamePers.setText(username);
                        binding.txvFullNamePers.setText(firstName + " " + lastName1 + " " + lastName2);
                        binding.txvBirthdatePers.setText(birthdate);
                        binding.txvMailPers.setText(email);
                        binding.txvPhonePers.setText(phoneNumber);
                        binding.txvAdressStreetPers.setText(adressStreet);
                        binding.txvAdressTownPers.setText(adressTown+","+zipCode);
                        binding.txvAdressCityCountryPers.setText(adressCity+","+country);

                    }

                    Log.e("patata","user info" + response);
                } else {
                    // Login failed, handle error
                    Log.e("patata","user info" + response);
                }
            }

            @Override
            public void onFailure(Call<UserInfoResponse> call, Throwable t) {
                // Handle failure
            }
        });
    }
}