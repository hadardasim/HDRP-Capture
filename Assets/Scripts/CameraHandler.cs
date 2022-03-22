using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Simulation;
using UnityEngine.Experimental.Rendering;
using System.IO;

public class CameraHandler : MonoBehaviour
{    
    string baseDir;
    Camera captureCamera;


    void Start()
    {
        baseDir = Manager.Instance.GetDirectoryFor(DataCapturePaths.ScreenCapture);
        
        if (captureCamera == null)
            captureCamera = GetComponent<Camera>();
    }

    
    void Update()
    {        
    }

    private void LateUpdate()
    {
        if (Time.frameCount < 10 || Time.frameCount > 20)
            return;

        string colorPath = Path.Combine(baseDir, $"rgb_{Time.frameCount}.png");
        string depthPath = Path.Combine(baseDir, $"depth_{Time.frameCount}.exr");

        CaptureCamera.CaptureColorAndDepthToFile(captureCamera, GraphicsFormat.B8G8R8A8_SRGB, colorPath, CaptureImageEncoder.ImageFormat.Png,
            GraphicsFormat.R32_SFloat, depthPath, CaptureImageEncoder.ImageFormat.Exr);
    }
}
