using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar2D : MonoBehaviour, IHealthBar
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private Image damageFill;
    [SerializeField] private Image healthFill;
    [SerializeField] private bool useDelayedDamageDisplay;

    [ShowIf("useDelayedDamageDisplay")]
    [SerializeField] private float damageAccumulationInterval;
    [ShowIf("useDelayedDamageDisplay")]
    [SerializeField] private float damageFadeInterval;

    private Coroutine _damageCoroutine;
    private WaitForSeconds _damageDelayWait;
    private bool _damageAnimationInProgress;

    public void Initialize(Camera camera = null)
    {
        canvas.worldCamera = camera;
        damageFill.fillAmount = 1;
        healthFill.fillAmount = 1;
        if (useDelayedDamageDisplay)
            _damageDelayWait = new WaitForSeconds(damageAccumulationInterval);
    }

    public void UpdateVisualDelayed(float currentHealth, float maxHealth)
    {
        if (useDelayedDamageDisplay)
        {
            healthFill.fillAmount = currentHealth / maxHealth;
            if (_damageCoroutine != null)
                StopCoroutine(_damageCoroutine);
            _damageCoroutine = StartCoroutine(DelayedDamageUpdate());
        }
        else
        {
            UpdateVisualInstant(currentHealth, maxHealth);
        }

    }

    public void UpdateVisualInstant(float currentHealth, float maxHealth)
    {
        var healthPercentage = currentHealth / maxHealth;
        healthFill.fillAmount = healthPercentage;
        damageFill.fillAmount = healthPercentage;
    }

    private IEnumerator DelayedDamageUpdate()
    {
        yield return _damageDelayWait;
        _damageCoroutine = null;

        while (_damageAnimationInProgress)
            yield return null;
        
        _damageAnimationInProgress = true;
        var accumulatedDamage = damageFill.fillAmount - healthFill.fillAmount;
        var elapsedTime = 0F;
        var targetFill = healthFill.fillAmount;
        while (elapsedTime < damageFadeInterval)
        {
            yield return null;
            elapsedTime += Time.deltaTime;
            var progress = Mathf.Min(1, elapsedTime / damageFadeInterval);
            var newFill = targetFill + (accumulatedDamage * (1 - progress));
            if (newFill < healthFill.fillAmount)
            {
                damageFill.fillAmount = healthFill.fillAmount;
                yield break;
            }
            damageFill.fillAmount = newFill;
        }

        _damageAnimationInProgress = false;
    }
}