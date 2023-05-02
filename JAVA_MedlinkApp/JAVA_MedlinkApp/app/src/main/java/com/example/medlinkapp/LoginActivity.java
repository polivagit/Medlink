package com.example.medlinkapp;

import androidx.annotation.RequiresApi;
import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.AsyncTask;
import android.os.Build;
import android.os.Bundle;
import android.text.TextUtils;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

import com.fasterxml.jackson.databind.ObjectMapper;
import com.google.gson.JsonParser;

import org.json.JSONException;
import org.json.JSONObject;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.OutputStreamWriter;
import java.net.HttpURLConnection;
import java.net.URL;
import java.util.Base64;
import java.util.HashMap;
import java.util.Map;

public class LoginActivity extends AppCompatActivity {

    private static final String USER_NAME = "pau";
    private static final String PASSWORD = "admin";
    private static final String WEBSERVICE_URL = "http://169.254.30.133/WebService/index.php/apix/Login/login";

    EditText etUser,etPassword;
    Button btnSignIn;

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
                // calling a class to login our user.
                new PostDataTask().execute();
            }
        });
    }

    private class PostDataTask extends AsyncTask<Void, Void, String> {

        @RequiresApi(api = Build.VERSION_CODES.O)
        @Override
        protected String doInBackground(Void... voids) {
            try {
                URL url = new URL(WEBSERVICE_URL);

                HttpURLConnection connection = (HttpURLConnection) url.openConnection();
                connection.setRequestMethod("POST");
                connection.setDoOutput(true);
                connection.setRequestProperty("Content-Type", "application/json");
                connection.setRequestProperty("Authorization", "Basic " + getAuthString());

                // JSON BODY PARAMETERS
                Map<String, String> jsonMap = new HashMap<>();
                jsonMap.put("user", etUser.getText().toString());
                jsonMap.put("pass", etPassword.getText().toString());

                // ObjectMapper below use Jackson (add dependencies in gradle)
                ObjectMapper objectMapper = new ObjectMapper();
                String jsonInputString = objectMapper.writeValueAsString(jsonMap);

                OutputStreamWriter wr = new OutputStreamWriter(connection.getOutputStream());
                wr.write(jsonInputString);
                wr.flush();
                wr.close();

                int responseCode = connection.getResponseCode();
                if (responseCode == HttpURLConnection.HTTP_OK) {
                    InputStream inputStream = connection.getInputStream();
                    BufferedReader bufferedReader = new BufferedReader(new InputStreamReader(inputStream, "UTF-8"));
                    StringBuilder response = new StringBuilder();
                    String line = null;
                    while ((line = bufferedReader.readLine()) != null) {
                        response.append(line);
                    }
                    return response.toString();
                } else {
                    return "Error " + responseCode;
                }

            } catch (IOException e) {
                e.printStackTrace();
                return "Error: " + e.getMessage();
            }


        }

        @Override
        protected void onPostExecute(String result) {

            try {
                JSONObject object = new JSONObject(result);
                String trobat = object.get("trobat").toString();
                if (trobat.equals("0")){
                    Intent i = new Intent(getApplicationContext(),MainActivity.class);
                    startActivity(i);
                }else if (trobat.equals("1")){
                    Toast.makeText(LoginActivity.this, "User or password wrong. Please try again",
                            Toast.LENGTH_LONG).show();
                }
            } catch (JSONException e) {
                throw new RuntimeException(e);
            }

            /*
            ========================================================================================
            THIS (result) IS THE RESPONSE (json) FROM THE WEBSERVICE
            ========================================================================================
            */
            Log.e("patata", "POST result: "+result);
        }
        @RequiresApi(api = Build.VERSION_CODES.O)
        private String getAuthString() {
            String authString = USER_NAME + ":" + PASSWORD;
            byte[] authEncBytes = Base64.getEncoder().encode(authString.getBytes());
            return new String(authEncBytes);
        }
    }
}
