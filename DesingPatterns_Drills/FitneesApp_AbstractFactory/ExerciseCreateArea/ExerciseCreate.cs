using FitneesApp_AbstractFactory.Abstract;

namespace FitneesApp_AbstractFactory.ExerciseCreateArea
{
    public class ExerciseCreate//Abstract Factory
    { 
        private IExerciseCreate _exerciseFactory;
        public ExerciseCreate(IExerciseCreate exerciseFactory)
        {
            _exerciseFactory = exerciseFactory;
        }

        public void FullBodyCreateExercise()
        {
            IDeadlift deadlift = _exerciseFactory.CreateDeadlift();
            IBenchPress benchPress = _exerciseFactory.CreateBenchPress();
            ISquat squat = _exerciseFactory.CreateSquat();
            IRow row = _exerciseFactory.CreateRow();

            Console.WriteLine("İdman Başlıyor");
            deadlift.PerformDeadlift();
            deadlift.RestDeadlift();
            benchPress.PerformBenchPress();
            benchPress.RestBenchPress();
            squat.PerformSquat();
            squat.RestSquat();
            row.PerformRow();
            row.RestRow();
            Console.WriteLine("İdman Bitti");




        }

        public void UpperBodyCreateExercise()
        {
            IBenchPress benchPress = _exerciseFactory.CreateBenchPress();
            IRow row = _exerciseFactory.CreateRow();
            Console.WriteLine("Üst Vücut İdmanı Başlıyor");
            benchPress.PerformBenchPress();
            benchPress.RestBenchPress();
            row.PerformRow();
            row.RestRow();
            Console.WriteLine("Üst Vücut İdmanı Bitti");
        }




    }
}
