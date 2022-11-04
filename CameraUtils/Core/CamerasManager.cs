using System.Collections.Generic;
using ModestTree;
using UnityEngine;

namespace CameraUtils.Core {
    public static class CamerasManager {
        #region Cameras

        private static readonly Dictionary<Camera, RegisteredCamera> Cameras = new();
        public static IEnumerable<RegisteredCamera> GetRegisteredCameras() => Cameras.Values;

        public static void RegisterDesktopCamera(Camera camera, CameraFlags cameraFlags = CameraFlags.None) {
            cameraFlags |= CameraFlags.Desktop;
            RegisterCamera(camera, cameraFlags);
            CullingMaskUtils.SetupDesktopCamera(camera);
        }

        internal static void RegisterHMDCamera(Camera camera, CameraFlags cameraFlags = CameraFlags.None) {
            cameraFlags |= CameraFlags.HMD;
            RegisterCamera(camera, cameraFlags);
            CullingMaskUtils.SetupHMDCamera(camera);
        }

        internal static void RegisterMirrorCamera(Camera mirrorCamera, Camera currentCamera) {
            var cameraFlags = CameraFlags.Mirror;

            if (Cameras.ContainsKey(currentCamera)) {
                var rc = Cameras[currentCamera];
                cameraFlags |= rc.CameraFlags;
            }

            RegisterCamera(mirrorCamera, cameraFlags);
            CullingMaskUtils.SetupMirrorCamera(mirrorCamera, currentCamera);
        }

        private static void RegisterCamera(Camera camera, CameraFlags cameraFlags) {
            UnRegisterCamera(camera);
            var rc = new RegisteredCamera(camera, cameraFlags);
            Cameras[camera] = rc;
            AddAllEffectsToCamera(rc);
        }

        public static void UnRegisterCamera(Camera camera) {
            if (!Cameras.ContainsKey(camera)) return;
            var rc = Cameras.GetValueAndRemove(camera);
            RemoveAllEffectsFromCamera(rc);
        }

        #endregion

        #region CameraEffects

        private static readonly HashSet<ICameraEffect> CameraEffects = new();
        public static IEnumerable<ICameraEffect> GetAllCameraEffects() => CameraEffects;

        public static void RegisterCameraEffect(ICameraEffect cameraEffect) {
            UnRegisterCameraEffect(cameraEffect);
            CameraEffects.Add(cameraEffect);
            AddEffectToAllCameras(cameraEffect);
        }

        public static void UnRegisterCameraEffect(ICameraEffect cameraEffect) {
            if (!CameraEffects.Remove(cameraEffect)) return;
            RemoveEffectFromAllCameras(cameraEffect);
        }

        private static void AddEffectToAllCameras(ICameraEffect rb) {
            foreach (var rc in GetRegisteredCameras()) {
                if (!rb.IsSuitableForCamera(rc)) continue;
                rb.HandleAddedToCamera(rc);
            }
        }

        private static void RemoveEffectFromAllCameras(ICameraEffect rb) {
            foreach (var rc in GetRegisteredCameras()) {
                if (!rb.IsSuitableForCamera(rc)) continue;
                rb.HandleRemovedFromCamera(rc);
            }
        }

        private static void AddAllEffectsToCamera(RegisteredCamera rc) {
            foreach (var rb in GetAllCameraEffects()) {
                if (!rb.IsSuitableForCamera(rc)) continue;
                rb.HandleAddedToCamera(rc);
            }
        }

        private static void RemoveAllEffectsFromCamera(RegisteredCamera rc) {
            foreach (var rb in GetAllCameraEffects()) {
                if (!rb.IsSuitableForCamera(rc)) continue;
                rb.HandleRemovedFromCamera(rc);
            }
        }

        #endregion
    }
}