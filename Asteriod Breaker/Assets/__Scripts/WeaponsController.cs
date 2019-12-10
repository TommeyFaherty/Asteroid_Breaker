using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class WeaponsController : MonoBehaviour
{
    // spawn the bullets when the user hits the space bar
    // give the bullets a speed, and an upwards direction (Vector2(0,1))

    [SerializeField]
    private float bulletSpeed = 5.0f;

    [SerializeField]
    private float firingRate = 0.4f;

    private GameObject bulletParent;

    [SerializeField]
    private Bullet bulletPrefab;
    private Dictionary<string, KeyCode> keys = new Dictionary<string,KeyCode>();

    // Start is called before the first frame update
    void Start()
    {
        // get the bullet parent - keep tidy
        bulletParent = ParentUtils.GetBulletParent();
        keys.Add("Shoot", (KeyCode)System.Enum.Parse(typeof(KeyCode),PlayerPrefs.GetString("Shoot","Space")));
    }

    // Update is called once per frame
    void Update()
    {
        if( Input.GetKeyDown(keys["Shoot"]))
        {
            FindObjectOfType<AudioManager>().Play("laser");
            InvokeRepeating("Shoot", 0f, firingRate);
        }
        if(Input.GetKeyUp(keys["Shoot"]))
        {
            CancelInvoke("Shoot");
        }
    }

    // need a method to create bullet
    // use invokeRepeating rather CoRoutine
    private void Shoot()
    {
        Bullet bullet = Instantiate(bulletPrefab, bulletParent.transform);
        bullet.transform.position = transform.position;
        // get the rigidbody from the bullet and give it movement
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.up * bulletSpeed;   // same as Vector2(0, 1);
        // play shooting sound clip here later
    }
}
