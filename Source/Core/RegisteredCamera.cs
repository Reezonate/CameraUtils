using UnityEngine;

namespace CameraUtils.Core {
    public readonly struct RegisteredCamera {
        public readonly Camera Camera;
        public readonly CameraFlags CameraFlags;

        public RegisteredCamera(Camera camera, CameraFlags cameraFlags) {
            Camera = camera;
            CameraFlags = cameraFlags;
        }
    }
}