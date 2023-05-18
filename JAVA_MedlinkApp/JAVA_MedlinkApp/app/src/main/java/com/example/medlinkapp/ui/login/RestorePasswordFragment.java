package com.example.medlinkapp.ui.login;

import android.app.AlertDialog;
import android.content.DialogInterface;
import android.content.Intent;
import android.os.Bundle;
import android.util.Base64;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

import androidx.fragment.app.Fragment;

import com.example.medlinkapp.LoginActivity;
import com.example.medlinkapp.MainActivity;
import com.example.medlinkapp.R;
import com.example.medlinkapp.databinding.FragmentRestorePasswordBinding;
import com.example.medlinkapp.utils.api.ApiService;
import com.example.medlinkapp.utils.login.LoginData;
import com.example.medlinkapp.utils.login.LoginResponse;
import com.example.medlinkapp.utils.login.RestorePasswordResponse;

import java.util.List;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;
import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;

public class RestorePasswordFragment extends Fragment {

    private FragmentRestorePasswordBinding binding;
    Button btnCancelRestorePass,btnOkRestorePass;
    String eMail;
    EditText restorePassMail;

    private static final String WEBSERVICE_URL = "http://169.254.30.133/Medlink/WEB_MedlinkApp/Server/index.php/apix/Request/";

    String authHeader = "Basic " + Base64.encodeToString("pau:admin".getBytes(), Base64.NO_WRAP);
    public RestorePasswordFragment() {
        // Required empty public constructor
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        View v = inflater.inflate(R.layout.fragment_restore_password,container,false);
        btnCancelRestorePass = v.findViewById(R.id.btnCancelRestorePass);
        btnCancelRestorePass.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent intent = new Intent(getActivity(), LoginActivity.class);
                startActivity(intent);

            }
        });

        btnOkRestorePass = v.findViewById(R.id.btnOkRestorePass);
        btnOkRestorePass.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                restorePassMail = v.findViewById(R.id.etMailRestorePass);
                eMail = restorePassMail.getText().toString();
                restorePassword(eMail);
            }
        });
        return v;
    }

    @Override
    public void onDestroyView() {
        super.onDestroyView();
        binding = null;
    }

    public void restorePassword (String email){

        Retrofit retrofit = new Retrofit.Builder()
                .baseUrl(WEBSERVICE_URL)
                .addConverterFactory(GsonConverterFactory.create())
                .build();

        ApiService apiService = retrofit.create(ApiService.class);

        Call<RestorePasswordResponse> call = apiService.restorePassword(authHeader,email);
        call.enqueue(new Callback<RestorePasswordResponse>() {
            @Override
            public void onResponse(Call<RestorePasswordResponse> call, Response<RestorePasswordResponse> response) {
                if (response.isSuccessful()) {
                    RestorePasswordResponse restorePass = response.body();
                    if (restorePass != null){
                        String status = restorePass.getStatus();
                        if (status.equals("Found")){
                            showDialog("Password restored successfully!");
                        }else{
                            showDialog("Could not restore the password. Please check if you have entered your email correctly.");
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
            public void onFailure(Call<RestorePasswordResponse> call, Throwable t) {
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
