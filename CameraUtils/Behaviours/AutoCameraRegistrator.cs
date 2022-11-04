using CameraUtils.Core;
using UnityEngine;

namespace CameraUtils.Behaviours {
    public class AutoCameraRegistrator : MonoBehaviour {
        #region CameraFlags

        private CameraFlags _additionalFlags = CameraFlags.None;

        public CameraFlags AdditionalFlags {
            get => _additionalFlags;
            set {
                if (_additionalFlags == value) return;
                _additionalFlags = value;
                Register();
            }
        }

        #endregion

        #region Events

        private Camera _camera;
        private bool _enabled;

        private void Awake() {
            _camera = GetComponent<Camera>();
        }

        private void OnEnable() {
            _enabled = true;
            Register();
        }

        private void OnDisable() {
            _enabled = false;
            UnRegister();
        }

        private void OnDestroy() {
            _enabled = false;
            UnRegister();
        }

        #endregion

        #region Register / UnRegister

        private void Register() {
            if (!_enabled) return;

            if (_camera.stereoEnabled) {
                CamerasManager.RegisterHMDCamera(_camera, AdditionalFlags);
            } else {
                CamerasManager.RegisterDesktopCamera(_camera, AdditionalFlags);
            }
        }

        private void UnRegister() {
            CamerasManager.UnRegisterCamera(_camera);
        }

        #endregion
    }
}