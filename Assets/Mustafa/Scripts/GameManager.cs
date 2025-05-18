using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Mirror;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : NetworkBehaviour
{
    public enum GameState
    {
        Lobby,
        Game
    }

    [SyncVar(hook = nameof(OnGameStateChanged))] public GameState gameState=GameState.Lobby;
    
    public Transform[] startPoses;

    public static Action<GameState> GameStateChanged;

    public static GameManager Init;

    public List<int> ids;

    public GameObject lobbyCamera;
    public GameObject startButton;
    
    [Command(requiresAuthority = false)]
    public void GetId(Player player)
    {
        List<Player> players = GameObject.FindObjectsOfType<Player>().ToList();
        foreach (var id in ids)
        {
            if (!players.Exists(a=>a.Id==id))
            {
                player.Id = id;
                return;
            }
        }
    }

    public List<Player> Players()
    {
        List<Player> players = GameObject.FindObjectsOfType<Player>().ToList();
        return players;
    }

    public Transform GetLobbyPos(int id)
    {
        return startPoses[id];
    }

    private void Awake()
    {
        Init = this;
    }

    public void OnGameStateChanged(GameState oldVal,GameState newVal)
    {
        GameStateChanged?.Invoke(newVal);
        if (newVal==GameState.Lobby)
        {
            lobbyCamera.SetActive(true);
        }
        else if(gameState == GameState.Game)
        {
            lobbyCamera.SetActive(false);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if (!NetworkManager.singleton)
        {
            SceneManager.LoadScene(0);
            return;
        }

        if (gameState==GameState.Lobby)
        {
            OnGameStateChanged(GameState.Lobby,GameState.Lobby);
        }
        startButton.SetActive(isServer);
    }

    public void StartGame()
    {
        gameState = GameState.Game;
    }
}
