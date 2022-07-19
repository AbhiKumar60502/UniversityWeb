namespace Shm.Servicing.Handlers
{
    public class CommonExceptionStrings
    {
        public const string CannotBeLessThanZero = "{0} cannot be less than or equal to zero";
        public const string ProblemUpdating = "Problem updating {0} with ID {1}";
        public const string CouldNotFindWorkOrderWithId = "Could not find a work order with Id {0} for marina {1}";
        public const string NotFound = "Could not find a {0} with Id {1}";
        public const string CouldNotCreate = "Inserting {0} did not return the inserted primary key";
    }
}
