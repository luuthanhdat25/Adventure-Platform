using RepeatUtils;
using UnityEngine;

namespace AbstractClass
{
    /// <summary>
    /// Abstract base class for movement components. Provides methods for controlling movement and rotation.
    /// </summary>
    public abstract class AbsMovement : RepeatMonoBehaviour
    {
        public abstract void Move(Vector3 moveDirectionOrDestination, float speed);
        public abstract void Rotate(Vector3 rotateDirection);

        /// <summary>
        /// Resets the movement state of the object.
        /// </summary>
        public virtual void ResetMovement()
        {
            return;
        } 
    }
}

