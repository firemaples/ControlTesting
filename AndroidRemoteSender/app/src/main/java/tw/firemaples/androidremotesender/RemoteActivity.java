package tw.firemaples.androidremotesender;

import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;

import tw.firemaples.androidremotesender.websocket.ServerFinder;

public class RemoteActivity extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_remote);

        ServerFinder serverFinder = new ServerFinder();
        serverFinder.findServers();
    }
}
