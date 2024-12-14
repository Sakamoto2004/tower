using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using static Helper.Constants.Dungeon;

namespace Models.Model;

public class SignBoard : Object{
    private bool _isOpened;
    public bool IsOpened{ get; set; }
    public SignBoard(){
        IsOpened = false;
        Name = "ClosedChest";
    }

    public void Load( ContentManager Content, bool right = true ){
        Rectangle temp = new Rectangle(){
            X = SourceLocationX[(int)Helper.Constants.Dungeon.Name.RightSign ] * TextureWidth,
            Y = SourceLocationY[(int)Helper.Constants.Dungeon.Name.RightSign ] * TextureHeight,
            Width = TextureWidth,
            Height = TextureHeight,
        };
        if( right == false ){
            temp.X = SourceLocationX[(int)Helper.Constants.Dungeon.Name.LeftSign ] * TextureWidth;
            temp.Y = SourceLocationY[(int)Helper.Constants.Dungeon.Name.LeftSign ] * TextureHeight;
        }
        SourceRectangle = temp;
        DefaultTexture = Content.Load<Texture2D>( TextureName );
        Scale = 2.5f;
        Position = new Rectangle(){
            X = -100,
            Y = -100,
            Width = TextureWidth,
            //There's 2 part of the door, upper and lower so we multiply by 2 here to get all the part of the door
            Height = TextureHeight,
        };
    }
}
