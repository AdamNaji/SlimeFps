using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;



    public class GameManager : MonoBehaviourPunCallbacks
    {

        [SerializeField] Camera standbyCamera;
        [SerializeField] private Transform[] spawnPoint;
        public static GameManager Instance;


    [SerializeField] private GameObject Player;
        bool StartGame = false;
        int numberPlayers;

        public override void OnLeftRoom()
        {
            SceneManager.LoadScene(0);
        }

        void Update()
        {
            CheckPlayers();
        }
        private void Start()
         {
             Instance = this;


        Spawn();

         }
        public void LeaveRoom()
        {
            Time.timeScale = 1;
            PhotonNetwork.LeaveRoom();
        }

        

        public override void OnPlayerEnteredRoom(Player other)
        {
            Debug.LogFormat("OnPlayerEnteredRoom() {0}", other.NickName); // not seen if you're the player connecting

            if (PhotonNetwork.IsMasterClient)
            {
                Debug.LogFormat("OnPlayerEnteredRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom


                LoadArena();
            }
        }


        public override void OnPlayerLeftRoom(Player other)
        {
            Debug.LogFormat("OnPlayerLeftRoom() {0}", other.NickName); // seen when other disconnects


            if (PhotonNetwork.IsMasterClient)
            {
                Debug.LogFormat("OnPlayerLeftRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom


                LoadArena();
            }
        }

        void LoadArena()
        {
            if (!PhotonNetwork.IsMasterClient)
            {
                Debug.LogError("PhotonNetwork : Trying to Load a level but we are not the master Client");
            }
            Debug.LogFormat("PhotonNetwork : Loading Level : {0}", PhotonNetwork.CurrentRoom.PlayerCount);
            PhotonNetwork.LoadLevel("Room for " + PhotonNetwork.CurrentRoom.PlayerCount);
            
        }
        void CheckPlayers()
        {
            numberPlayers = PhotonNetwork.CountOfPlayers;
            for (int i = 0; i <= numberPlayers;
                i++)
            {
                if (numberPlayers > 4)
                {
                    numberPlayers -= 4;
                }
            }
        }

        void Spawn()
        {
            CheckPlayers();
            if (numberPlayers == 1&& PlayerManager.LocalPlayerInstance == null)
            {
                PhotonNetwork.Instantiate(Player.name, spawnPoint[0].position, spawnPoint[0].rotation, 0);
                numberPlayers = 2;
            }

            else if (numberPlayers == 2 && PlayerManager.LocalPlayerInstance == null)
            {
                PhotonNetwork.Instantiate(Player.name, spawnPoint[1].position, spawnPoint[1].rotation, 0);
                numberPlayers = 3;
            }

            else if (numberPlayers == 3 && PlayerManager.LocalPlayerInstance == null)
            {
                PhotonNetwork.Instantiate(Player.name, spawnPoint[2].position, spawnPoint[2].rotation, 0);
                numberPlayers = 4;
            }

            else if (numberPlayers == 4 && PlayerManager.LocalPlayerInstance == null)
            {
                PhotonNetwork.Instantiate(Player.name, spawnPoint[3].position, spawnPoint[3].rotation, 0);
                numberPlayers = 1;
            }

        }
}
