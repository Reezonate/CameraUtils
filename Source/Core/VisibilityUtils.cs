using System.Collections.Generic;
using CameraUtils.Behaviours;
using UnityEngine;

namespace CameraUtils.Core {
    public static class VisibilityUtils {
        #region SetLayer

        public static void SetLayer(this Transform transform, VisibilityLayer layer) {
            transform.gameObject.SetLayer(layer);
        }

        public static void SetLayer(this GameObject gameObject, VisibilityLayer layer) {
            gameObject.layer = (int)layer;
        }

        #endregion

        #region SetLayerRecursively

        public static void SetLayerRecursively(this GameObject gameObject, VisibilityLayer layer) {
            gameObject.transform.SetLayerRecursively(layer);
        }

        public static void SetLayerRecursively(this Transform transform, VisibilityLayer layer) {
            transform.gameObject.layer = (int)layer;

            for (var i = 0; i < transform.childCount; i++) {
                var child = transform.GetChild(i);
                child.SetLayerRecursively(layer);
            }
        }

        #endregion

        #region GetOrAddRegistrator

        public static AutoCameraRegistrator GetOrAddCameraRegistrator(Camera camera) {
            if (!camera.gameObject.TryGetComponent(out AutoCameraRegistrator cameraRegistrator)) {
                cameraRegistrator = camera.gameObject.AddComponent<AutoCameraRegistrator>();
            }

            return cameraRegistrator;
        }

        #endregion

        #region InternalUtils

        private static readonly HashSet<string> KnownCameras = new() {
            "MenuMainCamera",
            "MainCamera",
            "SmoothCamera"
        };

        internal static void UpdateCameraIfKnown(Camera camera) {
            if (!KnownCameras.Contains(camera.name)) return;

            var cameraRegistrator = GetOrAddCameraRegistrator(camera);
            cameraRegistrator.AdditionalFlags |= CameraFlags.FirstPerson;
            if (camera.name == "SmoothCamera") {
                cameraRegistrator.AdditionalFlags |= CameraFlags.Composition;
            }
        }

        #endregion
    }
}