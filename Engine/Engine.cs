using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using Models.Model;
using Helper;

namespace Engine;

public class Engine : Game
{
//    private Witch _blueWitch; 
    GraphicsDeviceManager _graphics;
    SpriteBatch _spriteBatch;
    private Viewport _viewport;

    private Knight _knight;
    private MapObjects _map;

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
        _knight = new Knight(){ XSpeed = 5 };
        _map = new MapObjects();

        _map.AddObject(new Entity());
        _map.AddObject(new Entity());
        _map.AddObject(new Entity());
        _map.AddObject(new Entity());
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
        _knight.Position = new Rectangle(){
            X = _viewport.Width / 2,
            Y = _viewport.Height / 2,
            Height = Constants.Knight.SourceSize.Height,
            Width = Constants.Knight.SourceSize.Width
        };

        _map.Entities[0].Position = new Rectangle(){
            Y = _viewport.Height * 3 / 4,
            X = 0,
            Width = _viewport.Width,
            Height = 10
        };
        _map.Entities[1].Position = new Rectangle(){
            Y = _viewport.Height * 1 / 4,
            X = 0,
            Width = _viewport.Width,
            Height = 10
        }; 
        _map.Entities[2].Position = new Rectangle(){
            X = _viewport.Width * 1 / 4,
            Y = 250,
            Width = 10,
            Height = _viewport.Height
        }; 
        _map.Entities[3].Position = new Rectangle(){
            X = _viewport.Width * 3 / 4,
            Y = 250,
            Width = 30,
            Height = _viewport.Height
        }; 

        Texture2D defaultTexture = new Texture2D(GraphicsDevice, 1, 1);
        defaultTexture.SetData(new Color[] { Color.White });
        _map.Load( defaultTexture );
        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
        // TODO: Add your update logic here
        float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
        _knight.Control( elapsed );
        _knight.Moving(_map, elapsed);
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        Texture2D _texture;
        GraphicsDevice.Clear(Color.Black);
        // TODO: Add your drawing code here
        _texture = new Texture2D(GraphicsDevice, 1, 1);
        _texture.SetData(new Color[] { Color.White });
        _spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);

        _map.Draw( _spriteBatch );
        _spriteBatch.Draw(_texture, _knight.CalibratePosition(), Color.White);
        _knight.Draw(_spriteBatch);

        _spriteBatch.End();
        base.Draw(gameTime);
    }
}
