using UnityEngine;

public sealed class AndroidBattery : MonoBehaviour
{
    [SerializeField] private Renderer _renderer;
    public void SetMaterial(Material material)
    {
        if (material == null || _renderer ==null) return;
        // Получаем все материалы
        Material[] mats = _renderer.materials;

        // Изменяем цвет конкретного
        mats[2] = material;

        // Обновляем материалы на рендерере
        _renderer.materials = mats;
    }
}
