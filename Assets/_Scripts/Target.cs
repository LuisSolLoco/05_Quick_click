using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Target : MonoBehaviour
{

    private Rigidbody _rigidbody;
    private float minForce = 12,
        maxForce = 16,
        maxTorque = 10,
        xRange = 4,
        ySpawnPos = -6;

    public ParticleSystem explosionParticle;
    public int pointValue;

    private GameManager gameManager;
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.AddForce(RandomForce(),ForceMode.Impulse);
        _rigidbody.AddTorque(RandomTorque(),RandomTorque(),RandomTorque(),ForceMode.Impulse);
        transform.position = RandomSpawnPos();
        // gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        gameManager = FindObjectOfType<GameManager>();

    }
/// <summary>
/// Genera una posicion aleatoria
/// </summary>
/// <returns>regresa un vector 3</returns>
    private Vector3 RandomSpawnPos()
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos);
    }
/// <summary>
/// Genera una fuerza aleatoria
/// </summary>
/// <returns>Regresa un vector3 con la fuerza a aplicar</returns>
    private Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minForce, maxForce);
    }
/// <summary>
/// Genera un torque aleatorio
/// </summary>
/// <returns>Regresa un float</returns>
    private float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }

private void OnMouseOver()
{
        
    Destroy(gameObject);
    Instantiate(explosionParticle, transform.position,explosionParticle.transform.rotation);
    gameManager.SendMessage("UpdateScore",pointValue);
    if (gameObject.CompareTag("badGuys"))
    {
        gameManager.SendMessage("SetGameOver");
    }
}

private void OnTriggerEnter(Collider other)
{
    if (other.CompareTag("killZone"))
    {
        Destroy(gameObject);
        if (pointValue>0)
        {
            
            gameManager.SendMessage("UpdateScore",-10);
        }
        else
        {
            gameManager.SendMessage("UpdateScore",50);
        }
    }
}
    // Update is called once per frame
    void Update()
    {
        
    }
}
