using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsControlServer.Forms
{
    public partial class SightDialog : Form
    {
        private int size = 100;

        public delegate void SightDialogCallbackHandler(object sender, EventArgs e);
        public event SightDialogCallbackHandler OnEventClosed;

        private SightDialog()
        {
            InitializeComponent();

            this.Size = new System.Drawing.Size(size, size);
            pic_sight.Size = new System.Drawing.Size(size, size);
            pic_sight.Location = new Point(0, 0);
        }

        #region Callers

        public static SightDialog ShowSight(int x, int y, int size = 100)
        {
            var sightDialog = new SightDialog();
            sightDialog.size = size;
            sightDialog.StartPosition = FormStartPosition.Manual;
            sightDialog.Location = new Point(x - size / 2, y - size / 2);
            sightDialog.ShowDialog();
            return sightDialog;
        }

        public static SightDialog ShowSight(ShowPosition showPosition, int size = 100)
        {
            var screenWidth = SystemInformation.VirtualScreen.Width;
            var screenHeight = SystemInformation.VirtualScreen.Height;

            int x, y;
            switch (showPosition)
            {
                case ShowPosition.LeftTop:
                    x = 0;
                    y = 0;
                    break;
                case ShowPosition.RightTop:
                    x = screenWidth;
                    y = 0;
                    break;
                case ShowPosition.LeftBottom:
                    x = 0;
                    y = screenHeight;
                    break;
                default:
                    x = screenWidth;
                    y = screenHeight;
                    break;
            }
            return ShowSight(x, y);
        }
        #endregion

        #region Events
        private void OnFormLoaded(object sender, EventArgs e)
        {
            this.BackColor = Color.Black;
            this.TransparencyKey = Color.Black;
        }

        private void OnFormKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();

                if (OnEventClosed != null)
                    OnEventClosed(this, e);
            }
        }
        #endregion

        #region Others
        public enum ShowPosition
        {
            LeftTop,
            RightTop,
            LeftBottom,
            RightBottom
        }
        #endregion
    }
}
