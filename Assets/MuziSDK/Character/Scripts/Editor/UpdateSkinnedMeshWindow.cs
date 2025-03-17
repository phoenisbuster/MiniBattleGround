
using UnityEngine;
using UnityEditor;

namespace MuziCharacter
{

    public class UpdateModelMaterials : EditorWindow
    {
        [MenuItem("Muziverse/Utils/Update Model Materials")]
        public static void OpenWindow()
        {
            var window = GetWindow<UpdateModelMaterials>();
            window.titleContent = new GUIContent("Skin Updater");
        }
        private GUIContent statusContent = new GUIContent("Waiting...");
        private Transform targetTransform;
        private Transform sourceTransform;
        private bool includeInactive = true;
        private string statusText = "Waiting...";

        bool SamePart(string part1, string part2)
        {
            // var endSameCase1 = (part1.EndsWith("a") && part2.EndsWith("1")) || (part1.EndsWith("b") && part2.EndsWith("2"));
            // var endSameCase2 = (part2.EndsWith("a") && part1.EndsWith("1")) || (part2.EndsWith("b") && part1.EndsWith("2"));
            //
            // return (part1.Substring(0, 5) == part2.Substring(0, 5)) && (endSameCase1 || endSameCase2);
            return part1 == part2;
        }

        private void OnGUI()
        {
            sourceTransform = EditorGUILayout.ObjectField("Source", sourceTransform, typeof(Transform), true) as Transform;
            targetTransform = EditorGUILayout.ObjectField("Target", targetTransform, typeof(Transform), true) as Transform;
            includeInactive = EditorGUILayout.Toggle("Include Inactive", includeInactive);
            bool enabled = (targetTransform != null && sourceTransform != null);
            if (!enabled)
            {
                statusText = "Add a target SkinnedMeshRenderer and a root bone to process.";
            }
            GUI.enabled = enabled;
            if (GUILayout.Button("Match materials"))
            {
                var targetSkinnedMeshs = targetTransform.GetComponentsInChildren<SkinnedMeshRenderer>(includeInactive);
                var sourceMeshs = sourceTransform.GetComponentsInChildren<SkinnedMeshRenderer>(includeInactive);

                foreach (var mesh in sourceMeshs)
                {
                    foreach (var skinnedMesh in targetSkinnedMeshs)
                    {
                        string part1 = mesh.gameObject.name.ToLower();
                        string part2 = skinnedMesh.gameObject.name.ToLower();
                        if (part1.Contains(part2) || part2.Contains(part1) || SamePart(part1, part2))
                        {
                            skinnedMesh.sharedMaterials = mesh.sharedMaterials;
                        }
                    }
                }
                statusText = "Done";
            }
    
            // Draw status because yeh why not?
            statusContent.text = statusText;
            EditorStyles.label.wordWrap = true;
            GUILayout.Label(statusContent);
        }
    }
}