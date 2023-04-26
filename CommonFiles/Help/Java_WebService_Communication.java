package com.example.testing;

import androidx.appcompat.app.AppCompatActivity;

import android.os.AsyncTask;
import android.os.Bundle;
import android.util.Log;

import com.fasterxml.jackson.databind.ObjectMapper;

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

public class MainActivity extends AppCompatActivity {

    private static final String USER_NAME = "pau";
    private static final String PASSWORD = "admin";
    private static final String WEBSERVICE_URL = "http://169.254.30.133/WebService/index.php/apix/Login/login";

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        new PostDataTask().execute();
    }

    private class PostDataTask extends AsyncTask<Void, Void, String> {

        @Override
        protected String doInBackground(Void... params) {
            try {
                URL url = new URL(WEBSERVICE_URL);

                HttpURLConnection connection = (HttpURLConnection) url.openConnection();
                connection.setRequestMethod("POST");
                connection.setDoOutput(true);
                connection.setRequestProperty("Content-Type", "application/json");
                connection.setRequestProperty("Authorization", "Basic " + getAuthString());

                // JSON BODY PARAMETERS
                Map<String, String> jsonMap = new HashMap<>();
                jsonMap.put("user", "pgarcia1");
                jsonMap.put("pass", "4720A");

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

            /*
            ========================================================================================
            THIS (result) IS THE RESPONSE (json) FROM THE WEBSERVICE
            ========================================================================================
            */
            Log.e("MEDLINK", "POST result: "+result);
        }
    }

    private String getAuthString() {
        String authString = USER_NAME + ":" + PASSWORD;
        byte[] authEncBytes = Base64.getEncoder().encode(authString.getBytes());
        return new String(authEncBytes);
    }
}