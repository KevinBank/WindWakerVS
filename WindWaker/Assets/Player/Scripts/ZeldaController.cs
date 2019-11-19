using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeldaController : MonoBehaviour
{
    [Header("Value's")]
    [SerializeField] private float movementSpeed;
    [SerializeField] private float dashSpeed;
    [SerializeField] private int dashDistance;

    // Update is called once per frame
    void Update()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 move = input.normalized * movementSpeed;

        transform.Translate(new Vector3(move.x, 0, move.y) * Time.deltaTime);

        if(Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Dash(input));
        }
    }

    private IEnumerator Dash(Vector2 input)
    {
        for(int i = 0; i < dashDistance; i++)
        {
            Vector2 move = input.normalized * dashSpeed;
            transform.Translate(new Vector3(move.x, 0, move.y) * Time.deltaTime);
            yield return null;
        }
    }
}
