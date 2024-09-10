using RepeatUtils;
using UnityEngine;

namespace AbstractClass
{
    /// <summary>
    /// Abstract base class for controller components. Provides a unified interface for managing common game object components.
    /// </summary>
    public abstract class AbsController : RepeatMonoBehaviour
    {
        [SerializeField]
        protected AbsGraphic absGraphic;
        public AbsGraphic AbsGraphic => absGraphic;

        [SerializeField]
        protected AbsAnimator absAnimator;
        public AbsAnimator AbsAnimator => absAnimator;

        [SerializeField]
        protected AbsMovement absMovement;
        public AbsMovement AbsMovement => absMovement;

        [SerializeField]
        protected AbsVisionSensor absVisionSensor;
        public AbsVisionSensor AbsVisionSensor => absVisionSensor;

        [SerializeField]
        protected AbsStat absStat;
        public AbsStat AbsStat => absStat;

        [SerializeField]
        protected AbsDamageReciver absDamageReciver;
        public AbsDamageReciver AbsDamageReciver => absDamageReciver;

        [SerializeField]
        protected AbsDamageSender absDamageSender;
        public AbsDamageSender AbsDamageSender => absDamageSender;


        protected override void LoadComponents()
        {
            base.LoadComponents();
            LoadComponentInChild<AbsGraphic>(ref absGraphic, gameObject);
            LoadComponentInChild<AbsAnimator>(ref absAnimator, gameObject);
            LoadComponentInChild<AbsMovement>(ref absMovement, gameObject);
            LoadComponentInChild<AbsVisionSensor>(ref absVisionSensor, gameObject);
            LoadComponentInChild<AbsStat>(ref absStat, gameObject);
            LoadComponent<AbsDamageSender>(ref absDamageSender, gameObject);
            LoadComponent<AbsDamageReciver>(ref absDamageReciver, gameObject);
        }

        public virtual void SetLayerMark(int layerMaskIndex) => gameObject.layer = layerMaskIndex;
    }
}

