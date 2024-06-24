using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PO_game.Src.Controls;
using PO_game.Src.Utils;
using System;
using System.Collections.Generic;
using System.IO;

namespace PO_game.Src.Screens
{
    /// <summary>
    ///<c>LoadingGameScreen</c> is a class handling the loading of the game.
    /// <para>It allows the player to load the game from one of the five save slots.</para>
    /// </summary>
    
    
public class LoadGameScreen : Screen
{
    private Texture2D _emptyButtonTexture;
    private Texture2D _loadButtonTexture;
    private Texture2D _returnButtonTexture;
    private Texture2D _backgroundTexture;
    private List<Button> _buttons;
    private Button _exitButton;
    private int _buttonSpacing = 65;
    private Texture2D _windowTexture;
    private Window _window;
    /// <summary>
    /// Constructor of the <c>LoadGameScreen</c> class.
    /// </summary>
    /// <param name="content"></param>
    public LoadGameScreen(ContentManager content) : base(content) { }
    /// <summary>
    /// Close the window.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void OkCancelClick(object sender, EventArgs e)
    {
        ScreenManager.Instance.RemoveScreen();
        ScreenManager.Instance.AddScreen(new LoadGameScreen(content));
    }
    /// <summary>
    /// Delete the save.
    /// </summary>
    /// <param name="slot"></param>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void DeleteClick(int slot, object sender, EventArgs e)
    {
        File.Delete("save" + slot + ".json");
        ScreenManager.Instance.RemoveScreen();
        ScreenManager.Instance.AddScreen(new LoadGameScreen(content));
    }
    
    /// <summary>
    /// A method that creates a window with a message and buttons.
    /// </summary>
    /// <param name="numberOfButtons">Number of buttons in the window</param>
    /// <param name="slot"></param>
    
    
    public void CreateWindow(int numberOfButtons, int slot)
    {
        _windowTexture = content.Load<Texture2D>("Others/ramka1");
        _window = new Window(_windowTexture, content, numberOfButtons)
        {
            Position = new Vector2(Globals.ScreenWidth / 2, Globals.ScreenHeight / 2),
            Text = "Save is empty",
            
        };
        _window.ExitButton.leftClick += OkCancelClick;
        _window.ExitButton.Scale = 2f;

        if (numberOfButtons == 2)
        {
            _window.ExitButton2.leftClick += (sender, e) => DeleteClick(slot, sender, e);
            _window.Text = "Do you want to delete this save?";
            _window.ExitButton2.Scale = 2f;
        }
    }

    public override void LoadContent()
    {
        _backgroundTexture = content.Load<Texture2D>("Others/backgroundTexture");
        _emptyButtonTexture = content.Load<Texture2D>("Others/emptyButton");
        _loadButtonTexture = content.Load<Texture2D>("Others/loadButton");
        _returnButtonTexture = content.Load<Texture2D>("Others/returnButton");

        _buttons = new List<Button>();

        for (int i = 0; i < 5; i++)
        {
            var buttonTexture = File.Exists("save" + (i + 1) + ".json") ? _loadButtonTexture : _emptyButtonTexture;

            var button = new Button(buttonTexture)
            {
                Position = new Vector2(Globals.ScreenWidth / 2, buttonTexture.Height + i * (buttonTexture.Height + _buttonSpacing) + 50),
                Scale = 4f,
                Layer = 0.3f
            };

            switch (i)
            {
                case 0:
                    button.leftClick += Save1LeftClick;
                    button.rightClick += Save1RightClick;
                    break;
                case 1:
                    button.leftClick += Save2LeftClick;
                    button.rightClick += Save2RightClick;
                    break;
                case 2:
                    button.leftClick += Save3LeftClick;
                    button.rightClick += Save3RightClick;
                    break;
                case 3:
                    button.leftClick += Save4LeftClick;
                    button.rightClick += Delete4RightClick;
                    break;
                case 4:
                    button.leftClick += Save5LeftClick;
                    button.rightClick += Delete5RightClick;
                    break;
            }

            _buttons.Add(button);
        }

        _exitButton = new Button(_returnButtonTexture)
        {
            Position = new Vector2(_returnButtonTexture.Width * 2 , _returnButtonTexture.Height * 2),
            Scale = 3f,
            leftClick = new EventHandler(ExitButtonClick),
            Layer = 0.3f
        };
    }

    public void Save1LeftClick(object sender, EventArgs e)
    {
        if (File.Exists("save1.json"))
        {
            ScreenManager.Instance.RemoveScreen();
            ScreenManager.Instance.AddScreen(new GameScreen(content, 1));
        }
        else
        {
            CreateWindow(1, 0);
        }
    }

    public void Save2LeftClick(object sender, EventArgs e)
    {
        if (File.Exists("save2.json"))
        {
            ScreenManager.Instance.RemoveScreen();
            ScreenManager.Instance.AddScreen(new GameScreen(content, 2));
        }
        else
        {
            CreateWindow(1, 0);
        }
    }

    public void Save3LeftClick(object sender, EventArgs e)
    {
        if (File.Exists("save3.json"))
        {
            ScreenManager.Instance.RemoveScreen();
            ScreenManager.Instance.AddScreen(new GameScreen(content, 3));
        }
        else
        {
            CreateWindow(1, 0);
        }
    }

    public void Save4LeftClick(object sender, EventArgs e)
    {
        if (File.Exists("save4.json"))
        {
            ScreenManager.Instance.RemoveScreen();
            ScreenManager.Instance.AddScreen(new GameScreen(content, 4));
        }
        else
        {
            CreateWindow(1, 0);
        }
    }

    public void Save5LeftClick(object sender, EventArgs e)
    {
        if (File.Exists("save5.json"))
        {
            ScreenManager.Instance.RemoveScreen();
            ScreenManager.Instance.AddScreen(new GameScreen(content, 5));
        }
        else
        {
            CreateWindow(1, 0);
        }
    }

    public void Save1RightClick(object sender, EventArgs e)
    {

        if (File.Exists("save1.json"))
        {
            CreateWindow(2, 1);
        }
    }

    public void Save2RightClick(object sender, EventArgs e)
    {
        if (File.Exists("save2.json"))
        {
            CreateWindow(2, 2);
        }
    }

    public void Save3RightClick(object sender, EventArgs e)
    {
        if (File.Exists("save3.json"))
        {
            CreateWindow(2, 3);
        }
    }

    public void Delete4RightClick(object sender, EventArgs e)
    {
        if (File.Exists("save4.json"))
        {
            CreateWindow(2, 4);
        }
    }

    public void Delete5RightClick(object sender, EventArgs e)
    {
        if (File.Exists("save5.json"))
        {
            CreateWindow(2, 5);
        }
    }

    public void ExitButtonClick(object sender, EventArgs e)
    {
        ScreenManager.Instance.RemoveScreen();
    }

    public override void Update(GameTime gameTime)
    {

        _exitButton.Update();
        if (_window != null)
        {
            _window.Update();
        }
        else
        {
            foreach (var button in _buttons)
            {
                button.Update();
            }

        }
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        // spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp);
        spriteBatch.Begin(samplerState: SamplerState.PointClamp);
        spriteBatch.Draw(_backgroundTexture, new Rectangle(0, 0, Globals.ScreenWidth, Globals.ScreenHeight), Color.White);
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
}