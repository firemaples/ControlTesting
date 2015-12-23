using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsControlServer.Utils;
using WindowsControlServer.WebSocket.Commands;
using Newtonsoft.Json;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace WindowsControlServer.WebSocket
{
    class EchoServer : WebSocketBehavior
    {
        public readonly static int listenedPortDefault = 10988;
        public readonly static string listenedName = "/ECHO";
        private static WebSocketServer wsServer;
        private const string EchoString = "WCSEchoTest";

        #region ServerBasic

        public void Start()
        {
            if (wsServer == null)
            {
                var ip = CommonUtity.GetLocalIPAddress();
                wsServer = new WebSocketServer(string.Format("ws://{0}:{1}", ip, listenedPortDefault));
                wsServer.AddWebSocketService<WSServer>(listenedName);
            }

            wsServer.Start();
        }

        public void Stop()
        {
            if (wsServer != null && wsServer.IsListening)
            {
                wsServer.Stop();
                wsServer = null;
            }
        }

        protected override void OnOpen()
        {
            base.OnOpen();
            Console.WriteLine("ECHO OnOpen");
        }

        protected override void OnClose(CloseEventArgs e)
        {
            base.OnClose(e);
            Console.WriteLine("ECHO OnClose: " + e.Reason);
        }

        protected override void OnError(ErrorEventArgs e)
        {
            base.OnError(e);
            Console.WriteLine("ECHO OnError: " + e.Message);
        }

        protected override void OnMessage(MessageEventArgs e)
        {
            base.OnMessage(e);
            var msg = e.Data;

            Console.WriteLine("ECHO OnMessage: " + msg);
            if (msg == EchoString)
            {
                Send(EchoString);
            }
        }

        #endregion
    }

}
