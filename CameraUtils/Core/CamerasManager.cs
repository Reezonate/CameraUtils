using System.Collections.Generic;
using ModestTree;
using UnityEngine;
using UnityEngine.Rendering;

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
            AddAllBuffersToCamera(rc);
        }

        public static void UnRegisterCamera(Camera camera) {
            if (!Cameras.ContainsKey(camera)) return;
            var rc = Cameras.GetValueAndRemove(camera);
            RemoveAllBuffersFromCamera(rc);
        }

        #endregion

        #region CommandBuffers

        private static readonly Dictionary<CommandBuffer, RegisteredCommandBuffer> CommandBuffers = new();
        public static IEnumerable<RegisteredCommandBuffer> GetRegisteredCommandBuffers() => CommandBuffers.Values;

        public static void RegisterCommandBuffer(
            CameraEvent cameraEvent,
            CommandBuffer commandBuffer,
            CameraFlags includeFlags,
            CameraFlags excludeFlags
        ) {
            UnRegisterCommandBuffer(commandBuffer);
            var rb = new RegisteredCommandBuffer(cameraEvent, commandBuffer, includeFlags, excludeFlags);
            CommandBuffers[commandBuffer] = rb;
            AddBufferToAllCameras(rb);
        }

        public static void UnRegisterCommandBuffer(CommandBuffer commandBuffer) {
            if (!CommandBuffers.ContainsKey(commandBuffer)) return;
            var rb = CommandBuffers.GetValueAndRemove(commandBuffer);
            RemoveBufferFromAllCameras(rb);
        }

        private static void AddBufferToAllCameras(RegisteredCommandBuffer rb) {
            foreach (var rc in GetRegisteredCameras()) {
                if (!rb.IsSuitableForCamera(rc.CameraFlags)) continue;
                rc.Camera.AddCommandBuffer(rb.CameraEvent, rb.CommandBuffer);
            }
        }

        private static void RemoveBufferFromAllCameras(RegisteredCommandBuffer rb) {
            foreach (var rc in GetRegisteredCameras()) {
                if (!rb.IsSuitableForCamera(rc.CameraFlags)) continue;
                rc.Camera.RemoveCommandBuffer(rb.CameraEvent, rb.CommandBuffer);
            }
        }

        private static void AddAllBuffersToCamera(RegisteredCamera rc) {
            foreach (var rb in GetRegisteredCommandBuffers()) {
                if (!rb.IsSuitableForCamera(rc.CameraFlags)) continue;
                rc.Camera.AddCommandBuffer(rb.CameraEvent, rb.CommandBuffer);
            }
        }

        private static void RemoveAllBuffersFromCamera(RegisteredCamera rc) {
            foreach (var rb in GetRegisteredCommandBuffers()) {
                if (!rb.IsSuitableForCamera(rc.CameraFlags)) continue;
                rc.Camera.RemoveCommandBuffer(rb.CameraEvent, rb.CommandBuffer);
            }
        }

        #endregion
    }
}