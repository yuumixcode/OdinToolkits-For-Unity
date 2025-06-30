using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEngine;
using Yuumix.OdinToolkits.Modules.LearnArchive.EditorExtensions.Editor_API_Usage_Guide._10.UnityAssetDatabase.Editor;
using Yuumix.OdinToolkits.Modules.LearnArchive.EditorExtensions.Editor_API_Usage_Guide._11.UnityPrefabUtility.Editor;
using Yuumix.OdinToolkits.Modules.LearnArchive.EditorExtensions.Editor_API_Usage_Guide._12.UnityEditorApplication.Editor;
using Yuumix.OdinToolkits.Modules.LearnArchive.EditorExtensions.Editor_API_Usage_Guide._13.UnityCompilationPipeline.Editor;
using Yuumix.OdinToolkits.Modules.LearnArchive.EditorExtensions.Editor_API_Usage_Guide._9.UnityEditorUtility.Editor;
using Yuumix.OdinToolkits.Modules.LearnArchive.OdinEditorWindow.Editor;

namespace Yuumix.OdinToolkits.Modules.LearnArchive.EditorExtensions.Editor_API_Usage_Guide.General.Editor
{
    public class EditorAPILearnMenuEditorWindow : OdinMenuEditorWindow
    {
        static void ShowWindow()
        {
            var window = GetWindow<EditorAPILearnMenuEditorWindow>();
            window.titleContent = new GUIContent("EditorAPILearnMenuEditorWindow");
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(900, 650);
            window.Show();
        }

        #region Windows

        ShowToastLearnWindow _showToastLearnWindow;
        UnityEditorUtilityAPI _unityEditorUtilityAPI;
        UnityAssetDatabaseAPI _unityAssetDatabaseAPI;
        UnityPrefabUtilityAPI _unityPrefabUtilityAPI;
        UnityEditorApplicationAPI _unityEditorApplicationAPI;
        UnityCompilationPipelineAPI _unityCompilationPipelineAPI;

        #endregion

        protected override void OnEnable()
        {
            base.OnEnable();
            MenuWidth = 220;
            WindowPadding = new Vector4(7, 7, 7, 7);
        }

        protected override void Initialize()
        {
            base.Initialize();
            _showToastLearnWindow = CreateInstance<ShowToastLearnWindow>();
            // ---
            _unityEditorUtilityAPI = CreateInstance<UnityEditorUtilityAPI>();
            _unityEditorUtilityAPI.OwnerWindow = this;
            // ---
            _unityAssetDatabaseAPI = CreateInstance<UnityAssetDatabaseAPI>();
            _unityAssetDatabaseAPI.OwnerWindow = this;
            // ---
            _unityPrefabUtilityAPI = CreateInstance<UnityPrefabUtilityAPI>();
            _unityPrefabUtilityAPI.OwnerWindow = this;
            // ---
            _unityEditorApplicationAPI = CreateInstance<UnityEditorApplicationAPI>();
            _unityEditorApplicationAPI.OwnerWindow = this;
            // ---
            _unityCompilationPipelineAPI = CreateInstance<UnityCompilationPipelineAPI>();
            _unityCompilationPipelineAPI.OwnerWindow = this;
            // ---
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            _showToastLearnWindow = null;
            // --- 
            _unityEditorUtilityAPI.OwnerWindow = null;
            _unityEditorUtilityAPI = null;
            // ---
            _unityAssetDatabaseAPI.OwnerWindow = null;
            _unityAssetDatabaseAPI = null;
            // ---
            _unityPrefabUtilityAPI.OwnerWindow = null;
            _unityPrefabUtilityAPI = null;
            // ---
            _unityEditorApplicationAPI.OwnerWindow = null;
            _unityEditorApplicationAPI = null;
            // ---
            _unityCompilationPipelineAPI.OwnerWindow = null;
            _unityCompilationPipelineAPI = null;
            // ---
        }

        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree(true)
            {
                { "OdinEditorWindow/ShowToast 方法", _showToastLearnWindow },
                { "Unity 内置/EditorUtility API 简易版", _unityEditorUtilityAPI },
                { "Unity 内置/AssetDatabase API 简易版", _unityAssetDatabaseAPI },
                { "Unity 内置/PrefabUtility API 简易版", _unityPrefabUtilityAPI },
                { "Unity 内置/EditorApplication API 简易版", _unityEditorApplicationAPI },
                { "Unity 内置/CompilationPipeline API 简易版", _unityCompilationPipelineAPI },
            };
            tree.SortMenuItemsByName();
            return tree;
        }
    }
}
