using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;
    [SerializeField]
    private int powerupID;

    [SerializeField]
    private AudioClip _clip;

    // Update is called once per frame
    void Update()
    {
        transform.Translate (Vector3.left * _speed * Time.deltaTime);

        if (transform.position.x < -11)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collided with: " + other.name);

        if (other.tag == "Player")
        {
            PlayerControls player = other.GetComponent<PlayerControls>();

            AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position, 1f);

            if (player != null)
            {
                if (powerupID == 0)
                {
                    player.TripleShotPowerOn();
                }
                else if (powerupID == 1)
                {
                    player.SpeedBoostPowerOn();
                }
                //else if (powerupID == 2)
                //{

                //}

            }

            Destroy(this.gameObject);
        }

    }
}
