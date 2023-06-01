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
                _flagsDirty = true;
            }
        }

        #endregion

        #region Events

        private Camera _camera;

        private bool _flagsDirty = true;
        private int _lastCullingMask;

        private bool IsDirty => _flagsDirty || _camera.cullingMask != _lastCullingMask;

        private void Awake() {
            _camera = GetComponent<Camera>();
        }

        private void OnDisable() {
            UnRegister();
            _flagsDirty = true;
        }

        private void OnPreCull() {
            if (!IsDirty) return;
            Register();
            _lastCullingMask = _camera.cullingMask;
            _flagsDirty = false;
        }

        #endregion

        #region Register / UnRegister

        private void Register() {
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