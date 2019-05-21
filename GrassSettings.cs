using System;
using TheForest.Utils;
using UnityEngine;

namespace BetterSettings
{
    //public class GrassSettings : GrassModeManager
    //{
    //    public static GrassSettings instance;
    //    protected override void Awake()
    //    {
    //        if (GrassModeManager._instance != 
    //        {
    //            //UnityEngine.Object.Destroy(;
    //            ModAPI.Console.Write("aint it chief");
    //            return;
    //        }
    //        GrassModeManager._instance = 
    //        ActiveMode =  TheForestQualitySettings.GrassModes.CPU;
    //         TheForestQualitySettings.GrassModes activeMode = ActiveMode;
    //        if (activeMode !=  TheForestQualitySettings.GrassModes.CPU)
    //        {
    //            if (activeMode ==  TheForestQualitySettings.GrassModes.GPU)
    //            {
    //                InitializeGpu();
    //            }
    //        }
    //        else
    //        {
    //            InitializeCpu();
    //        }
    //        _initFrame = Time.renderedFrameCount;
    //        instance = 
    //        ModAPI.Console.Write("Initialized");

    //    }


    //}

    public class SettingMenu : MonoBehaviour
    {
        [ModAPI.Attributes.ExecuteOnGameStart]
        public static void Init()
        {
            new GameObject("__CUSTOM SETTINGS OBJ__").AddComponent<SettingMenu>();
            ModAPI.Console.Write("Initialized");
        }

        private Terrain TerrainSource;
        private float rr;
        private bool inPauseMenu = false;
        private float GrassDistance = 200f;
        private float GrassDensity = 1f;
        private float GrassHeight = 2.5f;
        private float GrassWidth = 1.75f;
        DetailPrototype[] _backupDetailPrototype;

        private void Start()
        {
            rr = Screen.height / 1080f;
            Terrain[] T = FindObjectsOfType<Terrain>();
            foreach (Terrain t in T)
            {
                //ModAPI.Console.Write(t.ToString());
                if (t.name.StartsWith("Main"))
                {
                    TerrainSource = t;
                }
            }

            _backupDetailPrototype = (DetailPrototype[])TerrainSource.terrainData.detailPrototypes.Clone();
            ApplyChanges();
        }

        private void Update()
        {
            inPauseMenu = LocalPlayer.IsInPauseMenu;
        }

        private void OnGUI()
        {
            if (inPauseMenu)
            {
                if (GUI.Button(new Rect(Screen.width - 400 * rr, 0, 400 * rr, 45 * rr), "APPLY"))
                {
                    ApplyChanges();
                }
                GUI.Label(new Rect(Screen.width - 400 * rr, 140 * rr, 400 * rr, 20 * rr), "Grass Distance - " + GrassDistance +"m");
                GrassDistance = GUI.HorizontalSlider(new Rect(Screen.width - 400 * rr, 160 * rr, 400 * rr, 20 * rr), GrassDistance, 0, 500);

                GUI.Label(new Rect(Screen.width - 400 * rr, 200 * rr, 400 * rr, 20 * rr), "Grass Density - " + GrassDensity * 100 + "%");
                GrassDensity = GUI.HorizontalSlider(new Rect(Screen.width - 400 * rr, 220 * rr, 400 * rr, 20 * rr), GrassDensity, 0, 2);

                GUI.Label(new Rect(Screen.width - 400 * rr, 260 * rr, 400 * rr, 20 * rr), "Grass Height - " + GrassHeight * 100 + "%");
                GrassHeight = GUI.HorizontalSlider(new Rect(Screen.width - 400 * rr, 280 * rr, 400 * rr, 20 * rr), GrassHeight, 0, 50);

                GUI.Label(new Rect(Screen.width - 400 * rr, 320 * rr, 400 * rr, 20 * rr), "Grass Width - " + GrassWidth * 100 + "%");
                GrassWidth = GUI.HorizontalSlider(new Rect(Screen.width - 400 * rr, 340 * rr, 400 * rr, 20 * rr), GrassWidth, 0, 50);
            }
        }

        private void ApplyChanges()
        {
            try
            {
                SetGrass(GrassDistance, GrassDensity);
                SetDetails();
            }
            catch (Exception e)
            {

                ModAPI.Console.Write(e.ToString());

            }

        }

        private void SetDetails()
        {
            DetailPrototype[] details = TerrainSource.terrainData.detailPrototypes;

            for (int i = 0; i < details.Length; i++)
            {
                details[i].minHeight = _backupDetailPrototype[i].minHeight * GrassHeight;
                details[i].maxHeight = _backupDetailPrototype[i].maxHeight * GrassHeight;
                details[i].minWidth = _backupDetailPrototype[i].minWidth * GrassWidth;
                details[i].maxWidth = _backupDetailPrototype[i].maxWidth * GrassWidth;
            }

            TerrainSource.terrainData.detailPrototypes = details;
        }


        public void SetGrass(float dist, float dens)
        {
            TerrainSource.detailObjectDistance = dist;
            TerrainSource.detailObjectDensity = dens;
            ModAPI.Console.Write(TerrainSource.ToString());
        }
    }

    //public class OceanMod : ImageEffectOptimizer
    //{
    //    protected override void Update()
    //    {
    //        {
    //            if (postProcessingProfile)
    //            {
    //                UnityEngine.PostProcessing.ColorGradingModel.Settings settings = postProcessingProfile.colorGrading.settings;
    //                float num =  PlayerPreferences.GammaWorldAndDay;
    //                float contrast =  PlayerPreferences.Contrast;
    //                if ( Clock.Dark || TheForest.Utils.LocalPlayer.IsInCaves)
    //                {
    //                    num =  PlayerPreferences.GammaCavesAndNight;
    //                }
    //                bool flag = TheForest.Utils.LocalPlayer.Inventory != null && TheForest.Utils.LocalPlayer.Inventory.CurrentView == TheForest.Items.Inventory.PlayerInventory.PlayerViews.Pause;
    //                if (flag || float.IsNaN(settings.basic.postExposure) || float.IsInfinity(settings.basic.postExposure) || Mathf.Abs(num - settings.basic.postExposure) < 0.001f)
    //                {
    //                    if (!settings.basic.postExposure.Equals(num))
    //                    {
    //                        settings.basic.postExposure = num;
    //                    }
    //                }
    //                else
    //                {
    //                    settings.basic.postExposure = Mathf.SmoothDamp(settings.basic.postExposure, num, ref gammaVelocity, 3f);
    //                }
    //                if (flag || float.IsNaN(settings.basic.contrast) || float.IsInfinity(settings.basic.contrast) || Mathf.Abs(contrast - settings.basic.contrast) < 0.001f)
    //                {
    //                    if (!settings.basic.contrast.Equals(contrast))
    //                    {
    //                        settings.basic.contrast = contrast;
    //                    }
    //                }
    //                else
    //                {
    //                    settings.basic.contrast = Mathf.SmoothDamp(settings.basic.contrast, contrast, ref contrastVelocity, 1f);
    //                }
    //                postProcessingProfile.colorGrading.settings = settings;
    //            }
    //            if (farShadowCascade)
    //            {
    //                bool flag2 =  TheForestQualitySettings.UserSettings.FarShadowMode ==  TheForestQualitySettings.FarShadowModes.On && !TheForest.Utils.LocalPlayer.IsInClosedArea;
    //                if (farShadowCascade.enableFarShadows != flag2)
    //                {
    //                    farShadowCascade.enableFarShadows = flag2;
    //                }
    //            }
    //            if (postProcessingProfile &&  PlayerPreferences.ColorGrading >= 0 &&  PlayerPreferences.ColorGrading < AmplifyColorGradients.Length)
    //            {
    //                postProcessingProfile.userLut.enabled = true;
    //                UnityEngine.PostProcessing.UserLutModel.Settings settings2 = postProcessingProfile.userLut.settings;
    //                settings2.lut = AmplifyColorGradients[ PlayerPreferences.ColorGrading];
    //                postProcessingProfile.userLut.settings = settings2;
    //            }
    //            if (frostEffect)
    //            {
    //                frostEffect.enabled = (frostEffect.coverage > 0f);
    //            }
    //            if (bleedEffect)
    //            {
    //                if (Application.isPlaying)
    //                {
    //                    bleedEffect.enabled = ( BleedBehavior.BloodAmount > 0f);
    //                }
    //                else
    //                {
    //                    bleedEffect.enabled = (bleedEffect.TestingBloodAmount > 0f);
    //                }
    //            }
    //             TheForestQualitySettings.AntiAliasingTechnique antiAliasingTechnique =  TheForestQualitySettings.UserSettings.AntiAliasing;
    //            if (SystemInfo.systemMemorySize <= 4096 ||  PlayerPreferences.LowMemoryMode)
    //            {
    //                antiAliasingTechnique =  TheForestQualitySettings.AntiAliasingTechnique.None;
    //            }
    //            if (postProcessingProfile)
    //            {
    //                bool flag3 = ! ForestVR.Enabled && antiAliasingTechnique !=  TheForestQualitySettings.AntiAliasingTechnique.None;
    //                postProcessingProfile.antialiasing.enabled = flag3;
    //                if (flag3)
    //                {
    //                    UnityEngine.PostProcessing.AntialiasingModel.Settings settings3 = postProcessingProfile.antialiasing.settings;
    //                    if (antiAliasingTechnique !=  TheForestQualitySettings.AntiAliasingTechnique.FXAA)
    //                    {
    //                        if (antiAliasingTechnique ==  TheForestQualitySettings.AntiAliasingTechnique.TAA)
    //                        {
    //                            settings3.method = UnityEngine.PostProcessing.AntialiasingModel.Method.Taa;
    //                        }
    //                    }
    //                    else
    //                    {
    //                        settings3.method = UnityEngine.PostProcessing.AntialiasingModel.Method.Fxaa;
    //                    }
    //                    postProcessingProfile.antialiasing.settings = settings3;
    //                }
    //            }
    //            if (vClouds)
    //            {
    //                if (TheForest.Utils.LocalPlayer.CurrentView == TheForest.Items.Inventory.PlayerInventory.PlayerViews.PlaneCrash ||  ForestVR.Enabled)
    //                {
    //                    vClouds.enabled = false;
    //                }
    //                else if ( TheForestQualitySettings.UserSettings.volumetricClouds ==  TheForestQualitySettings.VolumetricClouds.On)
    //                {
    //                    vClouds.enabled = true;
    //                }
    //                else
    //                {
    //                    vClouds.enabled = false;
    //                }
    //            }
    //            if ( ForestVR.Enabled && aa)
    //            {
    //                aa.enabled = ( PlayerPreferences.VRAntiAliasing !=  PlayerPreferences.VRAntiAliasingTypes.OFF);
    //                if (aa.enabled)
    //                {
    //                    aa.method = (( PlayerPreferences.VRAntiAliasing !=  PlayerPreferences.VRAntiAliasingTypes.SMAA) ? 1 : 0);
    //                }
    //            }
    //            if (postProcessingProfile)
    //            {
    //                bool flag4 = ! ForestVR.Enabled && !SkipMotionBlur &&  TheForestQualitySettings.UserSettings.MotionBlur !=  TheForestQualitySettings.MotionBlurQuality.None;
    //                postProcessingProfile.motionBlur.enabled = flag4;
    //                if (flag4)
    //                {
    //                    UnityEngine.PostProcessing.MotionBlurModel.Settings settings4 = postProcessingProfile.motionBlur.settings;
    //                    switch ( TheForestQualitySettings.UserSettings.MotionBlur)
    //                    {
    //                        case  TheForestQualitySettings.MotionBlurQuality.Low:
    //                            settings4.sampleCount = 4;
    //                            break;
    //                        case  TheForestQualitySettings.MotionBlurQuality.Medium:
    //                            settings4.sampleCount = 8;
    //                            break;
    //                        case  TheForestQualitySettings.MotionBlurQuality.High:
    //                            settings4.sampleCount = 16;
    //                            break;
    //                        case  TheForestQualitySettings.MotionBlurQuality.Ultra:
    //                            settings4.sampleCount = 32;
    //                            break;
    //                    }
    //                    postProcessingProfile.motionBlur.settings = settings4;
    //                }
    //            }
    //            if (postProcessingProfile)
    //            {
    //                bool flag5 = ! ForestVR.Enabled &&  TheForestQualitySettings.UserSettings.screenSpaceReflection ==  TheForestQualitySettings.ScreenSpaceReflection.On;
    //                flag5 = (flag5 && TheForest.Utils.LocalPlayer.IsInEndgame);
    //                postProcessingProfile.screenSpaceReflection.enabled = flag5;
    //            }
    //            if (postProcessingProfile)
    //            {
    //            }
    //            if (postProcessingProfile)
    //            {
    //                bool enabled = ! ForestVR.Enabled &&  TheForestQualitySettings.UserSettings.Fg ==  TheForestQualitySettings.FilmGrain.Normal;
    //                postProcessingProfile.grain.enabled = enabled;
    //            }
    //            if (postProcessingProfile)
    //            {
    //                bool enabled2 = ! ForestVR.Enabled &&  TheForestQualitySettings.UserSettings.CA ==  TheForestQualitySettings.ChromaticAberration.Normal;
    //                postProcessingProfile.chromaticAberration.enabled = enabled2;
    //            }
    //            if (postProcessingProfile)
    //            {
    //                bool enabled3 = ! ForestVR.Enabled &&  TheForestQualitySettings.UserSettings.SEBloom ==  TheForestQualitySettings.SEBloomTechnique.Normal;
    //                postProcessingProfile.bloom.enabled = enabled3;
    //            }
    //            if (Application.isPlaying)
    //            {
    //                if (!SunshineAtmosCam)
    //                {
    //                    SunshineAtmosCam = base.GetComponent< TheForestAtmosphereCamera>();
    //                    if (!SunshineAtmosCam)
    //                    {
    //                        SunshineAtmosCam = base.gameObject.AddComponent< TheForestAtmosphereCamera>();
    //                    }
    //                }
    //                SunshineCam.enabled = true;
    //                SunshinePP.enabled = true;
    //                if ( Sunshine.Instance)
    //                {
    //                     Sunshine.Instance.enabled = ( ImageEffectOptimizer.AllowSunshine && (! ForestVR.Enabled || TheForest.Utils.LocalPlayer.CurrentView != TheForest.Items.Inventory.PlayerInventory.PlayerViews.Pause));
    //                     Sunshine.Instance.ScatterResolution =  TheForestQualitySettings.UserSettings.ScatterResolution;
    //                     Sunshine.Instance.ScatterSamplingQuality =  TheForestQualitySettings.UserSettings.ScatterSamplingQuality;
    //                    if (SunshineOccluders.value != 0)
    //                    {
    //                        bool flag6 =  TheForestQualitySettings.UserSettings.SunshineOcclusion ==  TheForestQualitySettings.SunshineOcclusionOn.On || TheForest.Utils.LocalPlayer.IsInCaves;
    //                         Sunshine.Instance.Occluders = ((!flag6) ? 0 : SunshineOccluders.value);
    //                    }
    //                    else
    //                    {
    //                        SunshineOccluders =  Sunshine.Instance.Occluders;
    //                    }
    //                }
    //            }
    //             TheForestQualitySettings.SSAOTechnique ssaotechnique =  TheForestQualitySettings.UserSettings.SSAO;
    //            if (SystemInfo.systemMemorySize <= 4096 ||  PlayerPreferences.LowMemoryMode)
    //            {
    //                ssaotechnique =  TheForestQualitySettings.SSAOTechnique.Off;
    //            }
    //            if (amplifyOcclusion && ( TheForestQualitySettings.UserSettings.SSAOType ==  TheForestQualitySettings.SSAOTypes.AMPLIFY || !postProcessingProfile))
    //            {
    //                amplifyOcclusion.enabled = (! ForestVR.Enabled && ssaotechnique !=  TheForestQualitySettings.SSAOTechnique.Off);
    //                if (postProcessingProfile)
    //                {
    //                    postProcessingProfile.ambientOcclusion.enabled = false;
    //                }
    //                if (ssaotechnique !=  TheForestQualitySettings.SSAOTechnique.Ultra)
    //                {
    //                    if (ssaotechnique !=  TheForestQualitySettings.SSAOTechnique.High)
    //                    {
    //                        if (ssaotechnique ==  TheForestQualitySettings.SSAOTechnique.Low)
    //                        {
    //                            amplifyOcclusion.SampleCount =  AmplifyOcclusionBase.SampleCountLevel.Low;
    //                        }
    //                    }
    //                    else
    //                    {
    //                        amplifyOcclusion.SampleCount =  AmplifyOcclusionBase.SampleCountLevel.Medium;
    //                    }
    //                }
    //                else
    //                {
    //                    amplifyOcclusion.SampleCount =  AmplifyOcclusionBase.SampleCountLevel.High;
    //                }
    //            }
    //            else if (postProcessingProfile && ( TheForestQualitySettings.UserSettings.SSAOType ==  TheForestQualitySettings.SSAOTypes.UNITY || !amplifyOcclusion))
    //            {
    //                postProcessingProfile.ambientOcclusion.enabled = (! ForestVR.Enabled && ssaotechnique !=  TheForestQualitySettings.SSAOTechnique.Off);
    //                if (amplifyOcclusion)
    //                {
    //                    amplifyOcclusion.enabled = false;
    //                }
    //                UnityEngine.PostProcessing.AmbientOcclusionModel.Settings settings5 = postProcessingProfile.ambientOcclusion.settings;
    //                if (ssaotechnique !=  TheForestQualitySettings.SSAOTechnique.Ultra)
    //                {
    //                    if (ssaotechnique !=  TheForestQualitySettings.SSAOTechnique.High)
    //                    {
    //                        if (ssaotechnique ==  TheForestQualitySettings.SSAOTechnique.Low)
    //                        {
    //                            settings5.sampleCount = UnityEngine.PostProcessing.AmbientOcclusionModel.SampleCount.Lowest;
    //                        }
    //                    }
    //                    else
    //                    {
    //                        settings5.sampleCount = UnityEngine.PostProcessing.AmbientOcclusionModel.SampleCount.Low;
    //                    }
    //                }
    //                else
    //                {
    //                    settings5.sampleCount = UnityEngine.PostProcessing.AmbientOcclusionModel.SampleCount.Medium;
    //                }
    //                postProcessingProfile.ambientOcclusion.settings = settings5;
    //            }
    //            if (TheForest.Utils.LocalPlayer.WaterEngine)
    //            {
    //                if (TheForest.Utils.LocalPlayer.IsInClosedArea)
    //                {
    //                    TheForest.Utils.Scene.OceanFlat.SetActive(false);
    //                    TheForest.Utils.Scene.OceanCeto.SetActive(false);
    //                    CurrentOceanQuality = ( TheForestQualitySettings.OceanQualities)(-1);
    //                }
    //                else if ( TheForestQualitySettings.UserSettings.OceanQuality != CurrentOceanQuality)
    //                {
    //                    CurrentOceanQuality = ((! PlayerPreferences.is32bit) ?  TheForestQualitySettings.UserSettings.OceanQuality :  TheForestQualitySettings.OceanQualities.Flat);
    //                     TheForestQualitySettings.OceanQualities oceanQuality =  TheForestQualitySettings.UserSettings.OceanQuality;
    //                    if (oceanQuality !=  TheForestQualitySettings.OceanQualities.WaveDisplacementHigh)
    //                    {
    //                        if (oceanQuality !=  TheForestQualitySettings.OceanQualities.WaveDisplacementLow)
    //                        {
    //                            if (oceanQuality ==  TheForestQualitySettings.OceanQualities.Flat)
    //                            {
    //                                TheForest.Utils.Scene.OceanFlat.SetActive(true);
    //                                TheForest.Utils.Scene.OceanCeto.SetActive(false);
    //                                waterBlurCeto.enabled = false;
    //                            }
    //                        }
    //                        else
    //                        {
    //                            TheForest.Utils.Scene.OceanFlat.SetActive(false);
    //                            TheForest.Utils.Scene.OceanCeto.SetActive(true);
    //                            waterBlurCeto.enabled = true;
    //                            if (CetoTF.OceanQualitySettings.Instance != null)
    //                            {
    //                                CetoTF.OceanQualitySettings.Instance.QualityChanged(CetoTF.CETO_QUALITY_SETTING.LOW);
    //                            }
    //                        }
    //                    }
    //                    else
    //                    {
    //                        TheForest.Utils.Scene.OceanFlat.SetActive(false);
    //                        TheForest.Utils.Scene.OceanCeto.SetActive(true);
    //                        waterBlurCeto.enabled = true;
    //                        if (CetoTF.OceanQualitySettings.Instance != null)
    //                        {
    //                            CetoTF.OceanQualitySettings.Instance.QualityChanged(CetoTF.CETO_QUALITY_SETTING.HIGH);
    //                        }
    //                    }
    //                }
    //            }
    //            MyCamera.useOcclusionCulling =  ImageEffectOptimizer.AllowCulling;
    //            depthBuffer.enabled =  ImageEffectOptimizer.AllowDepthBuffer;
    //            ppb.enabled = ( ImageEffectOptimizer.AllowPostProcessing && (! ForestVR.Enabled || !TheForest.Utils.LocalPlayer.IsInPauseMenu));
    //            if ( ImageEffectOptimizer.ForceRenderingPath && MyCamera)
    //            {
    //                MyCamera.renderingPath =  ImageEffectOptimizer.ForcedRenderingPath;
    //            }
    //        }
    //    }
    //}
}
