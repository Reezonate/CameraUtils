namespace CameraUtils.Core;

public interface ICameraEffect {
    public bool IsSuitableForCamera(RegisteredCamera registeredCamera);
    public void HandleAddedToCamera(RegisteredCamera registeredCamera);
    public void HandleRemovedFromCamera(RegisteredCamera registeredCamera);
}