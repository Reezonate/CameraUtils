using System.Collections.Generic;
using CameraUtils.Behaviours;
using HarmonyLib;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CameraUtils.HarmonyPatches {
    [HarmonyPatch(typeof(GameScenesManager), "ActivatePresentedSceneRootObjects")]
    public static class OnSceneLoadedPatch {
        private static readonly HashSet<string> KnownCameras = new() {
            "MenuMainCamera",
            "MainCamera",
            "SmoothCamera"
        };

        [UsedImplicitly]
        private static void Postfix(List<string> scenesToPresent) {
            foreach (var sceneName in scenesToPresent) {
                foreach (var rootGameObject in SceneManager.GetSceneByName(sceneName).GetRootGameObjects()) {
                    foreach (var camera in rootGameObject.GetComponentsInChildren<Camera>()) {
                        if (!KnownCameras.Contains(camera.name)) continue;
                        AddAutoCameraRegistrator(camera);
                    }
                }

                if (sceneName == "GameCore") {
                    new GameObject("ScoreSaberReplayCameraRegistrator").AddComponent<ScoreSaberReplayCameraRegistrator>();
                }
            }
        }

        private static void AddAutoCameraRegistrator(Camera camera) {
            if (camera.gameObject.GetComponent<AutoCameraRegistrator>() != null) return;
            camera.gameObject.AddComponent<AutoCameraRegistrator>();
        }
    }
}