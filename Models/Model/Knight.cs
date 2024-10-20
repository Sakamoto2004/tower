#nullable disable
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Helper;
using static Helper.Constants.Knight;

namespace Models.Model;

public class Knight : Entity{
    //Because there's 2 state of the texture 
    public Texture2D[][] Textures{ get; set; }

    //This is because the texture pack has 2 state, unequipped and equipped, with many texutre, and in which has many small pictures
    public Rectangle[][][] SourceRectangle{ get; set; }
    private Constants.Knight.UnequippedState _currentUnequippedState{ get; set; }
    private  Constants.Knight.EquippedState _currentEquippedState{ get; set; }

    private bool _isCrouching;
    private int _armed;
    private int _unarmed;
    public Knight() : base(){
        _totalElapsed = 0;
        Rotation = 0;
        Paused = false;
        Scale = (float) 1.0;
        Depth = 1;
        _maxFrame = Constants.Knight.UnequippedTextureFrames[0];
        _frameCycle = (float)0.5;
        _timePerFrame = _frameCycle / _maxFrame;
        _armed = 1;
        _unarmed = 0;
    }

    public void Load(ContentManager content){
        Textures = new Texture2D[2][] ;
        Textures[0] = new Texture2D[(int)UnequippedState.Total];
        Textures[1] = new Texture2D[(int)EquippedState.Total];

        SourceRectangle = new Rectangle[2][][];
        for( int armed = 0; armed < 2; ++armed ){
            int maxState = 0;
            if( armed == 0 ){
                SourceRectangle[armed] = new Rectangle[(int)UnequippedState.Total][];
                maxState = (int)UnequippedState.Total;
            }
            else{
                SourceRectangle[armed] = new Rectangle[(int)EquippedState.Total][];
                maxState = (int)EquippedState.Total;
            }
            for( int state = 0; state < maxState; ++state ){
                if( armed == 0 ){
                    SourceRectangle[armed][state] = new Rectangle[(int) UnequippedTextureFrames[ state ]];
                    Textures[armed][state] = content.Load<Texture2D>( UnequippedTextureName[ state ] );
                } else {
                    SourceRectangle[armed][state] = new Rectangle[(int) EquippedTextureFrames[ state ]];
                    Textures[armed][state] = content.Load<Texture2D>( EquippedTextureName[ state ] );
                }
                for( int frame = 0; frame < SourceRectangle[armed][state].Length; ++frame ){
                    SourceRectangle[armed][state][frame].Y = 0;
                    SourceRectangle[armed][state][frame].X = SourceSize.Width * frame;
                    SourceRectangle[armed][state][frame].Height = SourceSize.Height;
                    SourceRectangle[armed][state][frame].Width = SourceSize.Width;
                }
            }
        }
        base.LoadSource();
    }

    public void ChangeUnequippedState( UnequippedState state = UnequippedState.Total ){
        if( state == _currentUnequippedState )
            return;
        _currentEquippedState = EquippedState.Total;
        _currentUnequippedState = state;
        _maxFrame = UnequippedTextureFrames[(int) state];
        _timePerFrame = _frameCycle / _maxFrame;
        _currentFrame = 0;
    }

    public void ChangeEquippedState( EquippedState state = EquippedState.Total ){
        if( state == _currentEquippedState )
            return;
        _currentUnequippedState = UnequippedState.Total;
        _currentEquippedState = state;
        _maxFrame = EquippedTextureFrames[(int) state];
        _timePerFrame = _frameCycle / _maxFrame;
        _currentFrame = 0;
    }

    public override Rectangle CurrentFrameSource(){
        if( _currentUnequippedState == UnequippedState.Total )
            return SourceRectangle[_armed][(int) _currentUnequippedState][_currentFrame];
        else return SourceRectangle[_unarmed][(int) _currentUnequippedState][_currentFrame];
    }

    public override void NextFrame(){
        _currentFrame = ++_currentFrame % _maxFrame;
    }

    public void Draw(SpriteBatch batch ){
        if( _currentUnequippedState == UnequippedState.Total )
            batch.Draw(Textures[_armed][(int) _currentEquippedState], Position, CurrentFrameSource(), Color.White, Rotation, Origin, Scale, TextureEffect, Depth );    
        else batch.Draw(Textures[_unarmed][(int) _currentUnequippedState], Position, CurrentFrameSource(), Color.White, Rotation, Origin, Scale, TextureEffect, Depth );    
    }

    public override void MoveLeft(float elapsed){
        if( _currentUnequippedState == UnequippedState.Total ){
            if( _isCrouching )
                ChangeEquippedState(EquippedState.CrouchingWalk);
            else ChangeEquippedState(EquippedState.Walking);
            ChangePosition(-Speed, 0);
        } else {
            if( _isCrouching )
                ChangeUnequippedState(UnequippedState.CrouchingWalk);
            else ChangeUnequippedState(UnequippedState.Walking);
            ChangePosition(-Speed, 0);
        }
        TextureEffect = SpriteEffects.FlipHorizontally;
        UpdateFrame(elapsed);
    }

    public override void MoveRight(float TotalTime){
        if( _currentUnequippedState == UnequippedState.Total ){
            if( _isCrouching )
                ChangeEquippedState(EquippedState.CrouchingWalk);
            else ChangeEquippedState(EquippedState.Walking);
            ChangePosition(Speed, 0);
        } else {
            if( _isCrouching )
                ChangeUnequippedState(UnequippedState.CrouchingWalk);
            else ChangeUnequippedState(UnequippedState.Walking);
            ChangePosition(Speed, 0);
        }
        TextureEffect = SpriteEffects.None;
        UpdateFrame(TotalTime);
    }

    public void Idling(float elapsed){
        if( _currentUnequippedState == UnequippedState.Total ){
            ChangeEquippedState(EquippedState.Idling);
        } else {
            ChangeUnequippedState(UnequippedState.Idling);
        }
        UpdateFrame(elapsed);
    }

    public void Crouching(float elapsed){
        if( _isCrouching == false ){
            if( _currentUnequippedState == UnequippedState.Total ){
                ChangeEquippedState(EquippedState.Crouching);
            } else {
                ChangeUnequippedState(UnequippedState.Crouching);
            }
            UpdateToggleFrame(elapsed);
            if( _currentFrame == _maxFrame -1 ){
                Speed = CrouchingSpeed;
                _isCrouching = true;
            }
        }
    }

    public void Control(float elapsed){
        if( Keyboard.GetState().IsKeyDown(Keys.Left) )
            MoveLeft(elapsed);
        else if( Keyboard.GetState().IsKeyDown(Keys.Right) )
            MoveRight(elapsed);
        else if( Keyboard.GetState().IsKeyDown(Keys.LeftControl) )
            Crouching(elapsed);
        else if( Keyboard.GetState().IsKeyUp( Keys.LeftControl ) ){
            Speed = WalkingSpeed;
            _isCrouching = false;
        }

        else if( Keyboard.GetState().IsKeyDown(Keys.Space) )
            ChangeEquippedState(EquippedState.Attack1);
        else if( Keyboard.GetState().IsKeyDown(Keys.A) )
            ChangeEquippedState(EquippedState.Crouching);
        else if( Keyboard.GetState().IsKeyDown(Keys.K) )
            ChangeEquippedState(EquippedState.Dying);
        else if( Keyboard.GetState().IsKeyDown(Keys.E) )
            ChangeUnequippedState(UnequippedState.Drinking);

        if( Keyboard.GetState().GetPressedKeyCount() == 0 )
            Idling(elapsed);
    }
}
