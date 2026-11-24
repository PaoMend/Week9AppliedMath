using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    public float maxHP = 100f;
    public float currentHP = 100f;
    public float damageAmount = 5f;
    public float regenerationRate = 1f;
    public Slider healthSlider;
    public GameObject gameover;

    private void Start()
    {
        currentHP = maxHP;

        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHP;
            healthSlider.value = currentHP;
        }
    }

    private void Update()
    {
        RegenerateHP();
        UpdateUI();
    }

    private void RegenerateHP()
    {
        if (currentHP < maxHP)
        {
            currentHP += regenerationRate * Time.deltaTime;

            currentHP = Mathf.Min(currentHP, maxHP);
        }
    }

    public void TakeDamage()
    {
        currentHP -= damageAmount;

        currentHP = Mathf.Max(currentHP, 0f);

        UpdateUI();

        if (currentHP <= 0f)
        {
            gameover.SetActive(true);
        }
    }

    private void UpdateUI()
    {
        if (healthSlider != null)
        {
            healthSlider.value = currentHP;
        }
    }
}
