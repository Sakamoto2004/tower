using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using Models.Model;
using Helper;

namespace Engine.Page;

public class GameLoader{
    private GraphicsDevice _graphicsDevice;
    private Knight _knight;
    private MapObjects _map;

    public GameLoader( GraphicsDevice graphicDevice){
        _graphicsDevice = graphicDevice;
    }

    public void Initialize(){
        _knight = new Knight(){ XSpeed = 5 };
        _map = new MapObjects();

        _map.AddEntity(new Entity());
        _map.AddEntity(new Entity());
        _map.AddEntity(new Entity());
        _map.AddEntity(new Entity());
        _map.AddObject(new Stone());
    }

    public void LoadContent(ContentManager content){
    
        _knight.Load(content);
        if(_knight.SourceRectangles == null)
            throw new Exception("Unable to initialize source rectangle");
        if( _knight.Textures == null )
            throw new Exception("Texture is null");

        StoneWall temp = new StoneWall();
        temp.Load( content, StoneWall.StoneWallName.LowerMiddle );
        temp.SetPosition( 0, 50, 800, 16);
        _map.AddObject( temp );

        temp = new StoneWall();
        temp.Load( content, StoneWall.StoneWallName.UpperMiddle );
        temp.SetPosition( 0, 350, 800, 16);
        _map.AddObject( temp );

        temp = new StoneWall();
        temp.Load( content, StoneWall.StoneWallName.MiddleRight );
        temp.SetPosition( 50, 250, 16, 800);
        _map.AddObject( temp );

        temp = new StoneWall();
        temp.Load( content, StoneWall.StoneWallName.MiddleLeft );
        temp.SetPosition( 350, 250, 16, 800);
        _map.AddObject( temp );

        _knight.Position = new Rectangle(){
            X = 150,
            Y = 150,
            Height = Constants.Knight.SourceSize.Height,
            Width = Constants.Knight.SourceSize.Width
        };

        Texture2D defaultTexture = new Texture2D(_graphicsDevice, 1, 1);
        defaultTexture.SetData(new Color[] { Color.White });
        _map.LoadEntities( defaultTexture );
        // TODO: use this.Content to load your game content here
    }

    public void Update(float elapsed){
        _knight.Control( elapsed );
        _knight.Moving(_map, elapsed);
    }

    public void Draw( SpriteBatch _spriteBatch ){
        Texture2D _texture;
        _texture = new Texture2D(_graphicsDevice, 1, 1);
        _texture.SetData(new Color[] { Color.White });
        _spriteBatch.Draw(_texture, _knight.CalibratePosition(), Color.White);
        _spriteBatch.Draw(_texture, _knight.CalibrateAttackHitbox(), Color.Red);
        _knight.Draw(_spriteBatch);
        _map.Draw( _spriteBatch );
    }
}   
