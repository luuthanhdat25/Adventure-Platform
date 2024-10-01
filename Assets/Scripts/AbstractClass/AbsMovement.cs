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
        
        public virtual void Rotate(Vector3 rotateDirection)
        {
            return;
        }

        /// <summary>
        /// Resets the movement state of the object.
        /// </summary>
        public virtual void ResetMovement()
        {
            return;
        } 

        public virtual void Flip()
        {
            Quaternion currentRotation = transform.parent.rotation;
            Quaternion flippedRotation = Quaternion.Euler(currentRotation.eulerAngles.x, currentRotation.eulerAngles.y + 180f, currentRotation.eulerAngles.z);
            transform.parent.rotation = flippedRotation;
        }
    }
}

