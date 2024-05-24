using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace UlearnGameMG
{
    public class PlayerInput
    {
        public GameLogic logic;
        public Mode mode = Mode.Choise;
        public GameInterface gameInterface;

        public PlayerInput(GameLogic logic, GameInterface gameInterface)
        {
            this.logic = logic;
            this.gameInterface = gameInterface;
        }

        public void SetButtonsAction()
        {
            gameInterface.buttons["NextTurn"].SetAction(() => { if (logic.Win == 0) logic.EndTurn(); });
        }

        public void ClickHandler()
        {
            if (logic.Win != 0)
            {
                if (InputManager.JustPressed(MouseInput.LeftButton))
                {
                    if (mode == Mode.Choise)
                    {
                        if (logic.ChoiseCharacter(InputManager.mouseCell))
                        {
                            if (logic.choise.moveDo) mode = Mode.Attack;
                            else mode = Mode.Move;
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
                            logic.ClearChoise();
                            mode = Mode.Choise;
                        }
                    }
                    if (Map.OutOfBounds(InputManager.mouseCell))
                    {
                        logic.ClearChoise();
                        mode = Mode.Choise;
                    }
                    logic.CheckState();
                }
                if (InputManager.JustPressed(Keys.D1))
                    if (mode != Mode.Choise)
                        mode = (mode == Mode.Attack && !logic.choise.moveDo) ? Mode.Move : Mode.Attack;
                if (InputManager.JustPressed(Keys.Escape))
                    if (mode != Mode.Choise)
                    {
                        mode = Mode.Choise;
                        logic.ClearChoise();
                    }
            }
            gameInterface.Update();
        }

        

    }

    public enum Mode
    {
        Choise,
        Move,
        Attack
    }
}
