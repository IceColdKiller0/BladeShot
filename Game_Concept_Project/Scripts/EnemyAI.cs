using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private float _speed = 7.0f;

    private UIManager _uiManager;
    [SerializeField]
    private AudioClip _clip;

    // Start is called before the first frame update
    void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        
    }

    // Update is called once per frame
    void Update()
    {
        if (_uiManager.score >= 40)
        {
            transform.Translate(Vector3.down * _speed * 1.8f * Time.deltaTime);

            if (transform.position.x < -11)
            {
                float randomX = Random.Range(-4f, 4f);
                transform.position = new Vector3(11, randomX, 0);
            }
        }
        else

        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.x < -11)
        {
            float randomX = Random.Range(-4f, 4f);
            transform.position = new Vector3(11, randomX, 0);
        }

        //if (_uiManager.score >= 20)
        //{
        //   transform.Translate(Vector3.down * _speed  * Time.deltaTime);

        //    if (transform.position.x < -11)
        //    {
        //        float randomX = Random.Range(-4f, 4f);
        //        transform.position = new Vector3(11, randomX, 0);
        //    }
        //}

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            if (other.transform.parent != null)
            {
                Destroy(other.transform.parent.gameObject);
            }

            Destroy(other.gameObject);
            _uiManager.updateScore();
            AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position);
            Destroy(this.gameObject);
        }
        else if (other.tag == "Player")
        {
            PlayerControls player = other.GetComponent<PlayerControls>();

            if (player != null)
            {
                player.Damage();
                
            }
            _uiManager.updateScore();
            AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position);
            Destroy(this.gameObject);
        }
    }

}
