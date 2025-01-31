// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using Microsoft.EntityFrameworkCore.TestUtilities;
using Xunit.Abstractions;

namespace Microsoft.EntityFrameworkCore.Query
{
    public class IncludeOneToOneMySqlTest : IncludeOneToOneTestBase<IncludeOneToOneMySqlTest.OneToOneQueryMySqlFixture>
    {
        public IncludeOneToOneMySqlTest(OneToOneQueryMySqlFixture fixture, ITestOutputHelper testOutputHelper)
            : base(fixture)
        {
            fixture.TestSqlLoggerFactory.Clear();
        }

        public override void Include_person()
        {
            base.Include_person();

            AssertSql(
                @"SELECT [a].[Id], [a].[City], [a].[Street], [p].[Id], [p].[Name]
FROM [Address] AS [a]
INNER JOIN [Person] AS [p] ON [a].[Id] = [p].[Id]");
        }

        public override void Include_person_shadow()
        {
            base.Include_person_shadow();

            AssertSql(
                @"SELECT [a].[Id], [a].[City], [a].[PersonId], [a].[Street], [p].[Id], [p].[Name]
FROM [Address2] AS [a]
INNER JOIN [Person2] AS [p] ON [a].[PersonId] = [p].[Id]");
        }

        public override void Include_address()
        {
            base.Include_address();

            AssertSql(
                @"SELECT [p].[Id], [p].[Name], [a].[Id], [a].[City], [a].[Street]
FROM [Person] AS [p]
LEFT JOIN [Address] AS [a] ON [p].[Id] = [a].[Id]");
        }

        public override void Include_address_shadow()
        {
            base.Include_address_shadow();

            AssertSql(
                @"SELECT [p].[Id], [p].[Name], [a].[Id], [a].[City], [a].[PersonId], [a].[Street]
FROM [Person2] AS [p]
LEFT JOIN [Address2] AS [a] ON [p].[Id] = [a].[PersonId]");
        }

        private void AssertSql(params string[] expected)
            => Fixture.TestSqlLoggerFactory.AssertBaseline(expected);

        public class OneToOneQueryMySqlFixture : OneToOneQueryFixtureBase
        {
            protected override ITestStoreFactory TestStoreFactory => MySqlTestStoreFactory.Instance;
            public TestSqlLoggerFactory TestSqlLoggerFactory => (TestSqlLoggerFactory)ListLoggerFactory;
        }
    }
}
