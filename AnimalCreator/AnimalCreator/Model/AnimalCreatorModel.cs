using AnimalCreator.Persistence;
using ProcedurallyGeneratedAnimals;
using ProcedurallyGeneratedAnimals.ShapeDataTypes;

namespace AnimalCreator.Model;

public sealed class AnimalCreatorModel
{
	private readonly AnimalCreatorData data;

	public AnimalCreatorData Data => data;

	public AnimalCreatorModel(int windowWidth, int windowHeight, int fPS) => data = new AnimalCreatorData(windowWidth, windowHeight, fPS);

	public ShapeData[] Draw(double x, double y)
	{
		Point<double> mouse = new(x, y);

		data.Animal.Step(mouse);

		return data.Animal.Draw();
	}
}