package com.example.medlinkapp.ui.start;

import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.fragment.app.Fragment;
import androidx.lifecycle.ViewModelProvider;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import com.example.medlinkapp.R;
import com.example.medlinkapp.adapters.MedicineStartAdapter;
import com.example.medlinkapp.adapters.TreatmentAdapter;
import com.example.medlinkapp.databinding.FragmentStartBinding;
import com.example.medlinkapp.model.Treatment;
import com.example.medlinkapp.model.TreatmentMedicine;

public class StartFragment extends Fragment {

    private FragmentStartBinding binding;

    public View onCreateView(@NonNull LayoutInflater inflater,
                             ViewGroup container, Bundle savedInstanceState) {
        View v = inflater.inflate(R.layout.fragment_start,container,false);
        RecyclerView rcyStart = v.findViewById(R.id.rcyStart);
        rcyStart.setLayoutManager(new LinearLayoutManager(requireContext()));
        rcyStart.setHasFixedSize(true);

        MedicineStartAdapter adapter = new MedicineStartAdapter(TreatmentMedicine.getTreatmentMedicines());
        rcyStart.setAdapter(adapter);
        /*StartViewModel startViewModel =
                new ViewModelProvider(this).get(StartViewModel.class);

        binding = FragmentStartBinding.inflate(inflater, container, false);
        View root = binding.getRoot();

        final TextView textView = binding.textStart;
        startViewModel.getText().observe(getViewLifecycleOwner(), textView::setText);
        return root;*/
        return v;
    }

    @Override
    public void onDestroyView() {
        super.onDestroyView();
        binding = null;
    }
}