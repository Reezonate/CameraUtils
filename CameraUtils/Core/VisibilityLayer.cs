/* <- BASE GAME LAYERS | v1.25.1
* 0: Default
* 1: TransparentFX
* 2: Ignore Raycast
* 3: 
* 4: Water
* 5: UI
* 6: 
* 7: 
* 8: Note
* 9: NoteDebris
* 10: Avatar
* 11: Obstacle
* 12: Saber
* 13: NeonLight
* 14: Environment
* 15: GrabPassTexture1
* 16: CutEffectParticles
* 17: 
* 18: 
* 19: NonReflectedParticles
* 20: EnvironmentPhysics
* 21: 
* 22: Event
* 23: 
* 24: 
* 25: FixMRAlpha
* 26: 
* 27: DontShowInExternalMRCamera
* 28: PlayersPlace
* 29: Skybox
* 30: MRForegroundClipPlane
* 31: Reserved
*/

namespace CameraUtils.Core {
    public enum VisibilityLayer {
        Default = 0,
        TransparentFX = 1,
        IgnoreRaycast = 2,
        ThirdPerson = 3, //<----------- Possibly used by Unity (see: https://docs.unity3d.com/ScriptReference/GameObject-layer.html)
        Water = 4,
        UI = 5,
        FirstPerson = 6, //<----------- Not used in Vanilla
        Layer7 = 7, //<----------- Not used in Vanilla
        Note = 8,
        NoteDebris = 9,
        Avatar = 10,
        Obstacle = 11,
        Saber = 12,
        NeonLight = 13,
        Environment = 14,
        GrabPassTexture1 = 15,
        CutEffectParticles = 16,
        HmdOnlyNoMirror = 17, //<----------- Not used in Vanilla
        DesktopOnlyNoMirror = 18, //<----------- Not used in Vanilla
        NonReflectedParticles = 19,
        EnvironmentPhysics = 20,
        AlwaysVisibleNoMirror = 21, //<----------- Not used in Vanilla
        Event = 22,
        DesktopOnly = 23, //<----------- Not used in Vanilla
        HmdOnly = 24, //<----------- Not used in Vanilla
        FixMRAlpha = 25,
        AlwaysVisible = 26, //<----------- Not used in Vanilla
        DontShowInExternalMRCamera = 27,
        PlayersPlace = 28,
        Skybox = 29,
        MRForegroundClipPlane = 30,
        Reserved = 31,
    }
}