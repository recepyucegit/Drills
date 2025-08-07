using FitneesApp_AbstractFactory.Abstract;

namespace FitneesApp_AbstractFactory.ConcreteExercises
{
    public class RowConcrete:IRow
    {
        public void PerformRow()
        {
            Console.WriteLine("4x8 RPE 7-8 Row yapılıyor...");
        }
        public void RestRow()
        {
            Console.WriteLine("3-5 dakika set arası dinlenildi...");
        }
    }
}
