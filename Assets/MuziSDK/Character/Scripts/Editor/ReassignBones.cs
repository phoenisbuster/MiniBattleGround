using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace MuziCharacter
{

    public class ReassignBones : EditorWindow
    {
        [MenuItem("Muziverse/Utils/Reassign Bones")]
        public static void OpenWindow()
        {
            var window = GetWindow<ReassignBones>();
            window.titleContent = new GUIContent("Bones");
        }
        private GUIContent statusContent = new GUIContent("Waiting...");
        private SkinnedMeshRenderer referenceMesh;
        private SkinnedMeshRenderer targetSkinnedMeshes;

        private bool includeInactive = true;
        private string statusText = "Waiting...";

        bool SamePart(string part1, string part2)
        {
            return false;
            //return (part1.Substring(0, part1.Length - 3) == part2.Substring(0, part2.Length - 3));
        }

        private void OnGUI()
        {
            referenceMesh = EditorGUILayout.ObjectField("Source", referenceMesh, typeof(SkinnedMeshRenderer), true) as SkinnedMeshRenderer;


            targetSkinnedMeshes = EditorGUILayout.ObjectField("Target", targetSkinnedMeshes, typeof(SkinnedMeshRenderer), true) as SkinnedMeshRenderer;
            includeInactive = EditorGUILayout.Toggle("Include Inactive", includeInactive);
            bool enabled = (targetSkinnedMeshes != null && referenceMesh != null);
            if (!enabled)
            {
                statusText = "Add a target SkinnedMeshRenderer and a root bone to process.";
            }
            GUI.enabled = enabled;
            if (GUILayout.Button("Assign Bones"))
            {
                targetSkinnedMeshes.bones = referenceMesh.bones;
                targetSkinnedMeshes.rootBone = referenceMesh.rootBone;
                statusText = "Done";
            }

            // Draw status because yeh why not?
            statusContent.text = statusText;
            EditorStyles.label.wordWrap = true;
            GUILayout.Label(statusContent);
        }
    }

}