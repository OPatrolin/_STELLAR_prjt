using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ColorVisionFeature : ScriptableRendererFeature
{
    public Material visionMaterial;
    private ColorVisionRenderPass pass;

    public override void Create()
    {
        pass = new ColorVisionRenderPass(visionMaterial);
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        if (visionMaterial == null) return;
        renderer.EnqueuePass(pass); // Toujours enqueue, le shader gère l'activation
    }

    class ColorVisionRenderPass : ScriptableRenderPass
    {
        Material mat;
        int tempID = Shader.PropertyToID("_TempTex");

        public ColorVisionRenderPass(Material m)
        {
            mat = m;
            renderPassEvent = RenderPassEvent.AfterRenderingTransparents;
        }

        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            if (mat == null) return;
            if (renderingData.cameraData.isSceneViewCamera) return;

            CommandBuffer cmd = CommandBufferPool.Get("ColorVision");

            RenderTargetIdentifier source = renderingData.cameraData.renderer.cameraColorTargetHandle;
            RenderTextureDescriptor desc = renderingData.cameraData.cameraTargetDescriptor;
            desc.depthBufferBits = 0;

            cmd.GetTemporaryRT(tempID, desc);
            cmd.Blit(source, tempID, mat);
            cmd.Blit(tempID, source);
            cmd.ReleaseTemporaryRT(tempID);

            context.ExecuteCommandBuffer(cmd);
            CommandBufferPool.Release(cmd);
        }
    }
}