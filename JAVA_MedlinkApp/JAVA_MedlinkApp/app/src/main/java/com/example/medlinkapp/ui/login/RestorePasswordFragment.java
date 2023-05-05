package com.example.medlinkapp.ui.login;

import android.content.Intent;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;

import androidx.fragment.app.Fragment;

import com.example.medlinkapp.LoginActivity;
import com.example.medlinkapp.R;
import com.example.medlinkapp.databinding.FragmentForgotPasswordBinding;
import com.example.medlinkapp.databinding.FragmentRestorePasswordBinding;

public class RestorePasswordFragment extends Fragment {

    private FragmentRestorePasswordBinding binding;
    Button btnCancelRestorePass;
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
        return v;
    }

    @Override
    public void onDestroyView() {
        super.onDestroyView();
        binding = null;
    }
}
