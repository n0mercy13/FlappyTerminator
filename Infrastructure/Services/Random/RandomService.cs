using UnityEngine;

namespace Codebase.Infrastructure
{
    public partial class RandomService
    {
        private readonly int _seed;
        private readonly System.Random _random;

        public RandomService()
        {
            _random = new System.Random(_seed);
        }
    }

    public partial class RandomService : IRandomService
    {
        public Vector2 GetRandomPositionInArea(Vector2 point1,  Vector2 point2)
        {
            float randomX = RandomInRange(point1.x, point2.x);
            float randomY = RandomInRange(point1.y, point2.y);

            return new Vector2(randomX, randomY);
        }

        private float RandomInRange(float number1, float number2)
        {
            if(number1 < number2)
            {
                return number1 + (number2 - number1) * (float)_random.NextDouble();
            }
            else
            {
                return number2 + (number1 - number2) * (float)_random.NextDouble();
            }
        }
    }
}
