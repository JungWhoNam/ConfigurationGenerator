using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ConfigGeneraor
{
    public class Config : MonoBehaviour
    {
        [HideInInspector]
        public string fileName = "config/display_settings.json";
        [HideInInspector]
        public bool negX = true, negY = false, negZ = false;

        [Header("Display Settings")]
        public Display masterDisplay;
        public Transform[] displayContainers;

        public void SaveToFile()
        {
            if (string.IsNullOrEmpty(fileName) || string.IsNullOrWhiteSpace(fileName))
            {
                Debug.LogWarning("File name is empty.");
                return;
            }

            // find all Display(s) (including the master display).
            List<Display> displays = new();
            displays.Add(masterDisplay);
            foreach (Transform container in this.displayContainers)
                displays.AddRange(container.GetComponentsInChildren<Display>());

            string str = "[";
            for (int i = 0; i < displays.Count; i++)
            {
                str += "\n";
                str += Display.ToJSON(displays[i], negX, negY, negZ);
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

        public void ResetMasterDisplay(bool alignWithMaxZ = true)
        {
            if (this.masterDisplay == null)
            {
                Debug.Log("Please assign \"rank zero\".");
                return;
            }

            // find all Display(s) under displayContainers.
            List<Display> displays = new();
            foreach (Transform container in this.displayContainers)
                displays.AddRange(container.GetComponentsInChildren<Display>());

            Debug.Log("Positioning and scaling the master display...");
            Debug.Log("\tgoing through " + displays.Count + " non-zero display(s)");

            // compute min and max values of x, y, z positions.
            Vector2 xMinMax = new Vector2(float.MaxValue, float.MinValue);
            Vector2 yMinMax = new Vector2(float.MaxValue, float.MinValue);
            Vector2 zMinMax = new Vector2(float.MaxValue, float.MinValue);
            foreach (Display display in displays)
            {
                {
                    Transform corner = display.topLeft;
                    xMinMax[0] = Mathf.Min(corner.position.x, xMinMax[0]); // update min
                    xMinMax[1] = Mathf.Max(corner.position.x, xMinMax[1]); // update max
                    yMinMax[0] = Mathf.Min(corner.position.y, yMinMax[0]); // update min
                    yMinMax[1] = Mathf.Max(corner.position.y, yMinMax[1]); // update max
                    zMinMax[0] = Mathf.Min(corner.position.z, zMinMax[0]); // update min
                    zMinMax[1] = Mathf.Max(corner.position.z, zMinMax[1]); // update max
                }
                {
                    Transform corner = display.botLeft;
                    xMinMax[0] = Mathf.Min(corner.position.x, xMinMax[0]); // update min
                    xMinMax[1] = Mathf.Max(corner.position.x, xMinMax[1]); // update max
                    yMinMax[0] = Mathf.Min(corner.position.y, yMinMax[0]); // update min
                    yMinMax[1] = Mathf.Max(corner.position.y, yMinMax[1]); // update max
                    zMinMax[0] = Mathf.Min(corner.position.z, zMinMax[0]); // update min
                    zMinMax[1] = Mathf.Max(corner.position.z, zMinMax[1]); // update max
                }
                {
                    Transform corner = display.botRight;
                    xMinMax[0] = Mathf.Min(corner.position.x, xMinMax[0]); // update min
                    xMinMax[1] = Mathf.Max(corner.position.x, xMinMax[1]); // update max
                    yMinMax[0] = Mathf.Min(corner.position.y, yMinMax[0]); // update min
                    yMinMax[1] = Mathf.Max(corner.position.y, yMinMax[1]); // update max
                    zMinMax[0] = Mathf.Min(corner.position.z, zMinMax[0]); // update min
                    zMinMax[1] = Mathf.Max(corner.position.z, zMinMax[1]); // update max
                }
            }

            // check if the values were updated.
            if (xMinMax[0] >= float.MaxValue || xMinMax[1] <= float.MinValue ||
                yMinMax[0] >= float.MaxValue || yMinMax[1] <= float.MinValue ||
                zMinMax[0] >= float.MaxValue || zMinMax[1] <= float.MinValue)
            {
                Debug.LogWarning("\tfailed to compute min and max values for x, y, z positions of corners");
                return;
            }
            else
            {
                Debug.Log("\tx-position min and max: " + xMinMax);
                Debug.Log("\ty-position min and max: " + yMinMax);
                Debug.Log("\tz-position min and max: " + zMinMax);
            }

            // position and update the master display
            Vector3 topLeft, botLeft, botRight;

            topLeft.x = xMinMax[0];
            botLeft.x = xMinMax[0];
            botRight.x = xMinMax[1];

            topLeft.y = yMinMax[1];
            botLeft.y = yMinMax[0];
            botRight.y = yMinMax[0];

            topLeft.z = zMinMax[alignWithMaxZ ? 1 : 0];
            botLeft.z = zMinMax[alignWithMaxZ ? 1 : 0];
            botRight.z = zMinMax[alignWithMaxZ ? 1 : 0];

            this.masterDisplay.transform.localScale = new Vector3(
                Vector3.Distance(botLeft, botRight),
                Vector3.Distance(topLeft, botLeft),
                1f);
            this.masterDisplay.transform.position = topLeft + (botRight - topLeft) * 0.5f;

            // update the screen height (based on new scale and screen.width)
            float w = this.masterDisplay.transform.localScale.x - this.masterDisplay.mullionLeft - this.masterDisplay.mullionRight;
            float h = this.masterDisplay.transform.localScale.y - this.masterDisplay.mullionTop - this.masterDisplay.mullionBottom;
            this.masterDisplay.screen.height = (int) (this.masterDisplay.screen.width * h / w);

            Debug.Log("\tfinished positioning and scaling the master display");
        }

    } // class Config
} // namespace ConfigGeneraor