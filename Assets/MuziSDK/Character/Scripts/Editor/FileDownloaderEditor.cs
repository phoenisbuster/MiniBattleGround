using MuziCharacter;
using UnityEditor;
using UnityEngine;


[RequireComponent(typeof(FileDownloader))]
public class FileDownloaderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
#if UNITY_EDITOR_OSX
        var script =  (FileDownloader) target;
        
        if (GUILayout.Button("Open Local Resource Path"))
        {
            script.RevealInFinder();
        }
#endif
    }
}
