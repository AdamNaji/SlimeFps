using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class DeleteBullet : MonoBehaviour
{
    [SerializeField] private PhotonView myPhotonView;
    [SerializeField] private GameObject bullet;
    [SerializeField] private float lifeTime = 1.0f;
    private float timer;

    void Start()
    {
        myPhotonView = GetComponent<PhotonView>();
    }
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= lifeTime)
        {
            PhotonNetwork.Destroy(bullet);
        }
    }

}

