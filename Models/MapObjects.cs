using Microsoft.Xna.Framework.Graphics;
using Models.Model;

public class MapObjects{
    public List<Entity> Entities{ get; set; }

    public MapObjects(){
        Entities = new List<Entity>();
    }

    public void AddObject(Entity entity){
        Entities.Add(entity);
    }

}
