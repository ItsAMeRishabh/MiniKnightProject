using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;
public class GameManager : MonoBehaviourPunCallbacks
{
    public GameObject playerPrefab;

    public GameObject lobbyUI;
    public GameObject GameUI;

    public Transform[] spawnPoints;

    public TextMeshProUGUI statusText;

    public TextMeshProUGUI playerCountText;

    public Button startGameButton;

    
    private void Start()
    {
        startGameButton.onClick.AddListener(StartGame);
        startGameButton.interactable = false;
    }

    private void Update()
    {
        statusText.text = "Status: " + PhotonNetwork.NetworkClientState.ToString();

        playerCountText.text = "Players Connected: " + PhotonNetwork.PlayerList.Length;

        if (PhotonNetwork.IsMasterClient)
        {
            startGameButton.interactable = true;
        }
    }

    void StartGame()
    {
        Photon.Realtime.Player[] players = PhotonNetwork.PlayerList;

        for(int i = 0 ; i < players.Length ; i++)
        {
            photonView.RPC("RPCStartGame", players[i], spawnPoints[i].position);
        }
    }



    [PunRPC]
    void RPCStartGame(Vector3 spawnPos)
    {
        lobbyUI.SetActive(false);
        GameUI.SetActive(true);
        PhotonNetwork.Instantiate(playerPrefab.name, spawnPos, Quaternion.identity);
    }

}
