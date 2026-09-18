using ProcedurallyGeneratedAnimals;
using ProcedurallyGeneratedAnimals.Descriptors;
using System;

namespace AnimalCreator.Persistence;

public sealed class AnimalCreatorData
{
	private const int LENGTH = 10;

	private readonly AnimalDescriptor[] animals;
	private Animal animal;
	private int fPS;
	private int waitTime;

	public AnimalDescriptor[] Animals => animals;
	public Animal Animal => animal;
	public int FPS
	{
		get => fPS;
		set
		{
			fPS = value;
			waitTime = 1000 / fPS;
		}
	}
	public int WaitTime => waitTime;

	public AnimalCreatorData(int windowWidth, int windowHeight)
	{
		AnimalDescriptor snake = new(
			[
				new AngledSegmentDescriptor(0, 26, 22, [new EyeDescriptor(115, 22, 10, new Color(0, 0, 0))]),
				new SegmentDescriptor(26, 29),
				new SegmentDescriptor(29, 23),
				new SegmentDescriptor(22, 22),
				new SegmentDescriptor(22, 22),
				new SegmentDescriptor(22, 22),
				new SegmentDescriptor(22, 22),
				new SegmentDescriptor(22, 21),
				new SegmentDescriptor(22, 21),
				new SegmentDescriptor(22, 21),
				new SegmentDescriptor(22, 21),
				new SegmentDescriptor(22, 20),
				new SegmentDescriptor(22, 20),
				new SegmentDescriptor(22, 20),
				new SegmentDescriptor(22, 20),
				new SegmentDescriptor(22, 19),
				new SegmentDescriptor(22, 19),
				new SegmentDescriptor(22, 19),
				new SegmentDescriptor(22, 19),
				new SegmentDescriptor(22, 18),
				new SegmentDescriptor(22, 18),
				new SegmentDescriptor(22, 18),
				new SegmentDescriptor(22, 18),
				new SegmentDescriptor(22, 17),
				new SegmentDescriptor(22, 17),
				new SegmentDescriptor(22, 17),
				new SegmentDescriptor(22, 17),
				new SegmentDescriptor(22, 16),
				new SegmentDescriptor(22, 16),
				new SegmentDescriptor(22, 16),
				new SegmentDescriptor(22, 16),
				new SegmentDescriptor(22, 15),
				new SegmentDescriptor(22, 15),
				new SegmentDescriptor(22, 15),
				new SegmentDescriptor(22, 15),
				new SegmentDescriptor(22, 14),
				new SegmentDescriptor(22, 14),
				new SegmentDescriptor(22, 14),
				new SegmentDescriptor(22, 13),
				new SegmentDescriptor(22, 13),
				new SegmentDescriptor(22, 13),
				new SegmentDescriptor(22, 12),
				new SegmentDescriptor(22, 12),
				new SegmentDescriptor(22, 12),
				new SegmentDescriptor(22, 11),
				new SegmentDescriptor(22, 11),
				new SegmentDescriptor(22, 11),
				new SegmentDescriptor(22, 10),
				new SegmentDescriptor(22, 10),
				new SegmentDescriptor(22, 10),
				new SegmentDescriptor(22, 9),
				new SegmentDescriptor(22, 9),
				new SegmentDescriptor(22, 9),
				new SegmentDescriptor(22, 8),
				new SegmentDescriptor(22, 8),
				new SegmentDescriptor(22, 7),
				new SegmentDescriptor(22, 7),
				new SegmentDescriptor(22, 6),
				new SegmentDescriptor(22, 5),
				new SegmentDescriptor(22, 4)
			],
			new Color(190, 0, 0),
			6
		);

		AnimalDescriptor lizard = new(
			[
				new AngledSegmentDescriptor(0, 26, 14, [new EyeDescriptor(115, 22, 10, new Color(0, 0, 0))]),
				new SegmentDescriptor(26, 29),
				new SegmentDescriptor(29, 20),
				new SegmentDescriptor(22, 30,
					[new LegDescriptor(
						[
							new LegSegmentDescriptor(25, 10, 0, 0),
							new LegSegmentDescriptor(18, 8, 0, 145),
							new LegSegmentDescriptor(15, 6, 0, 0,
								[
									new AntennaDescriptor(
										[
											new AntennaSegmentDescriptor(5, 3, 0),
											new AntennaSegmentDescriptor(7, 2, 0)
										],
										130,
										new Color(0, 190, 0),
										Render.Bottom
									),
									new AntennaDescriptor(
										[
											new AntennaSegmentDescriptor(5, 3, 0),
											new AntennaSegmentDescriptor(7, 2, 0),
										],
										180,
										new Color(0, 190, 0),
										Render.Bottom
									)
								]
							)
						],
						new Point<int>(22, 30),
						new Color(0, 190, 0)
					)]
				),
				new SegmentDescriptor(33, 34),
				new SegmentDescriptor(27, 36),
				new SegmentDescriptor(32, 32),
				new SegmentDescriptor(25, 25,
					[new LegDescriptor(
						[
							new LegSegmentDescriptor(28, 13, 0, 0),
							new LegSegmentDescriptor(21, 8, -145, -5),
							new LegSegmentDescriptor(18, 6, 0, 0,
								[
									new AntennaDescriptor(
										[
											new AntennaSegmentDescriptor(5, 3, 0),
											new AntennaSegmentDescriptor(10, 2, 0)
										],
										140,
										new Color(0, 190, 0),
										Render.Bottom
									),
									new AntennaDescriptor(
										[
											new AntennaSegmentDescriptor(5, 3, 0),
											new AntennaSegmentDescriptor(9, 2, 0)
										],
										180,
										new Color(0, 190, 0),
										Render.Bottom
									)
								]
							)
						],
						new Point<int>(18, 0),
						new Color(0, 190, 0)
					)]
				),
				new SegmentDescriptor(30, 14),
				new SegmentDescriptor(25, 8),
				new SegmentDescriptor(25, 6),
				new SegmentDescriptor(25, 5),
				new SegmentDescriptor(13, 4),
				new SegmentDescriptor(13, 3),
				new SegmentDescriptor(12, 3),
				new SegmentDescriptor(6, 2)
			],
			new Color(0, 190, 0),
			3
		);

		AnimalDescriptor fish = new(
			[
				new AngledSegmentDescriptor(0, 18, 20, [new EyeDescriptor(100, 16, 20, new Color(0, 0, 100), Render.Bottom)]),
				new SegmentDescriptor(22, 30),
				new SegmentDescriptor(33, 34,
					[
						new SideFinDescriptor(40, 12, 20, new Color(0, 0, 140)),
						new BackFinDescriptor(3, new Color(0, 0, 140))
					]
				),
				new SegmentDescriptor(27, 36),
				new SegmentDescriptor(32, 32),
				new SegmentDescriptor(25, 25),
				new SegmentDescriptor(30, 14),
				new SegmentDescriptor(20, 8),
				new SegmentDescriptor(15, 5),
				new SegmentDescriptor(10, 2, [new TailFinDescriptor([10, 10, 10, 10, 10], new Color(0, 0, 140))])
			],
			new Color(20, 130, 255),
			8
		);

		animals = new AnimalDescriptor[LENGTH];

		Array.Fill(animals, snake);
		animals[1] = fish;
		animals[2] = lizard;

		animal = snake.Create(new Point<int>(windowWidth, windowHeight));
	}

	private int? FirstEmptyIndex()
	{
		int index = 0;

		while (index < LENGTH && animals[index] is not null)
			++index;

		return index < LENGTH ? index : null;
	}

	public bool AddAnimal(AnimalDescriptor animal)
	{
		int? index = FirstEmptyIndex();

		if (index is int i)
		{
			animals[i] = animal;

			return true;
		}

		return false;
	}

	public void AddAnimal(AnimalDescriptor animal, uint index) => animals[index] = animal;

	public void SelectIndex(uint index, int windowWidth, int windowHeight)
	{
		if (index < LENGTH)
			animal = animals[index].Create(new Point<int>(windowWidth, windowHeight));
	}
}