using RepeatUtils;
using UnityEngine;
using UnityEngine.Serialization;

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

        [FormerlySerializedAs("absStat")] [SerializeField]
        protected AbsHealth absHealth;
        public AbsHealth AbsHealth => absHealth;

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
            LoadComponentInChild<AbsMovement>(ref absMovement, gameObject);
            LoadComponentInChild<AbsVisionSensor>(ref absVisionSensor, gameObject);
            LoadComponentInChild<AbsHealth>(ref absHealth, gameObject);
            LoadComponent<AbsAnimator>(ref absAnimator, gameObject);
            LoadComponent<AbsDamageSender>(ref absDamageSender, gameObject);
            LoadComponent<AbsDamageReciver>(ref absDamageReciver, gameObject);
        }

        public virtual void SetLayerMark(int layerMaskIndex) => gameObject.layer = layerMaskIndex;
    }
}

