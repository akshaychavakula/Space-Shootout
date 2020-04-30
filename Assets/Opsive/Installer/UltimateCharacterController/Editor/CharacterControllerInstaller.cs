/// ---------------------------------------------
/// Ultimate Character Controller
/// Copyright (c) Opsive. All Rights Reserved.
/// https://www.opsive.com
/// ---------------------------------------------

namespace Opsive.UltimateCharacterController.Editor
{
    using System;
    using System.IO;
    using UnityEditor;
    using UnityEngine;

    /// <summary>
    /// Creates a window which ensures the system is compatible with the latest version.
    /// </summary>
    [InitializeOnLoad]
    public class CharacterControllerInstaller : EditorWindow
    {
        private const string c_PackageGUID = "ff39a062419ff8c4da1bfb69c8dea35d";
        private const string c_CharacterLocomotionGUID = "472740fcb9be66341b68ccd04c5cf8da";
        private const string c_DarkSuccessTextureGUID = "2d48e8627e5fcdf40bc1f0759570ca61";
        private const string c_LightSuccessTextureGUID = "7cb22900eb3ee0144b56b008cb7fbe44";
        private const string c_DarkFailureTextureGUID = "a68f8c9042dadf048912bfd8f0c88018";
        private const string c_LightFailureTextureGUID = "05bc7a74b5f6c6c4390f27a6121cf08e";

        private static GUIStyle s_BoldLabel;
        private static GUIStyle BoldLabel {
            get {
                if (s_BoldLabel == null) {
                    s_BoldLabel = new GUIStyle(EditorStyles.largeLabel);
                    s_BoldLabel.fontStyle = FontStyle.Bold;
                }
                return s_BoldLabel;
            }
        }
        private static GUIStyle s_LinkStyle;
        private static GUIStyle LinkStyle {
            get {
                if (s_LinkStyle == null) {
                    s_LinkStyle = new GUIStyle("Label");
                    s_LinkStyle.normal.textColor = (EditorGUIUtility.isProSkin ? new Color(0.8f, 0.8f, 1.0f, 1.0f) : Color.blue);
                }
                return s_LinkStyle;
            }
        }
        private static Texture2D s_SuccessTexture;
        private static Texture2D SuccessTexture {
            get {
                if (s_SuccessTexture == null) {
                    s_SuccessTexture = AssetDatabase.LoadAssetAtPath<Texture2D>(AssetDatabase.GUIDToAssetPath(EditorGUIUtility.isProSkin ? c_DarkSuccessTextureGUID : c_LightSuccessTextureGUID));
                }
                return s_SuccessTexture;
            }
        }
        private static Texture2D s_FailureTexture;
        private static Texture2D FailureTexture {
            get {
                if (s_FailureTexture == null) {
                    s_FailureTexture = AssetDatabase.LoadAssetAtPath<Texture2D>(AssetDatabase.GUIDToAssetPath(EditorGUIUtility.isProSkin ? c_DarkFailureTextureGUID : c_LightFailureTextureGUID));
                }
                return s_FailureTexture;
            }
        }

        /// <summary>
        /// Perform editor checks as soon as the scripts are done compiling.
        /// </summary>
        static CharacterControllerInstaller()
        {
            EditorApplication.update += EditorStartup;
        }

        /// <summary>
        /// Shows the installer.
        /// </summary> 
        [MenuItem("Tools/Opsive/Ultimate Character Controller/Installer", false, -1)]
        public static CharacterControllerInstaller ShowWindow()
        {
            return EditorWindow.GetWindow<CharacterControllerInstaller>(false, "Installer");
        }

        /// <summary>
        /// Show the editor window if it hasn't been shown before.
        /// </summary>
        private static void EditorStartup()
        {
            if (EditorApplication.isCompiling) {
                return;
            }

            if (!EditorPrefs.GetBool("Opsive.UltimateCharacterController.Editor.InstallerWindowShown", false)) {
                EditorPrefs.SetBool("Opsive.UltimateCharacterController.Editor.InstallerWindowShown", true);
                ShowWindow();
            }

            EditorApplication.update -= EditorStartup;
        }

        /// <summary>
        /// Display the UI.
        /// </summary>
        private void OnGUI()
        {
            GUILayout.Label("Requirements", BoldLabel);
            var packagePath = Application.dataPath.Remove(Application.dataPath.Length - 6) + AssetDatabase.GUIDToAssetPath(c_PackageGUID);
            var validInstallPackage = !string.IsNullOrEmpty(packagePath) && File.Exists(packagePath);
            DrawRequirement("Located Install Package", validInstallPackage);
#if UNITY_2018_4_OR_NEWER || UNITY_2019_1
            var validUnityVersion = true;
#else
            var validUnityVersion= false;
#endif
            DrawRequirement("Unity 2018.4 or Newer", validUnityVersion);
#if UNITY_2019_3_OR_NEWER
            var validScriptingRuntime = true;
#else
            var validScriptingRuntime = PlayerSettings.scriptingRuntimeVersion == ScriptingRuntimeVersion.Latest;
#endif
            DrawRequirement(".NET 4.x Scripting Runtime", validScriptingRuntime);
            var height = 165;
            if (!validScriptingRuntime) {
                EditorGUILayout.HelpBox("Version 2.2 requires the .NET 4 framework. This can be changed in Unity's Player Settings editor.", MessageType.Warning);
                height += 45;
            }

            // If the user is updating from 2.0-2.1 then a dialog should warn about the major changes.
            var requireCleanInstall = true;
            var priorVersionInstalled = false;
            var locomotionLocation = AssetDatabase.GUIDToAssetPath(c_CharacterLocomotionGUID);
            if (!string.IsNullOrEmpty(locomotionLocation) && File.Exists(locomotionLocation)) {
                var assetInfoType = Type.GetType("Opsive.UltimateCharacterController.Utility.AssetInfo, Opsive.UltimateCharacterController");
                if (assetInfoType != null) {
                    var versionProperty = assetInfoType.GetProperty("Version", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);
                    if (versionProperty != null) {
                        var versionString = versionProperty.GetValue(null, null) as string;
                        var version = new Version(versionString);
                        if (version.CompareTo(new Version("2.2")) < 0) {
                            priorVersionInstalled = true;
                        } else {
                            requireCleanInstall = false;
                        }
                    }
                }
            }
            if (requireCleanInstall) {
                height += 15;
                DrawRequirement("Clean Install", !priorVersionInstalled);
                if (priorVersionInstalled) {
                    EditorGUILayout.HelpBox("Version 2.2 contains major structural changes. Before importing remove the Opsive/UltimateCharacterController folder. " +
                                            "Ensure you have first moved your own assets out of that folder and have made a backup.", MessageType.Warning);
                    height += 45;
                }
            }

            GUILayout.Label("Required Changes", BoldLabel);
            GUILayout.Label("Updates from versions prior to 2.2 require manual changes in order to upgrade your character.");
            GUILayout.BeginHorizontal();
            GUILayout.Label("See");
            GUILayout.Space(-3);
            if (GUILayout.Button("this page", LinkStyle, GUILayout.Width(55))) {
                Application.OpenURL("https://opsive.com/support/documentation/ultimate-character-controller/getting-started/version-2-2-update-guide/");
            }
            GUILayout.Space(-1);
            GUILayout.Label("for more information.");
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.Space(5);
            GUI.enabled = validUnityVersion && validScriptingRuntime && validInstallPackage && !priorVersionInstalled;
            if (GUILayout.Button("Install")) {
                Install();
            }
            GUI.enabled = true;
#if UNITY_2019_3_OR_NEWER
            height += 6;
#endif
            minSize = maxSize = new Vector2(620, height);
        }

        /// <summary>
        /// Draws the requirement line.
        /// </summary>
        /// <param name="text">The text that should be drawn.</param>
        /// <param name="success">Did the requirement pass?</param>
        private void DrawRequirement(string text, bool success)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label(success ? SuccessTexture : FailureTexture, GUILayout.Width(20));
            GUILayout.Space(-7);
            GUILayout.BeginVertical();
            GUILayout.Space(-1);
            GUILayout.Label(text);
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
        }

        /// <summary>
        /// Install the update.
        /// </summary>
        private void Install()
        {
            var packagePath = AssetDatabase.GUIDToAssetPath(c_PackageGUID);
            if (string.IsNullOrEmpty(packagePath)) {
                return;
            }
            AssetDatabase.ImportPackage(packagePath, true);
            AssetDatabase.Refresh();
        }
    }
}