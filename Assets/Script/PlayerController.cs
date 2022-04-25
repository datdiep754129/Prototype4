using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public bool powerUp;
    private float powerUpStrength = 15f;
    private Rigidbody playerRb;
    private GameObject focalPoint;
    public GameObject powerupIndicator;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * speed * forwardInput);
        powerupIndicator.transform.position = transform.position + new Vector3(0, 0, 0);
        powerupIndicator.transform.Rotate(0,1,0) ;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy") && powerUp)
        {
            //Get Enemy Component & set location away from player
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position);

            Debug.Log("Collied with " + collision.gameObject.name + " with Powerup set to " + powerUp);
            enemyRb.AddForce(awayFromPlayer * powerUpStrength, ForceMode.Impulse);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("PowerUp"))
        {
            powerUp = true;
            Destroy(other.gameObject);

            
            StartCoroutine(PowerCountdownRoutine());

            powerupIndicator.gameObject.SetActive(true);
        }
    }
    IEnumerator PowerCountdownRoutine()
    {
       
        yield return new WaitForSeconds(5);
        powerUp = false;
        powerupIndicator.gameObject.SetActive(false);
    }
}
