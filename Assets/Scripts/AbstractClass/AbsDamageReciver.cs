using RepeatUtils;
using UnityEngine;

namespace AbstractClass
{
    /// <summary>
    /// Abstract base class for objects that can receive damage.
    /// </summary>
    public abstract class AbsDamageReciver : RepeatMonoBehaviour
    {
        [SerializeField]
        protected AbsController absController;

        protected override void LoadComponents()
        {
            base.LoadComponents();
            LoadComponent<AbsController>(ref absController, gameObject);
        }

        /// <summary>
        /// Called when got hit.
        /// </summary>
        public abstract void GotHit();
    }
}