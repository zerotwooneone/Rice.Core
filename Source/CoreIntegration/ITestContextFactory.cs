﻿namespace CoreIntegration
{
    public interface ITestContextFactory
    {
        ITestContext Create(string testName);
    }
}