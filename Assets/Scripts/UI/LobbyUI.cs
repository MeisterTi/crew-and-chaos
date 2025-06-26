using TMPro;
using Unity.Netcode;
using UnityEngine;

public class LobbyUI : BaseUI
{
    [SerializeField] private TextMeshProUGUI hostIPAddressText;
    [SerializeField] private Transform playerUIContainer;
    [SerializeField] private Transform singlePlayerUIPrefab;

    private void Awake()
    {
        foreach (ulong clientId in NetworkManager.Singleton.ConnectedClientsIds)
        {
            Transform singlePlayerUITransform = Instantiate(singlePlayerUIPrefab, playerUIContainer);
            if (singlePlayerUITransform.TryGetComponent(out SinglePlayerUI singlePlayerUI))
            {
                singlePlayerUI.SetPlayerName("Test");
            }
        }
        if (NetworkManager.Singleton.IsServer)
        {
            hostIPAddressText.text = Networking.GetLocalIP();
        }
        else
        {
            hostIPAddressText.text = SceneName.Lobby.ToString();
        }
    }
}
