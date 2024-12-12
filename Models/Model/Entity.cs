#nullable disable
using Helper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Models.Model;

public class Entity{
    public Texture2D DefaultTexture{ get; set; }
    public CollisionChecker CollisionChecker{ get; set; }
    public Rectangle Position{ get; set; }
    public Rectangle SourceRectangle{ get; set; }
    public int XSpeed{ get; set; }
    public int YSpeed{ get; set; }
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
        DefaultTexture = null;
        Position = new Rectangle();
        XSpeed = 0;
        YSpeed = 0;
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

    public virtual void MoveLeft(float elapsed){
        ChangePosition(-Constants.Knight.WalkingSpeed, 0);
        TextureEffect = SpriteEffects.FlipHorizontally;
        UpdateFrame(elapsed);
    }

    public virtual void MoveRight(float elapsed){
        ChangePosition(Constants.Knight.WalkingSpeed, 0);
        TextureEffect = SpriteEffects.None;
        UpdateFrame(elapsed);
    }

    public virtual bool CheckCollision( Rectangle entity ){
        bool collided = false;
        //This collision check will check the left side of the entity
        if (Position.X + Position.Width > entity.X &&
            Position.X < entity.X + entity.Width && 
            Position.Y + Position.Height > entity.Y &&
            Position.Y < entity.Y + entity.Height 
            )
        {
            collided = true;
        } 
        return collided;
    }

    public void SetPosition( int x, int y ){
        Rectangle position = Position;
        position.X = x;
        position.Y = y;
        Position = position;
    }

    public Rectangle GetPosition( ){
        Rectangle position = Position;
        position.Width = (int) (Position.Width * Scale);
        position.Height = (int) (Position.Height * Scale);
        return position;
    }   


    /// This will simply add the @param name="x" and @param name="y" to the position of the object
    public void ChangePosition( int x, int y = 0){
        SetPosition( Position.X + x, Position.Y + y );
    }

    //This will check X and Y axis for collision, I'll do some optimization for it after some time
    public virtual void HandleCollision( Rectangle entity, float elapsed = 0.0f ){
        ChangePosition( 0, -YSpeed );
        if( CheckCollision( entity ) == true )
            ChangePosition( -XSpeed, 0 );
        ChangePosition( 0, YSpeed );
        if( CheckCollision( entity ) == true ){
            if( YSpeed > 0 ){
                SetPosition( Position.X,  entity.Y - Position.Height );
                PhysicEngine.ResetTimer();
            } else {
                SetPosition( Position.X, entity.Y + entity.Height );
            }
            YSpeed = 0;
        }
    }

    public void printCurrentPosition(){
        //Console.Write(ToString() + "\n=========\n" + $"X: {Position.X}\nY: {Position.Y}\nWidth: {Position.Width}\nHeight: {Position.Height}\n");
    }

    public virtual void Moving( MapObjects objects, float elapsed ){
        YSpeed = PhysicEngine.SpeedCalculator(Constants.Knight.FallingAcceleration, YSpeed, elapsed);
        ChangePosition( XSpeed, YSpeed );
        for( int i = 0; i < objects.Entities.Count; ++i ){
            Entity temp = objects.Entities[i];
            if( temp.CheckCollision( temp.Position ) )
                HandleCollision(temp.Position);
        }
        XSpeed = 0;
    }

    public virtual void Draw(SpriteBatch batch ){
        Vector2 position = new Vector2(){ X = Position.X, Y = Position. Y};
        batch.Draw(DefaultTexture, position, SourceRectangle, Color.White, Rotation, Origin, Scale, TextureEffect, Depth );    
    }
}
