namespace CodeFirst.Constants
{
    public static class BlogsControllerRoute
    {
        public const string DeleteBlog = ControllerName.Blog + nameof(DeleteBlog);
        public const string GetBlog = ControllerName.Blog + nameof(GetBlog);
        public const string GetBlogPage = ControllerName.Blog + nameof(GetBlogPage);
        public const string HeadBlog = ControllerName.Blog + nameof(HeadBlog);
        public const string HeadBlogPage = ControllerName.Blog + nameof(HeadBlogPage);
        public const string PatchBlog = ControllerName.Blog + nameof(PatchBlog);
        public const string PostBlog = ControllerName.Blog + nameof(PostBlog);
        public const string PutBlog = ControllerName.Blog + nameof(PutBlog);
    }
}