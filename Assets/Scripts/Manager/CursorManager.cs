using RepeatUtils.DesignPattern.SingletonPattern;
using UnityEngine;

namespace Manager
{
    public class CursorManager : Singleton<CursorManager>
    {
        [SerializeField] 
        private Sprite aimCursorSprite;

        [SerializeField] 
        private Sprite mouseCursorSprite;
    }
}
