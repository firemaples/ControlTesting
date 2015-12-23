package tw.firemaples.androidremotesender.websocket;

import org.java_websocket.client.WebSocketClient;
import org.java_websocket.handshake.ServerHandshake;

import java.net.URI;
import java.net.URISyntaxException;

/**
 * Created by Louis on 2015/12/23.
 */
public class EchoClient extends WebSocketClient {
    private final String ip;
    private final EchoClientCallback callback;
    private final static String EchoServerIpFormat = "ws://%s:10988/ECHO";
    private final String EchoString = "WCSEchoTest";

    public EchoClient(String ip, EchoClientCallback callback) throws URISyntaxException {
        super(new URI(String.format(EchoServerIpFormat, ip)));
        this.ip = ip;
        this.callback = callback;
    }

    @Override
    public void onOpen(ServerHandshake serverHandshake) {
        this.send(EchoString);
    }

    @Override
    public void onMessage(String s) {
        if (EchoString.equals(s)) {
            if (callback != null) {
                callback.OnServerFind(ip);
                this.close();
            }
        }
    }

    @Override
    public void onClose(int i, String s, boolean b) {

    }

    @Override
    public void onError(Exception e) {

    }

    public interface EchoClientCallback {
        void OnServerFind(String ip);
    }
}
