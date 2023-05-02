package com.example.medlinkapp;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.text.TextUtils;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

import com.android.volley.DefaultRetryPolicy;
import com.android.volley.NetworkResponse;
import com.android.volley.NoConnectionError;
import com.android.volley.Request;
import com.android.volley.RequestQueue;
import com.android.volley.Response;
import com.android.volley.TimeoutError;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.StringRequest;
import com.android.volley.toolbox.Volley;
import com.example.medlinkapp.model.KeyValuePair;
import com.google.gson.Gson;

import org.json.JSONException;
import org.json.JSONObject;

import java.util.ArrayList;

public class LoginActivity extends AppCompatActivity {

    private static final String USER_NAME = "pau";
    private static final String PASSWORD = "admin";
    private static final String WEBSERVICE_URL = "http://169.254.30.133/WebService/index.php/apix/Login/login";
    public static final String Sp_Status = "Status";
    public static final String MyPref = "MyPref";

    static int mStatusCode = 0;
    EditText etUser,etPassword;
    Button btnSignIn;

    Gson gson;

    SharedPreferences sharedPreferences;

    private Boolean exit = false;

    public String username, password;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_login);

        etUser = findViewById(R.id.etUser);
        etPassword = findViewById(R.id.etPassword);
        btnSignIn = findViewById(R.id.btnSignIn);

        btnSignIn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                String userName = etUser.getText().toString();
                String password = etPassword.getText().toString();
                // checking if the entered text is empty or not.
                if (TextUtils.isEmpty(userName) && TextUtils.isEmpty(password)) {
                    Toast.makeText(LoginActivity.this, "Please enter user name and password", Toast.LENGTH_SHORT).show();
                }
                // calling a method to login our user.
                loginUser();
            }
        });
    }
    private void loginUser() {
        ArrayList params = new ArrayList();
        params.add(new KeyValuePair("UserName",username));
        params.add(new KeyValuePair("Password",password));
        RequestQueue requestQueue = Volley.newRequestQueue(LoginActivity.this);
        StringRequest stringRequest = new StringRequest(Request.Method.GET, WEBSERVICE_URL, new Response.Listener<String>() {
            @Override
            public void onResponse(String response) {
                sharedPreferences = getSharedPreferences(MyPref, Context.MODE_PRIVATE);
                SharedPreferences.Editor editor = sharedPreferences.edit();
                gson = new Gson();
                switch (mStatusCode){
                    case 200:
                        try{
                            JSONObject jsonObject = new JSONObject(response);
                            gson.fromJson(jsonObject.toString(),KeyValuePair.class);
                            Toast.makeText(LoginActivity.this,"Login Succesful",Toast.LENGTH_SHORT).show();
                            editor.putString(Sp_Status,"LoggedIn");
                            editor.commit();
                            startActivity(new Intent(LoginActivity.this,MainActivity.class));
                            finish();
                        }catch (JSONException e){
                            e.printStackTrace();
                        }
                }
            }
        }, new Response.ErrorListener() {
            @Override
            public void onErrorResponse(VolleyError error) {
                if (error instanceof TimeoutError || error instanceof NoConnectionError){
                    Toast.makeText(LoginActivity.this,"Server Down",Toast.LENGTH_SHORT).show();
                }
            }
        }){
            @Override
            protected Response parseNetworkResponse(NetworkResponse response){
                mStatusCode = response.statusCode;
                return super.parseNetworkResponse(response);
            }
        };
        stringRequest.setRetryPolicy(new DefaultRetryPolicy(3000,
                DefaultRetryPolicy.DEFAULT_MAX_RETRIES,
                DefaultRetryPolicy.DEFAULT_BACKOFF_MULT));
        requestQueue.add(stringRequest);
    }

    //tutorial para acabar el login http://androidtutorialsamples.blogspot.com/2017/03/android-login-using-api-example.html

}