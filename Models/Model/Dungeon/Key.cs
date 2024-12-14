using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using static Helper.Constants.Dungeon;

namespace Models.Model;

public class Key : Object{
    public Key(){
        IsPassable = false;
        IsPickupable = true;
        Name = "Keys";
    }

    public void Load( ContentManager content ){
        SourceRectangle = new Rectangle(){
            X = SourceLocationX[(int)Helper.Constants.Dungeon.Name.Key] * TextureWidth,
            Y = SourceLocationY[(int)Helper.Constants.Dungeon.Name.Key] * TextureHeight,
            Width = TextureWidth,
            Height = TextureHeight,
        };
        Position = new Rectangle(){
            X = -100,
            Y = -100,
            Width = TextureWidth,
            Height = TextureHeight,
        };
        Scale = 2.5f;
        DefaultTexture = content.Load<Texture2D>(TextureName);
    }

    public override void Interact( Helper.Constants.Knight.Action action ){
        IsPickupable = false;
    }
}
