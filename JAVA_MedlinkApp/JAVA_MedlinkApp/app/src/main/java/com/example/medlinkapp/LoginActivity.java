package com.example.medlinkapp;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.text.TextUtils;
import android.util.Base64;
import android.util.Log;
import android.view.View;
import android.widget.Toast;

import com.example.medlinkapp.databinding.ActivityLoginBinding;
import com.example.medlinkapp.ui.login.ChangePasswordFragment;
import com.example.medlinkapp.ui.login.RestorePasswordFragment;
import com.example.medlinkapp.utils.api.ApiService;
import com.example.medlinkapp.utils.login.LoginData;
import com.example.medlinkapp.utils.login.LoginResponse;

import java.util.List;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;
import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;

public class LoginActivity extends AppCompatActivity {

    private static final String WEBSERVICE_URL = "http://169.254.30.133/Medlink/WEB_MedlinkApp/Server/index.php/apix/Request/";

    String authHeader = "Basic " + Base64.encodeToString("pau:admin".getBytes(), Base64.NO_WRAP);

    ActivityLoginBinding binding;

    private String persName;
    String userName;
    String password;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        binding = ActivityLoginBinding.inflate(getLayoutInflater());
        setContentView(binding.getRoot());

        binding.btnForgotPassword.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                getSupportFragmentManager().beginTransaction().replace(R.id.glChangeLogin, new ChangePasswordFragment()).commit();
                binding.glLogin.setVisibility(View.GONE);
                binding.glChangeLogin.setVisibility(View.VISIBLE);

            }
        });

        binding.btnRestorePassword.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                getSupportFragmentManager().beginTransaction().replace(R.id.glRestoreLogin, new RestorePasswordFragment()).commit();
                binding.glLogin.setVisibility(View.GONE);
                binding.glRestoreLogin.setVisibility(View.VISIBLE);
            }
        });
        binding.btnSignIn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                 userName = binding.etUser.getText().toString();
                password = binding.etPassword.getText().toString();

                if (TextUtils.isEmpty(userName) && TextUtils.isEmpty(password)) {
                    Toast.makeText(LoginActivity.this, "Please enter user name and password.", Toast.LENGTH_SHORT).show();
                }else {
                    login(userName, password);
                }
            }
        });
    }

    public void login(String username, String password) {

        Retrofit retrofit = new Retrofit.Builder()
                .baseUrl(WEBSERVICE_URL)
                .addConverterFactory(GsonConverterFactory.create())
                .build();

        ApiService apiService = retrofit.create(ApiService.class);

        Call<LoginResponse> call = apiService.login(authHeader,username, password);
        call.enqueue(new Callback<LoginResponse>() {
            @Override
            public void onResponse(Call<LoginResponse> call, Response<LoginResponse> response) {
                if (response.isSuccessful()) {
                    String patiPersonId = null;
                    // Login successful, handle response
                    Toast.makeText(LoginActivity.this, "Login successful", Toast.LENGTH_SHORT).show();
                    LoginResponse loginResponse = response.body();
                    List<LoginData> loginDataList = loginResponse.getData();
                    if (!loginDataList.isEmpty()) {
                        for (LoginData loginData: loginDataList) {
                            patiPersonId = loginData.getPatiPersonId();
                            persName = loginData.getPers_first_name();
                        }
                        Intent i = new Intent(LoginActivity.this,MainActivity.class);
                        i.putExtra("pati_person_id",patiPersonId);
                        i.putExtra("personaName",persName);
                        i.putExtra("userName",userName);
                        i.putExtra("pass",password);
                        startActivity(i);
                    }
                } else {
                    // Login failed, handle error
                    Toast.makeText(LoginActivity.this, "User or password wrong. Please try again",
                            Toast.LENGTH_LONG).show();
                    Log.e("patata","Result" + response);
                }
            }

            @Override
            public void onFailure(Call<LoginResponse> call, Throwable t) {
                // Handle failure
                Toast.makeText(LoginActivity.this, "Network error", Toast.LENGTH_SHORT).show();
            }
        });
    }
}
