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
    class WSServer : WebSocketBehavior
    {
        public readonly static int listenedPortDefault = 10987;
        public readonly static string listenedName = "/WCS";
        private static WebSocketServer wsServer;

        public delegate void WSServerStatusHandler(object sender, EventArgs e, StatusCode statusCode, string statusMessage);
        public event WSServerStatusHandler OnStatusChanged;

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
            SendStatusChanged(null, StatusCode.Start, "服務已啟動");
        }

        public void Stop()
        {
            if (wsServer != null && wsServer.IsListening)
            {
                wsServer.Stop();
                wsServer = null;
                SendStatusChanged(null, StatusCode.Stop, "服務已中止");
            }
        }

        protected override void OnOpen()
        {
            base.OnOpen();
            Console.WriteLine("WS OnOpen");
        }

        protected override void OnClose(CloseEventArgs e)
        {
            base.OnClose(e);
            Console.WriteLine("WS OnClose: " + e.Reason);
        }

        protected override void OnError(ErrorEventArgs e)
        {
            base.OnError(e);
            Console.WriteLine("WS OnError: " + e.Message);
        }

        protected override void OnMessage(MessageEventArgs e)
        {
            base.OnMessage(e);
            var jsonString = e.Data;
            SendStatusChanged(e, StatusCode.GetMessage, jsonString);

            if (jsonString.StartsWith("{"))
            {
                var wsCommand = JsonConvert.DeserializeObject<WSCommand>(jsonString);
                if (wsCommand != null)
                {
                    if (!OnCommand(wsCommand, jsonString))
                    {
                        SendStatusChanged(e, StatusCode.GetUnknownMessage, jsonString);
                    }
                }
            }
        }

        #endregion

        #region WSCommands

        private bool OnCommand(WSCommand wsCommand, string jsonString)
        {
            switch (wsCommand.type)
            {
                case CmdStrings.TakeAim:
                    OnCmdTakeAim();
                    break;

                case CmdStrings.Shot:
                    OnCmdShot();
                    break;

                default:
                    return false;
            }

            return true;
        }

        private void OnCmdTakeAim()
        {
            SendStatusChanged(null, StatusCode.Cmd_TakeAim, "設定準心");
        }

        private void OnCmdShot()
        {
            SendStatusChanged(null, StatusCode.Cmd_Shot, "射擊");
        }

        #endregion

        #region StatusChange

        protected void SendStatusChanged(EventArgs e, StatusCode statusCode, string statusMessage)
        {
            if (OnStatusChanged != null)
            {
                OnStatusChanged(this, e, statusCode, statusMessage);
            }
        }

        public enum StatusCode
        {
            Start,
            Stop,
            Error,
            GetMessage,
            GetUnknownMessage,
            Cmd_TakeAim,
            Cmd_Shot
        }

        #endregion
    }

}
