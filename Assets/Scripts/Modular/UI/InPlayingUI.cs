using RepeatUtils;
using UnityEngine;

namespace UI
{
    public class InPlayingUI : RepeatMonoBehaviour
    {
        [SerializeField]
        private PlayerStatsUI playerStatusUI;
        public PlayerStatsUI PlayerStatusUI => playerStatusUI;

        [SerializeField]
        private UpgradeUI upgradeUI;
        private UpgradeUI UpgradeUI => upgradeUI;

        protected override void LoadComponents()
        {
            base.LoadComponents();
            LoadComponentInChild(ref upgradeUI, gameObject);
            LoadComponentInChild(ref upgradeUI, gameObject);
        }

        private void Start()
        {
            upgradeUI.gameObject.SetActive(false);
        }

        public void ShowUI(bool isShow)
        {
            //playerStatusUI.gameObject.SetActive(isShow);
        }
    }
}

