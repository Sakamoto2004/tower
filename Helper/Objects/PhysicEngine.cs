
namespace Helper;
public static class PhysicEngine{
    public static float time;

    public static int SpeedCalculator(float acceleration, int speed, float elapsed){
        time += elapsed;
        float speedChange = time * acceleration;
        if( (int) speedChange == 0 ) 
            speedChange = 1;
        return (int) (speed + speedChange);
    }

    public static void ResetTimer(){
        time = 0;
    }
}
