using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.Rendering;
using UnityEngine.Experimental.Rendering;

class MyFullScreenCustomPass : CustomPass
{
    public RenderTexture renderTexture = null;
    public Material fullScreenMaterial = null;
    public Vector2 scale = new Vector2(1, 1);
    public Vector2 offset = new Vector2(0, 0);
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
        if (!Application.isPlaying)
            return;
        // Executed every frame for all the camera inside the pass volume.
        // The context contains the command buffer to use to enqueue graphics commands.
        RenderTexture rt = renderTexture;
        if (rt == null)
        {
            RenderTextureDescriptor desc = new RenderTextureDescriptor(1280, 1024, RenderTextureFormat.ARGB32, 0);
            rt = RenderTexture.GetTemporary(desc);            
        }
        
        CoreUtils.SetRenderTarget(ctx.cmd, rt, ClearFlag.Color);

        if (fullScreenMaterial != null)
        {
            if (Time.frameCount % 3 == 0)
                CoreUtils.DrawFullScreen(ctx.cmd, fullScreenMaterial);            
        }

        ctx.cmd.Blit(rt, ctx.cameraColorBuffer, scale, offset);        

        if (renderTexture == null)
            RenderTexture.ReleaseTemporary(rt);        
    }

    protected override void Cleanup()
    {
        // Cleanup code
    }
}