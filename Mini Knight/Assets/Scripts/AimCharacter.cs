using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimCharacter : MonoBehaviour
{
    private Transform AimTransform;
    public Transform barrel;

    Vector2 direction;

    public GameObject bullet;
    public GameObject Aimm;

    public float bulletSpeed = 50;
    public int GunDurability = 3;

    public string Bullets;

    private ObjectPooler objectpool;

    public static AimCharacter aim_instance;

    private void Start()
    {
        objectpool = FindObjectOfType<ObjectPooler>();
        aim_instance = this;
    }

    private void Awake()
    {
        AimTransform = transform.Find("Aim");
    }

    void Update()
    {
        //Aim at Mouse Pointer

        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
        AimTransform.up = direction;

        //Shooting
        if(GunDurability >= 1)
        {
            if(Input.GetMouseButtonDown(0))
            {
                GameObject bulletc = objectpool.SpawnFromPool(Bullets , barrel.position , Quaternion.Euler(0, 0, 1));
                bulletc.GetComponent<Rigidbody2D>().velocity = barrel.right * bulletSpeed;
                GunDurability--;
            }
        }
        if(GunDurability == 0)
        {
            Aimm.SetActive(false);
        }
    }
}
