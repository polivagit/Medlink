package com.example.medlinkapp.ui.start;

import androidx.lifecycle.LiveData;
import androidx.lifecycle.MutableLiveData;
import androidx.lifecycle.ViewModel;

public class StartViewModel extends ViewModel {

    private final MutableLiveData<String> mText;

    public StartViewModel() {
        mText = new MutableLiveData<>();
        mText.setValue("This is START fragment");
    }

    public LiveData<String> getText() {
        return mText;
    }
}