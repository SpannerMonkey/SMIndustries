using System.Collections.Generic;
using UnityEngine;

namespace SMIndustries
{
    [KSPAddon(KSPAddon.Startup.Instantly, false)]
    class SMISplashScreen : MonoBehaviour
    {
        private static float MAX_TIP_TIME = 4; // seconds

        private static readonly string[] SMI_TIP1 =
        {
            "SM Industries and Associates..The cause of an explosion near you"
        };

        /// <summary>
        /// Snark's sneaky little way of thanking various users of this mod for helpful contributions.
        /// </summary>
        private static readonly string[] SMI_TIP2 =
        {
            "Special thanks gomker,XOC,DocDavinci,Kergan,Azimech,Wrench, and the BDAc Dev team."
        };

        internal void Awake()
        {
            LoadingScreen.LoadingScreenState state = FindLoadingScreenState();

            if (state != null)
            {
                InsertTips(state);
                if (state.tipTime > MAX_TIP_TIME) state.tipTime = MAX_TIP_TIME;
            }
        }

        /// <summary>
        /// Finds the loading screen where we want to tinker with the tips,
        /// or null if there's no suitable candidate.
        /// </summary>
        /// <returns></returns>
        private static LoadingScreen.LoadingScreenState FindLoadingScreenState()
        {
            if (LoadingScreen.Instance == null) return null;

            List<LoadingScreen.LoadingScreenState> screens = LoadingScreen.Instance.Screens;

            if (screens == null) return null;
            for (int i = 0; i < screens.Count; ++i)
            {
                LoadingScreen.LoadingScreenState state = screens[i];

                if ((state != null) && (state.tips != null) && (state.tips.Length > 1)) return state;
            }
            return null;
        }

        /// <summary>
        /// Insert our list of tips into the specified loading screen state.
        /// </summary>
        /// <param name="state"></param>
        private static void InsertTips(LoadingScreen.LoadingScreenState state)
        {
            List<string> tipsList = new List<string>();
            tipsList.AddRange(state.tips);
            tipsList.AddRange(SMI_TIP1);
            int numThanks = 1 + (int)Mathf.Sqrt(SMI_TIP2.Length);
            System.Random random = new System.Random(System.DateTime.UtcNow.Second);

            for (int i = 0; i < numThanks; ++i)
            {
                tipsList.Add(string.Format(SMI_TIP2[random.Next(SMI_TIP2.Length)]));
            }
            state.tips = tipsList.ToArray();
        }
    }
}