using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using Models.Model;
using Helper;

namespace Engine;

public class Engine : Game
{
    private Witch _blueWitch; 
    GraphicsDeviceManager _graphics;
    SpriteBatch _spriteBatch;


    public Engine()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        _blueWitch = new Witch(){Speed = 5};
        if(_blueWitch.SourceRectangle == null)
            throw new Exception("Unable to initialize source rectangle");
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _blueWitch.Load(_spriteBatch);
        if( _blueWitch.Texture == null )
            throw new Exception("Texture is null");
        _blueWitch.Position = Vector2.Zero;

        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);


        // TODO: Add your drawing code here
        _spriteBatch.Begin();
        _spriteBatch.Draw(_blueWitch.Texture, _blueWitch.Position, _blueWitch.CurrentFrame(), Color.White);
        _spriteBatch.End();
        base.Draw(gameTime);
    }
}
