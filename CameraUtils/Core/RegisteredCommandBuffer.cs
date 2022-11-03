using UnityEngine.Rendering;

namespace CameraUtils.Core {
    public readonly struct RegisteredCommandBuffer {
        public readonly CameraEvent CameraEvent;
        public readonly CommandBuffer CommandBuffer;
        public readonly CameraFlags IncludeFlags;
        public readonly CameraFlags ExcludeFlags;

        public RegisteredCommandBuffer(
            CameraEvent cameraEvent,
            CommandBuffer commandBuffer,
            CameraFlags includeFlags,
            CameraFlags excludeFlags
        ) {
            CameraEvent = cameraEvent;
            CommandBuffer = commandBuffer;
            IncludeFlags = includeFlags;
            ExcludeFlags = excludeFlags;
        }

        public bool IsSuitableForCamera(CameraFlags cameraFlags) {
            return (cameraFlags & ExcludeFlags) == 0 && (cameraFlags & IncludeFlags) != 0;
        }
    }
}