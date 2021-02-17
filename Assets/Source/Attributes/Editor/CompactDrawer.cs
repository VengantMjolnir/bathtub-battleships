using UnityEditor;
using UnityEngine;
using System;

namespace RogueEyebrow.Attributes.Editor
{
    [CustomPropertyDrawer(typeof(CompactAttribute))]
    class CompactDrawer : PropertyDrawer
    {
        // Draw the property inside the given rect
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUIUtility.LookLikeControls();
            position.xMin += 4;
            position.xMax -= 4;
            //position.yMax += 50;
            // Now draw the property as a Slider or an IntSlider based on whether it's a float or integer.
            if (property.propertyType == SerializedPropertyType.Vector3)
            {
                property.vector3Value = EditorGUI.Vector3Field(position, label.text, property.vector3Value);
            } else
            if (property.propertyType == SerializedPropertyType.Vector2)
            {
                property.vector2Value = EditorGUI.Vector2Field(position, label.text, property.vector2Value);
            } else
                EditorGUI.LabelField(position, label.text, "Use Compact with Vector3 or Vector2.");
            //EditorGUILayout.Space();
        }
    }
}