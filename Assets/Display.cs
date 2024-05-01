using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ConfigGeneraor
{ 
    public class Display : MonoBehaviour
    {
        
        public static string ToJSON(string key, Vector3 value, bool negX, bool negY, bool negZ)
        {
            string str = "\"" + key + "\": [";
            str += (negX ? -value.x : value.x).ToString("f6") + ", ";
            str += (negY ? -value.y : value.y).ToString("f6") + ", ";
            str += (negZ ? -value.z : value.z).ToString("f6") + "]";

            return str;
        }

        public static string ToJSON(string key, bool value)
        {
            string str = "\"" + key + "\": ";
            str += value ? "true" : "false";

            return str;
        }

        public static string ToJSON(string key, float value)
        {
            string str = "\"" + key + "\": ";
            str += value.ToString("f6");

            return str;
        }

        public static string ToJSON(string key, int value)
        {
            string str = "\"" + key + "\": ";
            str += value;

            return str;
        }

        public static string ToJSON(string key, string value)
        {
            string str = "\"" + key + "\": ";
            str += "\"" + value + "\"";

            return str;
        }

        public static string ToJSON(Display value, bool negX, bool negY, bool negZ)
        {
            string str = "{";

            // host name
            str += "\n";
            str += "\t" + ToJSON(nameof(hostName), value.hostName) + ",\n";
            // physical properties
            float scaleFactor = 1f;
            str += "\n";
            str += "\t" + ToJSON(nameof(topLeft), value.topLeft.position * scaleFactor, negX, negY, negZ) + ",\n";
            str += "\t" + ToJSON(nameof(botLeft), value.botLeft.position * scaleFactor, negX, negY, negZ) + ",\n";
            str += "\t" + ToJSON(nameof(botRight), value.botRight.position * scaleFactor, negX, negY, negZ) + ",\n";
            str += "\t" + ToJSON(nameof(eye), value.eye.position * scaleFactor, negX, negY, negZ) + ",\n";
            str += "\t" + ToJSON(nameof(mullionLeft), value.mullionLeft * scaleFactor) + ",\n";
            str += "\t" + ToJSON(nameof(mullionRight), value.mullionRight * scaleFactor) + ",\n";
            str += "\t" + ToJSON(nameof(mullionTop), value.mullionTop * scaleFactor) + ",\n";
            str += "\t" + ToJSON(nameof(mullionBottom), value.mullionBottom * scaleFactor) + ",\n";
            // screen properties
            str += "\n";
            str += "\t" + ToJSON(nameof(display), value.display) + ",\n";
            str += "\t" + ToJSON("screenX", value.screen.x) + ",\n";
            str += "\t" + ToJSON("screenY", value.screen.y) + ",\n";
            str += "\t" + ToJSON("screenWidth", value.screen.width) + ",\n";
            str += "\t" + ToJSON("screenHeight", value.screen.height) + ",\n";
            str += "\t" + ToJSON(nameof(decorated), value.decorated) + ",\n";
            str += "\t" + ToJSON(nameof(showUI), value.showUI) + ",\n";
            str += "\t" + ToJSON(nameof(scaleRes), value.scaleRes) + ",\n";
            str += "\t" + ToJSON(nameof(scaleResNav), value.scaleResNav) + ",\n";
            str += "\t" + ToJSON(nameof(lockAspectRatio), value.lockAspectRatio) + "\n";


            str += "}";

            return str;
        }

        public string hostName;

        [Header("Physical Properties (in meters)")]
        public Transform topLeft;
        public Transform botLeft;
        public Transform botRight;
        public Transform eye;
        public float mullionLeft;
        public float mullionRight;
        public float mullionTop;
        public float mullionBottom;

        [Header("Screen Properties")]
        public int display;
        public RectInt screen;
        public bool decorated = false;
        public bool showUI = false;
        public float scaleRes = 0.5f;
        public float scaleResNav = 0.25f;
        public bool lockAspectRatio = true;

    } // class Display
} // namespace ConfigGeneraor