using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SkillTreeUI : MonoBehaviour
    {
        [SerializeField]
        private GameObject pauseMenu;
        

        public void OpenTabUpgrade()
        {
            pauseMenu.SetActive(true);
        }
        public void Home()
        {

        }
        public void CloseTabUpgrade()
        {
            pauseMenu.SetActive(false);
        }
    }
}

