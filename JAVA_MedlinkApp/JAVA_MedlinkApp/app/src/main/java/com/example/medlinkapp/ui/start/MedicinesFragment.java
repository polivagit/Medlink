package com.example.medlinkapp.ui.start;

import android.content.Intent;
import android.os.Bundle;
import android.speech.tts.TextToSpeech;
import android.util.Base64;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.MenuItem;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageButton;
import android.widget.TextView;
import android.widget.Toast;

import androidx.annotation.NonNull;
import androidx.fragment.app.Fragment;
import androidx.recyclerview.widget.DividerItemDecoration;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import com.example.medlinkapp.LoginActivity;
import com.example.medlinkapp.R;
import com.example.medlinkapp.adapters.MedicineStartAdapter;
import com.example.medlinkapp.databinding.FragmentMedicinesBinding;
import com.example.medlinkapp.utils.api.ApiService;
import com.example.medlinkapp.utils.medicines.MedicineTreatmentData;
import com.example.medlinkapp.utils.medicines.MedicineTreatmentResponse;
import com.example.medlinkapp.utils.treatment.TreatmentId;

import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.Date;
import java.util.List;
import java.util.Locale;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;
import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;

public class MedicinesFragment extends Fragment {

    private FragmentMedicinesBinding binding;
    TextToSpeech t1;
    TextView txvObservations;
    ImageButton btnSpeak;
    private String treatmentId;
    MedicineStartAdapter adapter;

    RecyclerView rcyStart;

    List<MedicineTreatmentData>mMedicineTreatment;

    private String dateStart,dateEnd,quantityPerDay,medicineName,unitOfMeasure,patientId;

    private static final String WEBSERVICE_URL = "http://169.254.30.133/Medlink/WEB_MedlinkApp/Server/index.php/apix/Request/";

    String authHeader = "Basic " + Base64.encodeToString("pau:admin".getBytes(), Base64.NO_WRAP);


    public View onCreateView(@NonNull LayoutInflater inflater,
                             ViewGroup container, Bundle savedInstanceState) {

        //getTreatmentIdFromWebService(patientId);
        mMedicineTreatment = new ArrayList<>();
        View v = inflater.inflate(R.layout.fragment_medicines,container,false);
        rcyStart = v.findViewById(R.id.rcyStart);
        rcyStart.setLayoutManager(new LinearLayoutManager(requireContext()));
        rcyStart.setHasFixedSize(true);
        DividerItemDecoration dividerItemDecoration = new DividerItemDecoration(rcyStart.getContext(),
                DividerItemDecoration.VERTICAL);
        rcyStart.addItemDecoration(dividerItemDecoration);
        //getMedicineTreatmentFromWebService(treatmentId);
        getTreatmentIdFromWebService(patientId);


        txvObservations = v.findViewById(R.id.txvObservations);
        btnSpeak = v.findViewById(R.id.btnSpeak);

        t1 = new TextToSpeech(getActivity(), new TextToSpeech.OnInitListener() {
            @Override
            public void onInit(int status) {
                if(status != TextToSpeech.ERROR){
                    t1.setLanguage(Locale.ENGLISH);
                }
            }
        });

        btnSpeak.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                String toSpeak = txvObservations.getText().toString();
                Toast.makeText(getActivity(),toSpeak,Toast.LENGTH_SHORT).show();
                t1.speak(toSpeak,TextToSpeech.QUEUE_FLUSH,null);
            }
        });
        return v;
    }

    @Override
    public void onDestroyView() {
        super.onDestroyView();
        binding = null;
    }

    public void onPause(){
        if(t1 !=null){
            t1.stop();
            t1.shutdown();
        }
        super.onPause();
    }
    public void receiveData(String data) {
        patientId = data;
        Log.d("algo", "Datos recibidos: " + data);
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

    public void getTreatmentIdFromWebService(String patientId){
        Retrofit retrofit = new Retrofit.Builder()
                .baseUrl(WEBSERVICE_URL)
                .addConverterFactory(GsonConverterFactory.create())
                .build();

        ApiService apiService = retrofit.create(ApiService.class);

        Call<List<TreatmentId>> call = apiService.treatmentsId(authHeader,patientId);
        call.enqueue(new Callback<List<TreatmentId>>() {
            @Override
            public void onResponse(Call<List<TreatmentId>> call, Response<List<TreatmentId>> response) {
                if (response.isSuccessful()) {
                    List<TreatmentId> treatments = response.body();
                    if (treatments != null) {
                        for(TreatmentId treaId : treatments){
                            treatmentId = treaId.getTreatmentId();
                            Log.e("treatments:","TreatmentId a start : " + treatmentId);
                            getMedicinesFromTreatmentId(treatmentId);

                        }
                    }else {
                        Log.e("treatments:", "La lista esta vacia");
                    }


                } else {
                    Log.e("patata","Result" + response);
                }
            }

            @Override
            public void onFailure(Call<List<TreatmentId>> call, Throwable t) {
                // Handle failure
            }
        });

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
                    Log.e("treatments:","Response del las medicinas: " + response);
                    List<MedicineTreatmentData> medicineTreatmentDataList = medicineTreatmentResponse.getData();
                    if (!medicineTreatmentDataList.isEmpty()) {
                        Log.e("treatments:","ENTRO");
                        Calendar currentDate = Calendar.getInstance();
                        for (MedicineTreatmentData medicineTreatmentData: medicineTreatmentDataList) {
                            medicineName = medicineTreatmentData.getMedi_name();
                            dateStart = medicineTreatmentData.getTrme_date_start();
                            dateEnd = medicineTreatmentData.getTrme_date_end();
                            quantityPerDay = medicineTreatmentData.getTrme_quantity_per_day();
                            unitOfMeasure = medicineTreatmentData.getUnme_abbreviation();
                            Calendar startCalendar = convertToDate(dateStart);
                            Calendar endCalendar = convertToDate(dateEnd);

                            if ((endCalendar != null && endCalendar.after(currentDate)&&(startCalendar!=null && endCalendar.before(currentDate)))) {
                                mMedicineTreatment.add(new MedicineTreatmentData(dateStart, dateEnd, quantityPerDay, medicineName, unitOfMeasure));
                            }
                        }
                        adapter = new MedicineStartAdapter(mMedicineTreatment);
                        rcyStart.setAdapter(adapter);
                    }else {
                        Log.e("treatments:", "La lista esta vacia");
                    }


                } else {
                    Log.e("patata","Result" + response);
                }
            }

            @Override
            public void onFailure(Call<MedicineTreatmentResponse> call, Throwable t) {
                // Handle failure
            }
        });
    }
    private Calendar convertToDate(String dateString) {
        SimpleDateFormat format = new SimpleDateFormat("yyyy-MM-dd", Locale.getDefault());
        try {
            Date date = format.parse(dateString);
            Calendar calendar = Calendar.getInstance();
            calendar.setTime(date);
            return calendar;
        } catch (ParseException e) {
            e.printStackTrace();
        }
        return null;
    }
}