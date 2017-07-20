using UnityEngine;
using UnityEditor;

namespace RandomUtility
{

    [CustomPropertyDrawer(typeof(RandomFloat))]
    public class RandomFloatDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight;
        }

        // Draw the property inside the given rect
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
            EditorGUI.EndProperty();

            SerializedProperty minValue = property.FindPropertyRelative("minValue");
            SerializedProperty maxValue = property.FindPropertyRelative("maxValue");
            float newMin = minValue.floatValue;
            float newMax = maxValue.floatValue;

            float labelWidth = 40.0f;
            float width = position.width * 0.5f;

            EditorGUI.LabelField(new Rect(position.x, position.y, labelWidth, EditorGUIUtility.singleLineHeight), "Min:");
            newMin = EditorGUI.FloatField(new Rect(position.x + labelWidth, position.y, width - labelWidth, EditorGUIUtility.singleLineHeight), newMin);

            EditorGUI.LabelField(new Rect(position.x + width, position.y, labelWidth, EditorGUIUtility.singleLineHeight), "Max:");
            newMax = EditorGUI.FloatField(new Rect(position.x + width + labelWidth, position.y, width - labelWidth, EditorGUIUtility.singleLineHeight), newMax);

            minValue.floatValue = newMin;
            maxValue.floatValue = newMax;
        }
    }

}

