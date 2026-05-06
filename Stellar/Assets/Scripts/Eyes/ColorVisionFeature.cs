using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ColorVisionFeature : ScriptableRendererFeature
{
    class ColorVisionPass : ScriptableRenderPass
    {
        private Material material;
        private RTHandle source;

        public ColorVisionPass(Material mat)
        {
            material = mat;
            renderPassEvent = RenderPassEvent.AfterRenderingPostProcessing;
        }

        public void SetSource(RTHandle src) => source = src;

        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            if (material == null) return;

            CommandBuffer cmd = CommandBufferPool.Get("ColorVision");
            Blitter.BlitCameraTexture(cmd, source, source, material, 0);
            context.ExecuteCommandBuffer(cmd);
            CommandBufferPool.Release(cmd);
        }
    }

    public Material visionMaterial;
    private ColorVisionPass pass;

    public override void Create()
    {
        pass = new ColorVisionPass(visionMaterial);
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        if (visionMaterial == null) return;
        renderer.EnqueuePass(pass);
    }
}