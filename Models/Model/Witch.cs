#nullable disable
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Helper;

namespace Models.Model;

public class Witch : Entity{
    public Witch()
        :base(){
            _timePerFrame = (float) 1 / _numberOfFrame;
            _totalElapsed = 0;
            Rotation = 0;
            Paused = false;
            Scale = 2;
            Depth = 1;
        }

    public void Load(ContentManager content){
        base.LoadSource(Constants.BlueWitch.BlueWitchIdleSource, Constants.BlueWitch.BlueWitchIdleFrames);
        Texture = content.Load<Texture2D>(Constants.BlueWitch.IdleTextureName);
    }
}
