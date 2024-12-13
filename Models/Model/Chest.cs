using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using static Helper.Constants.Dungeon;

namespace Models.Model;

public class Chest : Object{
    private bool _isOpened;
    public bool IsOpened{ get; set; }
    public Chest(){
        IsOpened = false;
    }

    public void Load( ContentManager Content ){
        SourceRectangle = new Rectangle(){
            X = SourceLocationX[(int) Name.ClosedChest ] * TextureWidth,
            Y = SourceLocationY[(int) Name.ClosedChest ] * TextureHeight,
            Width = TextureWidth,
            Height = TextureHeight,
        };
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

    public override void Interact( Helper.Constants.Knight.Action action ){
        if( IsOpened )
            return;
        Rectangle temp = SourceRectangle;
        temp.X = SourceLocationX[(int) Name.OpenedChest] * TextureWidth;
        temp.Y = SourceLocationY[(int) Name.OpenedChest] * TextureHeight;
        IsOpened = true;
        SourceRectangle = temp;
    }
}
