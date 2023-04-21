using System;

namespace CameraUtils.Core {
    [Flags]
    public enum CameraFlags {
        None = 0,
        Desktop = 1,
        HMD = 2,
        Mirror = 4,
        FirstPerson = 8,
        ThirdPerson = 16,
        Composition = 32,
    }
}