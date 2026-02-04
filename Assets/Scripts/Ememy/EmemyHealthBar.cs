using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EmemyHealthBar : MonoBehaviour
{

    [SerializeField] private float timeToDrain = 0.25f;
    [SerializeField] private Gradient healthBarG;
    private Image image;
    private float target =1f;

    private Color newHealthBar;

    private Coroutine drainHealthBarCoroutine;

    private void Start()
    {
        image = GetComponent<Image>();

        image.color = healthBarG.Evaluate(target);

        CheckHealthBarGA();
    }

    public void UpdateHealthBar(float maxHealth, float currentHealth)
    {
        target = currentHealth / maxHealth;

        drainHealthBarCoroutine = StartCoroutine(DrainHealthBar());

        CheckHealthBarGA();
    }

    private IEnumerator DrainHealthBar()
    {
        float fillAmount = image.fillAmount;
        Color currentColor = image.color;
        float elapsedTime = 0f;
        while (elapsedTime < timeToDrain)
        {
            elapsedTime += Time.deltaTime;

            image.fillAmount = Mathf.Lerp(fillAmount, target, (elapsedTime / timeToDrain));

            image.color = Color.Lerp(currentColor, newHealthBar, (elapsedTime / timeToDrain));

            yield return null;
        }
    }

    private void CheckHealthBarGA()
    {
        newHealthBar = healthBarG.Evaluate(target);
    }
    
}
