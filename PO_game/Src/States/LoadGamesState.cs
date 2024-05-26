using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PO_game.Src.Controls;
using PO_game.Src.States;
using PO_game.Src.Utils;

public class LoadGameState: State
{
    private Texture2D _buttonTexture;
    private SpriteFont _buttonFont;
    private List<Button> _buttons;
    private Button _exitButton;
    private int buttonSpacing = 20;

    public LoadGameState(ContentManager content): base(content){}

    public override void LoadContent()
    {
        _buttonTexture = content.Load<Texture2D>("startButton");
        _buttonFont = content.Load<SpriteFont>("Arial");

        _buttons = new List<Button>();

        for (int i = 0; i < 5; i++)
        {
            var button = new Button(_buttonTexture, _buttonFont)
            {
                Position = new Vector2(GlobalSettings.ScreenWidth / 2 , _buttonTexture.Height + i * (_buttonTexture.Height + buttonSpacing)),
                Text = "Empty Slot",
                Layer = 0.3f
            };

            switch (i)
            {
                case 0:
                    if (File.Exists("save1.json"))
                    {
                        button.Text = "Load Game 1";
                    }
                    button.leftClick += Safe1_left_Click;
                    button.rightClick += Safe1_right_Click;
                    break;
                case 1:
                    if (File.Exists("save2.json"))
                    {
                        button.Text = "Load Game 2";
                    }
                    button.leftClick += Safe2_left_Click;
                    button.rightClick += Safe2_right_Click;
                    break;
                case 2:
                    if (File.Exists("save3.json"))
                    {
                        button.Text = "Load Game 3";
                    }
                    button.leftClick += Safe3_left_Click;
                    button.rightClick += Safe3_right_Click;
                    break;
                case 3:
                    if (File.Exists("save4.json"))
                    {
                        button.Text = "Load Game 4";
                    }
                    button.leftClick += Safe4_left_Click;
                    button.rightClick += Safe4_right_Click;
                    break;
                case 4:
                    if (File.Exists("save5.json"))
                    {
                        button.Text = "Load Game 5";
                    }
                    button.leftClick += Safe5_left_Click;
                    button.rightClick += Safe5_right_Click;
                    break;
            }

            _buttons.Add(button);
        }
        
        _exitButton = new Button(_buttonTexture, _buttonFont)
        {
            Position = new Vector2(_buttonTexture.Width/2, _buttonTexture.Height/2),
            Text = "Return to Menu",
            leftClick = new EventHandler(Exit_Buttom_Click),
            Layer = 0.3f
        };
    }

    public void Safe1_left_Click(object sender, EventArgs e)
    {
        if (File.Exists("save1.json"))
        {
            StateManager.Instance.RemoveState();
            StateManager.Instance.AddState(new GameState(content, 1));
        }
        else
        {
            //trzeba zaimplementować wyświetlanie komunikatu o braku zapisu
        }
    }

    public void Safe2_left_Click(object sender, EventArgs e)
    {
        if (File.Exists("save2.json"))
        {
            StateManager.Instance.RemoveState();
            StateManager.Instance.AddState(new GameState(content, 2));
        }
        else
        {
            //trzeba zaimplementować wyświetlanie komunikatu o braku zapisu
        }
    }

    public void Safe3_left_Click(object sender, EventArgs e)
    {
        if (File.Exists("save3.json"))
        {
            StateManager.Instance.RemoveState();
            StateManager.Instance.AddState(new GameState(content, 3));
        }
        else
        {
            //trzeba zaimplementować wyświetlanie komunikatu o braku zapisu
        }
    }

    public void Safe4_left_Click(object sender, EventArgs e)
    {
        if (File.Exists("save4.json"))
        {
            StateManager.Instance.RemoveState();
            StateManager.Instance.AddState(new GameState(content, 4));
        }
        else
        {
            //trzeba zaimplementować wyświetlanie komunikatu o braku zapisu
        }
    }

    public void Safe5_left_Click(object sender, EventArgs e)
    {
        if (File.Exists("save5.json"))
        {
            StateManager.Instance.RemoveState();
            StateManager.Instance.AddState(new GameState(content, 5));
        }
        else
        {
            //trzeba zaimplementować wyświetlanie komunikatu o braku zapisu
        }
    }
    
    public void Safe1_right_Click(object sender, EventArgs e)
    {
        if (File.Exists("save1.json"))
        {
            File.Delete("save1.json");
            StateManager.Instance.RemoveState();
            StateManager.Instance.AddState(new LoadGameState(content));
        }
        else
        {
            //trzeba zaimplementować wyświetlanie komunikatu o braku zapisu
        }
    }
    
    public void Safe2_right_Click(object sender, EventArgs e)
    {
        if (File.Exists("save2.json"))
        {
            File.Delete("save2.json");
            StateManager.Instance.RemoveState();
            StateManager.Instance.AddState(new LoadGameState(content));
        }
        else
        {
            //trzeba zaimplementować wyświetlanie komunikatu o braku zapisu
        }
    }
    
    public void Safe3_right_Click(object sender, EventArgs e)
    {
        if (File.Exists("save3.json"))
        {
            File.Delete("save3.json");
            StateManager.Instance.RemoveState();
            StateManager.Instance.AddState(new LoadGameState(content));
        }
        else
        {
            //trzeba zaimplementować wyświetlanie komunikatu o braku zapisu
        }
    }
    
    public void Safe4_right_Click(object sender, EventArgs e)
    {
        if (File.Exists("save4.json"))
        {
            File.Delete("save4.json");
            StateManager.Instance.RemoveState();
            StateManager.Instance.AddState(new LoadGameState(content));
        }
        else
        {
            //trzeba zaimplementować wyświetlanie komunikatu o braku zapisu
        }
    }
    
    public void Safe5_right_Click(object sender, EventArgs e)
    {
        if (File.Exists("save5.json"))
        {
            File.Delete("save5.json");
            StateManager.Instance.RemoveState();
            StateManager.Instance.AddState(new LoadGameState(content));
        }
        else
        {
            //trzeba zaimplementować wyświetlanie komunikatu o braku zapisu
        }
    }
    
    public void Exit_Buttom_Click(object sender, EventArgs e)
    {
        StateManager.Instance.RemoveState();
    }

    public override void Update(GameTime gameTime)
    {
        foreach (var button in _buttons)
        {
            button.Update();
        }
        _exitButton.Update();
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Begin();

        foreach (var button in _buttons)
        {
            button.Draw(spriteBatch);
        }
        _exitButton.Draw(spriteBatch);
        spriteBatch.End();
    }
}