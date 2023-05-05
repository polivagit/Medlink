package com.example.medlinkapp;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.text.TextUtils;
import android.util.Base64;
import android.util.Log;
import android.view.View;
import android.widget.Toast;

import com.example.medlinkapp.databinding.ActivityLoginBinding;
import com.example.medlinkapp.ui.login.ForgotPasswordFragment;
import com.example.medlinkapp.ui.login.RestorePasswordFragment;
import com.example.medlinkapp.utils.ApiService;
import com.example.medlinkapp.utils.AuthTokenInterceptor;
import com.example.medlinkapp.utils.BasicAuthInterceptor;
import com.example.medlinkapp.utils.LoginResponse;

import java.io.IOException;
import java.util.concurrent.TimeUnit;

import okhttp3.Interceptor;
import okhttp3.OkHttpClient;
import okhttp3.Request;
import okhttp3.ResponseBody;
import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;
import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;

public class LoginActivity extends AppCompatActivity {

    private static final String USER_NAME = "pau";
    private static final String PASSWORD = "admin";
    private static final String WEBSERVICE_URL = "http://169.254.30.133/WebService/index.php/apix/Request/login/";

    public int patientId;

    private ApiService apiService;

    private String credentials;

    ActivityLoginBinding binding;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        binding = ActivityLoginBinding.inflate(getLayoutInflater());
        setContentView(binding.getRoot());

        OkHttpClient authClient = new OkHttpClient.Builder()
                .addInterceptor(new BasicAuthInterceptor(USER_NAME, PASSWORD))
                .build();

        Retrofit authRetrofit = new Retrofit.Builder()
                .baseUrl(WEBSERVICE_URL)
                .client(authClient)
                .addConverterFactory(GsonConverterFactory.create())
                .build();
        ApiService authService = authRetrofit.create(ApiService.class);

        //LoginRequest loginRequest = new LoginRequest(binding.etUser.getText().toString(), binding.etUser.getText().toString());



        binding.btnForgotPassword.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                getSupportFragmentManager().beginTransaction().replace(R.id.glChangeLogin, new ForgotPasswordFragment()).commit();
                binding.glLogin.setVisibility(View.GONE);
                binding.glChangeLogin.setVisibility(View.VISIBLE);

            }
        });

        binding.btnRestorePassword.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                getSupportFragmentManager().beginTransaction().replace(R.id.glRestoreLogin, new RestorePasswordFragment()).commit();
                binding.glLogin.setVisibility(View.GONE);
                binding.glRestoreLogin.setVisibility(View.VISIBLE);
            }
        });
        binding.btnSignIn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                String userName = binding.etUser.getText().toString();
                String password = binding.etPassword.getText().toString();


                Call<LoginResponse> authCall = authService.authenticate();
                //Call<LoginRequest> call = apiService.loginUser(loginRequest);

                authCall.enqueue(new Callback<LoginResponse>() {
                    @Override
                    public void onResponse(Call<LoginResponse> call, Response<LoginResponse> response) {

                        Interceptor interceptor = new Interceptor() {
                            @Override
                            public okhttp3.Response intercept(Chain chain) throws IOException {
                                Request newRequest = chain.request().newBuilder()
                                        .addHeader("Accept", "application/json")
                                        .build();
                                return chain.proceed(newRequest);
                            }
                        };

                        final OkHttpClient okHttpClient = new OkHttpClient.Builder()
                                .readTimeout(60, TimeUnit.SECONDS)
                                .connectTimeout(60, TimeUnit.SECONDS)
                                .addInterceptor(interceptor)
                                .build();
                        /*String token = response.body().getToken();
                        OkHttpClient client = new OkHttpClient.Builder()
                                .addInterceptor(new AuthTokenInterceptor(token))
                                .build();*/

                        Retrofit appRetrofit = new Retrofit.Builder()
                                .baseUrl(WEBSERVICE_URL)
                                .client(okHttpClient)
                                .addConverterFactory(GsonConverterFactory.create())
                                .build();
                        ApiService appService = appRetrofit.create(ApiService.class);
                        Call<LoginResponse> loginCall = appService.login(userName, password);
                        loginCall.enqueue(new Callback<LoginResponse>() {
                            @Override
                            public void onResponse(Call<LoginResponse> call, Response<LoginResponse> response) {
                                if (TextUtils.isEmpty(userName) && TextUtils.isEmpty(password)) {
                                    Toast.makeText(LoginActivity.this, "Please enter user name and password", Toast.LENGTH_SHORT).show();
                                }else {
                                    if (response.isSuccessful()) {
                                        Toast.makeText(LoginActivity.this, "Login successful", Toast.LENGTH_SHORT).show();

                                        Intent i = new Intent(getApplicationContext(),MainActivity.class);
                                        startActivity(i);
                                        Log.e("patata","Result" + response);


                                    } else {
                                        //Toast.makeText(LoginActivity.this, "Login failed", Toast.LENGTH_SHORT).show();
                                        Toast.makeText(LoginActivity.this, "User or password wrong. Please try again",
                                                Toast.LENGTH_LONG).show();
                                        Log.e("patata","Result" + response);
                                    }
                                }
                            }

                            @Override
                            public void onFailure(Call<LoginResponse> call, Throwable t) {
                                // handle network failure
                            }
                        });
                    }

                    @Override
                    public void onFailure(Call<LoginResponse> call, Throwable t) {
                        Toast.makeText(LoginActivity.this, "Network error", Toast.LENGTH_SHORT).show();
                    }
                });
                // calling a class to login our user.
                //new PostDataTask().execute();
            }
        });




    }



    /*private class PostDataTask extends AsyncTask<Void, Void, String> {

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
                jsonMap.put("user", binding.etUser.getText().toString());
                jsonMap.put("pass", binding.etPassword.getText().toString());

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
                JSONArray arr = object.getJSONArray("data");
                for (int i = 0; i < arr.length(); i++)
                {
                    patientId = arr.getJSONObject(i).getInt("pers_id");
                    //Log.e("patata", "POST result: "+patientId);
                }
                //patientId = object.getInt("pers_id");
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


            Log.e("patata", "POST result: "+result);
        }
        @RequiresApi(api = Build.VERSION_CODES.O)
        private String getAuthString() {
            String authString = USER_NAME + ":" + PASSWORD;
            byte[] authEncBytes = Base64.getEncoder().encode(authString.getBytes());
            return new String(authEncBytes);
        }
    }*/
}
