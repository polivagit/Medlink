package com.example.medlinkapp;

import android.os.Bundle;
import android.util.Base64;
import android.util.Log;
import android.view.MenuItem;
import android.view.Menu;

import com.example.medlinkapp.databinding.ActivityMainBinding;
import com.example.medlinkapp.ui.history.HistoryFragment;
import com.example.medlinkapp.ui.home.HomeFragment;
import com.example.medlinkapp.ui.start.MedicinesFragment;
import com.example.medlinkapp.utils.api.ApiService;
import com.example.medlinkapp.utils.treatment.TreatmentData;
import com.example.medlinkapp.utils.treatment.TreatmentResponse;
import com.google.android.material.navigation.NavigationView;

import androidx.annotation.NonNull;
import androidx.appcompat.app.ActionBarDrawerToggle;
import androidx.appcompat.widget.Toolbar;
import androidx.core.view.GravityCompat;
import androidx.navigation.NavController;
import androidx.navigation.Navigation;
import androidx.navigation.ui.AppBarConfiguration;
import androidx.navigation.ui.NavigationUI;
import androidx.drawerlayout.widget.DrawerLayout;
import androidx.appcompat.app.AppCompatActivity;

import java.util.List;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;
import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;


public class MainActivity extends AppCompatActivity{

    private AppBarConfiguration mAppBarConfiguration;
    private ActivityMainBinding binding;
    private int patientId;
    String aux,treaId,personName;

    DrawerLayout drawer;
    Toolbar toolbar;

    private static final String WEBSERVICE_URL = "http://169.254.30.133/Medlink/WEB_MedlinkApp/Server/index.php/apix/Request/";

    String authHeader = "Basic " + Base64.encodeToString("pau:admin".getBytes(), Base64.NO_WRAP);

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

        binding = ActivityMainBinding.inflate(getLayoutInflater());
        setContentView(binding.getRoot());
        Bundle extras = getIntent().getExtras();
        if (extras != null) {
            aux = extras.getString("pati_person_id");
            personName = extras.getString("personaName");
        }
        patientId = Integer.parseInt(aux);
        Log.e("patata","Patient id: " + patientId);


        setSupportActionBar(binding.toolbar);

        drawer = binding.drawerLayout;
        toolbar = findViewById(R.id.toolbar);
        setSupportActionBar(toolbar);
        ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(
                this, drawer, toolbar, R.string.navigation_drawer_open, R.string.navigation_drawer_close);
        drawer.addDrawerListener(toggle);
        toggle.syncState();
        NavigationView navigationView = binding.navView;
        navigationView.bringToFront();

        getTreatmentIdFromWebService(aux);
        Log.e("patata","Treatment id: " + aux);

        mAppBarConfiguration = new AppBarConfiguration.Builder(
                R.id.nav_home, R.id.nav_medicines, R.id.nav_history)
                .setOpenableLayout(drawer)
                .build();
        NavController navController = Navigation.findNavController(this, R.id.nav_host_fragment_content_main);
        NavigationUI.setupActionBarWithNavController(this, navController, mAppBarConfiguration);
        navigationView.setNavigationItemSelectedListener(new NavigationView.OnNavigationItemSelectedListener() {
            @Override
            public boolean onNavigationItemSelected(@NonNull MenuItem item) {
                Log.e("patata","patiend id main activity" + patientId);
                switch (item.getItemId()){

                    case R.id.nav_history:
                        HistoryFragment fragment = new HistoryFragment();
                        TreatmentData.set_treatmentId(patientId);
                        Log.e("patata","patiend id main activity" + patientId);

                        Navigation.findNavController(MainActivity.this,R.id.nav_host_fragment_content_main).navigate(R.id.action_global_nav_history);
                        break;
                    case R.id.nav_medicines:
                        MedicinesFragment fragment2 = new MedicinesFragment();
                        TreatmentData.set_treatmentId(patientId);
                        getSupportFragmentManager().beginTransaction()
                                .add(R.id.llStart,fragment2)
                                .commit();

                        fragment2.receiveData(aux);
                        Navigation.findNavController(MainActivity.this,R.id.nav_host_fragment_content_main).navigate(R.id.action_global_nav_start);
                        break;
                    case R.id.nav_home:
                        HomeFragment fragment3 = new HomeFragment();
                        Navigation.findNavController(MainActivity.this,R.id.nav_host_fragment_content_main).navigate(R.id.action_global_nav_home);
                        break;
                }
                drawer.closeDrawer(GravityCompat.START);
                return true;
            }
        });
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        // Inflate the menu; this adds items to the action bar if it is present.
        getMenuInflater().inflate(R.menu.main, menu);
        return true;
    }

    @Override
    public boolean onSupportNavigateUp() {
        NavController navController = Navigation.findNavController(this, R.id.nav_host_fragment_content_main);
        return NavigationUI.navigateUp(navController, mAppBarConfiguration)
                || super.onSupportNavigateUp();
    }

    public void getTreatmentIdFromWebService(String patientId){
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
                    List<TreatmentData> treatmentDataList = treatmentResponse.getData();
                    if (!treatmentDataList.isEmpty()) {
                        for (TreatmentData treatmentData: treatmentDataList) {
                            treaId = treatmentData.getTreaId();
                        }
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

}