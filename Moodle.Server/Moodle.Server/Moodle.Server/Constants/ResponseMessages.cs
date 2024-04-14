namespace Moodle.Server.Constants
{
    public static class ResponseMessages
    {
        //Course messages
        public const string CourseWithIdNotFound = "Course not found with the given id.";
        public const string CourseNotFound = "Course not found.";
        public const string UsersSuccesfullyListed = "Users successfully retrieved for the Course.";
        public const string EmptyCourseList = "No courses found.";

        //User messages
        public const string NoCourseFoundForUser = "This user has no courses yet.";
        public const string SubscriptionToCourseRejected = "The user is unable to subcribe to the course with this degree.";
        public const string AlreadySubcribed = "The user is already subscribed to this crouse.";
        public const string CourseAddedToUser = "The course has been successfully added to the user.";
        public const string UserNotFound = "User not found.";

        //Degree messages
        public const string NoCourseFoundForDegree = "No courses have been added to this degree yet.";
        public const string DegreeNotFound = "Degree not found.";

        //Auth messages
        public const string LoginInformationError = "The username or password is not correct.";
        public const string LoginSuccess = "User successfully logged in.";
        public const string UserAlreadyExists = "This username is already taken.";
        public const string UserRegistered = "User successfully registered.";

        //Event messages
        public const string EventNotFound = "Event not found.";

        //Other messages

    }
}
