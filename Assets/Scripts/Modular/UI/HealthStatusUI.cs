using AbstractClass;
using TMPro;
using UnityEngine;

namespace UI 
{
    public class HealthStatusUI : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text healthText;

        private void Awake()
        {
            //PlayerPublicInfor.Instance.Controller.AbsHealth.OnHealthChanged += OnHealthChanged;
        }

        private void OnHealthChanged(object sender, AbsHealth.OnHealthChangedEventArgs e)
        {
            healthText.text = e.HealthUpdated.ToString();
        }
    }
}
