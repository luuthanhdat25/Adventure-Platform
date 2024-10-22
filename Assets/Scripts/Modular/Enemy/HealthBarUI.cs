using AbstractClass;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] 
    private Image barImage;

    [SerializeField]
    private CharacterHealth characterHealth;

    private void Start()
    {
        characterHealth.OnHealthChanged += CharacterHealth_OnHealthChanged;
        barImage.fillAmount = 0;
        Hide();
    }

    private void CharacterHealth_OnHealthChanged(object sender, CharacterHealth.OnHealthChangedEventArgs e)
    {
        barImage.fillAmount = e.HealthPersent;

        if (e.HealthPersent == 0 || e.HealthPersent == 1)
            Hide();
        else
            Show();
    }

    private void Show() => gameObject.SetActive(true);
    private void Hide() => gameObject.SetActive(false);
}
