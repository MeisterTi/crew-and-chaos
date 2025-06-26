using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : BaseUI
{
    [SerializeField] private Button startGameButton;

    private void Awake()
    {
        startGameButton.onClick.AddListener(StartGameButtonClickHandler);
    }

    private void StartGameButtonClickHandler()
    {
        SceneLoader.LoadScene(SceneName.Networking);
    }
}
