using System.Linq;
using UnityEditor;
using UnityEditor.Graphs;
using UnityEngine;

[InitializeOnLoad]
public class CustomHierarchy : MonoBehaviour {

    private static Vector2 offset = new Vector2(0, 2);

    static CustomHierarchy()
    {
        EditorApplication.hierarchyWindowItemOnGUI += HandleHierarchyWindowItemOnGui;
    }

    private static void HandleHierarchyWindowItemOnGui(int instanceId, Rect selectionRect)
    {
        Color textColor = Color.blue;
        Color backgroundColor = new Color(.76f, .76f, .76f);

        var obj = EditorUtility.InstanceIDToObject(instanceId);
        if (obj != null)
        {

            var prefabType = PrefabUtility.GetPrefabType(obj);
            if (prefabType == PrefabType.PrefabInstance)
            {
                if (Selection.instanceIDs.Contains(instanceId))
                {
                    textColor = Color.white;
                    backgroundColor = new Color(0.24f, 0.48f, 0.90f);
                }

                Rect offsetRect = new Rect(selectionRect.position + offset, selectionRect.size);
                EditorGUI.DrawRect(selectionRect, backgroundColor);
                EditorGUI.LabelField(offsetRect, obj.name, new GUIStyle()
                {
                    normal = new GUIStyleState() {textColor = textColor},
                    fontStyle = FontStyle.Bold
                });
            }
        }
    }
}
