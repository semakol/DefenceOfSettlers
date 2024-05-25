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
        public GameInterface gameInterface = GameInterface.InGame;
        public GameInterface EndGameInterface = GameInterface.EndGame;

        public PlayerInput(GameLogic logic)
        {
            this.logic = logic;
        }

        public void SetButtonsAction(Action restart, Action menu, Action next)
        {
            gameInterface.buttons["NextTurn"].SetAction(() => { if (logic.Win == 0) logic.EndTurn(); });
            EndGameInterface.buttons["Restart"].SetAction(restart);
            EndGameInterface.buttons["MainMenu"].SetAction(menu);
            EndGameInterface.buttons["NextLevel"].SetAction(next);
        }

        public List<ITexturable> GetTexturables() 
        {  
            var result = new List<ITexturable>();
            result.AddRange(gameInterface.GetTexturables());
            result.AddRange(EndGameInterface.GetTexturables());
            return result;
        }

        public void ClickHandler(Action menu)
        {
            if (logic.Win == 0)
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
                if (InputManager.JustPressed(Keys.W))
                    if (mode != Mode.Choise)
                        mode = (mode == Mode.Attack && !logic.choise.moveDo) ? Mode.Move : Mode.Attack;
                if (InputManager.JustPressed(Keys.Q))
                    if (mode != Mode.Choise)
                    {
                        mode = Mode.Choise;
                        logic.ClearChoise();
                    }
                if (InputManager.JustPressed(Keys.Escape)) 
                {
                    menu.Invoke();
                }
                gameInterface.Update();
            }
            else EndGameInterface.Update();
        }
        

    }

    public enum Mode
    {
        Choise,
        Move,
        Attack
    }
}
