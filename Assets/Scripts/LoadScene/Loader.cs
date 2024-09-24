using UnityEngine.SceneManagement;

namespace LoadScene
{
    public static class Loader
    {
        public enum Scene
        {
            LoadingScene,
            GameMenuScene,
            GameScene
        }

        private static string targetScene;

        public static void Load(Scene targetScene) {
            Loader.targetScene = targetScene.ToString();
            SceneManager.LoadScene(Scene.LoadingScene.ToString());
        }

        public static void LoaderCallback() => SceneManager.LoadScene(targetScene);
    }
}