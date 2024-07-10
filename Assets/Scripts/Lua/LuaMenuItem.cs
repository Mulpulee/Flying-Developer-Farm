//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEditor;
//using System.IO;

//public class LuaMenuItem : MonoBehaviour
//{
//#if UNITY_EDITOR
//    //[MenuItem("Assets/Create/Lua Script")]
//    private static void CrteateLuaScript()
//    {
//        string path = AssetDatabase.GetAssetPath(Selection.activeObject);

//        if (string.IsNullOrEmpty(path))
//            path = "Assets";

//        string filename = "NewLuaScaript.lua";

//        string filePath = Path.Combine(path, filename);

//        File.Create(filePath).Dispose();

//        AssetDatabase.Refresh();
//    }
//#endif
//}
