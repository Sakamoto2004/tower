using static Helper.Constants.Dungeon;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Models.Model;
public class StoneWall : Object {
    public StoneWall()
        : base(){
        IsPassable = true;
    }
    
    public void Load(ContentManager content, int textureNo = 1 ){
        Rectangle source = Rectangle.Empty;
        int sourceTarget = -1;
        if( textureNo < 1 || textureNo > 33 )
            textureNo = 1;
        string[] temp = Enum.GetNames( typeof(Name) );
        for( int index = 0; index < temp.Length; ++index ){
            if( temp[index].Contains("Stone" + textureNo) ){
                sourceTarget = index;
                break;
            }
        }
        source.X = SourceLocationX[sourceTarget] * TextureWidth;
        source.Y = SourceLocationY[sourceTarget] * TextureHeight;
        source.Width = TextureWidth;
        source.Height = TextureHeight;
        SourceRectangle = source;
    }

    public void Draw(SpriteBatch batch ){
        Vector2 position = new Vector2(){ X = Position.X, Y = Position.Y };
        batch.Draw(DefaultTexture, position, SourceRectangle, Color.White, Rotation, Origin, Scale, TextureEffect, Depth );    
    }
}
