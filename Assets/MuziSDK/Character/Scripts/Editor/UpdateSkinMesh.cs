using UnityEngine;
using UnityEditor;

public class UpdateSkinMesh : EditorWindow
{
    [MenuItem("Window/Update Skinned Mesh Bones")]
    public static void OpenWindow()
    {
        var window = GetWindow<UpdateSkinMesh>();
        window.titleContent = new GUIContent("Skin Updater");
    }

    private GUIContent statusContent = new GUIContent("Waiting...");
    private Transform skinmeshHolder;
    private Transform rootBone;
    private bool includeInactive;
    private string statusText = "Waiting...";

    private void OnGUI()
    {
        skinmeshHolder =
            EditorGUILayout.ObjectField("Skin Meshes Parent", skinmeshHolder, typeof(Transform), true) as Transform;
        rootBone = EditorGUILayout.ObjectField("RootBone", rootBone, typeof(Transform), true) as Transform;
        includeInactive = EditorGUILayout.Toggle("Include Inactive", includeInactive);
        if (skinmeshHolder == null) return;
        var skinMeshes = skinmeshHolder.GetComponentsInChildren<SkinnedMeshRenderer>();
        if (skinMeshes.Length < 1) return;


        if (GUILayout.Button("Update Skinned Mesh Renderer"))
        {
            foreach (var targetSkin in skinMeshes)
            {
                bool enabled = (targetSkin != null && rootBone != null);
                if (!enabled)
                {
                    statusText = "Add a target SkinnedMeshRenderer and a root bone to process.";
                }

                GUI.enabled = enabled;
                statusText = "== Processing bones... ==";
                // Look for root bone
                string rootName = "";
                if (targetSkin.rootBone != null) rootName = targetSkin.rootBone.name;
                Transform newRoot = null;
                // Reassign new bones
                Transform[] newBones = new Transform[targetSkin.bones.Length];
                Transform[] existingBones = rootBone.GetComponentsInChildren<Transform>(includeInactive);
                int missingBones = 0;
                for (int i = 0; i < targetSkin.bones.Length; i++)
                {
                    if (targetSkin.bones[i] == null)
                    {
                        statusText += System.Environment.NewLine +
                                      "WARN: Do not delete the old bones before the skinned mesh is processed!";
                        missingBones++;
                        continue;
                    }

                    string boneName = targetSkin.bones[i].name;
                    bool found = false;
                    foreach (var newBone in existingBones)
                    {
                        if (newBone.name == rootName) newRoot = newBone;
                        if (newBone.name == boneName)
                        {
                            statusText += System.Environment.NewLine + "· " + newBone.name + " found!";
                            newBones[i] = newBone;
                            found = true;
                        }
                    }

                    if (!found)
                    {
                        statusText += System.Environment.NewLine + "· " + boneName + " missing!";
                        missingBones++;
                    }
                }

                targetSkin.bones = newBones;
                statusText += System.Environment.NewLine + "Done! Missing bones: " + missingBones;
                if (newRoot != null)
                {
                    statusText += System.Environment.NewLine + "· Setting " + rootName + " as root bone.";
                    targetSkin.rootBone = newRoot;
                }
            }
        }

        // Draw status because yeh why not?
        statusContent.text = statusText;
        EditorStyles.label.wordWrap = true;
        GUILayout.Label(statusContent);
    }
}