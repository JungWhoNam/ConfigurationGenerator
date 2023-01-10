using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace ConfigGeneraor
{
    [CustomEditor(typeof(Config))]
    public class DisplayPositioner : Editor
    {
        float xScale = 1.2268f;
        Vector3 origin = Vector3.zero;
        Vector3 left = Vector3.left;
        Vector3 right = Vector3.right;

        string fileName = "config/display_settings.json";
        bool negX = true;
        bool negY = false;
        bool negZ = false;

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            Config config = (Config)target;

            EditorGUILayout.Space(20f);
            xScale = EditorGUILayout.FloatField("X-Scale of a Display", xScale);
            origin = EditorGUILayout.Vector3Field("Origin", origin);
            left = EditorGUILayout.Vector3Field("From Dir", left);
            right = EditorGUILayout.Vector3Field("To Dir", right);
            if (GUILayout.Button("Position containers around Y-Axis"))
            {
                config.PositionContainersAroundYAxis(xScale, origin, left, right);
            }

            EditorGUILayout.Space(20f);
            fileName = EditorGUILayout.TextField("File name", fileName);
            negX = EditorGUILayout.Toggle("Negate x values", negX);
            negY = EditorGUILayout.Toggle("Negate y values", negY);
            negZ = EditorGUILayout.Toggle("Negate z values", negZ);
            if (GUILayout.Button("Save to the file"))
            {
                config.SaveToFile(fileName, negX, negY, negZ);
            }
        }

    } // class DisplayPositioner
} // namespace ConfigGeneraor