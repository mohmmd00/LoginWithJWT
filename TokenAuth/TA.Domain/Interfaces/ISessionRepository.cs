﻿using TA.Domain.Entities;

namespace TA.Domain.Interfaces
{
    public interface ISessionRepository
    {
        Session GetSessionBy(int userId);
        void CreateSession(Session session);
    }
}
