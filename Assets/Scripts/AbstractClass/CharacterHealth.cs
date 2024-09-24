using System;
using UnityEngine;

namespace AbstractClass
{
    /// <summary>
    /// Provides core functionalities like health management, damage, and death handling.
    /// </summary>
    public class CharacterHealth : AbsHealth
    {
        public Action OnDead;
        public EventHandler<OnHealthChangedEventArgs> OnHealthChanged;

        public class OnHealthChangedEventArgs : EventArgs
        {
            public int HealthUpdated;
        }

        protected int hpMax;
        protected bool isDead = false;
        protected int hpCurrent;
        protected int damage;

        private void OnEnable() => Reborn();

        public virtual void Reborn()
        {
            this.hpCurrent = this.hpMax;
            this.isDead = false;
            CallOnHealthChangedEvent(hpCurrent);
        }

        public void CallOnHealthChangedEvent(int healthUpdated)
        {
            OnHealthChanged?.Invoke(this, new OnHealthChangedEventArgs { HealthUpdated = healthUpdated });
        }

        public override void Add(int hpAdd)
        {
            if (this.isDead) return;
            if (this.hpCurrent >= this.hpMax) return;

            this.hpCurrent += hpAdd;
            CallOnHealthChangedEvent(hpCurrent);
        }

        public override void Deduct(int hpDeduct)
        {
            if (IsDead()) return;

            this.hpCurrent -= hpDeduct;
            CallOnHealthChangedEvent(hpCurrent);
            this.CheckIsDead();
        }

        protected virtual bool IsDead() => this.hpCurrent <= 0;

        protected virtual void CheckIsDead()
        {
            if (!this.IsDead()) return;
            this.isDead = true;
            OnDead?.Invoke();
        }

        public int GetCurrentHp() => this.hpCurrent;
        public int GetHpMax() => this.hpMax;

        public override float GetMoveSpeed() => 0f;

        public override int GetDamage() => damage;

        public override void SetDamage(int value) => damage = value;
    }

}
