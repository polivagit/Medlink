package com.example.medlinkapp;

import android.content.Intent;
import android.os.Bundle;
import android.util.Base64;
import android.util.Log;
import android.view.MenuItem;
import android.view.Menu;
import android.widget.TextView;

import com.example.medlinkapp.databinding.ActivityMainBinding;
import com.example.medlinkapp.ui.userInfo.UserInfoFragment;
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
    String aux,personName,user,pass;

    DrawerLayout drawer;
    Toolbar toolbar;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

        binding = ActivityMainBinding.inflate(getLayoutInflater());
        setContentView(binding.getRoot());
        Bundle extras = getIntent().getExtras();
        if (extras != null) {
            aux = extras.getString("pati_person_id");
            personName = extras.getString("personaName");
            user = extras.getString("userName");
            pass = extras.getString("pass");
        }
        patientId = Integer.parseInt(aux);

        drawer = binding.drawerLayout;
        toolbar = findViewById(R.id.toolbar);
        setSupportActionBar(toolbar);
        ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(
                this, drawer, toolbar, R.string.navigation_drawer_open, R.string.navigation_drawer_close);
        drawer.addDrawerListener(toggle);
        toggle.syncState();
        NavigationView navigationView = binding.navView;
        navigationView.bringToFront();

        mAppBarConfiguration = new AppBarConfiguration.Builder(
                R.id.nav_home, R.id.nav_medicines, R.id.nav_history,R.id.nav_user_info,R.id.nav_log_out)
                .setOpenableLayout(drawer)
                .build();
        NavController navController = Navigation.findNavController(this, R.id.nav_host_fragment_content_main);
        NavigationUI.setupActionBarWithNavController(this, navController, mAppBarConfiguration);
        navigationView.setNavigationItemSelectedListener(new NavigationView.OnNavigationItemSelectedListener() {
            @Override
            public boolean onNavigationItemSelected(@NonNull MenuItem item) {
                switch (item.getItemId()){
                    case R.id.nav_history:
                        TreatmentData.set_treatmentId(patientId);
                        Navigation.findNavController(MainActivity.this,R.id.nav_host_fragment_content_main).navigate(R.id.action_global_nav_history);
                        break;
                    case R.id.nav_medicines:
                        Bundle args = new Bundle();
                        args.putString("patientId", patientId+"");
                        Navigation.findNavController(MainActivity.this,R.id.nav_host_fragment_content_main).navigate(R.id.action_global_nav_start,args);
                        break;
                    case R.id.nav_home:
                        Navigation.findNavController(MainActivity.this,R.id.nav_host_fragment_content_main).navigate(R.id.action_global_nav_home);
                        break;
                    case R.id.nav_user_info:
                        Bundle args2 = new Bundle();
                        args2.putString("userName",user);
                        args2.putString("pass",pass);
                        Navigation.findNavController(MainActivity.this,R.id.nav_host_fragment_content_main).navigate(R.id.action_global_nav_user_info,args2);
                        break;
                    case R.id.nav_log_out:
                        Intent i = new Intent(MainActivity.this,LoginActivity.class);
                        startActivity(i);
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

}