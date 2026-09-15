using AnimalCreator.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace AnimalCreator.Model;

public sealed class AnimalCreatorModel
{
	private readonly AnimalCreatorData data;
	
	public AnimalCreatorData Data => data;

	public AnimalCreatorModel()
	{
		data = new AnimalCreatorData();
	}

	public void DrawLoop(CancellationTokenSource tokenSource)
	{
		Task.Run(() =>
		{
			while (!tokenSource.IsCancellationRequested)
			{
				data.Animal.Draw();

				Thread.Sleep(data.WaitTime);
			}
		}, tokenSource.Token);
	}
}