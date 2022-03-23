using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameDebug : MonoBehaviour
{    
    private void OnGUI()
    {
        Rect rect = new Rect(0,0, 150, 80);
        GUI.Label(rect, $"Current frame {Time.frameCount}");
    }
}
