using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class KnockBack : MonoBehaviourPun
{
    [SerializeField] private float knockBackStrength;

    void OnCollisionEnter(Collision collision)
    {
        if (photonView.IsMine)
        {
            photonView.RPC("KnokBak", RpcTarget.All, collision);
        }
    }

    [PunRPC]
    void KnokBak(Collision collision)
    {
        Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Debug.Log("collide");
            Vector3 direction = collision.transform.position - transform.position;
            rb.AddForce(direction.normalized * knockBackStrength);
        }
    }
}
