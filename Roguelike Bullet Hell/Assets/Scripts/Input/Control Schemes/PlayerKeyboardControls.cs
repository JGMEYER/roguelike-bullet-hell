using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKeyboardControls : IPlayerControls
{

    public KeyboardConfigNumber KeyboardConfigNumber { get; private set; }

    // Only UI should pull these values. Players should call included methods
    // in order to keep all player input logic controller agnostic.
    public KeyCode UpKey { get; private set; }
    public KeyCode LeftKey { get; private set; }
    public KeyCode DownKey { get; private set; }
    public KeyCode RightKey { get; private set; }
    public KeyCode ActionKey { get; private set; }

    public PlayerKeyboardControls(KeyboardConfigNumber keyboardConfigNumber, KeyCode upKey, KeyCode leftKey, KeyCode downKey, KeyCode rightKey, KeyCode actionKey)
    {
        KeyboardConfigNumber = keyboardConfigNumber;

        UpKey = upKey;
        LeftKey = leftKey;
        DownKey = downKey;
        RightKey = rightKey;
        ActionKey = actionKey;
    }

    float IPlayerControls.GetMovementHorizontal()
    {
        float horizontal = 0f;

        if (Input.GetKey(LeftKey))
        {
            horizontal += -1;
        }
        if (Input.GetKey(RightKey))
        {
            horizontal += 1;
        }

        return horizontal;
    }

    float IPlayerControls.GetMovementVertical()
    {
        float vertical = 0f;

        if (Input.GetKey(UpKey))
        {
            vertical += 1;
        }
        if (Input.GetKey(DownKey))
        {
            vertical += -1;
        }

        return vertical;
    }

    bool IPlayerControls.GetSubmitDown()
    {
        return Input.GetKeyDown(ActionKey);
    }

    bool IPlayerControls.GetCancelDown()
    {
        return Input.GetKeyDown(KeyCode.Escape);
    }

    bool IPlayerControls.GetExitDown()
    {
        return Input.GetKeyDown(KeyCode.Escape);
    }

    bool IPlayerControls.GetJoinGameDown()
    {
        return Input.GetKeyDown(ActionKey);
    }

    bool IPlayerControls.GetJump()
    {
        return Input.GetKey(UpKey);
    }

    bool IPlayerControls.GetUpKey()
    {
        return Input.GetKey(UpKey);
    }

    bool IPlayerControls.GetUpKeyDown()
    {
        return Input.GetKeyDown(UpKey);
    }

    bool IPlayerControls.GetLeftKey()
    {
        return Input.GetKey(LeftKey);
    }

    bool IPlayerControls.GetLeftKeyDown()
    {
        return Input.GetKeyDown(LeftKey);
    }

    bool IPlayerControls.GetDownKey()
    {
        return Input.GetKey(DownKey);
    }

    bool IPlayerControls.GetDownKeyDown()
    {
        return Input.GetKeyDown(DownKey);
    }

    bool IPlayerControls.GetRightKey()
    {
        return Input.GetKey(RightKey);
    }

    bool IPlayerControls.GetRightKeyDown()
    {
        return Input.GetKeyDown(RightKey);
    }

    bool IPlayerControls.GetActionKey()
    {
        return Input.GetKey(ActionKey); 
    }

    bool IPlayerControls.GetActionKeyDown()
    {
        return Input.GetKeyDown(ActionKey); 
    }

    string IPlayerControls.GetJoinGameKeyName()
    {
        return ActionKey.ToString();
    }
}
