using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class SceneChanger : MonoBehaviour
    {
        public void ChangeScene(string sceneToLoad)
        {
            SceneManager.LoadScene($"{sceneToLoad}");
        }
    }
}