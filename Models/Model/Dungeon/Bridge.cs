using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using static Helper.Constants.Dungeon;

namespace Models.Model;

public class Bridge : Object{
    private Rectangle[] SourceRectangles;
    public Bridge(){
        IsPassable = false;
        Name = "Bridge";
        SourceRectangles = new Rectangle[2];
    }

    public void Load( ContentManager Content, int texture = 1 ){
        Rectangle temp1 = new Rectangle(){
            X = SourceLocationX[ (int)Helper.Constants.Dungeon.Name.LeftBridge1 ] * TextureWidth,
            Y = SourceLocationY[ (int)Helper.Constants.Dungeon.Name.LeftBridge1 ] * TextureHeight,
            Width = TextureWidth,
            Height = TextureHeight,
        };
        Rectangle temp2 = new Rectangle(){
            X = SourceLocationX[ (int)Helper.Constants.Dungeon.Name.RightBridge1 ] * TextureWidth,
            Y = SourceLocationY[ (int)Helper.Constants.Dungeon.Name.RightBridge1 ] * TextureHeight,
            Width = TextureWidth,
            Height = TextureHeight,
        };
        Rectangle temp3 = new Rectangle(){
            X = SourceLocationX[ (int)Helper.Constants.Dungeon.Name.MiddleBridge1 ] * TextureWidth,
            Y = SourceLocationY[ (int)Helper.Constants.Dungeon.Name.MiddleBridge1 ] * TextureHeight,
            Width = TextureWidth,
            Height = TextureHeight,
        };
        switch( texture ){
            case 2:
                temp1.X = SourceLocationX[ (int)Helper.Constants.Dungeon.Name.LeftBridge2 ] * TextureWidth;
                temp1.Y = SourceLocationY[ (int)Helper.Constants.Dungeon.Name.LeftBridge2 ] * TextureHeight;
                temp2.X = SourceLocationX[ (int)Helper.Constants.Dungeon.Name.RightBridge2 ] * TextureWidth;
                temp2.Y = SourceLocationY[ (int)Helper.Constants.Dungeon.Name.RightBridge2 ] * TextureHeight;
                temp3.X = SourceLocationX[ (int)Helper.Constants.Dungeon.Name.MiddleBridge2 ] * TextureWidth;
                temp3.Y = SourceLocationY[ (int)Helper.Constants.Dungeon.Name.MiddleBridge2 ] * TextureHeight;
                break;
            case 3:
                temp1.X = SourceLocationX[ (int)Helper.Constants.Dungeon.Name.LeftBrokenBridge ] * TextureWidth;
                temp1.Y = SourceLocationY[ (int)Helper.Constants.Dungeon.Name.LeftBrokenBridge ] * TextureHeight;
                temp2.X = SourceLocationX[ (int)Helper.Constants.Dungeon.Name.RightBrokenBridge ] * TextureWidth;
                temp2.Y = SourceLocationY[ (int)Helper.Constants.Dungeon.Name.RightBrokenBridge ] * TextureHeight;
                Name = "BrokenBridge";
                break;

        }
        if( texture == 1 || texture == 2 )
             SourceRectangles = new Rectangle[3]{ temp1, temp2, temp3 };
        else SourceRectangles = new Rectangle[2]{ temp1, temp2 };

        DefaultTexture = Content.Load<Texture2D>( TextureName );
        Scale = 2.5f;
        Position = new Rectangle(){
            X = -100,
            Y = -100,
            Width = TextureWidth * 2,
            //There's 2 part of the door, upper and lower so we multiply by 2 here to get all the part of the door
            Height = TextureHeight,
        };
    }

    public override void SetPosition(int x, int y, int w = 0, int h = 0){
        Rectangle position = Position;
        position.X = x;
        position.Y = y;
        if( w < TextureWidth * 2 ) 
            position.Width = TextureWidth * 2;
        else position.Width = w;
        Position = position;
    }

    public override void Draw(SpriteBatch batch){
        Vector2 position = new Vector2();
        position.X = Position.X;
        position.Y = Position.Y;
        //In case the bridge is broken
        if( SourceRectangles.Length == 2 ){
            batch.Draw( DefaultTexture, position, SourceRectangles[0], Color.White, Rotation, Origin, Scale, TextureEffect, Depth );    
            position.X += Position.X + Position.Width - TextureWidth;
            batch.Draw( DefaultTexture, position, SourceRectangles[1], Color.White, Rotation, Origin, Scale, TextureEffect, Depth );    
        } else if( SourceRectangles.Length == 3 ){
            batch.Draw( DefaultTexture, position, SourceRectangles[0], Color.White, Rotation, Origin, Scale, TextureEffect, Depth );    
            while( position.X + TextureWidth * Scale < Position.X + GetPosition().Width - TextureWidth * Scale){
                position.X += TextureWidth * Scale; 
                batch.Draw( DefaultTexture, position, SourceRectangles[2], Color.White, Rotation, Origin, Scale, TextureEffect, Depth );    
            }
            position.X = Position.X + GetPosition().Width - TextureWidth * Scale;
            batch.Draw( DefaultTexture, position, SourceRectangles[1], Color.White, Rotation, Origin, Scale, TextureEffect, Depth );    
        }
    }

    public override bool CheckCollision( Rectangle entity, bool hardCollision = true ){
        bool collided = false;
        //this will only check collision if there is something standing on it
        if (Position.X + GetPosition().Width > entity.X &&
            Position.X < entity.X + entity.Width && 
            Position.Y + GetPosition().Height > entity.Y &&
            Position.Y < entity.Y + entity.Height 
            )
        {
            //This is when the character move (hardCollision)
            if( (IsPassable == false && hardCollision == true) || hardCollision == false )
                collided = true;
            
        } 
        return collided;
    }
}
