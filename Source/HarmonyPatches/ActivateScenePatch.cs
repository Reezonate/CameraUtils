using System.Collections.Generic;
using CameraUtils.Core;
using HarmonyLib;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CameraUtils.HarmonyPatches;

[HarmonyPatch(typeof(GameScenesManager), "ActivatePresentedSceneRootObjects")]
internal static class ActivateScenePatch {
    [UsedImplicitly]
    private static void Postfix(List<string> scenesToPresent) {
        foreach (var sceneName in scenesToPresent) {
            foreach (var rootGameObject in SceneManager.GetSceneByName(sceneName).GetRootGameObjects()) {
                foreach (var camera in rootGameObject.GetComponentsInChildren<Camera>()) {
                    VisibilityUtils.UpdateCameraIfKnown(camera);
                }
            }
        }
    }
}