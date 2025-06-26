using System;
using TMPro;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using UnityEngine.UI;

public class NetworkUI : BaseUI
{
    [SerializeField] private TMP_InputField playerNameInput;
    [SerializeField] private TMP_InputField ipInput;
    [SerializeField] private Button hostGameButton;
    [SerializeField] private Button joinGameButton;

    private void Awake()
    {
        hostGameButton.onClick.AddListener(StartHost);
        joinGameButton.onClick.AddListener(StartClient);
        playerNameInput.onValueChanged.AddListener(ChangeName);
        ipInput.onValueChanged.AddListener(ChangeIP);
        hostGameButton.interactable = false;
        joinGameButton.interactable = false;
    }

    private void Start()
    {
        NetworkManager.Singleton.OnClientConnectedCallback += NetworkManager_OnClientConnectedCallback;
    }

    private void NetworkManager_OnClientConnectedCallback(ulong clientId)
    {
        if (NetworkManager.Singleton.LocalClientId == clientId)
        {
            PlayerData playerData = new PlayerData(clientId, playerNameInput.text);
            PlayerManager.Instance.AddPlayerServerRpc(playerData);
        }
    }

    private void StartHost()
    {
        NetworkManager.Singleton.StartHost();
        SceneLoader.LoadNetworkScene(SceneName.Lobby);
    }

    private void StartClient()
    {
        NetworkManager.Singleton.GetComponent<UnityTransport>().SetConnectionData(ipInput.text, 7777);
        NetworkManager.Singleton.StartClient();
    }

    private void ChangeName(string name)
    {
        if (!string.IsNullOrEmpty(name))
        {
            hostGameButton.interactable = true;
            if (!string.IsNullOrEmpty(ipInput.text))
            {
                joinGameButton.interactable = true;
            }
        }
        else
        {
            hostGameButton.interactable = false;
            joinGameButton.interactable = false;
        }
    }

    private void ChangeIP(string name)
    {
        if (!string.IsNullOrEmpty(name))
        {
            if (!string.IsNullOrEmpty(playerNameInput.text))
            {
                joinGameButton.interactable = true;
            }
        }
        else
        {
            joinGameButton.interactable = false;
        }
    }
}
