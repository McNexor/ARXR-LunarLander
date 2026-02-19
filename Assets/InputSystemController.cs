using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PhysicsSystem))]
public class InputSystemController : MonoBehaviour
{

    public Sprite _landerBasic;
    public Sprite _landerThrust;

    public void HandleThrust(InputAction.CallbackContext context)
    {
        if (context.ReadValue<float>() == 1f)
        {
            GetComponent<PhysicsSystem>().Thrust();
            GetComponent<SpriteRenderer>().sprite = _landerThrust;
        }
        else
        {
            GetComponent<PhysicsSystem>().StopThrust();
            GetComponent<SpriteRenderer>().sprite = _landerBasic;
        }
    }

    public void HandleTilt(InputAction.CallbackContext context)
    {
        GetComponent<PhysicsSystem>().Tilt(context.ReadValue<float>());
    }
}
