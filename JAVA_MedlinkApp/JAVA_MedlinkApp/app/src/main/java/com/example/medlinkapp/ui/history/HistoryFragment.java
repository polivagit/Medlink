package com.example.medlinkapp.ui.history;

import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import android.text.Editable;
import android.text.TextWatcher;
import android.util.Base64;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.MenuItem;
import android.view.View;
import android.view.ViewGroup;
import android.widget.EditText;

import androidx.annotation.NonNull;
import androidx.fragment.app.Fragment;
import androidx.recyclerview.widget.DividerItemDecoration;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import com.example.medlinkapp.LoginActivity;
import com.example.medlinkapp.R;
import com.example.medlinkapp.adapters.TreatmentAdapter;
import com.example.medlinkapp.databinding.FragmentHistoryBinding;
import com.example.medlinkapp.model.Treatment;
import com.example.medlinkapp.utils.api.ApiService;
import com.example.medlinkapp.utils.treatment.TreatmentData;
import com.example.medlinkapp.utils.treatment.TreatmentResponse;

import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Date;
import java.util.GregorianCalendar;
import java.util.List;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;
import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;

public class HistoryFragment extends Fragment {

    private FragmentHistoryBinding binding;
    EditText etSearchTreatment;
    List<Treatment> mTreatments;
    TreatmentAdapter adapter;

    Context context = getActivity();

    private static final String WEBSERVICE_URL = "http://169.254.30.133/Medlink/WEB_MedlinkApp/Server/index.php/apix/Request/";

    String authHeader = "Basic " + Base64.encodeToString("pau:admin".getBytes(), Base64.NO_WRAP);

    private String treaId;
    private String treaName;
    private String treaDescription;
    private String treaDateStart;
    private String treaDateEnd;
    private String treaObservations;
    private String treaIsActive;
    private String treaDoctorId;
    private String treaPatientId,patientIdString;

    private int treaIdInt,patientIdInt,doctorId,patientId;
    private GregorianCalendar dateStart, dateEnd;
    private boolean isActive;

    RecyclerView rcyHistory;
    public HistoryFragment(){

    }

    public View onCreateView(@NonNull LayoutInflater inflater,
                             ViewGroup container, Bundle savedInstanceState) {
        View v = inflater.inflate(R.layout.fragment_history,container,false);
        mTreatments = new ArrayList<>();
        patientId = TreatmentData.get_patientId();
        Log.e("patata","id " + patientId);


        patientIdString = String.valueOf(patientId);

        rcyHistory = v.findViewById(R.id.rcyHistory);
        rcyHistory.setLayoutManager(new LinearLayoutManager(requireContext()));
        rcyHistory.setHasFixedSize(true);
        DividerItemDecoration dividerItemDecoration = new DividerItemDecoration(rcyHistory.getContext(),
                DividerItemDecoration.VERTICAL);
        rcyHistory.addItemDecoration(dividerItemDecoration);

        getTreatmentsFromWebService(patientIdString);


        etSearchTreatment = v.findViewById(R.id.etSearchTreatment);
        etSearchTreatment.addTextChangedListener(new TextWatcher() {
            @Override
            public void beforeTextChanged(CharSequence s, int start, int count, int after) {

            }

            @Override
            public void onTextChanged(CharSequence s, int start, int before, int count) {

            }

            @Override
            public void afterTextChanged(Editable s) {
                filter(s.toString());
            }
        });
        return v;
    }

    @Override
    public void onDestroyView() {
        super.onDestroyView();
        binding = null;
    }

    private void filter (String text){
        ArrayList<Treatment>filteredTreatments = new ArrayList<>();
        for (Treatment t: mTreatments){
            if(t.getTrea_name().toLowerCase().contains(text.toLowerCase())) {
                filteredTreatments.add(t);
            }
        }
        adapter.filterList(filteredTreatments);
    }

    public void getTreatmentsFromWebService(String patientId){
        Retrofit retrofit = new Retrofit.Builder()
                .baseUrl(WEBSERVICE_URL)
                .addConverterFactory(GsonConverterFactory.create())
                .build();

        ApiService apiService = retrofit.create(ApiService.class);

        Call<TreatmentResponse> call = apiService.treatments(authHeader,patientId);
        call.enqueue(new Callback<TreatmentResponse>() {
            @Override
            public void onResponse(Call<TreatmentResponse> call, Response<TreatmentResponse> response) {
                if (response.isSuccessful()) {
                    TreatmentResponse treatmentResponse = response.body();
                    Log.e("treatments:","Response: " + response);
                    Log.e("treatments:","treatment response" + treatmentResponse.getData());
                    List<TreatmentData> treatmentDataList = treatmentResponse.getData();
                    if (!treatmentDataList.isEmpty()) {
                        Log.e("treatments:","ENTRO");
                        for (TreatmentData treatmentData: treatmentDataList) {
                            treaId = treatmentData.getTreaId();
                            treaName = treatmentData.getTrea_name();
                            treaDescription = treatmentData.getTrea_description();
                            treaDateStart = treatmentData.getTrea_date_start();
                            treaDateEnd = treatmentData.getTrea_date_end();
                            treaObservations = treatmentData.getTrea_observations();
                            treaIsActive = treatmentData.getTrea_is_active();
                            treaDoctorId = treatmentData.getTrea_doctor_id();
                            treaPatientId = treatmentData.getTrea_patient_id();

                            if (treaIsActive.equals("0")){
                                isActive = false;
                            }else{
                                isActive = true;
                            }

                            treaIdInt = Integer.parseInt(treaId);
                            dateStart = convertStringToGregorianCalendar(treaDateStart);
                            dateEnd = convertStringToGregorianCalendar(treaDateEnd);
                            doctorId = Integer.parseInt(treaDoctorId);
                            patientIdInt = Integer.parseInt(treaPatientId);


                            mTreatments.add(new Treatment(treaIdInt,treaName,treaDescription,dateStart,dateEnd,treaObservations,isActive,doctorId,patientIdInt));
                        }
                        adapter = new TreatmentAdapter(mTreatments);
                        rcyHistory.setAdapter(adapter);
                    }else {
                        Log.e("treatments:", "La lista esta vacia");
                    }


                } else {
                    Log.e("patata","Result" + response);
                }
            }

            @Override
            public void onFailure(Call<TreatmentResponse> call, Throwable t) {
                // Handle failure
            }
        });

    }

    private GregorianCalendar convertStringToGregorianCalendar(String dateString){
        SimpleDateFormat sdf = new SimpleDateFormat("yyy-MM-dd");
        GregorianCalendar calendar = new GregorianCalendar();
        try {
            Date date = sdf.parse(dateString);
            calendar.setTime(date);
        } catch (ParseException e) {
            e.printStackTrace();
        }
        return calendar;

    }


    @Override
    public boolean onOptionsItemSelected(@NonNull MenuItem item) {
        int id = item.getItemId();

        if (id == R.id.action_logOut) {
            Intent intent = new Intent(getActivity(), LoginActivity.class);
            startActivity(intent);
            return true;
        }

        return super.onOptionsItemSelected(item);
    }
}