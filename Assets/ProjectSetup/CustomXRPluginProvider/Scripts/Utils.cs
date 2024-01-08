using System.Collections.Generic;

namespace ProjectSetup.CustomXRPluginProvider.Scripts
{
    /// <summary>
    /// XR plug-in management provider helpers.
    /// </summary>
    public static class Utils
    {
        public const string XR_ARCORE_LOADER = "UnityEngine.XR.ARCore.ARCoreLoader";
        public const string XR_OPENVR_LOADER = "Unity.XR.OpenVR.OpenVRLoader";
        public const string XR_OPENXR_LOADER = "UnityEngine.XR.OpenXR.OpenXRLoader";
        public const string XR_OCULUS_LOADER = "Unity.XR.Oculus.OculusLoader";
        public const string XR_CRDBRD_LOADER = "Google.XR.Cardboard.XRLoader";

        public static readonly Dictionary<TargetVR, string> StandaloneLoaderTargetDict = new()
        {
            { TargetVR.CardboardXR, XR_OCULUS_LOADER},  // Default to Oculus.
            { TargetVR.SteamVR    , XR_OPENVR_LOADER},
            { TargetVR.MetaXR     , XR_OCULUS_LOADER}
        };        
        public static readonly Dictionary<TargetVR, string> AndroidLoaderTargetDict = new()
        {
            { TargetVR.CardboardXR, XR_CRDBRD_LOADER}, 
            { TargetVR.SteamVR    , XR_OCULUS_LOADER},   // Default to Oculus.
            { TargetVR.MetaXR     , XR_OCULUS_LOADER}
        };
    
        public enum TargetVR
        {
            CardboardXR = 0,
            SteamVR = 1,
            MetaXR = 2,
        }
    }
}
