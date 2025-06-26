using Unity.Netcode;
using UnityEngine.SceneManagement;

public static class SceneLoader
{
    public static void LoadScene(SceneName sceneName)
    {
        SceneManager.LoadScene(sceneName.ToString());
    }

    public static void LoadNetworkScene(SceneName sceneName)
    {
        NetworkManager.Singleton.SceneManager.LoadScene(sceneName.ToString(), LoadSceneMode.Single);
    }
}
