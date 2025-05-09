using UnityEngine;

public sealed class AndroidBattery : MonoBehaviour
{
    [SerializeField] private Renderer _renderer;
    public void SetMaterial(Material material)
    {
        if (material == null || _renderer ==null) return;
        // �������� ��� ���������
        Material[] mats = _renderer.materials;

        // �������� ���� �����������
        mats[2] = material;

        // ��������� ��������� �� ���������
        _renderer.materials = mats;
    }
}
