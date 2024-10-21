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

        private static Rectangle _sourceSize = new Rectangle(){
            Height = 64,
            Width = 100
        };

        private static int _walkingSpeed = 5;
        private static int _crouchingSpeed = 2;
        private static float _fallingAcceleration = 3f;

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

    }

}
