using Microsoft.Xna.Framework;
namespace Helper;

public static class Constants
{
    public static class BlueWitch{
        private static Rectangle _blueWitchIdleSource = new Rectangle(){Height = 48, Width = 111};
        private static int _blueWitchIdleFrames = 6;
        private static float _framePerSecond = 2;
        private static string _idleTextureName = "bluewitchidle";

        public static Rectangle BlueWitchIdleSource => _blueWitchIdleSource;
        public static int BlueWitchIdleFrames => _blueWitchIdleFrames;
        public static float FramePerSecond => _framePerSecond;
        public static string IdleTextureName => _idleTextureName;
    }

}
