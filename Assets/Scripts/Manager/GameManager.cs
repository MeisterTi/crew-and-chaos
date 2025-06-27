using System;
using Unity.Netcode;

public class GameManager : NetworkSingleton<GameManager>
{
    public NetworkVariable<GameState> CurrentGamestate = new NetworkVariable<GameState>(GameState.INIT);

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
    }

    /// <summary>
    /// Set all players ready, changing this value triggers an event
    /// </summary>
    public void AllPlayersReady()
    {
        CurrentGamestate.Value = GameState.ALL_READY;
    }


    /// <summary>
    /// Start the Game, Loads the new game scene and triggers an event
    /// </summary>
    public void StartAdventure()
    {
        SceneLoader.LoadNetworkScene(SceneName.Game);
        CurrentGamestate.Value = GameState.COUNTDOWN_TO_START;
    }
}