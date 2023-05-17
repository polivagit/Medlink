package com.example.medlinkapp;

import android.os.Bundle;
import android.util.Log;
import android.view.MenuItem;
import android.view.Menu;

import com.example.medlinkapp.ui.history.HistoryFragment;
import com.example.medlinkapp.utils.treatment.TreatmentData;
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

import com.example.medlinkapp.databinding.ActivityMainBinding;


public class MainActivity extends AppCompatActivity{

    private AppBarConfiguration mAppBarConfiguration;
    private ActivityMainBinding binding;
    private int patientId;
    String aux;

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

        mAppBarConfiguration = new AppBarConfiguration.Builder(
                R.id.nav_start, R.id.nav_history)
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
                    case R.id.nav_start:
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