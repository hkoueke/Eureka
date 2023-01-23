using ErrorOr;

namespace EurekaBot.Domain.Errors;

public partial class Errors
{
    public static class Post
    {
        public static readonly Error NotFound = Error.NotFound(
            "Post.NotFound", 
            "Post was not found.");
    }
}
