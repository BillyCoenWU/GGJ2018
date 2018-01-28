namespace GGJ
{
    using UnityEngine;
    using System.Collections.Generic;

    public class Constantes
    {
        public const string DIURNO = "Diurnos";
        public const string NOTURNO = "Noturnos";
        public const string ATLAS_PATH = "Atlas/Hexagonos";
        
        public static readonly Vector2 VECTOR_TWO_ONE = Vector2.one;
        public static readonly Vector2 VECTOR_TWO_ZERO = Vector2.zero;

        public static readonly Vector3 FOWARD = Vector3.forward;
        public static readonly Vector3 VECTOR_THREE_ONE = Vector3.one;
        public static readonly Vector3 VECTOR_THREE_ZERO = Vector3.zero;

        public static readonly Quaternion IDENTITY = Quaternion.identity;

        public static readonly Dictionary<Food.TYPE, string> FOODS = new Dictionary<Food.TYPE, string>()
        {
            { Food.TYPE.FRUIT_ONE,    "Banana"  },
            { Food.TYPE.FRUIT_TWO,   "Mango" },
            { Food.TYPE.FRUIT_THREE,   "Papaya" },
            { Food.TYPE.MOTH,           "Moth" }
        };

        public static readonly Dictionary<Bird.TYPE, string> ANIMALS = new Dictionary<Bird.TYPE, string>()
        {
            { Bird.TYPE.OWL,    "Owl"  },
            { Bird.TYPE.HAWK,   "Hawk" }
        };

        public static readonly Dictionary<HexaTile.TYPE, string> PATHS = new Dictionary<HexaTile.TYPE, string>()
        {
            { HexaTile.TYPE.GRASS,  "Grass" },
            { HexaTile.TYPE.RIVER,  "River" },
            { HexaTile.TYPE.GROUND, "Ground" },

            { HexaTile.TYPE.ROCK,   "Rock" },
            { HexaTile.TYPE.TREE,   "Tree" },

            { HexaTile.TYPE.ANIMAL, "Animal" },
            { HexaTile.TYPE.FOOD,   "Food" },
        };
    }
}
