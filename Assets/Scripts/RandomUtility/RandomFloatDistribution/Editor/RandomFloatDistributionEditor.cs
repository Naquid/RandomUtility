using UnityEngine;
using UnityEditor;

namespace RandomUtility
{

    [CustomPropertyDrawer(typeof(RandomFloatDistribution))]
    public class RandomFloatDistributionDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            int lines = 0;

            SerializedProperty modeProperty = property.FindPropertyRelative("mode");
            DistributionMode mode = (DistributionMode)modeProperty.intValue;
            switch (mode)
            {
                case DistributionMode.Normal:
                    lines = 3;
                    break;

                case DistributionMode.Slope:
                    lines = 3;
                    break;

                case DistributionMode.Curve:
                    lines = 3;
                    break;

                case DistributionMode.Exp:
                    lines = 3;
                    break;

                case DistributionMode.List:
                    SerializedProperty listProp = property.FindPropertyRelative("probabilityList");
                    lines = listProp.arraySize + 2;
                    break;

                default:
                    lines = 2;
                    break;
            }

            return EditorGUIUtility.singleLineHeight * lines;
        }


        // Draw the property inside the given rect
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            Rect initialPost = position;

            EditorGUI.BeginProperty(position, label, property);
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            SerializedProperty modeProperty = property.FindPropertyRelative("mode");
            EditorGUI.PropertyField(position, modeProperty, GUIContent.none, true);

            Rect mainRect = new Rect(initialPost.x, initialPost.y + EditorGUIUtility.singleLineHeight, initialPost.width, EditorGUIUtility.singleLineHeight);

            EditorGUI.indentLevel += 1;
            DistributionMode mode = (DistributionMode)modeProperty.intValue;
            switch (mode)
            {
                case DistributionMode.Normal:
                    DisplayMinMax(mainRect, property);
                    Rect normalRect = new Rect(mainRect.x, mainRect.y + EditorGUIUtility.singleLineHeight, mainRect.width, EditorGUIUtility.singleLineHeight);
                    DisplayNormal(normalRect, property);
                    break;

                case DistributionMode.Slope:
                    DisplayMinMax(mainRect, property);
                    Rect slopeRect = new Rect(mainRect.x, mainRect.y + EditorGUIUtility.singleLineHeight, mainRect.width, EditorGUIUtility.singleLineHeight);
                    DisplaySlope(slopeRect, property);
                    break;

                case DistributionMode.Curve:
                    DisplayMinMax(mainRect, property);
                    Rect curveRect = new Rect(mainRect.x, mainRect.y + EditorGUIUtility.singleLineHeight, mainRect.width, EditorGUIUtility.singleLineHeight);
                    DisplayCurve(curveRect, property);
                    break;

                case DistributionMode.Exp:
                    DisplayMinMax(mainRect, property);
                    Rect expRect = new Rect(mainRect.x, mainRect.y + EditorGUIUtility.singleLineHeight, mainRect.width, EditorGUIUtility.singleLineHeight);
                    DisplayExp(expRect, property);
                    break;

                case DistributionMode.List:
                    DisplayList(mainRect, property);
                    break;

                default:
                    break;
            }

            EditorGUI.indentLevel -= 1;
            EditorGUI.EndProperty();
        }

        void DisplayMinMax(Rect position, SerializedProperty property)
        {
            SerializedProperty minValue = property.FindPropertyRelative("minValue");
            SerializedProperty maxValue = property.FindPropertyRelative("maxValue");

            float labelWidth = 60.0f;
            float width = position.width * 0.5f;

            EditorGUI.LabelField(new Rect(position.x, position.y, labelWidth, position.height), "Min:");
            EditorGUI.PropertyField(new Rect(position.x + labelWidth, position.y, width - labelWidth, position.height), minValue, GUIContent.none, false);

            EditorGUI.LabelField(new Rect(position.x + width, position.y, labelWidth, position.height), "Max:");
            EditorGUI.PropertyField(new Rect(position.x + width + labelWidth, position.y, width - labelWidth, position.height), maxValue, GUIContent.none, false);

        }

        void DisplayNormal(Rect position, SerializedProperty property)
        {
            SerializedProperty stdDevProp = property.FindPropertyRelative("stdDev");
            SerializedProperty meanProp = property.FindPropertyRelative("mean");

            float labelWidth = 60.0f;
            float width = position.width * 0.5f;

            EditorGUI.LabelField(new Rect(position.x, position.y, labelWidth, position.height), "Dev:");
            EditorGUI.PropertyField(new Rect(position.x + labelWidth, position.y, width - labelWidth, position.height), stdDevProp, GUIContent.none, false);

            EditorGUI.LabelField(new Rect(position.x + width, position.y, labelWidth, position.height), "Mean:");
            EditorGUI.PropertyField(new Rect(position.x + width + labelWidth, position.y, width - labelWidth, position.height), meanProp, GUIContent.none, false);
        }

        void DisplaySlope(Rect position, SerializedProperty property)
        {
            SerializedProperty slopeProp = property.FindPropertyRelative("slope");
            EditorGUI.PropertyField(position, slopeProp, false);
        }

        void DisplayExp(Rect position, SerializedProperty property)
        {
            SerializedProperty expProp = property.FindPropertyRelative("exp");
            EditorGUI.PropertyField(position, expProp, false);
        }

        void DisplayCurve(Rect position, SerializedProperty property)
        {
            SerializedProperty curveProp = property.FindPropertyRelative("curve");

            GUIContent content = new GUIContent("Curve:");
            EditorGUI.PropertyField(position, curveProp, content, false);
        }

        void DisplayList(Rect position, SerializedProperty property)
        {
            SerializedProperty listProp = property.FindPropertyRelative("probabilityList");

            //Array Size
            int arraySize = listProp.arraySize;
            arraySize = EditorGUI.IntField(position, "Size: ", arraySize);
            listProp.arraySize = arraySize;

            EditorGUI.indentLevel += 1;
            //Array Data
            for (int i = 0; i < arraySize; i++)
            {
                SerializedProperty arrayEntry = listProp.GetArrayElementAtIndex(i);
                SerializedProperty valueProp = arrayEntry.FindPropertyRelative("value");
                SerializedProperty probabilityProp = arrayEntry.FindPropertyRelative("probability");

                float labelWidth = 70.0f;
                float width = position.width * 0.5f;

                float posY = position.y + ((i + 1) * EditorGUIUtility.singleLineHeight);
                float height = EditorGUIUtility.singleLineHeight;

                EditorGUI.LabelField(new Rect(position.x, posY, labelWidth, height), "Value:");
                EditorGUI.PropertyField(new Rect(position.x + labelWidth, posY, width - labelWidth, height), valueProp, GUIContent.none, false);

                EditorGUI.LabelField(new Rect(position.x + width, posY, labelWidth, height), "Prob:");
                EditorGUI.PropertyField(new Rect(position.x + width + labelWidth, posY, width - labelWidth, height), probabilityProp, GUIContent.none, false);
            }

            EditorGUI.indentLevel -= 1;
        }
    }

}

