#nullable disable
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Models.Model;

public class Object{
    public Texture2D DefaultTexture{ get; set; }
    public Rectangle Position{ get; set; }
    public Rectangle SourceRectangle{ get; set; }
    public float Rotation{ get; set; }
    public float Scale{ get; set; }
    public float Depth{ get; set; }
    public Vector2 Origin{ get; set; }
    public SpriteEffects TextureEffect{ get; set; }
    public bool IsPassable{ get; set; }

    public Object(){
        DefaultTexture = null;
        Position = new Rectangle();
        Rotation = 0;
        Scale = 1;
        Depth = 0;
        Origin = Vector2.Zero;
        TextureEffect = SpriteEffects.None;
        IsPassable = true;
    }

    public virtual void LoadSource(){
        TextureEffect = SpriteEffects.None;
    }

    public void SetPosition( int x, int y ){
        Rectangle position = Position;
        position.X = x;
        position.Y = y;
        Position = position;
    }

    /// This will simply add the @param name="x" and @param name="y" to the position of the object
    public void ChangePosition( int x, int y = 0){
        SetPosition( Position.X + x, Position.Y + y );
    }

    public void printCurrentPosition(){
        //Console.Write(ToString() + "\n=========\n" + $"X: {Position.X}\nY: {Position.Y}\nWidth: {Position.Width}\nHeight: {Position.Height}\n");
    }
}
