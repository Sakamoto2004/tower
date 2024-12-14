using static Helper.Constants.Dungeon;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Models.Model;
public class StoneWall : Object {
    public StoneWall()
        : base(){
        IsPassable = false;
    }

    public enum StoneWallName{
        UpperLeft,
        UpperMiddle,
        UpperRight,
        MiddleLeft,
        Middle,
        MiddleRight,
        LowerLeft,
        LowerMiddle,
        LowerRight,
    }
    
    public void Load(ContentManager content, StoneWallName textureName){
        Rectangle source = Rectangle.Empty;
        int sourceTarget = -1;
        string[] temp = Enum.GetNames( typeof(Name) );
        string? target = Enum.GetName( textureName );
        if( target == null )
            target = "Middle";
        Console.WriteLine( target );
        for( int index = 0; index < temp.Length; ++index ){
            if( temp[index].Contains( textureName + "StoneWall") ){
                sourceTarget = index;
                break;
            }
        }
        if( sourceTarget == -1 )
            sourceTarget = 1;
        source.X = SourceLocationX[sourceTarget] * TextureWidth;
        source.Y = SourceLocationY[sourceTarget] * TextureHeight;
        source.Width = TextureWidth;
        source.Height = TextureHeight;
        SourceRectangle = source;
        Scale = 2;
        DefaultTexture = content.Load<Texture2D>( TextureName );
    }
    
    public void SetPosition( int x, int y, int width, int height){
        if( width < 16 )
            width = 16;
        if( height < 16 )
            height = 16;
        Rectangle temp = new Rectangle();
        temp.X = x;
        temp.Y = y;
        temp.Width = width;
        temp.Height = height;
        Position = temp;
    }

    public override void Draw( SpriteBatch batch ){
        //base.Debug();
        int times = Position.Width / TextureWidth;
        int temp = 0;
        while( times > 0 ){
            Vector2 tmpPosition = new Vector2(){ X = Position.X + TextureWidth * temp * Scale, Y = Position.Y };
            batch.Draw(DefaultTexture, tmpPosition, SourceRectangle, Color.White, Rotation, Origin, Scale, TextureEffect, Depth );    
            ++temp;
            --times;
        }
        times = Position.Height / TextureHeight;
        temp = 0;
        while( times > 0 ){
            Vector2 tmpPosition = new Vector2(){ X = Position.X , Y = Position.Y + TextureHeight * temp * Scale};
            batch.Draw(DefaultTexture, tmpPosition, SourceRectangle, Color.White, Rotation, Origin, Scale, TextureEffect, Depth );    
            ++temp;
            --times;
        }
    }
}
