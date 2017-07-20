using UnityEngine;
using UnityEditor;

namespace RandomUtility
{

    [CustomPropertyDrawer(typeof(RandomFloatRangeAttribute))]
    public class RandomFloatRangeDrawer : PropertyDrawer
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

            RandomFloatRangeAttribute range = attribute as RandomFloatRangeAttribute;
            SerializedProperty minValue = property.FindPropertyRelative("minValue");
            SerializedProperty maxValue = property.FindPropertyRelative("maxValue");
            float newMin = minValue.floatValue;
            float newMax = maxValue.floatValue;

            float labelWidthStart = 40.0f;
            float labelWidthEnd = 40.0f;
            float sliderWidth = position.width - labelWidthStart - labelWidthEnd;

            string myString = EditorGUI.TextField(new Rect(position.x, position.y, labelWidthStart, EditorGUIUtility.singleLineHeight), newMin.ToString("F1"));
            float parsedFloat;
            if (float.TryParse(myString, out parsedFloat))
            {
                newMin = parsedFloat;
            }

            EditorGUI.MinMaxSlider(new Rect(position.x + labelWidthStart, position.y, sliderWidth, EditorGUIUtility.singleLineHeight), ref newMin, ref newMax, range.minLimit, range.maxLimit);

            string myString2 = EditorGUI.TextField(new Rect(position.x + position.width - labelWidthEnd, position.y, labelWidthStart, EditorGUIUtility.singleLineHeight), newMax.ToString("F1"));
            float parsedFloat2;
            if (float.TryParse(myString2, out parsedFloat2))
            {
                newMax = parsedFloat2;
            }

            minValue.floatValue = newMin;
            maxValue.floatValue = newMax;
        }
    }

}