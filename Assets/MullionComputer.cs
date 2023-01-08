using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ConfigGeneraor
{
    public class MullionComputer : MonoBehaviour
    {
        public string note = "Just to leave a comment.";

        [Header("Native Settings")]
        public float widthInCM;
        public float heightInCM;

        public int pixelWidth;
        public int pixelHeight;

        public int ppi;

        void Start()
        {
            Debug.Log(note);
            {
                float whole = widthInCM;
                float displayable = pixelWidth / (ppi * 1f / 2.54f); // also in CM

                float mullion = (whole - displayable) * 0.5f;

                string result = "Mullion WIDTH should be:";
                result += "\twhole (cm): " + whole.ToString("f6");
                result += "\tdisplay (cm): " + displayable.ToString("f6");
                result += "\tmullion (cm) - just one-side: " + mullion.ToString("f6");
                result += "\tmullion (m) - just one-side: " + (mullion * 0.01).ToString("f6");
                Debug.Log(result);
            }
            {
                float whole = heightInCM;
                float displayable = pixelHeight / (ppi * 1f / 2.54f); // also in CM

                float mullion = (whole - displayable) * 0.5f;

                string result = "Mullion HEIGHT should be:";
                result += "\twhole (cm): " + whole.ToString("f6");
                result += "\tdisplay (cm): " + displayable.ToString("f6");
                result += "\tmullion (cm) - just one-side: " + mullion.ToString("f6");
                result += "\tmullion (m) - just one-side: " + (mullion * 0.01).ToString("f6");
                Debug.Log(result);
            }
        }
    } // class MullionComputer
} // namespace ConfigGeneraor