using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace UlearnGameMG
{
    public class PlayerInput
    {
        public GameLogic logic;
        public Mode mode = Mode.Choise;

        public PlayerInput(GameLogic logic)
        {
            this.logic = logic;
        }

        public void ClickHandler()
        {
            if (InputManager.JustPressed(MouseInput.LeftButton))
            {
                if (mode == Mode.Choise) 
                { 
                    if (logic.ChoiseCharacter(InputManager.mouseCell))
                    {
                        mode = Mode.Move;
                    }
                }
                else if (mode == Mode.Move) 
                {
                    if (logic.CharacterMove(InputManager.mouseCell))
                    {
                        mode = Mode.Attack;
                    }
                }
                else if (mode == Mode.Attack)
                {
                    if (logic.CharacterSpell(InputManager.mouseCell))
                    {
                        mode = Mode.Choise;
                    }
                }
                if (Map.OutOfBounds(InputManager.mouseCell))
                {
                    logic.ClearChoise();
                    mode = Mode.Choise;
                }
            }
            if (InputManager.JustPressed(Keys.D1))
                if (mode != Mode.Choise)
                    mode = mode == Mode.Attack ? Mode.Move : Mode.Attack;
            if (InputManager.JustPressed(Keys.Escape))
                if (mode != Mode.Choise)
                {
                    mode = Mode.Choise;
                    logic.ClearChoise();
                }
        }

    }

    public enum Mode
    {
        Choise,
        Move,
        Attack
    }
}
