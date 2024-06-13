using CopperDevs.Core.Data;

namespace CustomImage;

public class ImageData
{
    public List<Pixel> Pixels = [];

    public class Pixel
    {
        public Vector2Int Position;
        public Color Color;
    }

    public Vector2Int GetImageSize()
    {
        if (Pixels.Count == 0)
        {
            return new Vector2Int(0, 0);
        }

        var maxX = int.MinValue;
        var maxY = int.MinValue;

        foreach (var pixel in Pixels)
        {
            if (pixel.Position.X > maxX)
                maxX = pixel.Position.X;

            if (pixel.Position.Y > maxY)
                maxY = pixel.Position.Y;
        }

        return new Vector2Int(maxX + 1, maxY + 1);
    }

    public void SaveToFile(string path)
    {
        var lines = Pixels.Select(pixel => $"{pixel.Position.X} {pixel.Position.Y} {pixel.Color.ToInt()}").ToList();
        File.WriteAllLines(path, lines);
    }

    public static ImageData LoadFromFile(string? path)
    {
        if (string.IsNullOrEmpty(path))
        {
            return new ImageData()
            {
                Pixels =
                [
                    new Pixel
                    {
                        Position = new Vector2Int(1, 1),
                        Color = Color.Red
                    },
                    new Pixel
                    {
                        Position = new Vector2Int(1, 2),
                        Color = Color.Green
                    },
                    new Pixel
                    {
                        Position = new Vector2Int(2, 1),
                        Color = Color.Blue
                    },
                    new Pixel
                    {
                        Position = new Vector2Int(2, 2),
                        Color = Color.Purple
                    },
                ]
            };
        }

        var lines = File.ReadAllLines(path);

        var imageData = new ImageData();

        foreach (var line in lines)
        {
            var elements = line.Split(" ");

            var posX = int.Parse(elements[0]);
            var posY = int.Parse(elements[1]);
            var color = Color.FromInt(int.Parse(elements[2]));

            imageData.Pixels.Add(new Pixel { Position = new Vector2Int(posX, posY), Color = color });
        }

        return imageData;
    }
}