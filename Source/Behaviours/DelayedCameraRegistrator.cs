using System.Collections;
using CameraUtils.Core;
using UnityEngine;

namespace CameraUtils.Behaviours {
    internal class DelayedCameraRegistrator : MonoBehaviour {
        private void Start() {
            StartCoroutine(RegisterCoroutine());
        }

        private IEnumerator RegisterCoroutine() {
            yield return new WaitForEndOfFrame();

            foreach (var camera in FindObjectsOfType<Camera>()) {
                VisibilityUtils.UpdateCameraIfKnown(camera);
                UpdateReplayCamera(camera);
            }

            Destroy(gameObject);
        }

        private static void UpdateReplayCamera(Camera camera) {
            if (!camera.name.Contains("RecorderCamera") && !camera.name.Equals("ReplayerViewCamera")) return; // ScoreSaber / BeatLeader
            var cameraRegistrator = VisibilityUtils.GetOrAddCameraRegistrator(camera);
            cameraRegistrator.AdditionalFlags |= CameraFlags.FirstPerson;
            cameraRegistrator.AdditionalFlags |= CameraFlags.Composition;
        }
    }
}