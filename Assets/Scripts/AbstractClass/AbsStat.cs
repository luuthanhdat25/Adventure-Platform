using RepeatUtils;
using Unity.VisualScripting;

namespace AbstractClass
{
    /// <summary>
    /// Abstract base class for managing entity stats.
    /// </summary>
    public abstract class AbsStat : RepeatMonoBehaviour
    {
        public virtual void Deduct(int hpDeduct)
        {
            return;
        }

        public virtual void Add(int hpAdd)
        {
            return;
        }

        public virtual float GetMoveSpeed() => 0f;

        public virtual int GetDamage() => 0;

        public virtual void SetDamage(int value)
        {
            return;
        }
    }
}
