namespace OnlyBot_Models.Enums
{
    public static class EnumHelper
    {
        public static ServerEnum GetServerEnumFromString(string input)
        {
            if (Enum.TryParse<ServerEnum>(input, out var result))
            {
                return result;
            }
            else
            {
                throw new ArgumentException("La chaîne ne correspond à aucune valeur de ServerEnum.", nameof(input));
            }
        }

        public static BreedEnum GetBreedEnumFromString(string input)
        {
            if (Enum.TryParse<BreedEnum>(input, out var result))
            {
                return result;
            }
            else
            {
                throw new ArgumentException("La chaîne ne correspond à aucune valeur de BreedEnum.", nameof(input));
            }
        }

        public static InventoryTypeEnum GetInventoryTypeEnumFromString(string input)
        {
            if (Enum.TryParse<InventoryTypeEnum>(input, out var result))
            {
                return result;
            }
            else
            {
                throw new ArgumentException("La chaîne ne correspond à aucune valeur de InventoryTypeEnum.", nameof(input));
            }
        }
    }
}
