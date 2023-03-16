using UnityEngine;

public interface IHealthBar
{
    public void Initialize(Camera camera = null);
    public void UpdateVisualDelayed(float currentHealth, float maxHealth);
    public void UpdateVisualInstant(float currentHealth, float maxHealth);
}