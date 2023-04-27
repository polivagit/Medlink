package com.example.medlinkapp.adapters;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.recyclerview.widget.RecyclerView;

import com.example.medlinkapp.R;
import com.example.medlinkapp.model.Treatment;
import com.example.medlinkapp.model.TreatmentMedicine;

import java.util.List;

public class MedicineStartAdapter extends RecyclerView.Adapter<MedicineStartAdapter.ViewHolder> {

    private List<TreatmentMedicine> mTreatmentMedicines;

    public MedicineStartAdapter(List<TreatmentMedicine> pTreatmentMedicines){
        mTreatmentMedicines = pTreatmentMedicines;
    }

    @NonNull
    @Override
    public ViewHolder onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
        View fila = LayoutInflater.from(parent.getContext()).inflate(R.layout.start,parent,false);
        ViewHolder vh = new ViewHolder(fila);
        return vh;
    }

    @Override
    public void onBindViewHolder(@NonNull ViewHolder holder, int position) {
        TreatmentMedicine tm = mTreatmentMedicines.get(position);
        holder.txvMedicineName.setText(tm.getTrme_medicine_id()+"");
        holder.txvMedicineQuantity.setText(tm.getTrme_quantity_per_day()+"");
    }


    @Override
    public int getItemCount() {
        return mTreatmentMedicines.size();
    }

    public class ViewHolder extends RecyclerView.ViewHolder {

        TextView txvMedicineName;
        TextView txvMedicineQuantity;
        public ViewHolder(@NonNull View filaview) {
            super(filaview);
            txvMedicineName = filaview.findViewById(R.id.txvMedicineName);
            txvMedicineQuantity = filaview.findViewById(R.id.txvMedicineQuantity);
        }
    }
}
