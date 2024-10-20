using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using Models.Model;

namespace Engine;

public class Engine : Game
{
//    private Witch _blueWitch; 
    GraphicsDeviceManager _graphics;
    SpriteBatch _spriteBatch;
    private Viewport _viewport;

    private Knight _knight;

    public Engine()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
//        _blueWitch = new Witch(){Speed = 5};
        _knight = new Knight(){ Speed = 5 };
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
//        _blueWitch.Load(Content);
//        if(_blueWitch.SourceRectangle == null)
//            throw new Exception("Unable to initialize source rectangle");
//        if( _blueWitch.Textures == null )
//            throw new Exception("Texture is null");

        _knight.Load(Content);
        if(_knight.SourceRectangle == null)
            throw new Exception("Unable to initialize source rectangle");
        if( _knight.Textures == null )
            throw new Exception("Texture is null");

        _viewport = _graphics.GraphicsDevice.Viewport;
//        _blueWitch.Position = new Vector2(_viewport.Width / 2, _viewport.Height /2 );
        _knight.Position = new Vector2(_viewport.Width / 2, _viewport.Height /2 );

        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
        // TODO: Add your update logic here
        float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
        _knight.Control( elapsed );

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);
        // TODO: Add your drawing code here
        _spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);
        _knight.Draw(_spriteBatch);
        _spriteBatch.End();
        base.Draw(gameTime);
    }
}
