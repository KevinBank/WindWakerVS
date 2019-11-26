using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBullet : MonoBehaviour
{
    [SerializeField] private int speed;
    [SerializeField] private GameObject particle;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 10f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == ConstClass.PLAYER)
        {
            collision.gameObject.SendMessage("TakeDamage", 1);
            Instantiate(particle);
            Destroy(gameObject);
        }
        if (collision.transform.tag == ConstClass.ENVIROMENT)
        {
            Destroy(gameObject);
        }
    }
}
