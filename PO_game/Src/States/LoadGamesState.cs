using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
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
    private Texture2D _windowTexture;
    private SpriteFont _windowFont;
    private Window _window = null;

    public LoadGameState(ContentManager content): base(content){}
    
    public void EmptySlot_Click(object sender, EventArgs e)
    {
        StateManager.Instance.RemoveState();
        StateManager.Instance.AddState(new LoadGameState(content));
    }
    
    public void EmptySlot2_Click(int slot, object sender, EventArgs e)
    {
        File.Delete("save"+slot+".json");
        StateManager.Instance.RemoveState();
        StateManager.Instance.AddState(new LoadGameState(content));
    }
    
    public void creatWindow(int number_of_buttons, int slot)
    { 
            _windowTexture = content.Load<Texture2D>("ramka1"); 
            _windowFont = content.Load<SpriteFont>("Arial");
            _window = new Window(_windowTexture, _windowFont, content, number_of_buttons)
            {
                Position = new Vector2(GlobalSettings.ScreenWidth / 2, GlobalSettings.ScreenHeight / 2),
                Text = "Zapis jest pusty",
                Layer = 0.2f,
            };
            _window._exitButton.leftClick += EmptySlot_Click;
            if (number_of_buttons == 2)
                _window._exitButton2.leftClick += (sender, e) => EmptySlot2_Click(slot, sender, e);
    }
    
    
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
                    button.leftClick += Save1_left_Click;
                    button.rightClick += Save1_right_Click;
                    break;
                case 1:
                    if (File.Exists("save2.json"))
                    {
                        button.Text = "Load Game 2";
                    }
                    button.leftClick += Save2_left_Click;
                    button.rightClick += Save2_right_Click;
                    break;
                case 2:
                    if (File.Exists("save3.json"))
                    {
                        button.Text = "Load Game 3";
                    }
                    button.leftClick += Save3_left_Click;
                    button.rightClick += Save3_right_Click;
                    break;
                case 3:
                    if (File.Exists("save4.json"))
                    {
                        button.Text = "Load Game 4";
                    }
                    button.leftClick += Save4_left_Click;
                    button.rightClick += Save4_right_Click;
                    break;
                case 4:
                    if (File.Exists("save5.json"))
                    {
                        button.Text = "Load Game 5";
                    }
                    button.leftClick += Save5_left_Click;
                    button.rightClick += Save5_right_Click;
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

    public void Save1_left_Click(object sender, EventArgs e)
    {
        if (File.Exists("save1.json"))
        {
            StateManager.Instance.RemoveState();
            StateManager.Instance.AddState(new GameState(content, 1));
        }
        else
        {
            creatWindow(1,0);
        }
    }

    public void Save2_left_Click(object sender, EventArgs e)
    {
        if (File.Exists("save2.json"))
        {
            StateManager.Instance.RemoveState();
            StateManager.Instance.AddState(new GameState(content, 2));
        }
        else
        {
            creatWindow(1,0);
        }
    }

    public void Save3_left_Click(object sender, EventArgs e)
    {
        if (File.Exists("save3.json"))
        {
            StateManager.Instance.RemoveState();
            StateManager.Instance.AddState(new GameState(content, 3));
        }
        else
        {
            creatWindow(1, 0);
        }
    }

    public void Save4_left_Click(object sender, EventArgs e)
    {
        if (File.Exists("save4.json"))
        {
            StateManager.Instance.RemoveState();
            StateManager.Instance.AddState(new GameState(content, 4));
        }
        else
        {
            creatWindow(1, 0);
        }
    }

    public void Save5_left_Click(object sender, EventArgs e)
    {
        if (File.Exists("save5.json"))
        {
            StateManager.Instance.RemoveState();
            StateManager.Instance.AddState(new GameState(content, 5));
        }
        else
        {
            creatWindow(1, 0);
        }
    }
    
    public void Save1_right_Click(object sender, EventArgs e)
    {
        
        if (File.Exists("save1.json"))
        {
            creatWindow(2, 1);
        }
    }
    
    public void Save2_right_Click(object sender, EventArgs e)
    {
        if (File.Exists("save2.json"))
        {
            creatWindow(2 ,2);
        }
    }
    
    public void Save3_right_Click(object sender, EventArgs e)
    {
        if (File.Exists("save3.json"))
        {
            creatWindow(2, 3);
        }
    }
    
    public void Save4_right_Click(object sender, EventArgs e)
    {
        if (File.Exists("save4.json"))
        {
            creatWindow(2, 4);
        }
    }
    
    public void Save5_right_Click(object sender, EventArgs e)
    {
        if (File.Exists("save5.json"))
        {
            creatWindow(2, 5);
        }
    }
    
    public void Exit_Buttom_Click(object sender, EventArgs e)
    {
        StateManager.Instance.RemoveState();
    }

    public override void Update(GameTime gameTime)
    {
       
        _exitButton.Update();
        if (_window != null)
        {
            _window.Update();
        }
        else{ 
            foreach (var button in _buttons)
            {
                button.Update();
            }
            
        }
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Begin();
        foreach (var button in _buttons)
        {
            button.Draw(spriteBatch);
        }
        _exitButton.Draw(spriteBatch);
        if (_window != null)
        {
            _window.Draw(spriteBatch);
        }
        spriteBatch.End();
    }
}