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
    }
}