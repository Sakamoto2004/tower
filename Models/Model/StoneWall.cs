using static Helper.Constants.Dungeon;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace Models.Model;
public class StoneWall : Object {
    public StoneWall()
        : base(){
        IsPassable = true;
    }
    
    public void Load(ContentManager content, int textureNo = 0 ){
    }

}

