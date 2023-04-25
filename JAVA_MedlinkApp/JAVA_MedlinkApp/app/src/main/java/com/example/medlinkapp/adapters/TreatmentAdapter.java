package com.example.medlinkapp.adapters;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.recyclerview.widget.RecyclerView;

import com.example.medlinkapp.R;
import com.example.medlinkapp.model.Treatment;

import org.w3c.dom.Text;

import java.util.List;

public class TreatmentAdapter extends RecyclerView.Adapter<TreatmentAdapter.ViewHolder> {


    private List<Treatment> mTreatments;

    public TreatmentAdapter(List<Treatment> pTreatments){
        mTreatments = pTreatments;
    }

    @NonNull
    @Override
    public ViewHolder onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
        View fila = LayoutInflater.from(parent.getContext()).inflate(R.layout.history,parent,false);
        ViewHolder vh = new ViewHolder(fila);
        return vh;
    }

    @Override
    public void onBindViewHolder(@NonNull ViewHolder holder, int position) {
        Treatment t = mTreatments.get(position);
        holder.txvTreatmentName.setText(t.getTrea_name());
        holder.txvTreatmentLasted.setText(t.getTrea_date_start() + " - " + t.getTrea_date_end());
        holder.txvTreatmentDesc.setText(t.getTrea_description());

    }

    @Override
    public int getItemCount() {
        return 0;
    }

    public class ViewHolder extends RecyclerView.ViewHolder {

        TextView  txvTreatmentName;
        TextView txvTreatmentDesc;
        TextView txvTreatmentLasted;
        public ViewHolder(@NonNull View filaview) {
            super(filaview);
            txvTreatmentName = filaview.findViewById(R.id.txvTreatmentName);
            txvTreatmentDesc = filaview.findViewById(R.id.txvTreatmentDesc);
            txvTreatmentLasted = filaview.findViewById(R.id.txvTreatmentLasted);


        }
    }
}
