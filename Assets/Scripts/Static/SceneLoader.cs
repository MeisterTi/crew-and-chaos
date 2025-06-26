using Unity.Netcode;
using UnityEngine.SceneManagement;

public static class SceneLoader
{
    /// <summary>
    /// Load scene locally for players
    /// </summary>
    /// <param name="sceneName">SceneName enum contains the possible scene names</param>
    public static void LoadScene(SceneName sceneName)
    {
        SceneManager.LoadScene(sceneName.ToString());
    }

    /// <summary>
    /// Load scene for all players that are connected to the host network
    /// </summary>
    /// <param name="sceneName">ceneName enum contains the possible scene names</param>
    public static void LoadNetworkScene(SceneName sceneName)
    {
        NetworkManager.Singleton.SceneManager.LoadScene(sceneName.ToString(), LoadSceneMode.Single);
    }
}
