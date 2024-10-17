#nullable disable
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Models.Model;

public class Entity{
    public Texture2D Texture{ get; set; }
    public Rectangle[] SourceRectangle{ get; set; }
    public Vector2 Position{ get; set; }
    public float Speed{ get; set; }
    public bool Paused{get; set; }    
    protected int _numberOfFrame;
    protected int _currentFrame;
    protected float _timePerFrame;
    protected float _totalElapsed;

    public float Rotation, Scale, Depth;
    public Vector2 Origin;
    public SpriteEffects effect;

    public void LoadSource(Rectangle rect, int frames){
        _numberOfFrame = frames;
        _timePerFrame = (float)1 / _numberOfFrame;
        _currentFrame = 0;
        SourceRectangle = new Rectangle[frames];
        for( int index = 0; index < frames; ++index ){
            SourceRectangle[index].X = 0;
            SourceRectangle[index].Y = rect.Height * index;
            SourceRectangle[index].Height = rect.Height;
            SourceRectangle[index].Width = rect.Width;
        }

        effect = SpriteEffects.None;
        
    }

    public void NextFrame(){
        if( _currentFrame == _numberOfFrame - 1)
            _currentFrame = 0;
        else _currentFrame += 1;
    }

    public Rectangle CurrentFrameSource(){
        return SourceRectangle[_currentFrame];
    }

    public void UpdateFrame(float elapsed){
        if( Paused )
            return;

        _totalElapsed += elapsed;
        Console.WriteLine(_currentFrame);
        if(_totalElapsed > _timePerFrame){
            NextFrame();
            _totalElapsed -= _timePerFrame;
        }
    }

    public void Draw(SpriteBatch batch ){
        batch.Draw(Texture, Position, CurrentFrameSource(), Color.White, Rotation, Origin, Scale, effect, Depth );    
    }
}
