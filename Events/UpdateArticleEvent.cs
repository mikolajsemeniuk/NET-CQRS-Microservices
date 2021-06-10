using System;

namespace Events
{
    public record UpdateArticleEvent(Guid id, string title, string description);
}