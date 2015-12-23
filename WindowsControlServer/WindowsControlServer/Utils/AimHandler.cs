using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsControlServer.Forms;

namespace WindowsControlServer.Utils
{
    class AimHandler
    {
        public bool AimTaking { get; private set; }
        private int takenStep = 0;
        private SightDialog sightDialog;

        public AimHandler()
        {
            AimTaking = false;
        }


        public void StartAimTaking()
        {
            AimTaking = true;
            takenStep = 0;

            sightDialog = SightDialog.ShowSight(SightDialog.ShowPosition.LeftTop);

            sightDialog.OnEventClosed += (s, e) =>
            {
                StopTakeAim();
            };
        }

        public void TakeAimPoint()
        {
            if (AimTaking)
            {
                sightDialog.Close();
                switch (takenStep++)
                {
                    case 0:
                        sightDialog = SightDialog.ShowSight(SightDialog.ShowPosition.RightTop);
                        break;
                    case 1:
                        sightDialog = SightDialog.ShowSight(SightDialog.ShowPosition.RightBottom);
                        break;
                    case 2:
                        sightDialog = SightDialog.ShowSight(SightDialog.ShowPosition.LeftBottom);
                        break;
                    case 3:
                        sightDialog = null;
                        StopTakeAim();
                        break;
                }

                if (sightDialog != null)
                {
                    sightDialog.OnEventClosed += (s, e) =>
                    {
                        StopTakeAim();
                    };
                }
            }
        }

        public void StopTakeAim()
        {
            if (sightDialog != null)
            {
                sightDialog.Close();
            }
            AimTaking = false;
        }
    }
}
