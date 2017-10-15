using System;

namespace SMIndustries.info
{
    public class SMIdepthSounder : PartModule
    {

        [KSPField(guiActive = true, guiName = "Depth  Sounder")]
        public double radarAltitude;

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            double pqsAltitude = vessel.pqsAltitude;
            if (pqsAltitude < 0) pqsAltitude = 0;
            radarAltitude = Math.Floor(vessel.altitude - pqsAltitude);
        }
    }
}