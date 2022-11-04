using System.Collections;
using CameraUtils.Core;
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
                var cameraRegistrator = camera.gameObject.AddComponent<AutoCameraRegistrator>();
                cameraRegistrator.AdditionalFlags |= CameraFlags.FirstPerson;
                cameraRegistrator.AdditionalFlags |= CameraFlags.Composition;
            }

            Destroy(gameObject);
        }

        private static bool IsScoreSaberReplayCamera(Camera camera) {
            return camera.name.Contains("RecorderCamera");
        }
    }
}