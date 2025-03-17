using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace MuziCharacter
{
    // [CustomEditor(typeof(DataPackageForServer))]
    public class DataPackageForServerEditor : Editor
    {
        private GUIStyle _headerStyle;

        public override void OnInspectorGUI()
        {
            // _headerStyle ??= new GUIStyle()
            // {
            //     fontSize = 16,
            //     fontStyle = FontStyle.Bold
            // };
            // _headerStyle.normal.textColor = Color.white;
            // base.OnInspectorGUI();
            // var script = (DataPackageForServer)target;
            //
            // GUILayout.Space(10);
            // GUILayout.Label("Send Data To Server", _headerStyle);
            // if (GUILayout.Button("Step 1: Scan and Create Package Config", GUILayout.Height(20)))
            // {
            //     script.ScanResourceAndCreateConfig();
            // }
            //
            // if (GUILayout.Button("Step 2: Create YAML File", GUILayout.Height(20)))
            // {
            //     script.CreateYAMLFile();
            // }
            //
            // if (GUILayout.Button("Step 3: Create Zip File", GUILayout.Height(20)))
            // {
            //     script.CreateZipFile();
            // }
            //
            // if (GUILayout.Button("Step 4: Upload To Server", GUILayout.Height(20)))
            // {
            //     script.UploadToServer();
            // }
            // GUILayout.Space(10);
            // GUILayout.Label("Make local resource reference", _headerStyle);
            // if (GUILayout.Button("Initialize Local Resource Reference", GUILayout.Height(20)))
            // {
            //     script.CreateLocalResource();
            // }
        }
    }
}