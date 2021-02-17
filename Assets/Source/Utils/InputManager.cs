using System;
using System.Collections.Generic;
using UnityEngine;

public static class InputManager
{

    public enum InputMode
    {
        ARCADE_CABINET,
        KEYBOARD,
        JOYSTICK,
        COUNT
    }

    public enum Button
    {
        MoveLeft,
        MoveRight,
        MoveUp,
        MoveDown,
        Action1,
        Action2,
        Action3,
        Action4,
        Action5,
        Action6,
        UNASSIGNED
    }

    private static Dictionary<PlayerID, Dictionary<Button, KeyCode>> keyboardMapping = new Dictionary<PlayerID, Dictionary<Button, KeyCode>>()
    {
        {
            PlayerID.ONE, new Dictionary<Button, KeyCode> {
                { Button.MoveLeft, KeyCode.A },
                { Button.MoveRight, KeyCode.D },
                { Button.MoveUp, KeyCode.W },
                { Button.MoveDown, KeyCode.S },
                { Button.Action1, KeyCode.U },
                { Button.Action2, KeyCode.I },
                { Button.Action3, KeyCode.O },
                { Button.Action4, KeyCode.J },
                { Button.Action5, KeyCode.K },
                { Button.Action6, KeyCode.L }
            }
        }
    };

    private static Dictionary<PlayerID, Dictionary<Button, KeyCode>> joystickMapping = new Dictionary<PlayerID, Dictionary<Button, KeyCode>>()
    {
        {
            PlayerID.ONE, new Dictionary<Button, KeyCode> {
                { Button.MoveLeft, KeyCode.None },
                { Button.MoveRight, KeyCode.None },
                { Button.MoveUp, KeyCode.None },
                { Button.MoveDown, KeyCode.None },
                { Button.Action1, KeyCode.Joystick1Button4 },
                { Button.Action2, KeyCode.Joystick1Button3 },
                { Button.Action3, KeyCode.Joystick1Button5 },
                { Button.Action4, KeyCode.Joystick1Button2 },
                { Button.Action5, KeyCode.Joystick1Button0 },
                { Button.Action6, KeyCode.Joystick1Button1 }
            }
        }
    };

    private static Dictionary<PlayerID, Dictionary<Button, KeyCode>> arcadeMapping = new Dictionary<PlayerID, Dictionary<Button, KeyCode>>()
    {
        {
            PlayerID.ONE, new Dictionary<Button, KeyCode> {
                { Button.MoveLeft, KeyCode.Alpha7 },
                { Button.MoveRight, KeyCode.Alpha8 },
                { Button.MoveUp, KeyCode.Alpha9 },
                { Button.MoveDown, KeyCode.Alpha0 },
                { Button.Action1, KeyCode.Alpha1 },
                { Button.Action2, KeyCode.Alpha2 },
                { Button.Action3, KeyCode.Alpha3 },
                { Button.Action4, KeyCode.Alpha4 },
                { Button.Action5, KeyCode.Alpha5 },
                { Button.Action6, KeyCode.Alpha6 }
            }
        },
        {
            PlayerID.TWO, new Dictionary<Button, KeyCode> {
                { Button.MoveLeft, KeyCode.U },
                { Button.MoveRight, KeyCode.I },
                { Button.MoveUp, KeyCode.O },
                { Button.MoveDown, KeyCode.P },
                { Button.Action1, KeyCode.Q },
                { Button.Action2, KeyCode.W },
                { Button.Action3, KeyCode.E },
                { Button.Action4, KeyCode.R },
                { Button.Action5, KeyCode.T },
                { Button.Action6, KeyCode.Y }
            }
        },
        {
            PlayerID.THREE, new Dictionary<Button, KeyCode> {
                { Button.MoveLeft, KeyCode.J },
                { Button.MoveRight, KeyCode.K },
                { Button.MoveUp, KeyCode.L },
                { Button.MoveDown, KeyCode.Semicolon },
                { Button.Action1, KeyCode.A },
                { Button.Action2, KeyCode.S },
                { Button.Action3, KeyCode.D },
                { Button.Action4, KeyCode.F },
                { Button.Action5, KeyCode.G },
                { Button.Action6, KeyCode.H }
            }
        },
        {
            PlayerID.FOUR, new Dictionary<Button, KeyCode> {
                { Button.MoveLeft, KeyCode.M },
                { Button.MoveRight, KeyCode.Comma },
                { Button.MoveUp, KeyCode.Period },
                { Button.MoveDown, KeyCode.Slash },
                { Button.Action1, KeyCode.Z },
                { Button.Action2, KeyCode.X },
                { Button.Action3, KeyCode.C },
                { Button.Action4, KeyCode.V },
                { Button.Action5, KeyCode.B },
                { Button.Action6, KeyCode.N }
            }
        }
    };

    private static InputMode sInputMode = InputMode.KEYBOARD;
    public static InputMode inputMode { get { return sInputMode; } }
    public static void SetInputMode(InputMode mode)
    {
        InputManager.sInputMode = mode;
    }

    private static Dictionary<Button, KeyCode> GetMapping(PlayerID player)
    {
        Dictionary<Button, KeyCode> mapping = null;

        try
        {
            if (inputMode == InputMode.KEYBOARD)
            {
                mapping = keyboardMapping[player];
            } else if (inputMode == InputMode.ARCADE_CABINET)
            {
                mapping = arcadeMapping[player];
            } else if (inputMode == InputMode.JOYSTICK)
            {
                mapping = joystickMapping[player];
            }
        } catch (Exception)
        {
            return null;
        }
        return mapping;
    }

    public static bool GetButton(Button button, PlayerID player)
    {
        if (button == Button.UNASSIGNED)
        {
            return false;
        }
        Dictionary<Button, KeyCode> mapping = GetMapping(player);
        if (mapping != null)
        {
            KeyCode key = mapping[button];
            if (inputMode == InputMode.JOYSTICK)
            {
                float deadZone = 0.25f;
                string axis = "Joystick_" + ((int)player+1);
                float horizontal = Input.GetAxis(axis + "_LeftX");
                float vertical = Input.GetAxis(axis + "_LeftY");
                switch (button)
                {
                    case Button.MoveLeft:
                        return horizontal < -deadZone;
                    case Button.MoveRight:
                        return horizontal > deadZone;
                    case Button.MoveUp:
                        return vertical < -deadZone;
                    case Button.MoveDown:
                        return vertical > deadZone;
                }
            }
            return Input.GetKey(key);
        }
        return false;
    }

    public static bool GetButtonDown(Button button, PlayerID player)
    {
        if (button == Button.UNASSIGNED)
        {
            return false;
        }
        Dictionary<Button, KeyCode> mapping = GetMapping(player);
        if (mapping != null)
        {
            KeyCode key = mapping[button];
            return Input.GetKeyDown(key);
        }
        return false;
    }

    public static bool GetButtonUp(Button button, PlayerID player)
    {
        if (button == Button.UNASSIGNED)
        {
            return false;
        }
        Dictionary<Button, KeyCode> mapping = GetMapping(player);
        if (mapping != null)
        {
            KeyCode key = mapping[button];
            return Input.GetKeyUp(key);
        }
        return false;
    }
}