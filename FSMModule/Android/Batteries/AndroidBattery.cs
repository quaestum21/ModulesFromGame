using UnityEngine;

public sealed class AndroidBattery : MonoBehaviour
{
    [SerializeField] private Renderer _renderer;
    public void SetMaterial(Material material)
    {
        if (material == null || _renderer ==null) return;
        // give ll materials
        Material[] mats = _renderer.materials;

        // Changing the color of a specific
        mats[2] = material;

        // Updating materials on the renderer
        _renderer.materials = mats;
    }
}
