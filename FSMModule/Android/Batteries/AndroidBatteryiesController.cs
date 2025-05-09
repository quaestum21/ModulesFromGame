using System.Collections.Generic;
using UnityEngine;

public sealed class AndroidBatteryiesController : MonoBehaviour
{
    [SerializeField] private Health _health; // Ссылка на компонент здоровья
    [SerializeField] private List<AndroidBattery> _batteries; // Три батарейки
    [SerializeField] private Material _greenMaterial;
    [SerializeField] private Material _yellowMaterial;
    [SerializeField] private Material _redMaterial;

    private void Start() => UpdateBatteryVisual();
    private void OnEnable()
    {
        _health.OnTakeDamage += UpdateBatteryVisual;
        _health.OnDie += UpdateBatteryVisual;
    }
    private void OnDisable()
    {
        _health.OnTakeDamage -= UpdateBatteryVisual;
        _health.OnDie -= UpdateBatteryVisual;
    }
    public void UpdateBatteryVisual()
    {
        float healthPercent = _health.CurrentHealth / _health.MaxHealth;

        // Скрываем все батарейки сначала
        foreach (var battery in _batteries)
            battery.gameObject.SetActive(false);
        
        //в зависимости от кол-ва здоровья меняем визуальную часть батареек
        if (healthPercent > 0.7f)
        {
            for (int i = 0; i < _batteries.Count; i++)
            {
                _batteries[i].gameObject.SetActive(true);
                _batteries[i].SetMaterial(_greenMaterial);
            }
        }
        else if (healthPercent > 0.3f)
        {
            for (int i = 0; i < 2; i++)
            {
                _batteries[i].gameObject.SetActive(true);
                _batteries[i].SetMaterial(_yellowMaterial);
            }
        }
        else
        {
            _batteries[0].gameObject.SetActive(true);
            _batteries[0].SetMaterial(_redMaterial);
        }
    }
}
