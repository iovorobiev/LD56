namespace Utils
{
    public class LayersHelper
    {
        public static int GetAllLayersExcept(int layer)
        {
            return 0xFFFFFF ^ (1 << layer);
        }
    }
}