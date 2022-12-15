using CameraUtils.Core;
using UnityEngine;

namespace CameraUtils.Behaviours {
    [DisallowMultipleComponent]
    public class AutoCameraRegistrator : MonoBehaviour {
        #region CameraFlags

        [SerializeField]
        private CameraFlags additionalFlags = CameraFlags.None;

        public CameraFlags AdditionalFlags {
            get => additionalFlags;
            set {
                if (additionalFlags == value) return;
                additionalFlags = value;
                Register();
            }
        }

        #endregion

        #region Events

        private Camera _camera;
        private bool _isEnabled;

        private void Awake() {
            _camera = GetComponent<Camera>();
        }

        private void OnEnable() {
            _isEnabled = true;
            Register();
        }

        private void OnDisable() {
            _isEnabled = false;
            UnRegister();
        }

        #endregion

        #region Register / UnRegister

        private void Register() {
            if (!_isEnabled) return;

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