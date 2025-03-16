using UnityEditor;

using UnityEngine;

[CustomPropertyDrawer(typeof(Items.Item))]
public class ItemDrawer : PropertyDrawer {
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        EditorGUI.BeginProperty(position, label, property);

        int padding = 50;

        // Draw label
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        // Don't make child fields be indented
        int indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        // Calculate rects
        Rect typeRect = new Rect(position.x, position.y, 200, position.height);
        Rect dataRect = new Rect(position.x + typeRect.width + padding, position.y, 50, position.height);

        // Draw fields - passs GUIContent.none to each so they are drawn without labels
        EditorGUI.PropertyField(typeRect, property.FindPropertyRelative("Data"), GUIContent.none);
        EditorGUI.PropertyField(dataRect, property.FindPropertyRelative("Count"), GUIContent.none);

        // Set indent back to what it was
        EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty();
    }
}