namespace GGMS.Common
{
    public class ValidationConstants
    {
        public static class GymValidationConstants
        {
            public const int NameMaxLength = 128;

            public const int NameMinLength = 2;

            public const int AddressMaxLength = 128;

            public const int AddressMinLength = 5;
        }

        public static class FitnessProgramValidationConstants
        {
            public const int TitleMaxLength = 20;

            public const int TitleMinLength = 5;

            public const int DescriptionMaxLength = 126;

            public const int DescriptionMinLength = 10;
        }

        public static class UserValidationConstants
        {
            public const int FirstNameMaxLength = 128;

            public const int FirstNameMinLength = 2;

            public const int LastNameMaxLength = 128;

            public const int LastNameMinLength = 2;

            public const int AgeMaxValue = 100;

            public const int AgeMinValue = 18;

            public const int AddressMaxLength = 128;

            public const int AddressMinLength = 5;

            public const int TelephoneNumberMaxLength = 10;

            public const int TelephoneNumberMinLength = 3;

            public const int MinPasswordLength = 4;

            public const int MaxPasswordLength = 35;

            public const int MinEmailLength = 10;

            public const int MaxEmailLength = 80;
        }

        public static class TrainerValidationConstants
        {
            public const int BioghraphyMaxLength = 256;

            public const int BioghraphyMinLength = 20;
        }

        public static class RequestToTrainer
        {
            public const int DescriptionMaxLength = 256;

            public const int DescriptionMinLength = 10;

     
        }
    }
}
