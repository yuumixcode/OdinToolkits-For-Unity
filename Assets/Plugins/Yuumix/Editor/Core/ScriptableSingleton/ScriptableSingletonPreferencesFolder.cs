using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Yuumix.OdinToolkits.Editor.Core
{
    // PreferencesFolder 是指用户本地的文件夹，例如：Unity 5.x ，这个编辑器设置可以跨项目调用
    // 例如路径可能是：C:\Users\..\Unity\Editor\Preferences\ScriptableSingletonDemo\PreferencesFolder.yaml
    // 当获取这个静态单例对象时，会在对应目录生成文件
    [FilePath("ScriptableSingletonDemo/PreferencesFolder.yaml", FilePathAttribute.Location.PreferencesFolder)]
    public sealed class ScriptableSingletonPreferencesFolder : ScriptableSingleton<ScriptableSingletonPreferencesFolder>
    {
        public float number = 12;

        public List<string> stringList = new List<string>();

        public void ResetState()
        {
            number = 12;
            stringList.Clear();
        }

        public void Modify()
        {
            number += 2;
            stringList.Add("Modify" + number);

            Save(true);
            Debug.Log("Saved to: " + GetFilePath());
        }

        public static string GetFilePathPublic() => GetFilePath();

        public void SavePublic(bool saveAsText)
        {
            Save(saveAsText);
        }

        public void Log()
        {
            Debug.Log("EditorPreferenceFolderStateFile State: " + JsonUtility.ToJson(this, true));
        }
    }
}
