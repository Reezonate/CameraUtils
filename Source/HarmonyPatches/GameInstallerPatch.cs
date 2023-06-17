using CameraUtils.Behaviours;
using HarmonyLib;
using JetBrains.Annotations;
using UnityEngine;

namespace CameraUtils.HarmonyPatches;

[HarmonyPatch(typeof(GameplayCoreInstaller), "InstallBindings")]
internal static class GameInstallerPatch {
    [UsedImplicitly]
    // ReSharper disable once InconsistentNaming
    private static void Postfix(GameplayCoreInstaller __instance) {
        new GameObject("DelayedCameraRegistrator").AddComponent<DelayedCameraRegistrator>(); //Necessary for Replays and Multiplayer
    }
}