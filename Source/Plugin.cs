using System.Reflection;
using HarmonyLib;
using IPA;
using JetBrains.Annotations;
using IPALogger = IPA.Logging.Logger;

namespace CameraUtils {
    [Plugin(RuntimeOptions.SingleStartInit), UsedImplicitly]
    public class Plugin {
        #region Init

        internal static IPALogger Log { get; private set; }

        [Init]
        public Plugin(IPALogger logger) {
            Log = logger;

            var harmony = new Harmony("Reezonate.CameraUtils");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }

        #endregion

        #region OnApplicationStart

        [OnStart, UsedImplicitly]
        public void OnApplicationStart() { }

        #endregion

        #region OnApplicationQuit

        [OnExit, UsedImplicitly]
        public void OnApplicationQuit() { }

        #endregion
    }
}