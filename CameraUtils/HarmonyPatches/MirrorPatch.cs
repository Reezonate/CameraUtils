using CameraUtils.Core;
using HarmonyLib;
using JetBrains.Annotations;
using UnityEngine;

// ReSharper disable InconsistentNaming
namespace CameraUtils.HarmonyPatches {
    [HarmonyPatch(typeof(MirrorRendererSO), "CreateOrUpdateMirrorCamera")]
    public static class MirrorPatch {
        [UsedImplicitly]
        private static void Postfix(Camera ____mirrorCamera, Camera currentCamera) {
            CamerasManager.RegisterMirrorCamera(____mirrorCamera, currentCamera);
        }
    }

    [HarmonyPatch(typeof(MirrorRendererSO), "RenderMirror")]
    public static class MirrorOnDisablePatch {
        [UsedImplicitly]
        private static void Postfix(Camera ____mirrorCamera) {
            CamerasManager.UnRegisterCamera(____mirrorCamera);
        }
    }
}