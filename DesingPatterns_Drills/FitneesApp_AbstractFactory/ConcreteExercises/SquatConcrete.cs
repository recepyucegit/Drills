using FitneesApp_AbstractFactory.Abstract;

namespace FitneesApp_AbstractFactory.ConcreteExercises
{
    public class SquatConcrete:ISquat
    {
        public void PerformSquat()
        {
            Console.WriteLine("4x6 RPE 7-8 Squat yapıldı...");
        }

        public void RestSquat()
        {
            Console.WriteLine("3-5 Dakika dinlenildi...");
        }
    }
}
