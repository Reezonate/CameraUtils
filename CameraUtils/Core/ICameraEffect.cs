namespace CameraUtils.Core;

public interface ICameraEffect {
    public bool IsSuitableForCamera(RegisteredCamera registeredCamera);
    public void OnAddedToCamera(RegisteredCamera registeredCamera);
    public void OnRemovedFromCamera(RegisteredCamera registeredCamera);
}