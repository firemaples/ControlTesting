package tw.firemaples.androidremotesender.websocket;

import java.util.ArrayList;
import java.util.List;

import tw.firemaples.androidremotesender.utils.CommonUtils;

/**
 * Created by Louis on 2015/12/23.
 * <p/>
 * C# version http://stackoverflow.com/questions/7287179/find-server-listening-on-a-specific-port-on-local-network
 */
public class ServerFinder implements EchoClient.EchoClientCallback {
    private EchoClient echoClient;
    private List<String> serverList;

    public ServerFinder() {
        serverList = new ArrayList<>();
    }

    public void findServers() {
        String ipAddress = CommonUtils.getLocalIpAddress();
    }

    @Override
    public void OnServerFind(String ip) {

    }
}
