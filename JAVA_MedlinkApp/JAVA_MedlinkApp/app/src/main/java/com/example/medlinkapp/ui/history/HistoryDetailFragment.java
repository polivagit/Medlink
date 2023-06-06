package com.example.medlinkapp.ui.history;

import android.app.AlertDialog;
import android.content.DialogInterface;
import android.os.Bundle;

import androidx.fragment.app.Fragment;
import androidx.recyclerview.widget.DividerItemDecoration;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import android.util.Base64;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

import com.example.medlinkapp.R;
import com.example.medlinkapp.adapters.MedicineStartAdapter;
import com.example.medlinkapp.databinding.FragmentMedicinesBinding;
import com.example.medlinkapp.utils.api.ApiService;
import com.example.medlinkapp.utils.medicines.MedicineTreatmentData;
import com.example.medlinkapp.utils.medicines.MedicineTreatmentResponse;

import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;
import java.util.Locale;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;
import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;

public class HistoryDetailFragment extends Fragment  {

    private FragmentMedicinesBinding binding;
    private String treatmentId,treaName,treaDateStart,treaDateEnd,treaDesc;
    MedicineStartAdapter adapter;

    RecyclerView rcyMedicines;
    List<String> mTreatmentsIds;
    TextView txvSpeciality,txvTreaName,txvTreaDateStart,txvTreaDateEnd,txvTreaDesc;

    List<MedicineTreatmentData>mMedicineTreatment;

    private String dateStart,dateEnd,quantityPerDay,medicineName,unitOfMeasure,specialization;

    private static final String WEBSERVICE_URL = "http://169.254.30.133/Medlink/WEB_MedlinkApp/Server/index.php/apix/Request/";

    String authHeader = "Basic " + Base64.encodeToString("pau:admin".getBytes(), Base64.NO_WRAP);
    public HistoryDetailFragment() {
        // Required empty public constructor
    }


    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        Bundle b = getArguments();
        treatmentId = b.getString("treaId","");
        treaName = b.getString("treaName","");
        treaDateStart = b.getString("treaDateStart","");
        treaDateEnd = b.getString("treaDateEnd","");
        treaDesc = b.getString("treaDesc","");
        Log.e("patata","Treatment id a detall: " + treatmentId);
        mMedicineTreatment = new ArrayList<>();
        mTreatmentsIds = new ArrayList<>();
        View v = inflater.inflate(R.layout.fragment_history_detail, container, false);
        rcyMedicines = v.findViewById(R.id.rcyMedicines);
        txvSpeciality = v.findViewById(R.id.txvTreatDetailSpeciality);
        txvTreaName = v.findViewById(R.id.txvTreatNameDetail);
        txvTreaDateStart = v.findViewById(R.id.txvTreatDetailDateStart);
        txvTreaDateEnd = v.findViewById(R.id.txvTreatDetailDateEnd);
        txvTreaDesc = v.findViewById(R.id.txvTreatDetailDesc);
        txvTreaName.setText(treaName);
        txvTreaDateStart.setText(treaDateStart);
        txvTreaDateEnd.setText(treaDateEnd);
        txvTreaDesc.setText(treaDesc);
        getMedicinesFromTreatmentId(treatmentId);
        rcyMedicines.setLayoutManager(new LinearLayoutManager(requireContext()));
        rcyMedicines.setHasFixedSize(true);
        DividerItemDecoration dividerItemDecoration = new DividerItemDecoration(rcyMedicines.getContext(),
                DividerItemDecoration.VERTICAL);
        rcyMedicines.addItemDecoration(dividerItemDecoration);
        return v;
    }

    public void getMedicinesFromTreatmentId(String treatmentId){
        Retrofit retrofit = new Retrofit.Builder()
                .baseUrl(WEBSERVICE_URL)
                .addConverterFactory(GsonConverterFactory.create())
                .build();
        ApiService apiService = retrofit.create(ApiService.class);

        Call<MedicineTreatmentResponse> call2 = apiService.medicines(authHeader,treatmentId);
        call2.enqueue(new Callback<MedicineTreatmentResponse>() {
            @Override
            public void onResponse(Call<MedicineTreatmentResponse> call, Response<MedicineTreatmentResponse> response) {
                if (response.isSuccessful()) {
                    MedicineTreatmentResponse medicineTreatmentResponse = response.body();
                    List<MedicineTreatmentData> medicineTreatmentDataList = medicineTreatmentResponse.getData();
                    if (!medicineTreatmentDataList.isEmpty()) {
                        for (MedicineTreatmentData medicineTreatmentData: medicineTreatmentDataList) {
                            medicineName = medicineTreatmentData.getMedi_name();
                            dateStart = medicineTreatmentData.getTrme_date_start();
                            dateEnd = medicineTreatmentData.getTrme_date_end();
                            quantityPerDay = medicineTreatmentData.getTrme_quantity_per_day();
                            unitOfMeasure = medicineTreatmentData.getUnme_abbreviation();
                            specialization = medicineTreatmentData.getMeca_name();
                            txvSpeciality.setText(specialization);

                            mMedicineTreatment.add(new MedicineTreatmentData(dateStart, dateEnd, quantityPerDay, medicineName, unitOfMeasure));
                            Log.e("treatments:", "Llista medicine treatment: " + mMedicineTreatment);

                        }
                        adapter = new MedicineStartAdapter(mMedicineTreatment);
                        rcyMedicines.setAdapter(adapter);
                    }else {

                    }


                } else {
                    showDialog("Impossible to connect to the webservice. Please try again.");
                }
            }

            @Override
            public void onFailure(Call<MedicineTreatmentResponse> call, Throwable t) {
                // Handle failure
            }
        });
    }

    private void showDialog(String message) {
        AlertDialog.Builder builder = new AlertDialog.Builder(requireContext());
        builder.setMessage(message)
                .setPositiveButton("OK", new DialogInterface.OnClickListener() {
                    public void onClick(DialogInterface dialog, int id) {
                        dialog.dismiss();
                    }
                });
        builder.create().show();
    }

}