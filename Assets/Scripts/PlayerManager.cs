using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviourPunCallbacks, IPunObservable
{
    [SerializeField] private GameObject playerUiPrefab;
    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private Camera cam;
    public static GameObject LocalPlayerInstance;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)

        {
                stream.SendNext(rigidbody.position);
                stream.SendNext(rigidbody.velocity);

        }

        else

        {
                rigidbody.position = (Vector3)stream.ReceiveNext();
                rigidbody.velocity = (Vector3)stream.ReceiveNext();

                float lag = Mathf.Abs((float)(PhotonNetwork.Time - info.SentServerTime));

                rigidbody.position += rigidbody.velocity * lag;

        }

    }
    

    void Awake()
    {
        if (photonView.IsMine)
        {
            PlayerManager.LocalPlayerInstance = this.gameObject;
        }

        DontDestroyOnLoad(this.gameObject);

    }


    private void Start()
    {


        cam = GetComponentInChildren<Camera>();

        if (!photonView.IsMine)
        {
            cam.enabled = false;
            return;
        }

        if (playerUiPrefab != null)
        {
            GameObject _uiGo = Instantiate(playerUiPrefab);
            _uiGo.SendMessage("SetTarget", this, SendMessageOptions.RequireReceiver);
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode loadMode)
    {
        CalledOnLevelWasLoaded(scene.buildIndex);
    }

    void OnLevelWasLoaded(int level)
    { 
        CalledOnLevelWasLoaded(level);
    }

    void CalledOnLevelWasLoaded(int level)
    {
        GameObject _uiGo = Instantiate(playerUiPrefab);
        _uiGo.SendMessage("SetTarget", this, SendMessageOptions.RequireReceiver);
    }


}

