using UnityEngine;

namespace CameraUtils.Core {
    public static class CullingMaskUtils {
        #region Constants

        public const int Nothing = 0b000_0000_0000_0000_0000_0000_0000_0000;
        public const int Everything = 0b111_1111_1111_1111_1111_1111_1111_1111;

        private static readonly int MirrorMask_OR = Nothing;
        private static readonly int MirrorMask_AND = Everything;
        private static readonly int HMDMask_OR = Nothing;
        private static readonly int HMDMask_AND = Everything;
        private static readonly int DesktopMask_OR = Nothing;
        private static readonly int DesktopMask_AND = Everything;

        static CullingMaskUtils() {
            MirrorMask_OR |= 1 << (int)VisibilityLayer.AlwaysVisible;
            MirrorMask_OR |= 1 << (int)VisibilityLayer.DesktopOnly;
            MirrorMask_OR |= 1 << (int)VisibilityLayer.HmdOnly;
            MirrorMask_AND &= ~(1 << (int)VisibilityLayer.AlwaysVisibleNoMirror);
            MirrorMask_AND &= ~(1 << (int)VisibilityLayer.DesktopOnlyNoMirror);
            MirrorMask_AND &= ~(1 << (int)VisibilityLayer.HmdOnlyNoMirror);

            HMDMask_OR |= 1 << (int)VisibilityLayer.AlwaysVisible;
            HMDMask_OR |= 1 << (int)VisibilityLayer.AlwaysVisibleNoMirror;
            HMDMask_OR |= 1 << (int)VisibilityLayer.HmdOnly;
            HMDMask_OR |= 1 << (int)VisibilityLayer.HmdOnlyNoMirror;
            HMDMask_AND &= ~(1 << (int)VisibilityLayer.DesktopOnly);
            HMDMask_AND &= ~(1 << (int)VisibilityLayer.DesktopOnlyNoMirror);

            DesktopMask_OR |= 1 << (int)VisibilityLayer.AlwaysVisible;
            DesktopMask_OR |= 1 << (int)VisibilityLayer.AlwaysVisibleNoMirror;
            DesktopMask_OR |= 1 << (int)VisibilityLayer.DesktopOnly;
            DesktopMask_OR |= 1 << (int)VisibilityLayer.DesktopOnlyNoMirror;
            DesktopMask_AND &= ~(1 << (int)VisibilityLayer.HmdOnly);
            DesktopMask_AND &= ~(1 << (int)VisibilityLayer.HmdOnlyNoMirror);
        }

        #endregion

        #region SetupMirrorCamera

        public static void SetupMirrorCamera(Camera mirrorCamera, Camera currentCamera) {
            var cullingMask = mirrorCamera.cullingMask;
            cullingMask |= MirrorMask_OR;
            cullingMask &= MirrorMask_AND;
            cullingMask &= currentCamera.cullingMask;
            mirrorCamera.cullingMask = cullingMask;
        }

        #endregion

        #region SetupHMDCamera

        public static void SetupHMDCamera(Camera camera) {
            var cullingMask = camera.cullingMask;
            cullingMask |= HMDMask_OR;
            cullingMask &= HMDMask_AND;
            camera.cullingMask = cullingMask;
        }

        #endregion

        #region SetupDesktopCamera

        public static void SetupDesktopCamera(Camera camera) {
            var cullingMask = camera.cullingMask;
            cullingMask |= DesktopMask_OR;
            cullingMask &= DesktopMask_AND;
            camera.cullingMask = cullingMask;
        }

        #endregion
    }
}