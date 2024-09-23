using UnityEngine;

namespace LoadScene
{
    public class LoadingCallback : MonoBehaviour
    {
        private bool isFirstUpadate = true;

        private void Update()
        {
            if (isFirstUpadate)
            {
                isFirstUpadate = false;
                Loader.LoaderCallback();
            }
        }
    }
}