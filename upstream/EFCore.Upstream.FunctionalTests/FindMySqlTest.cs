// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.TestUtilities;
using Xunit;

namespace Microsoft.EntityFrameworkCore
{
    public abstract class FindMySqlTest : FindTestBase<FindMySqlTest.FindMySqlFixture>
    {
        protected FindMySqlTest(FindMySqlFixture fixture)
            : base(fixture)
        {
            fixture.TestSqlLoggerFactory.Clear();
        }

        public class FindMySqlTestSet : FindMySqlTest
        {
            public FindMySqlTestSet(FindMySqlFixture fixture)
                : base(fixture)
            {
            }

            protected override TEntity Find<TEntity>(DbContext context, params object[] keyValues)
                => context.Set<TEntity>().Find(keyValues);

            protected override ValueTask<TEntity> FindAsync<TEntity>(DbContext context, params object[] keyValues)
                => context.Set<TEntity>().FindAsync(keyValues);
        }

        public class FindMySqlTestContext : FindMySqlTest
        {
            public FindMySqlTestContext(FindMySqlFixture fixture)
                : base(fixture)
            {
            }

            protected override TEntity Find<TEntity>(DbContext context, params object[] keyValues)
                => context.Find<TEntity>(keyValues);

            protected override ValueTask<TEntity> FindAsync<TEntity>(DbContext context, params object[] keyValues)
                => context.FindAsync<TEntity>(keyValues);
        }

        public class FindMySqlTestNonGeneric : FindMySqlTest
        {
            public FindMySqlTestNonGeneric(FindMySqlFixture fixture)
                : base(fixture)
            {
            }

            protected override TEntity Find<TEntity>(DbContext context, params object[] keyValues)
                => (TEntity)context.Find(typeof(TEntity), keyValues);

            protected override async ValueTask<TEntity> FindAsync<TEntity>(DbContext context, params object[] keyValues)
                => (TEntity)await context.FindAsync(typeof(TEntity), keyValues);
        }

        public override void Find_int_key_tracked()
        {
            base.Find_int_key_tracked();

            Assert.Equal("", Sql);
        }

        public override void Find_int_key_from_store()
        {
            base.Find_int_key_from_store();

            AssertSql(
                @"@__p_0='77'

SELECT TOP(1) [i].[Id], [i].[Foo]
FROM [IntKey] AS [i]
WHERE ([i].[Id] = @__p_0) AND @__p_0 IS NOT NULL");
        }

        public override void Returns_null_for_int_key_not_in_store()
        {
            base.Returns_null_for_int_key_not_in_store();

            AssertSql(
                @"@__p_0='99'

SELECT TOP(1) [i].[Id], [i].[Foo]
FROM [IntKey] AS [i]
WHERE ([i].[Id] = @__p_0) AND @__p_0 IS NOT NULL");
        }

        public override void Find_nullable_int_key_tracked()
        {
            base.Find_int_key_tracked();

            Assert.Equal("", Sql);
        }

        public override void Find_nullable_int_key_from_store()
        {
            base.Find_int_key_from_store();

            AssertSql(
                @"@__p_0='77'

SELECT TOP(1) [i].[Id], [i].[Foo]
FROM [IntKey] AS [i]
WHERE ([i].[Id] = @__p_0) AND @__p_0 IS NOT NULL");
        }

        public override void Returns_null_for_nullable_int_key_not_in_store()
        {
            base.Returns_null_for_int_key_not_in_store();

            AssertSql(
                @"@__p_0='99'

SELECT TOP(1) [i].[Id], [i].[Foo]
FROM [IntKey] AS [i]
WHERE ([i].[Id] = @__p_0) AND @__p_0 IS NOT NULL");
        }

        public override void Find_string_key_tracked()
        {
            base.Find_string_key_tracked();

            Assert.Equal("", Sql);
        }

        public override void Find_string_key_from_store()
        {
            base.Find_string_key_from_store();

            AssertSql(
                @"@__p_0='Cat' (Size = 450)

SELECT TOP(1) [s].[Id], [s].[Foo]
FROM [StringKey] AS [s]
WHERE ([s].[Id] = @__p_0) AND @__p_0 IS NOT NULL");
        }

        public override void Returns_null_for_string_key_not_in_store()
        {
            base.Returns_null_for_string_key_not_in_store();

            AssertSql(
                @"@__p_0='Fox' (Size = 450)

SELECT TOP(1) [s].[Id], [s].[Foo]
FROM [StringKey] AS [s]
WHERE ([s].[Id] = @__p_0) AND @__p_0 IS NOT NULL");
        }

        public override void Find_composite_key_tracked()
        {
            base.Find_composite_key_tracked();

            Assert.Equal("", Sql);
        }

        public override void Find_composite_key_from_store()
        {
            base.Find_composite_key_from_store();

            AssertSql(
                @"@__p_0='77'
@__p_1='Dog' (Size = 450)

SELECT TOP(1) [c].[Id1], [c].[Id2], [c].[Foo]
FROM [CompositeKey] AS [c]
WHERE (([c].[Id1] = @__p_0) AND @__p_0 IS NOT NULL) AND (([c].[Id2] = @__p_1) AND @__p_1 IS NOT NULL)");
        }

        public override void Returns_null_for_composite_key_not_in_store()
        {
            base.Returns_null_for_composite_key_not_in_store();

            AssertSql(
                @"@__p_0='77'
@__p_1='Fox' (Size = 450)

SELECT TOP(1) [c].[Id1], [c].[Id2], [c].[Foo]
FROM [CompositeKey] AS [c]
WHERE (([c].[Id1] = @__p_0) AND @__p_0 IS NOT NULL) AND (([c].[Id2] = @__p_1) AND @__p_1 IS NOT NULL)");
        }

        public override void Find_base_type_tracked()
        {
            base.Find_base_type_tracked();

            Assert.Equal("", Sql);
        }

        public override void Find_base_type_from_store()
        {
            base.Find_base_type_from_store();

            AssertSql(
                @"@__p_0='77'

SELECT TOP(1) [b].[Id], [b].[Discriminator], [b].[Foo], [b].[Boo]
FROM [BaseType] AS [b]
WHERE [b].[Discriminator] IN (N'BaseType', N'DerivedType') AND (([b].[Id] = @__p_0) AND @__p_0 IS NOT NULL)");
        }

        public override void Returns_null_for_base_type_not_in_store()
        {
            base.Returns_null_for_base_type_not_in_store();

            AssertSql(
                @"@__p_0='99'

SELECT TOP(1) [b].[Id], [b].[Discriminator], [b].[Foo], [b].[Boo]
FROM [BaseType] AS [b]
WHERE [b].[Discriminator] IN (N'BaseType', N'DerivedType') AND (([b].[Id] = @__p_0) AND @__p_0 IS NOT NULL)");
        }

        public override void Find_derived_type_tracked()
        {
            base.Find_derived_type_tracked();

            Assert.Equal("", Sql);
        }

        public override void Find_derived_type_from_store()
        {
            base.Find_derived_type_from_store();

            AssertSql(
                @"@__p_0='78'

SELECT TOP(1) [b].[Id], [b].[Discriminator], [b].[Foo], [b].[Boo]
FROM [BaseType] AS [b]
WHERE ([b].[Discriminator] = N'DerivedType') AND (([b].[Id] = @__p_0) AND @__p_0 IS NOT NULL)");
        }

        public override void Returns_null_for_derived_type_not_in_store()
        {
            base.Returns_null_for_derived_type_not_in_store();

            AssertSql(
                @"@__p_0='99'

SELECT TOP(1) [b].[Id], [b].[Discriminator], [b].[Foo], [b].[Boo]
FROM [BaseType] AS [b]
WHERE ([b].[Discriminator] = N'DerivedType') AND (([b].[Id] = @__p_0) AND @__p_0 IS NOT NULL)");
        }

        public override void Find_base_type_using_derived_set_tracked()
        {
            base.Find_base_type_using_derived_set_tracked();

            AssertSql(
                @"@__p_0='88'

SELECT TOP(1) [b].[Id], [b].[Discriminator], [b].[Foo], [b].[Boo]
FROM [BaseType] AS [b]
WHERE ([b].[Discriminator] = N'DerivedType') AND (([b].[Id] = @__p_0) AND @__p_0 IS NOT NULL)");
        }

        public override void Find_base_type_using_derived_set_from_store()
        {
            base.Find_base_type_using_derived_set_from_store();

            AssertSql(
                @"@__p_0='77'

SELECT TOP(1) [b].[Id], [b].[Discriminator], [b].[Foo], [b].[Boo]
FROM [BaseType] AS [b]
WHERE ([b].[Discriminator] = N'DerivedType') AND (([b].[Id] = @__p_0) AND @__p_0 IS NOT NULL)");
        }

        public override void Find_derived_type_using_base_set_tracked()
        {
            base.Find_derived_type_using_base_set_tracked();

            Assert.Equal("", Sql);
        }

        public override void Find_derived_using_base_set_type_from_store()
        {
            base.Find_derived_using_base_set_type_from_store();

            AssertSql(
                @"@__p_0='78'

SELECT TOP(1) [b].[Id], [b].[Discriminator], [b].[Foo], [b].[Boo]
FROM [BaseType] AS [b]
WHERE [b].[Discriminator] IN (N'BaseType', N'DerivedType') AND (([b].[Id] = @__p_0) AND @__p_0 IS NOT NULL)");
        }

        public override void Find_shadow_key_tracked()
        {
            base.Find_shadow_key_tracked();

            Assert.Equal("", Sql);
        }

        public override void Find_shadow_key_from_store()
        {
            base.Find_shadow_key_from_store();

            AssertSql(
                @"@__p_0='77'

SELECT TOP(1) [s].[Id], [s].[Foo]
FROM [ShadowKey] AS [s]
WHERE ([s].[Id] = @__p_0) AND @__p_0 IS NOT NULL");
        }

        public override void Returns_null_for_shadow_key_not_in_store()
        {
            base.Returns_null_for_shadow_key_not_in_store();

            AssertSql(
                @"@__p_0='99'

SELECT TOP(1) [s].[Id], [s].[Foo]
FROM [ShadowKey] AS [s]
WHERE ([s].[Id] = @__p_0) AND @__p_0 IS NOT NULL");
        }

        private string Sql => Fixture.TestSqlLoggerFactory.Sql;

        private void AssertSql(params string[] expected)
            => Fixture.TestSqlLoggerFactory.AssertBaseline(expected);

        public class FindMySqlFixture : FindFixtureBase
        {
            public TestSqlLoggerFactory TestSqlLoggerFactory => (TestSqlLoggerFactory)ListLoggerFactory;
            protected override ITestStoreFactory TestStoreFactory => MySqlTestStoreFactory.Instance;
        }
    }
}
