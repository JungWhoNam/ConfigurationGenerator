using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ConfigGeneraor
{
    public class Config : MonoBehaviour
    {
        [Header("Display Settings")]
        public Display rankZero;
        public Transform[] displayContainers;

        // left-hand (unity), right-hand (ospray)
        public void SaveToFile(string fileName, bool negX3D, bool negY3D, bool negZ3D)
        {
            if (string.IsNullOrEmpty(fileName) || string.IsNullOrWhiteSpace(fileName))
            {
                Debug.LogWarning("File name is empty.");
                return;
            }

            // find all Display(s) under displayContainers.
            List<Display> displays = new();
            displays.Add(rankZero);
            foreach (Transform container in this.displayContainers)
                displays.AddRange(container.GetComponentsInChildren<Display>());

            string str = "[";
            for (int i = 0; i < displays.Count; i++)
            {
                str += "\n";
                str += Display.ToJSON(displays[i], negX3D, negY3D, negZ3D);
                str += i < displays.Count - 1 ? "," : "\n";
            }
            str += "]";

            System.IO.File.WriteAllText(Application.dataPath + "/" + fileName, str);

            Debug.Log("Saved " + fileName + ", which contains " + displays.Count + " display(s) information.");
        }

        public void PositionContainersAroundYAxis(float scaleX, Vector3 origin, Vector3 fromDir, Vector3 toDir)
        {
            if (this.displayContainers == null || this.displayContainers.Length <= 1)
            {
                Debug.LogWarning("There is only one or no displayContainer(s).");
                return;
            }

            float angleIncAmt = Vector3.Angle(fromDir, toDir) / (this.displayContainers.Length - 1);
            float distFromOrigin = (scaleX * 0.5f) / Mathf.Tan(Mathf.Deg2Rad * angleIncAmt * 0.5f);

            for (int i = 0; i < this.displayContainers.Length; i++)
            {
                Vector3 dir = Quaternion.AngleAxis(angleIncAmt * i, Vector3.up) * fromDir;
                this.displayContainers[i].transform.position = origin + dir * distFromOrigin;
                this.displayContainers[i].transform.LookAt(origin + dir * (distFromOrigin + 1f), Vector3.up);
            }

            Debug.Log("Positioned " + this.displayContainers.Length + " container(s), placed " + distFromOrigin + " from the origin.");
        }

    } // class Config
} // namespace ConfigGeneraor