using FitneesApp_AbstractFactory.ExerciseCreateArea;
using FitneesApp_AbstractFactory.ConcretesFactorty;
using FitneesApp_AbstractFactory.Abstract;

IExerciseCreate exerciseFactory = new ExerciseFactory();

var exerciseCreate = new ExerciseCreate(exerciseFactory);

exerciseCreate.FullBodyCreateExercise();