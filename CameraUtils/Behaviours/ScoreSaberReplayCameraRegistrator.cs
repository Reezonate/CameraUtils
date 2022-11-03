using System.Collections;
using UnityEngine;

namespace CameraUtils.Behaviours {
    internal class ScoreSaberReplayCameraRegistrator : MonoBehaviour {
        private void Start() {
            StartCoroutine(RegisterCoroutine());
        }

        private IEnumerator RegisterCoroutine() {
            yield return new WaitForEndOfFrame();

            foreach (var camera in FindObjectsOfType<Camera>()) {
                if (!IsScoreSaberReplayCamera(camera)) continue;
                camera.gameObject.AddComponent<AutoCameraRegistrator>();
            }

            Destroy(gameObject);
        }

        private static bool IsScoreSaberReplayCamera(Camera camera) {
            return camera.name.Contains("RecorderCamera");
        }
    }
}