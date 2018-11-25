using System;
using System.Collections.Generic;
using UnityEngine.Events;

public class InputEventManager : PersistentSingleton<InputEventManager> {

    // Keep singleton-only by disabling constructor
    protected InputEventManager() { }

    private Dictionary<InputEvent, TrackedUnityEvent> eventDictionary;

    private void Awake()
    {
        eventDictionary = new Dictionary<InputEvent, TrackedUnityEvent>();
    }

    private void Update()
    {
        PollPlayerInputForEvents();
    }

    public void StartListening(InputEvent inputEvent, UnityAction listener)
    {
        TrackedUnityEvent thisEvent = null;
        if (eventDictionary.TryGetValue(inputEvent, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new TrackedUnityEvent();
            thisEvent.AddListener(listener);
            eventDictionary.Add(inputEvent, thisEvent);
        }
    }

    public void StopListening(InputEvent inputEvent, UnityAction listener)
    {
        TrackedUnityEvent thisEvent = null;
        if (eventDictionary.TryGetValue(inputEvent, out thisEvent))
        {
            thisEvent.RemoveListener(listener);

            if (!thisEvent.HasListeners())
            {
                eventDictionary.Remove(inputEvent);
            }
        }
    }

    public void TriggerEvent(InputEvent inputEvent)
    {
        TrackedUnityEvent thisEvent = null;
        if (eventDictionary.TryGetValue(inputEvent, out thisEvent))
        {
            thisEvent.Invoke();
        }
    }

    public bool ListeningOnEvent(InputEvent inputEvent)
    {
        TrackedUnityEvent thisEvent = null;
        return eventDictionary.TryGetValue(inputEvent, out thisEvent);
    }

    // ====================
    // Input Event Checks
    // ====================

    private void PollPlayerInputForEvents()
    {
        if (ListeningOnEvent(InputEvent.PlayerPressedUp) && PlayerPressedUp())
        {
            TriggerEvent(InputEvent.PlayerPressedUp);
        }

        if (ListeningOnEvent(InputEvent.PlayerPressedLeft) && PlayerPressedLeft())
        {
            TriggerEvent(InputEvent.PlayerPressedLeft);
        }

        if (ListeningOnEvent(InputEvent.PlayerPressedDown) && PlayerPressedDown())
        {
            TriggerEvent(InputEvent.PlayerPressedDown);
        }

        if (ListeningOnEvent(InputEvent.PlayerPressedRight) && PlayerPressedRight())
        {
            TriggerEvent(InputEvent.PlayerPressedRight);
        }

        if (ListeningOnEvent(InputEvent.PlayerPressedSubmit) && PlayerPressedSubmit())
        {
            TriggerEvent(InputEvent.PlayerPressedSubmit);
        }

        if (ListeningOnEvent(InputEvent.PlayerPressedCancel) && PlayerPressedCancel())
        {
            TriggerEvent(InputEvent.PlayerPressedCancel);
        }

        if (ListeningOnEvent(InputEvent.PlayerPressedExit) && PlayerPressedExit())
        {
            TriggerEvent(InputEvent.PlayerPressedExit);
        }

        if (ListeningOnEvent(InputEvent.PlayerControlsAssigned) && PlayerControlsAssigned())
        {
            TriggerEvent(InputEvent.PlayerControlsAssigned);
        }
    }

    private bool PlayerPressedUp()
    {
        foreach (PlayerNumber playerNumber in Enum.GetValues(typeof(PlayerNumber)))
        {
            IPlayerControls playerControls = InputManager.Instance.PlayerControls(playerNumber);
            if (playerControls != null && playerControls.GetUpKeyDown())
            {
                return true;
            }
        }

        return false;
    }

    private bool PlayerPressedLeft()
    {
        foreach (PlayerNumber playerNumber in Enum.GetValues(typeof(PlayerNumber)))
        {
            IPlayerControls playerControls = InputManager.Instance.PlayerControls(playerNumber);
            if (playerControls != null && playerControls.GetLeftKeyDown())
            {
                return true;
            }
        }

        return false;
    }

    private bool PlayerPressedDown()
    {
        foreach (PlayerNumber playerNumber in Enum.GetValues(typeof(PlayerNumber)))
        {
            IPlayerControls playerControls = InputManager.Instance.PlayerControls(playerNumber);
            if (playerControls != null && playerControls.GetDownKeyDown())
            {
                return true;
            }
        }

        return false;
    }

    private bool PlayerPressedRight()
    {
        foreach (PlayerNumber playerNumber in Enum.GetValues(typeof(PlayerNumber)))
        {
            IPlayerControls playerControls = InputManager.Instance.PlayerControls(playerNumber);
            if (playerControls != null && playerControls.GetRightKeyDown())
            {
                return true;
            }
        }

        return false;
    }

    private bool PlayerPressedSubmit()
    {
        GlobalControls globalControls = InputManager.Instance.GlobalControls();
        if (globalControls.GetSubmitKeyDown())
        {
            return true;
        }

        foreach (PlayerNumber playerNumber in Enum.GetValues(typeof(PlayerNumber)))
        {
            IPlayerControls playerControls = InputManager.Instance.PlayerControlsAssignments[playerNumber];
            if (playerControls != null && playerControls.GetSubmitDown())
            {
                return true;
            }
        }

        return false;
    }

    private bool PlayerPressedCancel()
    {
        foreach (PlayerNumber playerNumber in Enum.GetValues(typeof(PlayerNumber)))
        {
            IPlayerControls playerControls = InputManager.Instance.PlayerControlsAssignments[playerNumber];
            if (playerControls != null && playerControls.GetCancelDown())
            {
                return true;
            }
        }

        return false;
    }

    private bool PlayerPressedExit()
    {
        GlobalControls globalControls = InputManager.Instance.GlobalControls();
        if (globalControls.GetExitKeyDown())
        {
            return true;
        }

        foreach (PlayerNumber playerNumber in Enum.GetValues(typeof(PlayerNumber)))
        {
            IPlayerControls playerControls = InputManager.Instance.PlayerControlsAssignments[playerNumber];
            if (playerControls != null && playerControls.GetExitDown())
            {
                return true;
            }
        }

        return false;
    }

    private bool PlayerControlsAssigned()
    {
        foreach (IPlayerControls playerControls in InputManager.Instance.AvailablePlayerControls)
        {
            if (playerControls.GetJoinGameDown())
            {
                bool assignSuccessful = InputManager.Instance.AssignControlsToNextAvailablePlayer(playerControls);
                if (assignSuccessful)
                {
                    return true;
                }
            }
        }

        return false;
    }

}
