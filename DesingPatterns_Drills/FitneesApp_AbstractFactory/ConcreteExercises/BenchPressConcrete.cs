using FitneesApp_AbstractFactory.Abstract;

namespace FitneesApp_AbstractFactory.ConcreteExercises
{
    public class BenchPressConcrete : IBenchPress
    {
        public void PerformBenchPress()
        {
            Console.WriteLine("4x6 RPE 7-8 Flat Benchpress yapıldı...");
        }

        public void RestBenchPress()
        {
            Console.WriteLine("3-5 dakika dinlenildi");
        }
    }
}
