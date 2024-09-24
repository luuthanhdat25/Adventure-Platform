using RepeatUtils;
using System.Collections.Generic;
using UnityEngine;

namespace AbstractClass
{
    /// <summary>
    /// Abstract base class for objects that can send damage.
    /// </summary>
    public abstract class AbsDamageSender : RepeatMonoBehaviour
    {
        [SerializeField]
        protected AbsController controller;

        [SerializeField] 
        protected int damage;
        
        protected override void LoadComponents()
        {
            base.LoadComponents();
            LoadComponent<AbsController>(ref controller, gameObject);
        }

        /// <summary>
        /// Handles collision with a damage sender.
        /// </summary>
        /// <param name="damageSender">The object that caused the collision.</param>
        public virtual void CollisionWithController(AbsController absController)
        {
            if (absController == null) return;
            absController.AbsHealth.Deduct(GetDamage());
            absController.AbsDamageReciver.GotHit();
        }

        public void SetDamage(int damage) => this.damage = damage;
        
        public int GetDamage() => this.damage;

        public virtual List<AbsController> CheckCollision() => null;
    }
}