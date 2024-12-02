using Microsoft.Xna.Framework;
namespace Helper;

public static class Constants
{
    public static class Witch{
        public enum State{
            Idle,
            Run,
            Charge,
            Attack,
            Damaged, 
            Death,
            Total
        }

        public static class Blue{
            private static int[] _textureFrames = new int[(int)State.Total]{6, 8, 5, 9, 3, 12};
            private static Rectangle _rectSource = new Rectangle(){Height = 48, Width = 111};
            private static string[] _textureName = new string[(int) State.Total]{
                "bluewitchidle",
                "bluewitchrun",
                "bluewitchcharge",
                "bluewitchattack",
                "bluewitchtakedamage",
                "bluewitchdeath"
            };

            public static int[] TextureFrames{ get => _textureFrames; }
            public static Rectangle RectSource{ get => _rectSource; }
            public static string[] TextureName{ get => _textureName; }
        }
    }

    public static class Knight{

        private static Rectangle _sourceSize = new Rectangle(){
            Height = 64,
            Width = 100
        };

        private static int _walkingSpeed = 5;
        private static int _crouchingSpeed = 2;
        private static float _fallingAcceleration = 3f;
        private static float _jumpTime = 0.2f;
        private static int _jumpForce = 10;
        private static int _sourceTextureOffset = 31;
        private static float _swapCooldown = 1f;
        private static float _powerUpTime = 5f;

        public static float PowerUpTime{
            get => _powerUpTime;
        }

        public static float SwapCooldown{
            get => _swapCooldown;
        }

        public static int SourceTextureOffset{
            get => _sourceTextureOffset;
        }
        public static int JumpForce{
            get => _jumpForce;
        }

        public static float JumpTime{
            get => _jumpTime;
        }

        public static float FallingAcceleration{
            get => _fallingAcceleration;
        }

        public static int WalkingSpeed{
            get => _walkingSpeed;
        }

        public static int CrouchingSpeed{
            get => _crouchingSpeed;
        }

        public static int[] UnequippedTextureFrames{
            get => _unequippedTextureFrames;
        }

        public static int[] EquippedTextureFrames{
            get => _equippedTextureFrames;
        }

        public static string[] UnequippedTextureName{
            get => _unequippedTextureName;
        }

        public static string[] EquippedTextureName{
            get => _equippedTextureName;
        }

        public static Rectangle SourceSize{
            get => _sourceSize;
        }

        public enum UnequippedState{
            CrouchingIdling,
            Crouching,
            CrouchingWalk,
            Drinking,
            Dying,
            Falling,
            GrappedIdling,
            Hurting,
            Idling,
            Jumpping,
            Emote,
            Climbing,
            Landing,
            LedgeGrabbing,
            Pushing,
            Talking,
            Walking,
            Total
        }
        
        private static int[] _unequippedTextureFrames = new int[(int) UnequippedState.Total]{
            3,
            3,
            4,
            7,
            5,
            3,
            3,
            3,
            4,
            6,
            5,
            4,
            4,
            6,
            5,
            4,
            7,
        };

        private static string[] _unequippedTextureName = new string[(int) UnequippedState.Total]{
            "Knight/Crouching_Idle_KG_1",
            "Knight/Crouching_KG_1",
            "Knight/Crouching_Walk_KG_1",
            "Knight/Drinking_KG_1",
            "Knight/Dying_KG_1",
            "Knight/Fall_KG_1",
            "Knight/Grab_idle_KG_1",
            "Knight/Hurt_KG_1",
            "Knight/Idle_KG_1",
            "Knight/Jump_KG_1",
            "Knight/knight_win",
            "Knight/ladders_KG_1",
            "Knight/Landing_KG_1",
            "Knight/Ledge_Grab_KG_1",
            "Knight/Pushing_KG_1",
            "Knight/Talking_KG",
            "Knight/Walking_KG_1"
        };

        public enum EquippedState{
            Attack1,
            Attack2,
            Attack3,
            Attack4,
            CrouchingIdle,
            Crouching,
            CrouchingWalk,
            Dashing,
            Dying,
            Falling,
            Hurting,
            Idling,
            Jumpping,
            Landing,
            PoweringUp,
            Shielding,
            ShieldBashing,
            ShieldUp,
            Walking,
            Total
        }

        private static int[] _equippedTextureFrames = new int[(int) EquippedState.Total]{
            6,
            6,
            9,
            5,
            3,
            4,
            4,
            4,
            6,
            3,
            4,
            4,
            6,
            4,
            10,
            4,
            5,
            7,
            7
        };
        private static string[] _equippedTextureName = new string[(int) EquippedState.Total]{
            "Knight/Attack_KG_1",
            "Knight/Attack_KG_2",
            "Knight/Attack_KG_3",
            "Knight/Attack_KG_4",
            "Knight/Crouching_Idle_KG_2",
            "Knight/Crouching_KG_2",
            "Knight/Crouching_Walk_KG_2",
            "Knight/Dashing_KG_1",
            "Knight/Dying_KG_2",
            "Knight/Fall_KG_2",
            "Knight/Hurt_KG_2",
            "Knight/Idle_KG_2",
            "Knight/Jump_KG_2",
            "Knight/Landing_KG_2",
            "Knight/Power_Up_KG_1",
            "Knight/Shield_idle_KG",
            "Knight/Shield_Bash_KG",
            "Knight/Shield_Up_KG_1",
            "Knight/Walking_KG_2"
        };
    }

    public static class Dungeon{
        private static int TextureWidth = 16;
        private static int TextureHeight = 16;

        public enum Name{
            UpperLeftStoneWall,
            UpperMiddleStoneWall,
            UpperRightStoneWall,
            RightStairWay,
            LeftStairWay,
            Stone1,
            Stone2,
            UpperLeftWindow,
            UpperRightWindow,
            Stone3,
            Stone4,

            LeftStoneWall,
            MiddleStoneWall,
            RightStoneWall,
            UpperLeftStairInside,
            UpperRigthStairInside,
            Stone5,
            Stone6, 
            LowerLeftWindow,
            LowerRightWindow,
            Stone7, 
            Stone8,

            LowerLeftStoneWall,
            LowerMiddelStoneWall,
            LowerRightStoneWall,
            LowerLeftStairInside,
            LowerRightStairInside,
            Stone9,
            Stone10,
            UpperLeftSewer,
            UpperRigthSewer,
            Stone11,
            Stone12,
            
            Stone13,
            Stone14,
            LowerLeftSewer,
            LowerRightSewer,
            Stone15,

            UpperLeftSmoothStone,
            UpperMiddleSmoothStone,
            UpperRightSmoothStone,
            RightSlopeWall,
            LeftSlopeWall,
            Stone16,
            Stone17,
            BrickWall,
            Stone18, 
            Stone19,

            LeftSmoothStone,
            InnerSmoothStone,
            RightSmoothStone,
            Stone20,
            Stone21,
            Stone22, 
            Stone23, 
            Stone24, 
            Stone25, 
            Stone26, 
            
            LowerLeftSmoothStone,
            LowersmoothStone,
            LowerRightSmoothStone,
            Stone27,
            Stone28,
            Stone29,
            Stone30,
            Stone31,
            Stone32,
            Stone33,

            LeftBridge1, 
            Middlebridge1, 
            RightBridge1,
            TopLadder,
            MiddleTable,
            RightTable,
            LeftTable,
            BrownBrick,
            Key,
            ClosedChest,
            OpenedChest,

            Total,
        }
        
        public static int[] SourceLocationX = new int[(int)Name.Total]{
            1, 2, 3,    5, 6,    8, 9, 10, 11, 12, 13,
            1, 2, 3,    5, 6,    8, 9, 10, 11, 12, 13,
            1, 2, 3,    5, 6,    8, 9, 10, 11, 12, 13,
                                 8, 9, 10, 11, 12,
            1, 2, 3,    5, 6,    8, 9, 10, 11, 12,
            1, 2, 3,       6, 7, 8, 9, 10, 11, 12,
            1, 2, 3,       6, 7, 8, 9, 10, 11, 12,
            1, 2, 3,    5,    7, 8, 9, 10, 11, 12, 13, 

        };
        public static int[] SourceLocationY = new int[(int)Name.Total]{
            1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
            2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2,
            3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3,
            4, 4, 4, 4, 4,
            5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 
            6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 
            7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 
            9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9,
        };


    }
}
