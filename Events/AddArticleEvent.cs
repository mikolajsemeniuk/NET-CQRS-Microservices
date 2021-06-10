using System;

namespace Events
{
    public record AddArticleEvent(Guid id, string title, string description);
}