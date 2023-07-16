namespace RentACar.Common
{
    public static class EntityValidationConstants
    {
        public static class Agent
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 50;
        }

        public static class Car
        {
            public const int ModelMinLength = 5;
            public const int ModelMaxLength = 50;

            public const int DescriptionMaxLength = 500;
            public const int DescriptionMinLength = 50;

            public const string PricePerDayMinValue = "0";
            public const string PricePerDayMaxValue = "300";

            public const int ImageUrlMaxLength = 2048;
        }

        public static class Category
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 50;
        }

        public static class Location
        {
            public const int AddressMinLength = 5;
            public const int AddressMaxLength = 100;
        }

        public static class Order
        {
            public const int DaysRentedMinValue = 1;
        }

        public static class Review
        {
            public const int CommentMinLength = 10;
            public const int CommentMaxLength = 500;
        }
    }
}
