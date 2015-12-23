using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WindowsControlServer.Forms;
using WindowsControlServer.Utils;
using WindowsControlServer.WebSocket;

namespace WindowsControlServer
{
    public partial class Form1 : Form
    {
        private bool closeByApp = false;
        private readonly WSServer wsServer;
        private readonly EchoServer echoServer;
        private readonly AimHandler aimHandler;
        private AppStatus appStatus = AppStatus.None;

        public Form1()
        {
            InitializeComponent();
            IniNotifyIcon();

            aimHandler = new AimHandler();

            wsServer = new WSServer();
            wsServer.OnStatusChanged += wsServer_OnStatusChanged;
            wsServer.Start();

            echoServer = new EchoServer();
            echoServer.Start();
        }

        #region Events

        #region Form

        private void OnFormLoaded(object sender, EventArgs e)
        {
            //WindowState = FormWindowState.Minimized;
            //this.Hide();

            var screenWidth = SystemInformation.VirtualScreen.Width;
            var screenHeight = SystemInformation.VirtualScreen.Height;


            //VirtualMouse.MoveTo(32768, 32768);

        }

        private void OnFormResize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == this.WindowState)
            {
                notifyIcon1.BalloonTipTitle = this.Text;
                notifyIcon1.BalloonTipText = "程式已縮小至工作列，雙擊圖示可以開啟。";

                notifyIcon1.ShowBalloonTip(500);
                this.Hide();
            }
        }

        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            if (!closeByApp)
            {
                WindowState = FormWindowState.Minimized;

                e.Cancel = true;
            }
        }

        private void OnFormClosed(object sender, FormClosedEventArgs e)
        {
            wsServer.Stop();
            echoServer.Stop();
            base.OnClosed(e);
        }

        #endregion

        #region NotifyIcon

        private void IniNotifyIcon()
        {
            notifyIcon1.ContextMenu = new ContextMenu();
            //notifyIcon1.ContextMenu.MenuItems.Add("手動檢查更新", (s, e) =>
            //{
            //    AppUpdateHandler.InstallUpdateSyncWithInfo();
            //});
            //notifyIcon1.ContextMenu.MenuItems.Add("清除暫存文件", (s, e) =>
            //{
            //    FileHandler.ClearTempFolder();
            //});
            //notifyIcon1.ContextMenu.MenuItems.Add("版本資訊", (s, e) =>
            //{
            //    VersionInfo.Show();
            //});
            notifyIcon1.ContextMenu.MenuItems.Add("退出", (s, e) =>
            {
                closeByApp = true;
                Application.Exit();
            });
        }

        private void OnNotifyIconDbClick(object sender, MouseEventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        #endregion

        #region WSServer

        void wsServer_OnStatusChanged(object sender, EventArgs e, WSServer.StatusCode statusCode, string statusMessage)
        {
            if (statusMessage != null)
                ShowBalloonMessage(statusMessage);

            switch (statusCode)
            {
                case WSServer.StatusCode.Cmd_TakeAim:
                    appStatus = AppStatus.Act_AimTaking;
                    aimHandler.StartAimTaking();
                    break;

                case WSServer.StatusCode.Cmd_Shot:
                    if (aimHandler.AimTaking)
                    {
                        aimHandler.TakeAimPoint();
                    }
                    else
                    {
                        
                    }
                    break;
            }
        }

        #endregion

        #endregion

        public void ShowBalloonMessage(string message, string title = null)
        {
            notifyIcon1.BalloonTipTitle = title ?? this.Text;
            notifyIcon1.BalloonTipText = message;

            notifyIcon1.ShowBalloonTip(500);
        }

        private enum AppStatus
        {
            None,
            Mode_RedPoint,
            Mode_Gun,
            Act_AimTaking
        }
    }
}
