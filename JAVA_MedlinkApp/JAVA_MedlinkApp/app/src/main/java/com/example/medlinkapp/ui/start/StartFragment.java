package com.example.medlinkapp.ui.start;

import android.content.Intent;
import android.os.Bundle;
import android.speech.tts.TextToSpeech;
import android.view.LayoutInflater;
import android.view.MenuItem;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageButton;
import android.widget.TextView;
import android.widget.Toast;

import androidx.annotation.NonNull;
import androidx.fragment.app.Fragment;
import androidx.lifecycle.ViewModelProvider;
import androidx.recyclerview.widget.DividerItemDecoration;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import com.example.medlinkapp.LoginActivity;
import com.example.medlinkapp.R;
import com.example.medlinkapp.adapters.MedicineStartAdapter;
import com.example.medlinkapp.adapters.TreatmentAdapter;
import com.example.medlinkapp.databinding.FragmentStartBinding;
import com.example.medlinkapp.model.Treatment;
import com.example.medlinkapp.model.TreatmentMedicine;

import java.util.Locale;

public class StartFragment extends Fragment {

    private FragmentStartBinding binding;
    TextToSpeech t1;
    TextView txvObservations;
    ImageButton btnSpeak;

    public View onCreateView(@NonNull LayoutInflater inflater,
                             ViewGroup container, Bundle savedInstanceState) {
        View v = inflater.inflate(R.layout.fragment_start,container,false);
        RecyclerView rcyStart = v.findViewById(R.id.rcyStart);
        rcyStart.setLayoutManager(new LinearLayoutManager(requireContext()));
        rcyStart.setHasFixedSize(true);
        DividerItemDecoration dividerItemDecoration = new DividerItemDecoration(rcyStart.getContext(),
                DividerItemDecoration.VERTICAL);
        rcyStart.addItemDecoration(dividerItemDecoration);

        MedicineStartAdapter adapter = new MedicineStartAdapter(TreatmentMedicine.getTreatmentMedicines());
        rcyStart.setAdapter(adapter);

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