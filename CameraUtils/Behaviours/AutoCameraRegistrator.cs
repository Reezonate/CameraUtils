using CameraUtils.Core;
using UnityEngine;

namespace CameraUtils.Behaviours {
    public class AutoCameraRegistrator : MonoBehaviour {
        private Camera _camera;
        public CameraFlags additionalFlags = CameraFlags.None;

        private void Awake() {
            _camera = GetComponent<Camera>();
        }

        private void OnEnable() {
            if (_camera.stereoEnabled) {
                CamerasManager.RegisterHMDCamera(_camera, additionalFlags);
            } else {
                CamerasManager.RegisterDesktopCamera(_camera, additionalFlags);
            }
        }

        private void OnDisable() {
            CamerasManager.UnRegisterCamera(_camera);
        }

        private void OnDestroy() {
            CamerasManager.UnRegisterCamera(_camera);
        }
    }
}