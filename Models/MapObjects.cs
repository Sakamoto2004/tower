using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

using Models.Model;

public class MapObjects{
    public List<Entity> Entities{ get; set; }

    public MapObjects(){
        Entities = new List<Entity>();
    }

    public void AddObject(Entity entity){
        Entities.Add(entity);
    }

    public void Load( Texture2D defaultTexture ){
        for( int i = 0; i < Entities.Count; ++i ){
            if( Entities[i].DefaultTexture == null ){
                Entities[i].DefaultTexture = defaultTexture;
            }
        }
    }

    public void Draw( SpriteBatch spriteBatch ){
        foreach( Entity entity in Entities ){
            if( entity.DefaultTexture == null )
                continue;
            spriteBatch.Draw(entity.DefaultTexture, entity.Position, Color.White);
        }
    }

}
