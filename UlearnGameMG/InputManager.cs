using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace UlearnGameMG
{
    static class InputManager
    {
        static public KeyboardState currentKeyboardState = Keyboard.GetState();
        static public KeyboardState previousKeyboardState = Keyboard.GetState();
        static public MouseState currentMouseState = Mouse.GetState();
        static public MouseState previousMouseState = Mouse.GetState();
        static public Point mousePos = currentMouseState.Position;
        static public Point mouseCell = Draw.WindToCell(mousePos);

        //public InputManager(MouseState mouse, KeyboardState keyboard)
        //{
        //    currentKeyboardState = keyboard;
        //    previousKeyboardState = keyboard;
        //    currentMouseState = mouse;
        //    previousMouseState = mouse;
        //}

        static public void Update()
        {
            previousMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();
            previousKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();
            mousePos = currentMouseState.Position;
            mouseCell = Draw.WindToCell(mousePos);
        }

        static public bool IsPressed(Keys key)
        {
            return currentKeyboardState.IsKeyDown(key);
        }
        static public bool IsPressed(MouseInput input)
        {
            return IsPressed(currentMouseState, input);
        }
        static private bool IsPressed(MouseState state, MouseInput input)
        {
            switch (input)
            {
                case MouseInput.LeftButton:
                    return state.LeftButton == ButtonState.Pressed;
                case MouseInput.MiddleButton:
                    return state.MiddleButton == ButtonState.Pressed;
                case MouseInput.RightButton:
                    return state.RightButton == ButtonState.Pressed;
                case MouseInput.Button1:
                    return state.XButton1 == ButtonState.Pressed;
                case MouseInput.Button2:
                    return state.XButton2 == ButtonState.Pressed;
            }
            return false;
        }

        static public bool IsHeld(Keys key)
        {
            return currentKeyboardState.IsKeyDown(key) && previousKeyboardState.IsKeyDown(key);
        }
        static public bool IsHeld(MouseInput input)
        {
            return IsPressed(currentMouseState, input) && IsPressed(previousMouseState, input);
        }

        static public bool JustPressed(Keys key)
        {
            return currentKeyboardState.IsKeyDown(key) && !previousKeyboardState.IsKeyDown(key);
        }
        static public bool JustPressed(MouseInput input)
        {
            return IsPressed(currentMouseState, input) && !IsPressed(previousMouseState, input);
        }

        static public bool JustReleased(Keys key)
        {
            return !currentKeyboardState.IsKeyDown(key) && previousKeyboardState.IsKeyDown(key);
        }
        static public bool JustReleased(MouseInput input)
        {
            return !IsPressed(currentMouseState, input) && IsPressed(previousMouseState, input);
        }
    }

    public enum MouseInput
    {
        None,
        LeftButton,
        MiddleButton,
        RightButton,
        Button1,
        Button2
    }
}
