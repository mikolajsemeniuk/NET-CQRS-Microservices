namespace Events
{
    public record UpdateArticleEvent(int id, string title, string description);
}