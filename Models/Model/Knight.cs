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
    public Rectangle[][][] SourceRectangles{ get; set; }
    public int Damage{ get; set; }

    //I have to split into 2 because there are 2 state with different textures.
    private Constants.Knight.UnequippedState _currentUnequippedState{ get; set; }
    private  Constants.Knight.EquippedState _currentEquippedState{ get; set; }
    private float _swapCooldown;
    private float _jumpTimer;
    private float _dashCoolDown;
    private bool _isDrinking;
    private short _health;
    private bool _isHurting;
    private bool _isCrouching;
    private int _armed;
    private int _unarmed;
    private int _isAttacking;
    private bool _isDashing;
    private bool _isPoweringUp;
    private float _powerUpTime;
    private bool _isPoweredUp;
    private int _damageChanged;
    private bool _isLanding;
    private bool _isPushing;
    //Have to add this because the model only push if they're moving, which mean we have to have this variable to know
    //How long has the model pushed
    private float _pushingTime;
    private float _interactCooldown;
    private int _keys;

    public Knight() : base(){
        _jumpTimer = Constants.Knight.JumpTime;
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
        _health = 5;
        _isHurting = false;
        _isAttacking = 0;
        _isDashing = false;
        _dashCoolDown = 0.5f;
        _isPoweringUp = false;
        _powerUpTime = -20f;
        _isPushing = false;
        _pushingTime = 0.0f;
        Damage = 1;
    }
    
    public void Load(ContentManager content){
        Textures = new Texture2D[2][] ;
        Textures[0] = new Texture2D[(int)UnequippedState.Total];
        Textures[1] = new Texture2D[(int)EquippedState.Total];

        SourceRectangles = new Rectangle[2][][];
        for( int armed = 0; armed < 2; ++armed ){
            int maxState = 0;
            if( armed == 0 ){
                SourceRectangles[armed] = new Rectangle[(int)UnequippedState.Total][];
                maxState = (int)UnequippedState.Total;
            }
            else{
                SourceRectangles[armed] = new Rectangle[(int)EquippedState.Total][];
                maxState = (int)EquippedState.Total;
            }
            for( int state = 0; state < maxState; ++state ){
                if( armed == 0 ){
                    SourceRectangles[armed][state] = new Rectangle[(int) UnequippedTextureFrames[ state ]];
                    Textures[armed][state] = content.Load<Texture2D>( UnequippedTextureName[ state ] );
                } else {
                    SourceRectangles[armed][state] = new Rectangle[(int) EquippedTextureFrames[ state ]];
                    Textures[armed][state] = content.Load<Texture2D>( EquippedTextureName[ state ] );
                }
                for( int frame = 0; frame < SourceRectangles[armed][state].Length; ++frame ){
                    SourceRectangles[armed][state][frame].Y = 0;
                    SourceRectangles[armed][state][frame].X = SourceSize.Width * frame;
                    SourceRectangles[armed][state][frame].Height = SourceSize.Height;
                    SourceRectangles[armed][state][frame].Width = SourceSize.Width;
                    SourceRectangles[armed][state][frame] = CalibrateSource( SourceRectangles[armed][state][frame] );
                }
            }
        }
        base.LoadSource();
    }

    public Rectangle CalibrateSource( Rectangle origin ){
        Rectangle result = origin;
        int offset = Constants.Knight.SourceTextureOffset;
//        result.X = origin.X + offset;
//        result.Width = origin.Width - offset * 2;
        return result;
    }

    public Rectangle CalibratePosition(){
        Rectangle position = Position;
        int offset = (int) (  (float) Constants.Knight.SourceTextureOffset * Scale );
        if( _currentUnequippedState != UnequippedState.Total ){
            position.X = Position.X + offset;
            position.Width = Position.Width - offset * 2;
        } else if( _currentEquippedState != EquippedState.Total ){
            position.X = Position.X + offset - 3;
            position.Width = Position.Width - (offset - 3) * 2;
        } 
        if( _isCrouching ){
            offset = 20;
            position.Y = Position.Y + offset;
            position.Height = Position.Height - offset;
            if (TextureEffect != SpriteEffects.FlipHorizontally ){
                position.X += 5;
            } else if (TextureEffect == SpriteEffects.FlipHorizontally ){
                position.X -= 5;
            }
        }
        return position;
    }

    public Rectangle CalibrateAttackHitbox(){
        Rectangle position = Position;
        int offset = (int) (  (float) Constants.Knight.SourceTextureOffset * Scale );
        if( _currentUnequippedState != UnequippedState.Total ){
            position.X = Position.X + offset;
            position.Width = Position.Width - offset * 2 - offset / 3;
        } else if( _currentEquippedState != EquippedState.Total ){
            position.X = Position.X + offset - 3;
            position.Width = Position.Width - (offset - 3) * 2 - offset / 3;
        } 
        if( TextureEffect == SpriteEffects.FlipHorizontally || _isAttacking == 4 )
            position.X -= position.Width;
        else position.X += ( position.Width + offset / 3 );
        if( _isAttacking == 4 )
            position.Width = 2 * position.Width + CalibratePosition().Width;
        return position;
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

    public void ChangeState( string state ){
        EquippedState tempE = EquippedState.Total;
        string[] temp = Enum.GetNames( typeof(EquippedState) );
        for( int index = 0; index < (int) EquippedState.Total; ++index ){
            if( temp[index].Equals( state ) ){
                tempE = (EquippedState) index;
                break;
            }
        }   
        //If the state is Total (Which mean the knight is in different state), we'll set the other state and return
        if( _currentUnequippedState == UnequippedState.Total && tempE != EquippedState.Total ){
            ChangeEquippedState( tempE );
            return;
        }
        UnequippedState tempU = UnequippedState.Total;
        temp = Enum.GetNames( typeof( UnequippedState ) );
        for( int index = 0; index < (int) UnequippedState.Total; ++index ){
            if( temp[index].Equals( state ) ){
                tempU = (UnequippedState) index;
                break;
            }
        }
        if( tempU == UnequippedState.Total )
            return;
        ChangeUnequippedState( tempU );
    }

    public void SwapState( float elapsed ){
        if( _swapCooldown > 0 ){
            _swapCooldown -= elapsed;
            return;
        }
        _swapCooldown = Constants.Knight.SwapCooldown;
        if( _currentUnequippedState == UnequippedState.Total ){
            _currentUnequippedState = UnequippedState.Idling;
            _currentEquippedState = EquippedState.Total;
            return;
        }
        _currentEquippedState = EquippedState.Idling;
        _currentUnequippedState = UnequippedState.Total;
        XSpeed = -4;
        if( TextureEffect == SpriteEffects.FlipHorizontally )
            XSpeed *= -1;
    }

    public override Rectangle CurrentFrameSource(){
        //Console.WriteLine( _currentEquippedState + ", " + _currentFrame );
        if( _currentUnequippedState == UnequippedState.Total )
            return SourceRectangles[_armed][(int) _currentEquippedState][_currentFrame];
        else return SourceRectangles[_unarmed][(int) _currentUnequippedState][_currentFrame];
    }

    public override void NextFrame(){
        _currentFrame = ++_currentFrame % _maxFrame;
    }

    public override void Draw(SpriteBatch batch ){
        Vector2 position = new Vector2(){ X = Position.X, Y = Position.Y };
        if( _currentUnequippedState == UnequippedState.Total )
            batch.Draw(Textures[_armed][(int) _currentEquippedState], position, CurrentFrameSource(), Color.White, Rotation, Origin, Scale, TextureEffect, Depth );    
        else batch.Draw(Textures[_unarmed][(int) _currentUnequippedState], position, CurrentFrameSource(), Color.White, Rotation, Origin, Scale, TextureEffect, Depth );    
    }

    public override void MoveLeft(float elapsed){
        if( _currentUnequippedState == UnequippedState.Total ){
            if( _isCrouching ){
                ChangeState("CrouchingWalk");
                XSpeed = -CrouchingSpeed;
            } else {
                ChangeState("Walking");
                XSpeed = -WalkingSpeed;
            }
        } else {
            if( _isCrouching ){
                ChangeState("CrouchingWalk");
                XSpeed = -CrouchingSpeed;
            } else {
                ChangeState("Walking");
                XSpeed = -WalkingSpeed;
            }
        }
        TextureEffect = SpriteEffects.FlipHorizontally;
        UpdateFrame(elapsed);
    }

    public override void MoveRight(float TotalTime){
        if( _currentUnequippedState == UnequippedState.Total ){
            if( _isCrouching ){
                ChangeState("CrouchingWalk");
                XSpeed = CrouchingSpeed;
            } else {
                ChangeState("Walking");
                XSpeed = WalkingSpeed;
            }
        } else {
            if( _isCrouching ){
                ChangeState("CrouchingWalk");
                XSpeed = CrouchingSpeed;
            } else {
                ChangeState("Walking");
                XSpeed = WalkingSpeed;
            }
        }
        TextureEffect = SpriteEffects.None;
        UpdateFrame(TotalTime);
    }

    public override void Moving( MapObjects objects, float elapsed ){
        if( _currentUnequippedState == UnequippedState.LedgeGrabbing ||
            _currentUnequippedState == UnequippedState.GrappedIdling ){
            return;
        }
        YSpeed = PhysicEngine.SpeedCalculator(Constants.Knight.FallingAcceleration, YSpeed, elapsed);
        ChangePosition( XSpeed, YSpeed );
        if( YSpeed < 0 ){
            ChangeState("Jumpping");
        } else if ( YSpeed > 1 ){
            ChangeState("Falling");
        }
        for( int i = 0; i < objects.Entities.Count; ++i ){
            Entity temp = objects.Entities[i];
            if( temp.CheckCollision( GetPosition() ) )
                HandleCollision( temp.GetPosition(), elapsed );
        }
        for( int i = 0; i < objects.Objects.Count; ++i ){
            Object temp = objects.Objects[i];
            if( temp.CheckCollision( GetPosition() ) ){
                if( temp.IsPickupable && temp.ObjectName == "Keys" ){
                    _keys += 1;
                    objects.Objects.Remove( temp );
                }
                HandleCollision( temp.GetPosition(), elapsed );
            }
        }
        XSpeed = 0;
        //Console.WriteLine("Falling speed: " + YSpeed );
    }

    public void Idling(float elapsed){
        ChangeState("Idling");
        UpdateFrame(elapsed);
    }

    public void Crouching(float elapsed){
        if( _isCrouching == false ){
            ChangeState("Crouching");
            UpdateToggleFrame(elapsed);
            if( _currentFrame == _maxFrame -1 ){
                _isCrouching = true;
                if( TextureEffect != SpriteEffects.FlipHorizontally ){
                    ChangePosition( -5, 0);
                } else if( TextureEffect == SpriteEffects.FlipHorizontally ){
                    ChangePosition( 5, 0);
                }
            }
        }
    }

    public void Hurting(float elapsed){
        if( _isHurting == true ){
            if( _currentFrame == _maxFrame - 1 && _totalElapsed + elapsed >= _timePerFrame )
                _isHurting = false;
            UpdateFrame(elapsed);
            return;
        }
        ChangeState("Hurting");
        _isHurting = true;
        --_health;
    }

    public void PoweringUp( float elapsed, int increase = 2, float powerUpTime = 5f ){
         if( _isPoweringUp == true ){
            if( _currentFrame == _maxFrame - 1 && _totalElapsed + elapsed >= _timePerFrame ){
                _isPoweringUp = false;
                Damage += increase;
                _isPoweredUp = true;
                _damageChanged = increase;
                _powerUpTime = powerUpTime;
                //Console.WriteLine( Damage + ": " + _powerUpTime );
            }
            UpdateFrame(elapsed);
            return;
         }
         if( _powerUpTime > -10f )
             return;
         ChangeState("PoweringUp");
         _timePerFrame = (float) 1 / _maxFrame;
         _isPoweringUp = true;
    }

    public void PowerRemain( float elapsed ){
        if( _powerUpTime < 0 && _isPoweredUp ){
            Damage -= _damageChanged;
            _damageChanged = 0;
            _isPoweredUp = false;
        }

        //The negative will be the cooldown for the next power up
        if( _powerUpTime > -10f ){
            _powerUpTime -= elapsed;
            //Console.WriteLine( Damage + ": " + _powerUpTime );
            return;
        }
    }

    public void Drinking( float elapsed ){
         if( _isDrinking == true ){
            if( _currentFrame == _maxFrame - 1 && _totalElapsed + elapsed >= _timePerFrame ){
                _isDrinking = false;
                ++_health;
            }
            UpdateFrame(elapsed);
            return;
         }
         ChangeState("Drinking");
         _timePerFrame = (float) 1 / _maxFrame;
         _isDrinking = true;
    }

    public bool IsDeath(){
        if( _health == 0 )
            return true;
        return false;
    }

    public void Dying( float elapsed ){
        ChangeState( "Dying" );
        UpdateToggleFrame( elapsed );
    }

    public void Landing( float elapsed ){
         if( _isLanding == true ){
            if( _currentFrame == _maxFrame - 1 && _totalElapsed + elapsed >= _timePerFrame ){
                _isLanding = false;
            }
            UpdateFrame(elapsed);
            return;
         }
         ChangeState("Landing");
         _timePerFrame = (float) 0.5f / _maxFrame;
         _isLanding = true;
    }

    public void UpdatePushingFrame( float elapsed ){
        if( Paused )
            return;
        _pushingTime += elapsed;
        //Console.WriteLine(_totalElapsed + " | " + _timePerFrame);
        ChangeState("Pushing");
         _timePerFrame = (float) 0.75f / _maxFrame;
        if(_pushingTime > _timePerFrame){
            int frameIndex = (int) (_pushingTime / _timePerFrame);
            if( frameIndex >= _maxFrame ){
                frameIndex = 0;
                _pushingTime -= _maxFrame * _timePerFrame;
            }
            _currentFrame = frameIndex;
        }
    }

    //I have to make it check using flag as moving already changed the texture so I can't make it check using state
    public void Pushing( float elapsed ){
         if( _isPushing ){
            UpdatePushingFrame(elapsed);
            return;
         }
         _isPushing = true;
    }

    //Only use this when there's already collision on X axis
    public bool IsGrabbable( Rectangle entity, float elapsed = 0.0f ){
        Rectangle position = CalibratePosition();
        //Console.WriteLine("Grab position: " + (position.Y - position.Height * 1 / 10));
        //Console.WriteLine("entity position: " +  entity.Y );
        if( position.Y + position.Height * 1 / 3 < entity.Y ) 
            return false;
        if( position.Y > entity.Y )
            return false ;
        return true;
    }

    //This will check X and Y axis for collision, I'll do some optimization for it after some time
    public override void HandleCollision( Rectangle entity, float elapsed = 0.0f ){
        //We already made change to the position, so this will reset 1 axis for checking collision at the other axis
        //The will reset the Y axis to check collision to the X axis 
        ChangePosition( 0, -YSpeed );
        if( CheckCollision( entity ) == true ){
            ChangePosition( -XSpeed, 0 );
            if( IsGrabbable( entity ) ){
                Grabbing( elapsed );
                return;
            } else if( _isAttacking == 0 && _isDashing == false ){
                Pushing( elapsed );
            }
        }
        ChangePosition( 0, YSpeed );
        if( CheckCollision( entity ) == true ){
            if( YSpeed > 0 ){
                SetPosition( Position.X,  entity.Y - Position.Height );
                PhysicEngine.ResetTimer();
                _jumpTimer = Constants.Knight.JumpTime;
                if( (_currentEquippedState == EquippedState.Falling || _currentUnequippedState == UnequippedState.Falling) && YSpeed > 16 )
                    Landing( 0.01f );
            } else {
                SetPosition( Position.X, entity.Y + entity.Height );
            }
            YSpeed = 0;
        }
    }

    public void Jumping( float elapsed)
    {
        _jumpTimer -= elapsed;
        if( _jumpTimer > 0 ){
            YSpeed = -Constants.Knight.JumpForce;
        }
    }

    public void Attacking( float elapsed, bool isContinue ){
        if( _currentUnequippedState != UnequippedState.Total ){
            return;
        }
        if( _currentEquippedState == EquippedState.Shielding ){
            ChangeState("ShieldBashing");
        }
        if( _isAttacking == 0 && _currentEquippedState != EquippedState.ShieldBashing ){
            ChangeState("Attack1");
            _timePerFrame = (float) 0.65 / _maxFrame;
            _isAttacking = 1;
            return;
        }
        if( _currentFrame < _maxFrame - 1 || _totalElapsed + elapsed < _timePerFrame ){
            if( _isAttacking == 3 && _currentFrame == 5 )
                XSpeed = 15;
            if( _isAttacking == 4 )
                XSpeed = 2;
            if( TextureEffect == SpriteEffects.FlipHorizontally )
                XSpeed = -XSpeed;
            UpdateFrame(elapsed);
            return;
        }
        if( isContinue ){
            if( _currentEquippedState == EquippedState.ShieldBashing ){
                ChangeState("Idling");
                return;
            }
            if( _isAttacking == 4 )
                _isAttacking = 1;
            else _isAttacking += 1;

            ChangeState("Attack" + _isAttacking );
            switch( _isAttacking ){
                case 2: 
                    _timePerFrame = (float) 0.65 / _maxFrame;
                    break;
                case 3: 
                    _timePerFrame = (float) 0.75 / _maxFrame;
                    break;
                case 4:
                    _timePerFrame = (float) 0.65 / _maxFrame;
                    break;
            }
            //Console.WriteLine("Attack" + _isAttacking );
        } else {
            _isAttacking = 0;
        }
    }

    public void Dashing( float elapsed ){
        //ignore dashing input if not equipping any weapons
        if ( _currentEquippedState == EquippedState.Total )
            return;
        if( _dashCoolDown > 0 ){
            return;
        }
        if( _isDashing == false ){
            ChangeState( "Dashing" );
            _isDashing = true;
            _timePerFrame = 0.1f;
            return;
        }
        if( _currentFrame < _maxFrame - 1 || _totalElapsed + elapsed < _timePerFrame ){
            if( _currentFrame == 2 ){
                XSpeed = 5;
            }
            else if( _currentFrame == 3 ){
                XSpeed = 20;
                _timePerFrame = 0.15f;
            }
            if( TextureEffect == SpriteEffects.FlipHorizontally )
                XSpeed = -XSpeed;
            UpdateFrame(elapsed);
            return;
        } else {
            _isDashing = false;
            _dashCoolDown = 0.5f;
            ChangeState("Idling");
        }
    }

    public override bool CheckCollision( Rectangle entity ){
        bool collided = false;
        //This collision check will check the left side of the entity
        Rectangle position = CalibratePosition();
        if (position.X + position.Width > entity.X &&
            position.X < entity.X + entity.Width && 
            position.Y + position.Height > entity.Y &&
            position.Y < entity.Y + entity.Height
            )
        {
            collided = true;
        } 
        return collided;
    }

    public void ShieldUp( float elapsed, bool attack = false ){
        if( _currentEquippedState != EquippedState.Shielding && 
            _currentEquippedState != EquippedState.ShieldUp &&
            _currentEquippedState != EquippedState.ShieldBashing ){
            ChangeState("ShieldUp");
            _timePerFrame = 0.5f / _maxFrame;
            return;
        }
        if( _currentEquippedState == EquippedState.ShieldUp ){
            if( _currentFrame == _maxFrame - 1 && _totalElapsed + elapsed > _timePerFrame ){
                ChangeState("Shielding");
                return;
            }
        }
        UpdateFrame( elapsed );
    }

    public void Grabbing( float elapsed ){
        if( _currentUnequippedState == UnequippedState.GrappedIdling ){
            UpdateFrame( elapsed );   
            return;
        }
        ChangeState("GrappedIdling");
        _timePerFrame = 0.5f / _maxFrame;
    }

    public void LedgeClimbing( float elapsed ){
        //Console.WriteLine( _currentEquippedState + ", " + _currentFrame );
        if( _currentFrame == _maxFrame - 1 && _totalElapsed + elapsed > _timePerFrame ){
            ChangeState("Idling");
            return;
        }
        if( _currentUnequippedState == UnequippedState.LedgeGrabbing ){
            XSpeed = 0; YSpeed = 0;
            switch( _currentFrame ){
                case 3: 
                case 4:
                    YSpeed = -3;
                    break;
                case 5:
                    XSpeed = 4;
                    break;
            }
            if( TextureEffect == SpriteEffects.FlipHorizontally )
                XSpeed = -XSpeed;
            ChangePosition(XSpeed, YSpeed);
            UpdateFrame( elapsed );
            return;
        }
        ChangeState("LedgeGrabbing");
        _timePerFrame = 1f / _maxFrame;
    }

    public void Interact( MapObjects objects ){
        if( _interactCooldown > 0 ){
            return;
        }
        for( int i = 0; i < objects.Objects.Count; ++i ){
            Object temp = objects.Objects[i];
            if( temp.CheckCollision( CalibrateAttackHitbox(), false ) )
                if( temp.ObjectName == "ClosedChest" && _keys > 0 ){
                    temp.Interact( Constants.Knight.Action.Interact );
                    _keys -= 1;
                } else if( temp.ObjectName != "ClosedChest" ){
                    temp.Interact( Constants.Knight.Action.Interact );
                }
        }
        _interactCooldown = 0.5f;
    }   

    public void Control(float elapsed, MapObjects mapObjects){
        KeyboardState temp = Keyboard.GetState();
        if( _currentUnequippedState == UnequippedState.GrappedIdling ){
            if( temp.IsKeyDown( Keys.Up ) ){
                LedgeClimbing( elapsed );
            } else if( temp.IsKeyDown( Keys.Down ) ){
                ChangeState("Falling");
            }
            return;
        }
        if( _currentUnequippedState == UnequippedState.LedgeGrabbing ){
            LedgeClimbing( elapsed );
            return;
        }
        if( _isAttacking > 0 || _currentEquippedState == EquippedState.ShieldBashing ){
            Attacking( elapsed, temp.IsKeyDown(Keys.F) );
            return;
        }
        if( IsDeath() == true ){
            Dying( elapsed );
            return;
        }
        if( _isDashing == true ){
            Dashing( elapsed );
            return;
        } else if( _dashCoolDown > 0 ){
            _dashCoolDown -= elapsed;
        }
        if( _isDrinking == true ){
            Drinking( elapsed );
            return;
        }
        if( _isHurting == true ){
            Hurting( elapsed );
            return;
        }
        if( _isLanding ){
            Landing( elapsed );
            return;
        }
        if( _isPoweringUp ){
            PoweringUp( elapsed );
            return;
        } 
        if( _interactCooldown > 0 ){
            _interactCooldown -= elapsed;
        }
        PowerRemain( elapsed );
        if( temp.IsKeyDown(Keys.LeftControl) ){
            Crouching(elapsed);
            if( _isCrouching == false )
                return;
        } 
        if( temp.IsKeyDown(Keys.Left) )
            MoveLeft(elapsed);
        if( temp.IsKeyDown(Keys.Right) )
            MoveRight(elapsed);
        if( temp.IsKeyDown(Keys.Space) )
            Jumping( elapsed );
        if(  temp.IsKeyUp(Keys.Space) && _jumpTimer != JumpTime )
            _jumpTimer = 0;
        if( temp.IsKeyDown(Keys.A) )
            ChangeEquippedState(EquippedState.Crouching);
        if( temp.IsKeyDown(Keys.E) )
            Drinking( elapsed );
        if( temp.IsKeyDown(Keys.I) )
            Interact( mapObjects );
        if( temp.IsKeyDown(Keys.K) ){
            Hurting( elapsed );
            return;
        }
        if( temp.IsKeyDown( Keys.G ) ){
            PoweringUp( elapsed );
            return;
        }
        if( temp.IsKeyDown( Keys.Z ) ){
            ShieldUp( elapsed );
        }
        if( temp.IsKeyDown( Keys.Q ) ){
            SwapState( elapsed );
            Idling(elapsed);
        } else if(  temp.IsKeyUp( Keys.Q ) && _swapCooldown > 0 ){
            _swapCooldown = Constants.Knight.SwapCooldown;
        }
        if( temp.IsKeyUp( Keys.LeftControl ) ){
            if( _isCrouching == true ){
                if( TextureEffect != SpriteEffects.FlipHorizontally ){
                    ChangePosition( 5, 0);
                } else if( TextureEffect == SpriteEffects.FlipHorizontally ){
                    ChangePosition( -5, 0);
                }
                _isCrouching = false;
            }
        }
        if( temp.IsKeyUp( Keys.Left ) && temp.IsKeyUp( Keys.Right ) ){
            _isPushing = false;
            _pushingTime = 0;
        }
        if( temp.IsKeyDown( Keys.LeftShift ) ){
            Dashing( elapsed );
        }
        if( temp.IsKeyDown( Keys.F ) ){
            Attacking( elapsed, false );
        }
        if( temp.GetPressedKeyCount() == 0 )
            Idling(elapsed);
    }
}
