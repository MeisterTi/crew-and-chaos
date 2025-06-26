using System;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUI : BaseUI
{
    [SerializeField] private TextMeshProUGUI hostIPAddressText;
    [SerializeField] private Transform playerUIContainer;
    [SerializeField] private Transform singlePlayerUIPrefab;
    [SerializeField] private Button startSailingAdventureButton;

    private void Awake()
    {
        if (NetworkManager.Singleton.IsServer)
        {
            hostIPAddressText.text = Networking.GetLocalIP();
            startSailingAdventureButton.gameObject.SetActive(true);
            startSailingAdventureButton.onClick.AddListener(() => GameManager.Instance.StartAdventure());
            startSailingAdventureButton.interactable = false;
        }
        else
        {
            hostIPAddressText.text = SceneName.Lobby.ToString();
            startSailingAdventureButton.gameObject.SetActive(false);
        }
    }

    private void Start()
    {
        PlayerManager.Instance.OnPlayerListChanged += PlayerManager_OnPlayerListChanged;
        if (NetworkManager.Singleton.IsServer)
        {
            GameManager.Instance.CurrentGamestate.OnValueChanged += GameManager_OnValueChanged;
        }
        UpdatePlayerListUI();
    }

    private void GameManager_OnValueChanged(GameState previousValue, GameState newValue)
    {
        if (newValue == GameState.ALL_READY)
        {
            startSailingAdventureButton.interactable = true;
        }
    }

    private void PlayerManager_OnPlayerListChanged()
    {
        UpdatePlayerListUI();
    }

    private void UpdatePlayerListUI()
    {
        foreach (Transform child in playerUIContainer)
        {
            Destroy(child.gameObject);
        }
        HashSet<SailingRole> takenRoles = new HashSet<SailingRole>();
        foreach (ulong clientId in NetworkManager.Singleton.ConnectedClientsIds)
        {
            PlayerData playerData = PlayerManager.Instance.GetPlayerDataByClientId(clientId);
            if (playerData.SailingRole != SailingRole.NONE)
            {
                takenRoles.Add(playerData.SailingRole);
            }
        }
        foreach (ulong clientId in NetworkManager.Singleton.ConnectedClientsIds)
        {
            PlayerData playerData = PlayerManager.Instance.GetPlayerDataByClientId(clientId);
            Transform singlePlayerUITransform = Instantiate(singlePlayerUIPrefab, playerUIContainer);
            if (singlePlayerUITransform.TryGetComponent(out SinglePlayerUI singlePlayerUI))
            {
                singlePlayerUI.SetPlayerName(playerData.PlayerName.Value);
                if (clientId == NetworkManager.Singleton.LocalClientId)
                {
                    singlePlayerUI.SetAvailableRoles(takenRoles);
                }
            }
        }
    }
}
