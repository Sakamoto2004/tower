using static Helper.Constants;

namespace Helper;
public class CollisionChecker{
    public bool Left{ get; set; }
    public bool Right{ get; set; }
    public bool Up{ get; set; }
    public bool Down{ get; set; }

    public bool CollisionCheck(){
        if( Left || Right || Down || Up )
            return true;
        return false;
    }

    public void Reset(){
        Left = false;
        Right = false;
        Up = false;
        Down = false;
    }
    
}
