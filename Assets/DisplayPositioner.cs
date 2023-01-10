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
        bool alignWithMaxZ = false;

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            Config config = (Config)target;

            EditorGUILayout.Space(20f);
            EditorGUILayout.HelpBox("Position Display Containers around y-axis from a position.", MessageType.Info);
            xScale = EditorGUILayout.FloatField("X-Scale of a Display", xScale);
            origin = EditorGUILayout.Vector3Field("Origin", origin);
            left = EditorGUILayout.Vector3Field("From Dir", left);
            right = EditorGUILayout.Vector3Field("To Dir", right);
            if (GUILayout.Button("Position containers around Y-Axis"))
            {
                config.PositionContainersAroundYAxis(xScale, origin, left, right);
            }

            EditorGUILayout.Space(20f);
            EditorGUILayout.HelpBox("Fit the master display to Display(s) under the containers.\n" +
                "- Updates its screen.height based on its new scale and screen.width.\n" +
                "- Assume Display(s) are rectangle and thier sizes are equal.\n" +
                "- Assuem thier y-axes are aligned with world's y-aixs.", MessageType.Info);
            alignWithMaxZ = EditorGUILayout.Toggle("Set z-position to max", alignWithMaxZ);
            if (GUILayout.Button("Reset the master display"))
            {
                config.ResetMasterDisplay(alignWithMaxZ);
            }

            EditorGUILayout.Space(20f);
            EditorGUILayout.HelpBox("Save the configuration to a JSON file.\n" +
                "- Unity uses a left-handed, Y-Up coordinate system.\n" +
                "- OSPRay uses a right-handed, Y-Up coordinate system.", MessageType.Info);
            config.fileName = EditorGUILayout.TextField("File name", config.fileName);
            config.negX = EditorGUILayout.Toggle("Negate x values", config.negX);
            config.negY = EditorGUILayout.Toggle("Negate y values", config.negY);
            config.negZ = EditorGUILayout.Toggle("Negate z values", config.negZ);
            if (GUILayout.Button("Save to the file"))
            {
                config.SaveToFile();
            }
        }

    } // class DisplayPositioner
} // namespace ConfigGeneraor