using static Helper.Constants.Dungeon;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Helper;

namespace Models.Model;
public class Door : Object{
    private bool _isClosed;

    public Door()
        : base(){
        IsPassable = false;
        _isClosed = true;
        Name = "Door";
    }

    public void Load( ContentManager Content ){
        SourceRectangle = new Rectangle(){
            X = SourceLocationX[(int)Constants.Dungeon.Name.UpperClosedDoor ] * TextureWidth,
            Y = SourceLocationY[(int)Constants.Dungeon.Name.UpperClosedDoor ] * TextureHeight,
            Width = TextureWidth,
            //There's 2 part of the door, upper and lower so we multiply by 2 here to get all the part of the door
            Height = TextureHeight * 2,
        };
        DefaultTexture = Content.Load<Texture2D>( TextureName );
        Scale = 2.5f;
        Position = new Rectangle(){
            X = -100,
            Y = -100,
            Width = TextureWidth * 2 / 5,
            //There's 2 part of the door, upper and lower so we multiply by 2 here to get all the part of the door
            Height = TextureHeight * 2,
        };
    }

    public override void Interact( Constants.Knight.Action action ){
        Rectangle temp = Position;
        if( _isClosed ){
            SourceRectangle = new Rectangle(){
                X = SourceLocationX[(int)Constants.Dungeon.Name.UpperOpenedDoor ] * TextureWidth,
                Y = SourceLocationY[(int)Constants.Dungeon.Name.UpperOpenedDoor ] * TextureHeight,
                Width = TextureWidth,
                //There's 2 part of the door, upper and lower so we multiply by 2 here to get all the part of the door
                Height = TextureHeight * 2,
            };
            IsPassable = true;
            temp.Width = TextureWidth;
        } else {
            SourceRectangle = new Rectangle(){
                X = SourceLocationX[(int)Constants.Dungeon.Name.UpperClosedDoor ] * TextureWidth,
                Y = SourceLocationY[(int)Constants.Dungeon.Name.UpperClosedDoor ] * TextureHeight,
                Width = TextureWidth,
                //There's 2 part of the door, upper and lower so we multiply by 2 here to get all the part of the door
                Height = TextureHeight * 2,
            };
            IsPassable = false;
            temp.Width = TextureWidth * 2 / 5;
        }
        _isClosed = !_isClosed;
        Position = temp;
    }
}
