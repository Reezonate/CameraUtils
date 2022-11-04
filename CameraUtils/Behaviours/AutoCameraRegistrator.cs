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
        private bool _isEnabled;
        private bool _isStarted;

        private void Awake() {
            _camera = GetComponent<Camera>();
        }

        private void Start() {
            _isStarted = true;
            Register();
        }

        private void OnEnable() {
            _isEnabled = true;
            Register();
        }

        private void OnDisable() {
            _isEnabled = false;
            UnRegister();
        }

        private void OnDestroy() {
            _isEnabled = false;
            _isStarted = false;
            UnRegister();
        }

        #endregion

        #region Register / UnRegister

        private void Register() {
            if (!_isStarted || !_isEnabled) return;

            if (_camera.stereoEnabled) {
                CamerasManager.RegisterHMDCamera(_camera, AdditionalFlags);
            } else {
                CamerasManager.RegisterDesktopCamera(_camera, AdditionalFlags);
            }
        }

        private void UnRegister() {
            if (!_isStarted) return;
            CamerasManager.UnRegisterCamera(_camera);
        }

        #endregion
    }
}