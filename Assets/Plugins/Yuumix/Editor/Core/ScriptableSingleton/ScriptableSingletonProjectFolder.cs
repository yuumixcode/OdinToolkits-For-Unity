using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Yuumix.OdinToolkits.Editor.Core
{
    // ProjectFolder 是指这个文件所在的根目录是项目文件夹，是和 Assets 文件夹同级的目录，所以在编辑器中看不到它
    // 当获取这个静态单例对象时，会在对应目录生成文件
    [FilePath("ScriptableSingletonDemo/ProjectFolder.yaml", FilePathAttribute.Location.ProjectFolder)]
    public class ScriptableSingletonProjectFolder : ScriptableSingleton<ScriptableSingletonProjectFolder>
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
            Debug.Log("EditorCrossProjectStateFile State: " + JsonUtility.ToJson(this, true));
        }
    }
}
