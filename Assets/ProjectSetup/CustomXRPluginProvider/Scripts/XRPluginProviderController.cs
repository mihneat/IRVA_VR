#if UNITY_EDITOR // Editor script only. Build fails if removed!

using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEditor.XR.Management;
using UnityEditor.XR.Management.Metadata;
using UnityEngine;
using UnityEngine.XR.Management;

namespace ProjectSetup.CustomXRPluginProvider.Scripts
{
    /// <summary>
    /// Logic related to XR plug-in management provider setup for either Android (Cardboard XR, Meta XR), or Standalone (SteamVR, Oculus Link).
    /// </summary>
    public class XRPluginProviderController : MonoBehaviour, IPreprocessBuildWithReport
    {
        [SerializeField] private Utils.TargetVR targetVR;

        private BuildTargetGroup buildTargetGroup = BuildTargetGroup.Unknown;
        private XRGeneralSettings xrGeneralSettings;
        public int callbackOrder { get; }

        /// <summary>
        /// Called when an inspector field changes (in this script, when `targetVR` is changed).<br/>
        /// </summary>
        private void OnValidate() => SetupAllXRPluginProviders();

        /// <summary>
        /// Called when a build is started. Sets the Android XR plug-in provider.
        /// </summary>
        /// <param name="report">Unused.</param>
        public void OnPreprocessBuild(BuildReport report) => 
            SetupXRPluginProvider(BuildTargetGroup.Android, Utils.AndroidLoaderTargetDict);
        
        private void SetupAllXRPluginProviders()
        {
            SetupXRPluginProvider(BuildTargetGroup.Standalone, Utils.StandaloneLoaderTargetDict);
            SetupXRPluginProvider(BuildTargetGroup.Android, Utils.AndroidLoaderTargetDict);
        }

        /// <summary>
        /// Sets the XR plug-in management provider as such:<br/>
        /// - `BuildTargetGroup.Standalone` for SteamVR, Quest Link.<br/>
        /// - `BuildTargetGroup.Android` for Cardboard XR, Meta XR<br/>
        /// </summary>
        /// <param name="btg">Target platform group.</param>
        /// <param name="loaderTargetDict">Dictionary which contains loader settings.</param>
        private void SetupXRPluginProvider(
            BuildTargetGroup btg, 
            IReadOnlyDictionary<Utils.TargetVR, string> loaderTargetDict)
        {
            if(Application.isPlaying || BuildPipeline.isBuildingPlayer)
            {
                return;
            }
            
            GetSettingsForBuildTarget(btg);
            loaderTargetDict.TryGetValue(targetVR, out var requiredLoader);
            
            if(IsLoaderAlreadySet(requiredLoader))
            {
                return;
            }

            switch (buildTargetGroup)
            {
                case BuildTargetGroup.Standalone:
                    // Remove all standalone loaders.
                    RemoveLoader(Utils.XR_OCULUS_LOADER);
                    RemoveLoader(Utils.XR_OPENVR_LOADER);
                    RemoveLoader(Utils.XR_OPENXR_LOADER);
                    break;
                case BuildTargetGroup.Android:
                    // Remove all Android loaders.
                    RemoveLoader(Utils.XR_ARCORE_LOADER);
                    RemoveLoader(Utils.XR_OCULUS_LOADER);
                    RemoveLoader(Utils.XR_CRDBRD_LOADER);
                    break;
                default: Debug.LogWarning($"[XRPluginSceneController] Build target group {buildTargetGroup} not supported!");
                    break;
            }
            
            AssignLoader(requiredLoader);
            
            Debug.Log($"[XRPluginSceneController] Set loader {requiredLoader} for target {buildTargetGroup}.");
        }

        // Check if loader attempted to be set is already contained in the active loader list.
        private bool IsLoaderAlreadySet(string loaderStr) => 
            xrGeneralSettings.AssignedSettings.activeLoaders.Any(loader => loader.ToString().Contains(loaderStr));

        private void GetSettingsForBuildTarget(BuildTargetGroup btg)
        {
            EditorBuildSettings.TryGetConfigObject(XRGeneralSettings.k_SettingsKey, out XRGeneralSettingsPerBuildTarget buildTargetSettings);
            xrGeneralSettings = buildTargetSettings.SettingsForBuildTarget(buildTargetGroup = btg);
        }

        private void AssignLoader(string loader) => XRPackageMetadataStore.AssignLoader(xrGeneralSettings.Manager, loader, buildTargetGroup);
        private void RemoveLoader(string loader) => XRPackageMetadataStore.RemoveLoader(xrGeneralSettings.Manager, loader, buildTargetGroup);
    }
}

#endif