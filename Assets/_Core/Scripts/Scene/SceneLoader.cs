using UnityEngine;
using UnityEngine.SceneManagement;

namespace FABRE.Scene
{
    public class SceneLoader : MonoBehaviour
    {
        public static void LoadSingle(string name)
        {
            SceneManager.LoadScene(name, LoadSceneMode.Single);
        }

        public static void Quit()
        {
            Application.Quit();
        }
    }
}