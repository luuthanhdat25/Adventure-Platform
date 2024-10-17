    using System.Collections;
    using System.Collections.Generic;
    using AbstractClass;
    using RepeatUtils.DesignPattern.SingletonPattern;
    using UnityEngine;

    public class PlayerSingleton : Singleton<PlayerSingleton>
    {
        [SerializeField] private playerSO playerStat;
        private PlayerHealth _playerHealth;



        private int currentHealth;
        private int maxHealth;
        
        protected override void Awake()
        {
            base.Awake();
            this.maxHealth = playerStat.Health;
            this.currentHealth = playerStat.Health;

            _playerHealth = GetComponentInChildren<PlayerHealth>();
            _playerHealth.SetHealth(playerStat.Health);
        }
        
        public void AddHealth(int hpAdd)
        {
            _playerHealth.Add(hpAdd); 
            currentHealth = Mathf.Clamp(_playerHealth.GetCurrentHp(), 0, maxHealth);
        }

        public void DeductHealth(int hpDeduct)
        {
            _playerHealth.Deduct(hpDeduct); 
            currentHealth = Mathf.Clamp(_playerHealth.GetCurrentHp(), 0, maxHealth);
        }
        public int GetHealth()=>currentHealth;

        public float GetHealthPersen() => (float) currentHealth / maxHealth;
        
        public void GetSkill()
        {
            
        }

        public void UpdateSkill()
        {
            
        }

        public void GetMoveSpeed()
        {
            
        }
    }
