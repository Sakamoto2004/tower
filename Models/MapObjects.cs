using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

using Models.Model;

public class MapObjects{
    public List<Entity> Entities{ get; set; }
    public List<Models.Model.Object> Objects{ get; set; }

    public MapObjects(){
        Entities = new List<Entity>();
        Objects = new List<Models.Model.Object>();
    }

    public void AddEntity(Entity entity){
        Entities.Add(entity);
    }

    public void AddObject(Models.Model.Object temp){
        Objects.Add( temp );
    }

    public void LoadEntities( Texture2D defaultTexture ){
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
        foreach( Models.Model.Object temp in Objects ){
            temp.Draw( spriteBatch );            
        }
    }


}
