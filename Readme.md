# CameraUtils
Beat Saber modding library that standardizes use of GameObject layers and provides tools for easier render pipeline modifications

## Usage examples
Check out [CameraUtilsSandbox project](https://github.com/Reezonate/CameraUtilsSandbox) for more code/usage examples

### HMD-only rendering
```c
class HmdOnlyExample: MonoBehaviour {
    private void Awake() {
        gameObject.SetLayer(VisibilityLayer.HmdOnlyAndReflected);
        //If you don't want object to be rendered in reflections, use VisibilityLayer.HmdOnly
    }
}
```

### Desktop-only rendering
```c
class DesktopOnlyExample: MonoBehaviour {
    private void Awake() {
        gameObject.SetLayer(VisibilityLayer.DesktopOnlyAndReflected);
        //If you don't want object to be rendered in reflections, use VisibilityLayer.DesktopOnly
    }
}
```

### Registering custom Camera
- To add your camera to the system, simply call `CamerasManager.RegisterDesktopCamera(Camera yourCamera)`
- Make sure to remove your camera on destroy, using `CamerasManager.UnRegisterCamera(Camera yourCamera)`
- Alternatively, you can just add `AutoCameraRegistrator` component to the Camera's GameObject
```c
private void InitCamera(Camera camera) {
    camera.gameObject.AddComponent<AutoCameraRegistrator>();
}
```

### Registering CameraEffect
- To insert custom logic into the render pipeline, use `CamerasManager.RegisterCameraEffect()`
- Make sure to remove your effects on dispose, using `CamerasManager.UnRegisterCameraEffect()`

See: [simple Post-Process effect example](https://github.com/Reezonate/CameraUtilsSandbox/blob/master/CameraUtilsSandbox%20Plugin/CameraUtilsSandbox/Core/PostProcessDemo.cs)
