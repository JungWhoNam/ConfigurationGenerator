using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// MacBook Pro 16" (4th Generation)
// https://support.apple.com/kb/SP809?locale=en_US
// - native - 35.79 X 24.59 (in cm), 3072 x 1920, 226 PPI
// - use as - 1792 x 1120
// DELL P2715Q
// - native - 64.074 x 37.966 (in cm), 3840 x 2160, 163 PPI
// - use as - 2560 x 1440 
// For getting helps with computing mullion width and height, see MullionComputer.cs.
namespace ConfigGeneraor
{
    public class Config : MonoBehaviour
    {
        public bool saveToFile = true;
        public string fileName = "display_settings.json";

        // left-hand (unity), right-hand (ospray)
        [Space(10)]
        public bool negX3D = true;
        public bool negY3D = false;
        public bool negZ3D = false;

        [Space(10)]
        public Display[] displays;

        private void Start()
        {
            string str = "[";
            for (int i = 0; i < displays.Length; i++) {
                if (displays[i] == null) {
                    continue;
                }

                displays[i].gameObject.name = i + ": " + displays[i].hostName;

                str += "\n";
                str += Display.ToJSON(displays[i], negX3D, negY3D, negZ3D);
                str += i < displays.Length - 1 ? "," : "\n";
            }
            str += "]";

            Debug.Log(str);

            if (saveToFile) {
                System.IO.File.WriteAllText(Application.dataPath + "/" + fileName, str);
            }
        }

    } // class Config
} // namespace ConfigGeneraor