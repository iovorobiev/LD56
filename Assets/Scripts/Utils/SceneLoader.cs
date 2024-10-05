using System.Collections;
using UnityEngine.SceneManagement;

namespace Utils
{
    public class SceneLoader
    {
        public static IEnumerator LoadSceneAsync(int index)
        {
            var asyncOperation = SceneManager.LoadSceneAsync(index);
            while (!asyncOperation.isDone)
            {
                yield return null;
            }
        } 
    }
}