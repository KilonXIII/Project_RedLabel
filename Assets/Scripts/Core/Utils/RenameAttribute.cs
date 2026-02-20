using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class RenameAttribute : PropertyAttribute
{
    public string NewName { get; private set; }
    public RenameAttribute(string name) { NewName = name; }
}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(RenameAttribute))]
public class RenameEditor : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        label.text = ((RenameAttribute)attribute).NewName;
        EditorGUI.PropertyField(position, property, label);
    }
}
#endif