using static Helper.Constants.Dungeon;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Models.Model;
public class Door : Object{
    private bool _isClosed;

    public Door()
        : base(){
        IsPassable = false;
        _isClosed = true;
    }

    public void Load( ContentManager Content ){
        SourceRectangle = new Rectangle(){
            X = SourceLocationX[(int) Name.UpperClosedDoor ] * TextureWidth,
            Y = SourceLocationY[(int) Name.UpperClosedDoor ] * TextureHeight,
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

    public void Interact(){
        if( _isClosed ){
            SourceRectangle = new Rectangle(){
                X = SourceLocationX[(int) Name.UpperOpenedDoor ],
                Y = SourceLocationY[(int) Name.UpperOpenedDoor ],
                Width = TextureWidth,
                //There's 2 part of the door, upper and lower so we multiply by 2 here to get all the part of the door
                Height = TextureHeight * 2,
            };
            IsPassable = true;
        } else {
            SourceRectangle = new Rectangle(){
                X = SourceLocationX[(int) Name.UpperClosedDoor ],
                Y = SourceLocationY[(int) Name.UpperClosedDoor ],
                Width = TextureWidth,
                //There's 2 part of the door, upper and lower so we multiply by 2 here to get all the part of the door
                Height = TextureHeight * 2,
            };
            IsPassable = false;
        }
        _isClosed = !_isClosed;
    }
}
