package com.example.medlinkapp.adapters;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.recyclerview.widget.RecyclerView;

import com.example.medlinkapp.R;
import com.example.medlinkapp.utils.medicines.MedicineTreatmentData;

import java.util.List;

public class MedicineStartAdapter extends RecyclerView.Adapter<MedicineStartAdapter.ViewHolder> {

    private List<MedicineTreatmentData> mTreatmentMedicines;

    public MedicineStartAdapter(List<MedicineTreatmentData> pTreatmentMedicines){
        mTreatmentMedicines = pTreatmentMedicines;
    }

    @NonNull
    @Override
    public ViewHolder onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
        View fila = LayoutInflater.from(parent.getContext()).inflate(R.layout.medicines,parent,false);
        ViewHolder vh = new ViewHolder(fila);
        return vh;
    }

    @Override
    public void onBindViewHolder(@NonNull ViewHolder holder, int position) {
        MedicineTreatmentData tm = mTreatmentMedicines.get(position);
        holder.txvMedicineName.setText(tm.getMedi_name()+"");
        holder.txvMedicineQuantity.setText(tm.getTrme_quantity_per_day()+"");
        holder.txvMedicineUnitOfMeasure.setText(tm.getUnme_abbreviation());
    }


    @Override
    public int getItemCount() {
        return mTreatmentMedicines.size();
    }

    public class ViewHolder extends RecyclerView.ViewHolder {

        TextView txvMedicineName;
        TextView txvMedicineQuantity;
        TextView txvMedicineUnitOfMeasure;
        public ViewHolder(@NonNull View filaview) {
            super(filaview);
            txvMedicineName = filaview.findViewById(R.id.txvMedicineName);
            txvMedicineQuantity = filaview.findViewById(R.id.txvMedicineQuantity);
            txvMedicineUnitOfMeasure = filaview.findViewById(R.id.txvMedicineUnitOfMeasure);
        }
    }
}
