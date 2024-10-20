#nullable disable
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Models.Model;

public class Entity{
    public Vector2 Position{ get; set; }
    public int Speed{ get; set; }
    public bool Paused{ get; set; }    
    public float Rotation{ get; set; }
    public float Scale{ get; set; }
    public float Depth{ get; set; }
    public Vector2 Origin{ get; set; }
    public SpriteEffects TextureEffect{ get; set; }

    protected float _frameCycle{ get; set; }
    protected int _currentFrame{ get; set; }
    protected float _totalElapsed{ get; set; }
    protected float _timePerFrame{ get; set; }
    protected int _maxFrame { get; set; }

    public Entity(){
        Position = Vector2.Zero;
        Speed = 0;
        Paused = true;
        Rotation = 0;
        Scale = 1;
        Depth = 0;
        Origin = Vector2.Zero;
        TextureEffect = SpriteEffects.None;
    }

    public virtual void LoadSource(){
        TextureEffect = SpriteEffects.None;
    }

    public virtual void NextFrame(){
        if( _currentFrame == _maxFrame - 1)
            _currentFrame = 0;
        else _currentFrame += 1;
    }

    public void NextToggleFrame(){
        if( _currentFrame == _maxFrame - 1 )
            return;
        NextFrame();
    }

    public virtual Rectangle CurrentFrameSource(){
        return Rectangle.Empty;
    }

    public void UpdateFrame(float elapsed){
        if( Paused )
            return;
        _totalElapsed += elapsed;
        //Console.WriteLine(_totalElapsed + " | " + _timePerFrame);
        if(_totalElapsed > _timePerFrame){
            NextFrame();
            _totalElapsed -= _timePerFrame;
        }
    }

    public void UpdateToggleFrame(float elapsed){
        if( Paused )
            return;
        _totalElapsed += elapsed;
        //Console.WriteLine(_totalElapsed + " | " + _timePerFrame);
        if(_totalElapsed > _timePerFrame){
            NextToggleFrame();
            _totalElapsed -= _timePerFrame;
        }
    }

    /// This will simply add the @param name="x" and @param name="y" to the position of the object
    public void ChangePosition( int x, int y ){
        Vector2 position = Position;
        position.X += x;
        position.Y += y;
        Position = position;
    }

    public virtual void MoveLeft(float elapsed){
        ChangePosition(-Speed, 0);
        TextureEffect = SpriteEffects.FlipHorizontally;
        UpdateFrame(elapsed);
    }

    public virtual void MoveRight(float elapsed){
        ChangePosition(Speed, 0);
        TextureEffect = SpriteEffects.None;
        UpdateFrame(elapsed);
    }

}
