using System;
using System.Collections.Generic;
using Unity.Netcode;

public class PlayerManager : NetworkSingleton<PlayerManager>
{
    public NetworkList<PlayerData> PlayerList = new NetworkList<PlayerData>();
    public Action OnPlayerListChanged;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        PlayerList = new NetworkList<PlayerData>();
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        PlayerList.OnListChanged += PlayerList_OnListChanged;
    }

    private void PlayerList_OnListChanged(NetworkListEvent<PlayerData> changeEvent)
    {
        OnPlayerListChanged?.Invoke();
    }

    /// <summary>
    /// Gets the player data by client ID
    /// </summary>
    /// <param name="clientId">The client ID</param>
    /// <returns>PlayerData that contains the clientID param</returns>
    public PlayerData GetPlayerDataByClientId(ulong clientId)
    {
        foreach (PlayerData p in PlayerList)
        {
            if (p.ClientId == clientId)
            {
                return p;
            }
        }
        return new PlayerData();
    }

    /// <summary>
    /// Server RPC to add the PlayerData to the list
    /// </summary>
    /// <param name="playerData">PlayerData struct: clientID, playerName and Role (sett initially to NONE)</param>
    [ServerRpc(RequireOwnership = false)]
    public void AddPlayerServerRpc(PlayerData playerData)
    {
        PlayerList.Add(playerData);
    }

    /// <summary>
    /// Server RPC updates the role of the player by sender client id
    /// </summary>
    /// <param name="sailingRole">The new role the player wants</param>
    /// <param name="serverRpcParams">contains the client id of the sender</param>
    [ServerRpc(RequireOwnership = false)]
    public void SetPlayerSailingRoleServerRpc(SailingRole sailingRole, ServerRpcParams serverRpcParams = default)
    {
        UpdatePlayerSailingRole(serverRpcParams.Receive.SenderClientId, sailingRole);
    }

    private void UpdatePlayerSailingRole(ulong senderClientId, SailingRole sailingRole)
    {
        for (int i = 0; i < PlayerList.Count; i++)
        {
            if (PlayerList[i].ClientId == senderClientId)
            {
                PlayerData updatePlayerData = PlayerList[i];
                updatePlayerData.SailingRole = sailingRole;
                PlayerList[i] = updatePlayerData;
                break;
            }
        }
        CheckRoleAssignmentCompleted();
    }

    private void CheckRoleAssignmentCompleted()
    {
        HashSet<SailingRole> requiredRoles = new HashSet<SailingRole>
        {
        SailingRole.HELMSMAN,
        SailingRole.SAIL_TRIMMER,
        SailingRole.LOOKOUT,
        SailingRole.NAVIGATOR
        };

        HashSet<SailingRole> assignedRoles = new HashSet<SailingRole>();

        foreach (PlayerData playerData in PlayerList)
        {
            if (requiredRoles.Contains(playerData.SailingRole))
            {
                assignedRoles.Add(playerData.SailingRole);
            }
        }

        if (assignedRoles.SetEquals(requiredRoles))
        {
            GameManager.Instance.AllPlayersReady();
        }
    }
}