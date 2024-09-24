using RepeatUtils;
using UnityEngine;

namespace UI
{
    public class InPlayingUI : RepeatMonoBehaviour
    {
        [SerializeField]
        private GunStatusUI playerGunStatusUI;
        public GunStatusUI PlayerGunStatusUI => playerGunStatusUI;

        [SerializeField]
        private HealthStatusUI playerHealthStatusUI;
        private HealthStatusUI PlayerHealthStatusUI => playerHealthStatusUI;

        protected override void LoadComponents()
        {
            base.LoadComponents();
            LoadComponentInChild(ref playerGunStatusUI, gameObject);
            LoadComponentInChild(ref playerHealthStatusUI, gameObject);
        }

        public void ShowUI(bool isShow)
        {
            playerGunStatusUI.gameObject.SetActive(isShow);
            playerHealthStatusUI.gameObject.SetActive(isShow);
        }
    }
}

