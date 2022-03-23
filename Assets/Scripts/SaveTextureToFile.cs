using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.Rendering;
using UnityEngine.Experimental.Rendering;

class SaveTextureToFile : CustomPass
{
    public RenderTexture renderTexture = null;
    public string baseDir = @"c:\temp\shuky\";
    public int startFrame = 10;
    public int captureCount = 10;

    public enum CaptureFormat
    {
        PNG,
        EXR
    };
    public CaptureFormat captureFormat = CaptureFormat.PNG;
    // It can be used to configure render targets and their clear state. Also to create temporary render target textures.
    // When empty this render pass will render to the active camera render target.
    // You should never call CommandBuffer.SetRenderTarget. Instead call <c>ConfigureTarget</c> and <c>ConfigureClear</c>.
    // The render pipeline will ensure target setup and clearing happens in an performance manner.
    protected override void Setup(ScriptableRenderContext renderContext, CommandBuffer cmd)
    {
        // Setup code here
    }

    protected override void Execute(CustomPassContext ctx)
    {
        // Executed every frame for all the camera inside the pass volume.
        // The context contains the command buffer to use to enqueue graphics commands.
        if (!Application.isPlaying)
            return;

        int frameNum = Time.frameCount;
        if (frameNum < startFrame)
            return;
        if (frameNum - startFrame > captureCount)
            return;
        
        Graphics.CreateGraphicsFence(GraphicsFenceType.AsyncQueueSynchronisation, SynchronisationStageFlags.AllGPUOperations);
        var rt = renderTexture;
        Vector2Int size = ctx.cameraColorBuffer.referenceSize;
        if (rt == null)
        {
            rt = ctx.cameraColorBuffer;             
        } else
            size = new Vector2Int(renderTexture.width, renderTexture.height);

        var org = RenderTexture.active;
        RenderTexture.active = rt;

        TextureFormat texFormat = (captureFormat == CaptureFormat.PNG) ? TextureFormat.ARGB32 : TextureFormat.RGBAFloat;
        Texture2D tex = new Texture2D(size.x, size.y, texFormat, false);
        tex.ReadPixels(new Rect(0, 0, size.x, size.y), 0, 0);

        if (captureFormat == CaptureFormat.EXR)
        {
            byte[] bytes = tex.EncodeToEXR(Texture2D.EXRFlags.OutputAsFloat);
            System.IO.File.WriteAllBytes(baseDir + frameNum.ToString("00000") + ".exr", bytes);
        }
        else
        {
            byte[] bytes = tex.EncodeToPNG();
            System.IO.File.WriteAllBytes(baseDir + frameNum.ToString("00000") + ".png", bytes);
        }
        

        RenderTexture.active = org;
    }

    protected override void Cleanup()
    {
        // Cleanup code
    }
}