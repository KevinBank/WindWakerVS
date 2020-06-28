using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private ZeldaController controller;
    [SerializeField] private ZeldasCameraController cameraController;
    [SerializeField] private BossHealth bossHealth;

    public void LockOn(bool locked)
    {
        if (locked)
        {
            try
            {
                target = GameObject.FindWithTag(ConstClass.TARGET).transform;
                controller.State = MovementState.LOCKED_ON;
                controller.Target = target;
                cameraController.State = MovementState.LOCKED_ON;
                cameraController.Target = target;
            }
            catch
            {
                Debug.Log("no object with target tag found");
            }
        }
        else if (!locked)
        {
            target = null;
            controller.State = MovementState.NORMAL;
            cameraController.State = MovementState.NORMAL;
        }
    }

    public void Shoot()
    {
        if (target != null)
        {
            bossHealth.TakeDamage(1);
            target.gameObject.SetActive(false);
        }
    }
}
