package com.example.medlinkapp.ui.login;

import android.app.AlertDialog;
import android.content.DialogInterface;
import android.content.Intent;
import android.graphics.Color;
import android.os.Bundle;

import androidx.fragment.app.Fragment;

import android.text.Editable;
import android.text.TextWatcher;
import android.util.Base64;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

import com.example.medlinkapp.LoginActivity;
import com.example.medlinkapp.R;
import com.example.medlinkapp.databinding.FragmentChangePasswordBinding;
import com.example.medlinkapp.utils.api.ApiService;
import com.example.medlinkapp.utils.login.ChangePasswordResponse;
import com.example.medlinkapp.utils.login.RestorePasswordResponse;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;
import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;

public class ChangePasswordFragment extends Fragment {

    private FragmentChangePasswordBinding binding;

    private static final String WEBSERVICE_URL = "http://169.254.30.133/Medlink/WEB_MedlinkApp/Server/index.php/apix/Request/";

    String authHeader = "Basic " + Base64.encodeToString("pau:admin".getBytes(), Base64.NO_WRAP);
    Button btnCancelChangePass,btnOkChangePass;
    EditText etNewPass,etRepeatNewPass,etUserName,etOldPass;
    private String userName,newPass,oldPass,repeatNewPass;

    public ChangePasswordFragment() {
        // Required empty public constructor
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        View v = inflater.inflate(R.layout.fragment_change_password,container,false);
        btnCancelChangePass = v.findViewById(R.id.btnCancelChangePass);
        btnOkChangePass = v.findViewById(R.id.btnOkChangePass);
        etNewPass = v.findViewById(R.id.etNewPass);
        etRepeatNewPass = v.findViewById(R.id.etNewPass);
        etUserName = v.findViewById(R.id.etUserNameChangePass);
        etOldPass = v.findViewById(R.id.etOldPass);

        btnCancelChangePass.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent intent = new Intent(getActivity(), LoginActivity.class);
                startActivity(intent);

            }
        });

        btnOkChangePass.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                userName = etUserName.getText().toString();
                oldPass = etOldPass.getText().toString();
                newPass = etNewPass.getText().toString();
                repeatNewPass = etRepeatNewPass.getText().toString();
                /*if (newPass.equals(repeatNewPass)) {
                    // Las contraseñas coinciden
                    etRepeatNewPass.setBackgroundColor(Color.WHITE);
                    changePassword(userName, oldPass, newPass); // Llama al webservice
                } else {
                    // Las contraseñas no coinciden
                    etRepeatNewPass.setBackgroundColor(Color.RED);
                    Toast.makeText(getActivity(), "Passwords do not match", Toast.LENGTH_SHORT).show();
                }*/


            }
        });
        TextWatcher passwordTextWatcher = new TextWatcher() {
            @Override
            public void beforeTextChanged(CharSequence s, int start, int count, int after) {
                // No se requiere implementación
            }

            @Override
            public void onTextChanged(CharSequence s, int start, int before, int count) {
                // No se requiere implementación
            }

            @Override
            public void afterTextChanged(Editable s) {
                userName = etUserName.getText().toString();
                oldPass = etOldPass.getText().toString();
                newPass = etNewPass.getText().toString();
                repeatNewPass = etRepeatNewPass.getText().toString();

                if (newPass.equals(repeatNewPass)) {
                    // Las contraseñas coinciden
                    etRepeatNewPass.setBackgroundColor(Color.WHITE);
                    Log.e("patata","afterTexthanged if ");
                    changePassword(userName, oldPass, newPass); // Llama al webservice
                } else {
                    // Las contraseñas no coinciden
                    etRepeatNewPass.setBackgroundColor(Color.RED);
                    Toast.makeText(getActivity(), "Passwords do not match", Toast.LENGTH_SHORT).show();
                    Log.e("patata","afterTexthanged else ");
                }
            }
        };
        etNewPass.addTextChangedListener(passwordTextWatcher);
        etRepeatNewPass.addTextChangedListener(passwordTextWatcher);

        return v;
    }


    @Override
    public void onDestroyView() {
        super.onDestroyView();
        binding = null;
    }

    public void changePassword (String user,String oldPass,String newPass){

        Retrofit retrofit = new Retrofit.Builder()
                .baseUrl(WEBSERVICE_URL)
                .addConverterFactory(GsonConverterFactory.create())
                .build();

        ApiService apiService = retrofit.create(ApiService.class);

        Call<ChangePasswordResponse> call = apiService.changePassword(authHeader,user,oldPass,newPass);
        call.enqueue(new Callback<ChangePasswordResponse>() {
            @Override
            public void onResponse(Call<ChangePasswordResponse> call, Response<ChangePasswordResponse> response) {
                if (response.isSuccessful()) {
                    ChangePasswordResponse changePass = response.body();
                    if (changePass != null){
                        String status = changePass.getStatus();
                        if (status.equals("Found")){
                            showDialog("Password changed successfully!");
                        }else{
                            showDialog("Could not change the password. Please check if you have entered your username correctly.");
                        }
                    }else{
                        Log.e("patata","La resposta del webservice esta buida" );
                    }

                    Log.e("patata","Result" + response);
                } else {

                    Log.e("patata","Result" + response);
                }
            }

            @Override
            public void onFailure(Call<ChangePasswordResponse> call, Throwable t) {
                // Handle failure

            }
        });
    }

    private void showDialog(String message) {
        AlertDialog.Builder builder = new AlertDialog.Builder(requireContext());
        builder.setMessage(message)
                .setPositiveButton("OK", new DialogInterface.OnClickListener() {
                    public void onClick(DialogInterface dialog, int id) {
                        Intent intent = new Intent(getActivity(), LoginActivity.class);
                        startActivity(intent);
                    }
                });
        builder.create().show();
    }
}