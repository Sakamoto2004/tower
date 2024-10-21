#nullable disable
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Helper;

namespace Models.Model;

public class Witch : Entity{

    public Texture2D[] Textures{ get; set; }
    public Rectangle[][] SourceRectangle{ get; set; }
    public Constants.Witch.State CurrentState{ get; set; }

    public Witch()
        :base(){
            _totalElapsed = 0;
            Rotation = 0;
            Paused = false;
            Scale = (float)2.5;
            Depth = 1;
            _maxFrame = Constants.Witch.Blue.TextureFrames[0];
            _timePerFrame = (float) 0.8 / _maxFrame;
        }

    public void Load(ContentManager content){
        Textures = new Texture2D[(int)Constants.Witch.State.Total];
        SourceRectangle = new Rectangle[(int) Constants.Witch.State.Total][];
        
        for( int state = 0; state < (int) Constants.Witch.State.Total; ++state ){
            SourceRectangle[state] = new Rectangle[ Constants.Witch.Blue.TextureFrames[state] ];
            for( int index = 0; index < SourceRectangle[state].Length; ++index ){
                SourceRectangle[state][index].Y = index * Constants.Witch.Blue.RectSource.Height;
                SourceRectangle[state][index].X = 0;
                SourceRectangle[state][index].Width = Constants.Witch.Blue.RectSource.Width;
                SourceRectangle[state][index].Height = Constants.Witch.Blue.RectSource.Height;
            }
            Textures[state] = content.Load<Texture2D>(Constants.Witch.Blue.TextureName[state]);
        }
        
        base.LoadSource();
    }

    public void ChangeState( Constants.Witch.State state ){
        if( CurrentState == state )
            return;
        CurrentState = state;
        _maxFrame = Constants.Witch.Blue.TextureFrames[(int) state];
        _currentFrame = 0;
    }

    public override void NextFrame(){
        if( _currentFrame == Constants.Witch.Blue.TextureFrames[(int) CurrentState] - 1)
            _currentFrame = 0;
        else _currentFrame += 1;
    }

    public override Rectangle CurrentFrameSource(){
        return SourceRectangle[(int) CurrentState][_currentFrame];
    }

    public void Draw(SpriteBatch batch ){
        Vector2 position = new Vector2(){X = Position.X, Y = Position.Y};
        batch.Draw(Textures[(int) CurrentState], position, CurrentFrameSource(), Color.White, Rotation, Origin, Scale, TextureEffect, Depth );    
    }
}
