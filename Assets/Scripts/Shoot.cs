using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class Shoot : MonoBehaviourPunCallbacks , IPunObservable
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private float bulletSpeed = 100f;
    [SerializeField] private float maxShotRythm = 1f;
    [SerializeField] private Camera cam;
    [SerializeField] private Rigidbody rigidbody;
    private float shotTimer;
    private float shotTimer2;

    private bool canShoot = true;
    private PhotonView phtView;

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
    void Update()
    {
        phtView = GetComponentInParent<PhotonView>();
        if (!phtView.IsMine)
        {
            return;
        }
        if (Input.GetMouseButton(1)&& canShoot == true)
        {
            GameObject spawnBullet = PhotonNetwork.Instantiate(bullet.name, transform.position, Quaternion.identity);
            Rigidbody spawnBulletRB = spawnBullet.GetComponent<Rigidbody>();

            spawnBulletRB.velocity = cam.transform.forward*bulletSpeed;
            canShoot = false;
        }

        if (!canShoot)
        {
            shotTimer += Time.deltaTime;

        }

        if (shotTimer > maxShotRythm)
        {
            canShoot = true;
            shotTimer = 0f;
        }

    }
   

}
