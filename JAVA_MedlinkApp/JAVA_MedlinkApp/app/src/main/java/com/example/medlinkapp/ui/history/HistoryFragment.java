package com.example.medlinkapp.ui.history;

import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.core.content.ContextCompat;
import androidx.fragment.app.Fragment;
import androidx.lifecycle.ViewModelProvider;
import androidx.recyclerview.widget.DividerItemDecoration;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import com.example.medlinkapp.R;
import com.example.medlinkapp.adapters.TreatmentAdapter;
import com.example.medlinkapp.databinding.FragmentHistoryBinding;
import com.example.medlinkapp.model.Treatment;

public class HistoryFragment extends Fragment {

    private FragmentHistoryBinding binding;
    public HistoryFragment(){

    }

    public View onCreateView(@NonNull LayoutInflater inflater,
                             ViewGroup container, Bundle savedInstanceState) {
        View v = inflater.inflate(R.layout.fragment_history,container,false);
        RecyclerView rcyHistory = v.findViewById(R.id.rcyHistory);
        rcyHistory.setLayoutManager(new LinearLayoutManager(requireContext()));
        rcyHistory.setHasFixedSize(true);
        DividerItemDecoration dividerItemDecoration = new DividerItemDecoration(rcyHistory.getContext(),
                DividerItemDecoration.VERTICAL);
        rcyHistory.addItemDecoration(dividerItemDecoration);

        TreatmentAdapter adapter = new TreatmentAdapter(Treatment.getTreatments());
        rcyHistory.setAdapter(adapter);
        /*HistoryViewModel historyViewModel =
                new ViewModelProvider(this).get(HistoryViewModel.class);

        binding = FragmentHistoryBinding.inflate(inflater, container, false);
        View root = binding.getRoot();

        final TextView textView = binding.textHistory;
        historyViewModel.getText().observe(getViewLifecycleOwner(), textView::setText);
        return root;*/
        return v;
    }

    @Override
    public void onDestroyView() {
        super.onDestroyView();
        binding = null;
    }
}