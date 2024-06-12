using ClassLibraryContainerShip.Exceptions;

namespace ClassLibraryContainerShip
{
    public class Container
    {
        public int Weight { get; private set; } 
        private readonly int MaxWeight = 30;
        public int MinWeight { get; private set; } = 4;

        public ContainerTypes ContainerType { get; private set; }
        public UnfitReasons UnfitReason { get; set; }

        public Container(int weight, int type) 
        {
            Weight = SetWeight(weight);
            ContainerType = (ContainerTypes)type;
        }

        public enum ContainerTypes
        {
            Normal = 1,
            Valuable = 2,
            Coolable = 3,
            CoolableValuable = 4
        }

        public enum UnfitReasons
        {
            ExceedsMaxWeight = 1,
            Reserved = 2,
            TooManyValuables = 3,
            Other = 4,
            TooManyCoolables = 5
        }

        private int SetWeight(int weight)
        {
            if (weight < MinWeight)
            {
                throw new ContainerException("Weight mininum is 4 tons");
            }
            else if (weight > MaxWeight)
            {
                throw new ContainerException("Weight maximun is 30 tons");
            }

            return weight;
        }
    }
}
