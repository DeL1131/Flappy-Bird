using System;
using UnityEngine;

public class BirdInput : MonoBehaviour
{
    private const KeyCode CommandJump = KeyCode.Space;
    private const KeyCode CommandAttack = KeyCode.Mouse0;

    public event Action SpacePressed;
    public event Action Mouse0Pressed;

    private void Update()
    {
        if (Input.GetKeyDown(CommandJump))
        {
            SpacePressed?.Invoke();
        }

        if (Input.GetKeyDown(CommandAttack))
        {
            Mouse0Pressed?.Invoke();
        }
    }
}